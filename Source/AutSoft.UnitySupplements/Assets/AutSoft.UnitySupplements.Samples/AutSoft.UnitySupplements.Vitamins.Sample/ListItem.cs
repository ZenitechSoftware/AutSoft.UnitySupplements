#nullable enable
using Injecter;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutSoft.UnitySupplements.Vitamins.Sample
{
    public class ListItem : MonoBehaviour, IPointerClickHandler
    {
        [Inject] private readonly ListBindingData _listData = default!;

        [SerializeField] private TMP_Text _titleText = default!;
        [SerializeField] private TMP_Text _numberText = default!;

        private ListItemData _data = default!;
        private bool _handleClick;

        private void Awake()
        {
            this.CheckSerializedField(x => x._titleText);
            this.CheckSerializedField(x => x._numberText);
        }

        public void Initialize(ListItemData data, bool handleClick)
        {
            _handleClick = handleClick;

            _data = data;

            _data.BindOneWay(gameObject, x => x.Title, t => _titleText.text = t);
            _data.BindOneWay(gameObject, x => x.Number, n => _numberText.text = n.ToString());
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_handleClick) return;

            _listData.Selected = _data;
        }
    }
}
