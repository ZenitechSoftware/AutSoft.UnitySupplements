using System;
using System.Text.RegularExpressions;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation.PostProcessors
{
    /// <summary>
    /// A PostProcessor that removes duplicate newlines.
    /// </summary>
    public sealed class RemoveDuplicateNewLines : IResourcePostProcessor
    {
        private static readonly Regex _multipleNewLines = new(@"(?:\r\n|\r(?!\n)|(?!<\r)\n){2,}", RegexOptions.Compiled, TimeSpan.FromSeconds(10));

        public int PostProcessPriority { get; } = 0;

        public string PostProcess(ResourceContext context, string resourceFileContent) => _multipleNewLines.Replace(resourceFileContent, Environment.NewLine + Environment.NewLine);
    }
}
