#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.ResourceGenerator.Sample
{
    public sealed class LoadSceneNext : MonoBehaviour
    {
        [SerializeField] private Button _loadButton = default!;

        private void Start()
        {
            this.CheckSerializedField(_loadButton, nameof(_loadButton));

            _loadButton.onClick.AddListener(() => SceneManager.LoadScene(ResourcePaths.Scenes.LoadSceneNext));
        }
    }
}
