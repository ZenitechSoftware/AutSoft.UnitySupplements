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

            const string classBegin =
                @"        public static partial class Layers
        {

";
            const string classEnd = "        }";

            var builder = new StringBuilder();

            builder.AppendLine(classBegin);

            layers.ForEach(l =>
            {
                var layerCsName = PropertyNameGenerator.GeneratePropertyName(l);
                builder.Append("            public const string ").Append(layerCsName).Append(" = \"").Append(l).AppendLine("\";");
                builder.Append("            public static int Get").Append(layerCsName).Append("Index() => LayerMask.NameToLayer(").Append(layerCsName).AppendLine(");");
                builder.Append("            public static int Get").Append(layerCsName).Append("Mask() => LayerMask.GetMask(").Append(layerCsName).AppendLine(");");
            });

            builder.AppendLine(classEnd);

            return builder.ToString();
        }
    }
}
