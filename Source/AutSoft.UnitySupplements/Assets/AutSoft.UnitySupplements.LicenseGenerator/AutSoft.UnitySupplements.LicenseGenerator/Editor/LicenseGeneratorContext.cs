using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutSoft.UnitySupplements.LicenseGenerator.Editor
{
    public class LicenseGeneratorContext
    {
        public LicenseGeneratorSettings Settings { get; }
        private readonly string _assetsFolder;

        public LicenseGeneratorContext(LicenseGeneratorSettings settings)
        {
            Settings = settings;
            _assetsFolder = Path.GetFullPath(Application.dataPath);
            if (Settings.LogInfo)
                Info = Debug.Log;
            if (Settings.LogError)
                Error = Debug.LogError;
        }

        /// <summary>
        /// Info level logger
        /// </summary>
        public Action<string> Info { get; } = (s) => { };

        /// <summary>
        /// Error level logger
        /// </summary>
        public Action<string> Error { get; } = (s) => { };
    }
}
