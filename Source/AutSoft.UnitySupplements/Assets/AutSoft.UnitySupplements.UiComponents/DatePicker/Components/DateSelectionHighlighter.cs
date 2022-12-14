using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class DateSelectionHighlighter : BaseSelectionHighlighter
    {
        [SerializeField] private Toggleable _currentToggle = default!;

        public override bool IsHighlighted
        {
            get => _currentToggle.IsOn;
            protected set => _currentToggle.IsOn = value;
        }
        public override bool Interactable => _currentToggle.Interactable;

        protected override void Awake()
        {
            base.Awake();
            this.Bind(_currentToggle.onValueChanged, OnToggleChangeForHighlight);
            this.Bind(_currentToggle.onInteractibleChanged, OnToggleChangeForHighlight);
        }

        private void OnToggleChangeForHighlight(bool _) => SetColorsByState();
    }
}
