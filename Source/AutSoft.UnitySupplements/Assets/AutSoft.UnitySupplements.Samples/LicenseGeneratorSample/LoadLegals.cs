#nullable enable
using System;
using TMPro;
using UnityEngine;
using AutSoft.UnitySupplements.Samples.ResourceGeneratorSamples;

namespace AutSoft.UnitySupplements.Samples.LicenseGeneratorSample
{
    public sealed class LoadLegals : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text = default!;

        private void Start()
        {
            if (_text == null) throw new InvalidOperationException($"Text is not set on {gameObject.name}");

            _text.text = ResourcePaths.TextAssets.LoadLegal().text;
        }
    }
}
