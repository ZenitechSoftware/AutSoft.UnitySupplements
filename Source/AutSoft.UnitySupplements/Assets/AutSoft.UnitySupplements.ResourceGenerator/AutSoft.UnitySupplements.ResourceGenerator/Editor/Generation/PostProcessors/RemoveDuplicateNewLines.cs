using System;
using System.Text.RegularExpressions;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation.PostProcessors
{
    /// <summary>
    /// Removes duplicate newlines
    /// </summary>
    public sealed class RemoveDuplicateNewLines : IResourcePostProcessor
    {
        private static readonly Regex MultipleNewLines = new Regex(@"(?:\r\n|\r(?!\n)|(?!<\r)\n){2,}", RegexOptions.Compiled, TimeSpan.FromSeconds(10));

        public int PostProcessPriority { get; } = 0;

        public string PostProcess(ResourceContext context, string resourceFileContent) => MultipleNewLines.Replace(resourceFileContent, Environment.NewLine + Environment.NewLine);
    }
}
