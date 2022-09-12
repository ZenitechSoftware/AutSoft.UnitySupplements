﻿#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using Injecter;
using Injecter.Unity;
using UnityEngine;

namespace AutSoft.UnitySupplements.Samples.VitaminSamples.BindingSamples
{
    public class SelectedItemDisplay : MonoBehaviourScoped
    {
        [Inject] private readonly ListBindingData _listBindingData = default!;
        [Inject] private readonly IGameObjectFactory _factory = default!;

        [SerializeField] private GameObject _itemPrefab = default!;
        [SerializeField] private Transform _transformParent = default!;

        protected override void Awake()
        {
            base.Awake();

            this.CheckSerializedField(x => x._itemPrefab);
            this.CheckSerializedField(x => x._transformParent);
        }

        private void Start() =>
            _listBindingData.Bind(gameObject, x => x.Selected, s =>
            {
                _transformParent.DestroyChildren();
                if (s is null) return;

                _factory.Instantiate(_itemPrefab, _transformParent, true).GetComponent<ListItem>().Initialize(s);
            });
    }
}