#nullable enable

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation
{
    /// <summary>
    /// Used by the file generation pipeline.
    /// Implement this interface to create a piece of string that you want to see in the generated file
    /// </summary>
    public interface IModuleGenerator
    {
        /// <summary>
        /// Create a piece of code as part of the generated file.
        /// Could be static methods, properties, fields or inner classes
        /// </summary>
        /// <param name="context">User configured settings and logger</param>
        /// <returns>Generated code module</returns>
        string Generate(ResourceContext context);
    }
}
