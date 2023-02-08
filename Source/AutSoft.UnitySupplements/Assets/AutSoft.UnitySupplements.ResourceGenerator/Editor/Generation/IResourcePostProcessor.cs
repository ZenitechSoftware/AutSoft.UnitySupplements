#nullable enable

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation
{
    /// <summary>
    /// Implement this interface to do post processing of the generated file.
    /// </summary>
    public interface IResourcePostProcessor
    {
        /// <summary>
        /// Priority during post processing. Higher number means it will run before others.
        /// </summary>
        int PostProcessPriority { get; }

        /// <summary>
        /// Does post processing of the generated file.
        /// </summary>
        /// <param name="context">User configured settings and logger.</param>
        /// <param name="resourceFileContent">The current state of the generated file.</param>
        /// <returns>The next state of the generated file.</returns>
        string PostProcess(ResourceContext context, string resourceFileContent);
    }
}
