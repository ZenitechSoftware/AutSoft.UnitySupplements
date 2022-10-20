#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents
{
    public class ListView : MonoBehaviour
    {
        [SerializeField] private Transform _contentParent = default!;
        private GameObject _itemPrefab = default!;

        private void Awake() => this.CheckSerializedFields();

        public void Initialze<TItem, TCollection, TItemView>(TCollection collection, GameObject itemPrefab, Action<TItemView, TItem?>? initialize = null)
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
                    initialize?.Invoke(itemObject.GetComponent<TItemView>(), args.NewItems![0]);
                }
                else if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    Destroy(_contentParent.GetChild(args.OldStartingIndex).gameObject);
                }
                else if (args.Action == NotifyCollectionChangedAction.Reset)
                {
                    _contentParent.DestroyChildren();
                }
                else if (args.Action == NotifyCollectionChangedAction.Move)
                {
                    _contentParent.GetChild(args.OldStartingIndex).SetSiblingIndex(args.NewStartingIndex);
                }
                else if (args.Action == NotifyCollectionChangedAction.Replace)
                {
                    Destroy(_contentParent.GetChild(args.OldStartingIndex).gameObject);

                    var itemObject = Instantiate(_itemPrefab, _contentParent);
                    initialize?.Invoke(itemObject.GetComponent<TItemView>(), args.NewItems![0]);

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
