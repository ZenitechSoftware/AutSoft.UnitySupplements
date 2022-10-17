#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using AutSoft.UnitySupplements.Vitamins.UiComponents;
using Injecter;
using Injecter.Unity;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.Samples.VitaminSamples.BindingSamples
{
    [RequireComponent(typeof(MonoInjector))]
    public class ListBindingSample : MonoBehaviour
    {
        [Inject] private readonly ListBindingData _data = default!;

        [SerializeField] private ListView _listView = default!;

        [SerializeField] private Button _addButton = default!;
        [SerializeField] private Button _removeButton = default!;
        [SerializeField] private Button _orderButton = default!;
        [SerializeField] private Button _swapButton = default!;
        [SerializeField] private Button _replaceButton = default!;

        [SerializeField] private GameObject _itemsPrefab = default!;

        [SerializeField] private GameObject _newItemParent = default!;
        [SerializeField] private GameObject _newItemPrefab = default!;

        private void Awake() => this.CheckSerializedFields();

        private void Start()
        {
            _listView.Initialze<ListItemData, ReadOnlyObservableCollection<ListItemData>, ListItem>(_data.Items, _itemsPrefab, (itemObject, item) => itemObject.Initialize(item));

            _addButton.onClick.Bind(gameObject, () =>
            {
                var item = _newItemParent.transform.GetChild(0).GetComponent<EditableListItem>().Data;
                _data.Add(item);

                _newItemParent.transform.DestroyChildren();

                Instantiate(_newItemPrefab, _newItemParent.transform);
            });

            _orderButton.onClick.Bind(gameObject, _data.Order);
            _removeButton.onClick.Bind(gameObject, _data.RemoveSelected);
            _swapButton.onClick.Bind(gameObject, _data.SwapFirstAndLast);
            _replaceButton.onClick.Bind(gameObject, () =>
            {
                var item = _newItemParent.transform.GetChild(0).GetComponent<EditableListItem>().Data;
                _data.ReplaceFirst(item);

                _newItemParent.transform.DestroyChildren();
                Instantiate(_newItemPrefab, _newItemParent.transform);
            });
        }
    }
}
