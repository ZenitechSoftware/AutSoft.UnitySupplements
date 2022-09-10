#nullable enable
using Injecter;
using Injecter.Unity;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.Vitamins.Sample
{
    public class ListBindingSample : MonoBehaviour
    {
        [Inject] private readonly ListBindingData _data = default!;
        [Inject] private readonly IGameObjectFactory _factory = default!;

        [SerializeField] private Transform _contentParent = default!;

        [SerializeField] private Button _addButton = default!;
        [SerializeField] private Button _removeButton = default!;
        [SerializeField] private Button _orderButton = default!;

        [SerializeField] private GameObject _itemsPrefab = default!;

        private void Awake()
        {
            this.CheckSerializedField(x => x._contentParent);
            this.CheckSerializedField(x => x._addButton);
            this.CheckSerializedField(x => x._removeButton);
            this.CheckSerializedField(x => x._orderButton);
            this.CheckSerializedField(x => x._itemsPrefab);
        }

        private void Start()
        {
            _data.BindOneWay(gameObject, x => x.Items, items =>
            {
                _contentParent.DestroyChildren();

                foreach(var item in items)
                {
                    var itemObject = _factory.Instantiate(_itemsPrefab, _contentParent, true);
                    itemObject.GetComponent<ListItem>().Initialize(item);
                }
            });

            _addButton.onClick.AddWeak(gameObject, () =>
            {
                var item = Guid.NewGuid();
                _data.Add(new ListItemData { Title = item.ToString(), Number = item.GetHashCode() });
            });

            _orderButton.onClick.AddWeak(gameObject, _data.Order);
        }
    }
}
