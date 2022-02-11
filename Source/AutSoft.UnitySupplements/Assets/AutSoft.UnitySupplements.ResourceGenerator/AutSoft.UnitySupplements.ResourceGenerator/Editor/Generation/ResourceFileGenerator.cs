using AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation
{
    public static class ResourceFileGenerator
    {
        private static readonly Regex _normalizedLineEndings = new(@"\r\n|\n\r|\n|\r", RegexOptions.Compiled, TimeSpan.FromSeconds(10));

        /// <summary>
        /// Main generator. Loads and calls all implementations of <see cref="IModuleGenerator"/> and <see cref="IResourcePostProcessor"/>
        /// </summary>
        /// <param name="context">Resource context from settings</param>
        /// <returns>The generated file content</returns>
        public static string CreateResourceFile(ResourceContext context)
        {
            // ReSharper disable once MissingIndent
            const string fileBeginHasNamespace =
@"
namespace {0}
{
    // ReSharper disable PartialTypeWithSinglePart
    // ReSharper disable InconsistentNaming
    // ReSharper disable IncorrectBlankLinesNearBraces
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable RCS1036 // Remove redundant empty line.
    public static partial class {1}
    {";

            // ReSharper disable once MissingIndent
            const string fileEndHasNamespace =
@"    }
}";

            // ReSharper disable once MissingIndent
            const string fileBeginNoNamespace =
@"
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable InconsistentNaming
// ReSharper disable IncorrectBlankLinesNearBraces
#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable RCS1036 // Remove redundant empty line.
public static partial class {1}
{";

            const string fileEndNoNamespace = "}";

            var allConcreteTypes = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => !t.IsAbstract && !t.IsGenericType && !t.IsInterface)
                .ToArray();

            var fileContent = new StringBuilder()
                .AppendMultipleLines(context.Usings.Select(u => $"using {u};"))
                .AppendLine(
                    (string.IsNullOrWhiteSpace(context.BaseNamespace) ? fileBeginNoNamespace : fileBeginHasNamespace)
                    .Replace("{0}", context.BaseNamespace)
                    .Replace("{1}", context.ClassName))
                .AppendMultipleLines(
                    allConcreteTypes
                        .Where(t => t.GetInterfaces().Any(i => typeof(IModuleGenerator).IsAssignableFrom(i)))
                        .Select(t => (IModuleGenerator)Activator.CreateInstance(t))
                        .Select(m => m.Generate(context)))
                .Append(string.IsNullOrWhiteSpace(context.BaseNamespace) ? fileEndNoNamespace : fileEndHasNamespace)
                .ToString();

            fileContent = allConcreteTypes
                .Where(t => t.GetInterfaces().Any(i => typeof(IResourcePostProcessor).IsAssignableFrom(i)))
                .Select(t => (IResourcePostProcessor)Activator.CreateInstance(t))
                .OrderByDescending(p => p.PostProcessPriority)
                .Aggregate(fileContent, (current, processor) => processor.PostProcess(context, current));

            fileContent = _normalizedLineEndings.Replace(fileContent, "\r\n");

            return fileContent;
        }
    }
}
