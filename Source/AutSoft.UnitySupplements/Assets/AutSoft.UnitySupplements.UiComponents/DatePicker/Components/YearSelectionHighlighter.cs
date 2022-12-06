using AutSoft.UnitySupplements.Vitamins;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    [RequireComponent(typeof(Image))]
    public class YearSelectionHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private ColorBlock _backgroundColors;
        [SerializeField] private ColorBlock _textColors;
        [SerializeField] private TMP_Text _label = default!;
        [SerializeField] private Image _pressedImage = default!;

        private Image _background = default!;
        private bool _highlighted;
        private bool _clicked;

        private void Awake()
        {
            this.CheckSerializedFields();

            _background = GetComponent<Image>();
            SetColorsByState();
        }

        public void HighLightButton()
        {
            _highlighted = true;
            SetColorsByState();
        }

        private void SetColorsByState()
        {
            if (_clicked) return;
            _background.color = _highlighted ? _backgroundColors.selectedColor : _backgroundColors.normalColor;
            _label.color = _highlighted ? _textColors.selectedColor : _textColors.normalColor;
            _label.fontStyle = _highlighted ? FontStyles.Bold : FontStyles.Normal;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_highlighted) return;
            _background.color = _backgroundColors.highlightedColor;
            _label.color = _textColors.highlightedColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_highlighted) return;
            SetColorsByState();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _clicked = false;
            _pressedImage.gameObject.SetActive(false);
            SetColorsByState();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _clicked = true;
            _label.fontStyle = FontStyles.Bold;
            _label.color = _textColors.pressedColor;
            _background.color = _backgroundColors.pressedColor;
            _pressedImage.gameObject.SetActive(true);
        }
    }
}
