#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.Samples.ResourceGeneratorSamples
{
    public sealed class LoadSceneNext : MonoBehaviour
    {
        [SerializeField] private Button _loadButton = default!;

        private void Start()
        {
            this.CheckSerializedFields();

            _loadButton.onClick.AddListener(() => SceneManager.LoadScene(ResourcePaths.Scenes.LoadSceneNext));
        }
    }
}
