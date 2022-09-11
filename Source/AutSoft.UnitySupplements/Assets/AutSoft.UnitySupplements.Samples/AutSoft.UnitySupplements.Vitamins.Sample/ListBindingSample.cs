#nullable enable
using Injecter;
using Injecter.Unity;
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

        [SerializeField] private GameObject _newItemParent = default!;
        [SerializeField] private GameObject _newItemPrefab = default!;

        private void Awake()
        {
            this.CheckSerializedField(x => x._contentParent);
            this.CheckSerializedField(x => x._addButton);
            this.CheckSerializedField(x => x._removeButton);
            this.CheckSerializedField(x => x._orderButton);
            this.CheckSerializedField(x => x._itemsPrefab);
            this.CheckSerializedField(x => x._newItemParent);
            this.CheckSerializedField(x => x._newItemPrefab);
        }

        private void Start()
        {
            _data.Bind(gameObject, x => x.Items, items =>
            {
                _contentParent.DestroyChildren();

                foreach (var item in items)
                {
                    var itemObject = _factory.Instantiate(_itemsPrefab, _contentParent, true);
                    itemObject.GetComponent<ListItem>().Initialize(item);
                }
            });

            _addButton.onClick.Bind(gameObject, () =>
            {
                var item = _newItemParent.transform.GetChild(0).GetComponent<EditableListItem>().Data;

                _newItemParent.transform.DestroyChildren();
                _factory.Instantiate(_newItemPrefab, _newItemParent.transform, true);

                _data.Add(item);
            });

            _orderButton.onClick.Bind(gameObject, _data.Order);
            _removeButton.onClick.Bind(gameObject, _data.RemoveSelected);
        }
    }
}
