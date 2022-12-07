using AutSoft.UnitySupplements.Vitamins.Bindings;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class DateSelectionHighlighter : BaseSelectionHighlighter
    {
        [SerializeField] private Toggle _currentToggle = default!;

        public override bool IsHighlighted
        {
            get => _currentToggle.isOn;
            protected set => _currentToggle.isOn = value;
        }

        protected override void Awake()
        {
            base.Awake();
            this.Bind(_currentToggle.onValueChanged, OnToggleChangeForHighlight);
        }

        private void OnToggleChangeForHighlight(bool _) => SetColorsByState();

        public override void SetColorsByState()
        {
            base.SetColorsByState();
            if (!_currentToggle.interactable) _label.color = _textColors.disabledColor;
        }
    }
}
