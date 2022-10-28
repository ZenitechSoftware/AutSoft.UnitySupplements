#nullable enable
namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    public class LicenseModel
    {
        public string? HolderName { get; set; }
        public string LicensedWorkName { get; set; } = default!;
        public string Text { get; set; } = default!;
    }
}
