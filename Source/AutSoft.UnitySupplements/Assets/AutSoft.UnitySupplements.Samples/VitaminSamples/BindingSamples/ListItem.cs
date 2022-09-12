#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using Injecter;
using Injecter.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutSoft.UnitySupplements.Samples.VitaminSamples.BindingSamples
{
    public class ListItem : MonoBehaviourScoped, IPointerClickHandler
    {
        [Inject] private readonly ListBindingData _listData = default!;

        [SerializeField] private TMP_Text _titleText = default!;
        [SerializeField] private TMP_Text _numberText = default!;

        private ListItemData _data = default!;

        protected override void Awake()
        {
            base.Awake();

            this.CheckSerializedField(x => x._titleText);
            this.CheckSerializedField(x => x._numberText);
        }

        public void Initialize(ListItemData? data)
        {
            if (data is null) return;

            _data = data;

            _data.Bind(gameObject, x => x.Title, t => _titleText.text = t);
            _data.Bind(gameObject, x => x.Number, n => _numberText.text = n.ToString());
        }

        public void OnPointerClick(PointerEventData eventData) => _listData.Selected = _data;
    }
}
