#nullable enable
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.Samples.ResourceGeneratorSamples
{
    public sealed class LoadSceneInitial : MonoBehaviour
    {
        [SerializeField] private Button _loadButton = default!;

        private void Start()
        {
            if (_loadButton == null) throw new InvalidOperationException($"Button is not set on {gameObject.name}");

            _loadButton.onClick.AddListener(() => SceneManager.LoadScene(ResourcePaths.Scenes.LoadSceneInitial));
        }
    }
}
