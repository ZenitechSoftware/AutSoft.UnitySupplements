using AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation.Modules;
using System.Collections.Generic;

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Generation
{
    /// <summary>
    /// Data used by <see cref="AllResources"/>
    /// </summary>
    public interface IResourceData
    {
        /// <summary>
        /// Name of the generated class
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// File extensions to look for.
        /// </summary>
        IReadOnlyList<string> FileExtensions { get; }

        /// <summary>
        /// Data type returned by Resources.Load
        /// </summary>
        string DataType { get; }
    }
}