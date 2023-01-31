#nullable enable
namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    /// <summary>
    /// Contains information about a license.
    /// </summary>
    public class LicenseModel
    {
        /// <summary>
        /// Name of the license holder. Optional. The information is omitted in the generated asset when the holder is unknown (null).
        /// </summary>
        public string? HolderName { get; set; }

        /// <summary>
        /// Name of the licensed work.
        /// </summary>
        public string LicensedWorkName { get; set; } = default!;

        /// <summary>
        /// The full text of the license.
        /// </summary>
        public string Text { get; set; } = default!;
    }
}
