#nullable enable
using Injecter;
using Injecter.Unity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    public class ListView : MonoBehaviour
    {
        [Inject] private readonly IGameObjectFactory _factory = default!;

        [SerializeField] private Transform _contentParent = default!;
        private GameObject _itemPrefab = default!;

        private void Awake() => this.CheckSerializedField(x => x._contentParent);

        public void Initialze<TItem, TCollection, TItemView>(TCollection collection, GameObject itemPrefab)
            where TItem : class
            where TCollection : INotifyCollectionChanged, IEnumerable<TItem>
            where TItemView : MonoBehaviour, IInitialzeViewItem<TItem>
        {
#if UNITY_EDITOR
            if (!itemPrefab.TryGetComponent<TItemView>(out var _)) throw new ArgumentOutOfRangeException(nameof(itemPrefab), null, $"Could not find component {typeof(TItemView).FullName} on prefab {itemPrefab.name}");
#endif

            _itemPrefab = itemPrefab;

            collection.Bind<TCollection, TItem>(gameObject, args =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    var itemObject = _factory.Instantiate(_itemPrefab, _contentParent, true);
                    itemObject.GetComponent<TItemView>().Initialize(args.NewItem);
                }
                else if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    Destroy(_contentParent.transform.GetChild(args.OldStartingIndex).gameObject);
                }
                else if (args.Action == NotifyCollectionChangedAction.Reset)
                {
                    _contentParent.DestroyChildren();
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
