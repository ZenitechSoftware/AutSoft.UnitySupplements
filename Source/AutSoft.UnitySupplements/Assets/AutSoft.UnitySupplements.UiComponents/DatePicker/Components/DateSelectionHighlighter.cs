using AutSoft.UnitySupplements.Vitamins;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    [RequireComponent(typeof(Image))]
    public class DateSelectionHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private ColorBlock _backgroundColors;
        [SerializeField] private ColorBlock _textColors;
        [SerializeField] private TMP_Text _label = default!;
        [SerializeField] private Image _pressedImage = default!;
        [SerializeField] private Toggle _currentToggle = default!;
        [SerializeField] private Image _background = default!;

        private void Awake()
        {
            this.CheckSerializedFields();

            SetColorsByState();
            _currentToggle.onValueChanged.AddListener(OnToggleChangeForHighlight);
        }

        private void OnDestroy() => _currentToggle.onValueChanged.RemoveListener(OnToggleChangeForHighlight);

        private void OnToggleChangeForHighlight(bool _) => SetColorsByState();

        public void SetColorsByState()
        {
            _background.color = _currentToggle.isOn ? _backgroundColors.selectedColor : _backgroundColors.normalColor;
            _label.color = _currentToggle.isOn ? _textColors.selectedColor : _textColors.normalColor;
            _label.fontStyle = _currentToggle.isOn ? FontStyles.Bold : FontStyles.Normal;
            if (!_currentToggle.interactable) _label.color = _textColors.disabledColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_currentToggle.isOn || !_currentToggle.interactable) return;
            _background.color = _backgroundColors.highlightedColor;
            _label.color = _textColors.highlightedColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_currentToggle.isOn || !_currentToggle.interactable) return;
            SetColorsByState();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_currentToggle.interactable) return;
            _pressedImage.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_currentToggle.interactable) return;
            _label.fontStyle = FontStyles.Bold;
            _label.color = _textColors.pressedColor;
            _background.color = _backgroundColors.pressedColor;
            _pressedImage.gameObject.SetActive(true);
        }
    }
}
