#nullable enable
using MoreLinq;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation.Modules
{
    public sealed class LayerGenerator : IModuleGenerator
    {
        public string Generate(ResourceContext context)
        {
            if (!context.GenerateLayers) return string.Empty;

            var layers = Enumerable.Range(0, 32)
                .Select(l => LayerMask.LayerToName(l))
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToArray();
            const string enumBegin =
@"        [Flags]
        public enum Layers
        {
            None = 0,";

            const string enumEnd =
@"
        }
";

            const string classBegin =
@"        public static partial class LayerNames
        {
";
            const string classEnd = "        }";

            var builder = new StringBuilder();

            builder.AppendLine(enumBegin);

            layers.ForEach(l =>
            {
                var layerCsName = PropertyNameGenerator.GeneratePropertyName(l);
                builder.Append("            ").Append(layerCsName).Append(" = 1 << ").Append(LayerMask.NameToLayer(l)).AppendLine(",");
            });

            builder.Append("            All = ");

            for (var i = 0; i < layers.Length; ++i)
            {
                builder.Append(PropertyNameGenerator.GeneratePropertyName(layers[i]));
                if (i != layers.Length - 1) builder.Append(" | ");
            }

            builder.AppendLine(enumEnd);

            builder.AppendLine(classBegin);

            layers.ForEach(l =>
            {
                var layerCsName = PropertyNameGenerator.GeneratePropertyName(l);
                builder.Append("            public const string ").Append(layerCsName).Append(" = \"").Append(l).AppendLine("\";");
            });

            builder.AppendLine(classEnd);

            return builder.ToString();
        }
    }
}
