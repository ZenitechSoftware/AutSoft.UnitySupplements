#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.Helpers
{
    [DefaultExecutionOrder(-1)]
    [RequireComponent(typeof(Toggle))]
    public class RegularToggle : Toggleable
    {
        private Toggle _toggle = default!;

        [Header("Optional")]
        private ToggleGroup? _toggleGroup;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();

            _toggle.isOn = IsOn;

            if (!_toggleGroup.IsObjectNull())
                _toggle.group = _toggleGroup;

            this.Bind(onInteractibleChanged, OnInteractableChanged);
            Interactable = _toggle.interactable;
            this.Bind(onValueChanged, (isOn) =>
            {
                _toggle.isOn = isOn;
            });
            this.Bind(_toggle.onValueChanged, (isOn) =>
            {
                if (IsOn == isOn) return;
                IsOn = isOn;
            });
        }

        public override void SetIsOnWithoutNotify(bool isOn)
        {
            base.SetIsOnWithoutNotify(isOn);
            _toggle.SetIsOnWithoutNotify(isOn);
        }

        protected override void OnInteractableChanged(bool interactable) => _toggle.interactable = interactable;

        public override void SetToggleGroup(ToggleGroup toggleGroup)
        {
            _toggleGroup = toggleGroup;
            if (_toggle != null)
                _toggle.group = toggleGroup;
        }
    }
}
