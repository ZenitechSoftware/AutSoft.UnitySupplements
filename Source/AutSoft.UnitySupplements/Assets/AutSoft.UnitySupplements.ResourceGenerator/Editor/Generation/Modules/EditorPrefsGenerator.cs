#nullable enable
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation.Modules
{
    public sealed class EditorPrefsGenerator : IModuleGenerator
    {
        public string Generate(ResourceContext context)
        {
            if (!context.EditorPrefsData.Generate) return string.Empty;

            var builder = new StringBuilder();

            builder.AppendLine(
@"#if UNITY_EDITOR
        public static partial class KnownEditorPrefs
        {

");

            context
                .EditorPrefsData
                .Preferences
                .GroupBy(p => p.Category)
                .ForEach(category =>
                {
                    builder.Append(
@"
            public static partial class ").Append(category.Key).AppendLine(
@"
            {
");

                    category.ForEach(preference =>
                    {
                        var compilableName = PropertyNameGenerator.GeneratePropertyName(preference.Key);
                        var (typeName, typeMethodName) = preference.Type switch
                        {
                            DataType.Bool => ("bool", "Bool"),
                            DataType.Float => ("float", "Float"),
                            DataType.Int => ("int", "Int"),
                            DataType.String => ("string", "String"),
                            _ => throw new ArgumentOutOfRangeException($"Unhandled data type: {preference.Type}"),
                        };

                        builder.Append("                public static string ").Append(compilableName).Append(" { get; } = \"").Append(preference.Key).AppendLine("\";");
                        builder.Append("                public static ").Append(typeName).Append(" Get").Append(compilableName).Append("() => UnityEditor.EditorPrefs.Get").Append(typeMethodName).Append("(").Append(compilableName).AppendLine(");");
                        builder.Append("                public static void Set").Append(compilableName).Append("(").Append(typeName).Append(" value) => UnityEditor.EditorPrefs.Set").Append(typeMethodName).Append("(").Append(compilableName).AppendLine(", value);");
                        builder.Append("                public static void Delete").Append(compilableName).Append("() => UnityEditor.EditorPrefs.DeleteKey").Append("(").Append(compilableName).AppendLine(");");

                        builder.AppendLine();
                    });

                    builder.AppendLine(
@"
            }");
                });

            builder.AppendLine(
@"
        }
#endif");

            return builder.ToString();
        }
    }

    public sealed class EditorPrefsData
    {
        public EditorPrefsData(bool generate, IReadOnlyList<Preference> preferences)
        {
            Generate = generate;
            Preferences = preferences;
        }

        public bool Generate { get; }

        public IReadOnlyList<Preference> Preferences { get; }
    }

    [Serializable]
    public sealed class Preference
    {
        [SerializeField] private string _category = default!;
        [SerializeField] private string _key = default!;
        [SerializeField] private DataType _type = default!;

        [Obsolete("Used by serializer")]
        public Preference() { }

        public Preference(string category, string key, DataType type)
        {
            _category = category;
            _key = key;
            _type = type;
        }

        public string Category => _category;
        public string Key => _key;
        public DataType Type => _type;
    }

    public enum DataType
    {
        Bool,
        Float,
        Int,
        String,
    }
}
