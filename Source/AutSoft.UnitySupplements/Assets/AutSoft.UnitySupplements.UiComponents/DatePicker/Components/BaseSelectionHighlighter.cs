using AutSoft.UnitySupplements.Vitamins;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public abstract class BaseSelectionHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("BackgroundColor")]
        [SerializeField] private ColorBlock _backgroundColors;
        [Header("ForegroundColor")]
        [SerializeField] protected ColorBlock _textColors;
        [SerializeField] protected TMP_Text _label = default!;
        [SerializeField] private Image _pressedImage = default!;
        [SerializeField] private Image _background = default!;

        public abstract bool IsHighlighted { get; protected set; }

        public abstract bool Interactable { get;}

        protected virtual void Awake()
        {
            this.CheckSerializedFields();
            SetColorsByState();
        }

        public virtual void SetColorsByState()
        {
            _background.color = IsHighlighted ? _backgroundColors.selectedColor : _backgroundColors.normalColor;
            _label.color =   IsHighlighted
                ? _textColors.selectedColor
                :!Interactable
                    ? _textColors.disabledColor
                    : _textColors.normalColor;

            _label.fontStyle = IsHighlighted ? FontStyles.Bold : FontStyles.Normal;
        }

        public void Highlight(bool highlighted)
        {
            IsHighlighted = highlighted;
            SetColorsByState();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsHighlighted) return;
            _background.color = _backgroundColors.highlightedColor;
            _label.color = _textColors.highlightedColor;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (IsHighlighted) return;
            SetColorsByState();
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            _pressedImage.gameObject.SetActive(false);
            SetColorsByState();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _label.fontStyle = FontStyles.Bold;
            _label.color = _textColors.pressedColor;
            _background.color = _backgroundColors.pressedColor;
            _pressedImage.gameObject.SetActive(true);
        }
    }
}
