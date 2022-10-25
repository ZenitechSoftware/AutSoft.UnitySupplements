#nullable enable
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation.Modules
{
    public static class PropertyNameGenerator
    {
        private static readonly Regex _nonAlphaNumeric =
            new("[^a-zA-Z0-9]", RegexOptions.Compiled, TimeSpan.FromSeconds(1));

        private static readonly Regex _startsWithNumber = new(@"^\d", RegexOptions.Compiled, TimeSpan.FromSeconds(1));

        public static string GeneratePropertyName(string nameToFix)
        {
            var name = Path.GetFileNameWithoutExtension(nameToFix).Replace(" ", string.Empty);

            if (_startsWithNumber.IsMatch(name)) name = name.Insert(0, "_");

            return _nonAlphaNumeric.Replace(name, "_");
        }
    }
}
