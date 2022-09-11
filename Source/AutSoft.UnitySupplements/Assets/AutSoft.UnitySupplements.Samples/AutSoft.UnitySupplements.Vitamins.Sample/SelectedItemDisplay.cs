#nullable enable
using Injecter;
using Injecter.Unity;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins.Sample
{
    public class SelectedItemDisplay : MonoBehaviour
    {
        [Inject] private readonly ListBindingData _listBindingData = default!;
        [Inject] private readonly IGameObjectFactory _factory = default!;

        [SerializeField] private GameObject _itemPrefab = default!;
        [SerializeField] private Transform _transformParent = default!;

        private void Awake()
        {
            this.CheckSerializedField(x => x._itemPrefab);
            this.CheckSerializedField(x => x._transformParent);
        }

        private void Start() =>
            _listBindingData.BindOneWay(gameObject, x => x.Selected, s =>
            {
                _transformParent.DestroyChildren();
                if (s is null) return;

                _factory.Instantiate(_itemPrefab, _transformParent, true).GetComponent<ListItem>().Initialize(s);
            });
    }
}
