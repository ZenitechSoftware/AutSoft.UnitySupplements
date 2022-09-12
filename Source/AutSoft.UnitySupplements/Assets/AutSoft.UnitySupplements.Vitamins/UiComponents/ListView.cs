#nullable enable
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins.UiComponents
{
    public class ListView : MonoBehaviour
    {
        [SerializeField] private Transform _contentParent = default!;
        private GameObject _itemPrefab = default!;

        private void Awake() => this.CheckSerializedField(x => x._contentParent);

        public void Initialze<TItem, TCollection, TItemView>(TCollection collection, GameObject itemPrefab, Action<TItemView, TItem?>? initialize = null)
            where TItem : class
            where TCollection : INotifyCollectionChanged, IEnumerable<TItem>
            where TItemView : MonoBehaviour
        {
#if UNITY_EDITOR
            if (!itemPrefab.TryGetComponent<TItemView>(out var _)) throw new ArgumentOutOfRangeException(nameof(itemPrefab), null, $"Could not find component {typeof(TItemView).FullName} on prefab {itemPrefab.name}");
#endif

            _itemPrefab = itemPrefab;

            collection.Bind<TCollection, TItem>(gameObject, args =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    var itemObject = Instantiate(_itemPrefab, _contentParent);
                    initialize?.Invoke(itemObject.GetComponent<TItemView>(), args.NewItem);
                }
                else if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    Destroy(_contentParent.transform.GetChild(args.OldStartingIndex).gameObject);
                }
                else if (args.Action == NotifyCollectionChangedAction.Reset)
                {
                    _contentParent.DestroyChildren();
                }
                else if (args.Action == NotifyCollectionChangedAction.Move)
                {
                    var child1 = _contentParent.transform.GetChild(args.OldStartingIndex);
                    var child2 = _contentParent.transform.GetChild(args.NewStartingIndex);

                    child1.SetSiblingIndex(args.NewStartingIndex);
                    child2.SetSiblingIndex(args.OldStartingIndex);
                }
                else if (args.Action == NotifyCollectionChangedAction.Replace)
                {
                    var oldChild = _contentParent.transform.GetChild(args.OldStartingIndex);
                    Destroy(oldChild.gameObject);

                    var itemObject = Instantiate(_itemPrefab, _contentParent);
                    initialize?.Invoke(itemObject.GetComponent<TItemView>(), args.NewItem);
                    itemObject.transform.SetSiblingIndex(args.NewStartingIndex);
                }
                else
                {
#pragma warning disable S3928 // Parameter names used into ArgumentException constructors should match an existing one 
                    throw new ArgumentOutOfRangeException(nameof(args.Action), args.Action, "Could not handle collection change type");
#pragma warning restore S3928 // Parameter names used into ArgumentException constructors should match an existing one 
                }
            });
        }
    }
}
