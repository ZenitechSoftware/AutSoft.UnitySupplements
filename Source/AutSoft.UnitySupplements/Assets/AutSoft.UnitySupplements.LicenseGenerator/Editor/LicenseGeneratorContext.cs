#nullable enable
using System;
using UnityEngine;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    /// <summary>
    /// Context object used by <see cref="LicenseGenerator.GenerateAsset(LicenseGeneratorContext)"/>.
    /// </summary>
    public class LicenseGeneratorContext
    {
        public LicenseGeneratorSettings Settings { get; }

        public LicenseGeneratorContext(LicenseGeneratorSettings settings)
        {
            Settings = settings;
            if (Settings.LogInfo)
                Info = Debug.Log;
            if (Settings.LogError)
                Error = Debug.LogError;
        }

        /// <summary>
        /// Info level logger
        /// </summary>
        public Action<string> Info { get; } = (_) => { };

        /// <summary>
        /// Error level logger
        /// </summary>
        public Action<string> Error { get; } = (_) => { };
    }
}
