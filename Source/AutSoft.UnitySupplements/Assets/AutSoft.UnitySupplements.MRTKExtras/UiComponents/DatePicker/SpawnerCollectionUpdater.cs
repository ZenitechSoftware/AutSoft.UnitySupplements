using AutSoft.UnitySupplements.UiComponents.DatePicker.Components;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using Cysharp.Threading.Tasks;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;

namespace AutSoft.UnitySupplements.MRTKExtras.UiComponents.DatePicker
{
    public class SpawnerCollectionUpdater : MonoBehaviour
    {
        [SerializeField] private BaseObjectCollection _collectionToUpdate = default!;
        [SerializeField] private DateSpawner _spawner = default!;

        private void Awake()
        {
            this.CheckSerializedFields();

            this.Bind(_spawner.onSpawned, async () =>
            {
                await UniTask.NextFrame();
                _collectionToUpdate.UpdateCollection();
            });
        }
    }

}
