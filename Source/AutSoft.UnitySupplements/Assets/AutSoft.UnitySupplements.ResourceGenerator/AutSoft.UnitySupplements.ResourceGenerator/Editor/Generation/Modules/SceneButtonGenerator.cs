using MoreLinq;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation.Modules
{
    public sealed class SceneButtonGenerator : IModuleGenerator
    {
        public string Generate(ResourceContext context)
        {
            if (!context.GenerateSceneButtons) return string.Empty;

            const string classBegin =
@"#if UNITY_EDITOR
        public static partial class LoadSceneButtons
        {

";
            const string classEnd =
@"        }
#endif";

            var dataPathFull = Path.GetFullPath(Application.dataPath);
            var scenes = Directory
                .EnumerateFiles(dataPathFull, "*.unity", SearchOption.AllDirectories)
                .Select(s => s.Replace(dataPathFull, string.Empty))
                .ToArray();

            var builder = new StringBuilder();

            builder.AppendLine(classBegin);

            scenes.ForEach(s =>
            {
                var sceneFileName = Path.GetFileNameWithoutExtension(s);
                var sceneCsName = sceneFileName.Replace(" ", string.Empty);
                builder.Append("            [UnityEditor.MenuItem(\"Load Scene / ").Append(sceneFileName).AppendLine("\")]");
                builder.Append("            public static void Load").Append(sceneCsName).AppendLine("()");
                builder.AppendLine("            {");
                builder.AppendLine("                UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();");
                builder.Append("                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(@\"Assets").Append(s).AppendLine("\");");
                builder.AppendLine("            }");
            });

            builder.AppendLine();

            scenes
                .Where(s => context.SceneNames.Contains(Path.GetFileNameWithoutExtension(s)))
                .ForEach(s =>
            {
                var sceneFileName = Path.GetFileNameWithoutExtension(s);
                var sceneCsName = sceneFileName.Replace(" ", string.Empty);
                builder.Append("            [UnityEditor.MenuItem(\"Play Scene / ").Append(sceneFileName).AppendLine("\")]");
                builder.Append("            public static void Play").Append(sceneCsName).AppendLine("()");
                builder.AppendLine("            {");
                builder.AppendLine(
@"                if (UnityEditor.EditorApplication.isPlaying)
                {
                    UnityEditor.EditorApplication.isPlaying = false;
                    return;
                }");
                builder.AppendLine();
                builder.AppendLine("                UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();");
                builder.Append("                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(@\"Assets").Append(s).AppendLine("\");");
                builder.AppendLine("                UnityEditor.EditorApplication.isPlaying = true;");
                builder.AppendLine("            }");
            });

            builder.AppendLine(classEnd);

            return builder.ToString();
        }
    }
}
