#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.Helpers
{
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

            _toggle.onValueChanged.AddListener(OnToggleableValueChanged);
            Interactable = _toggle.interactable;
        }

        private void OnDestroy() => _toggle.onValueChanged.RemoveListener(OnToggleableValueChanged);

        public override void ChangeStateWithoutNotifiy(bool isOn)
        {
            base.ChangeStateWithoutNotifiy(isOn);
            _toggle.SetIsOnWithoutNotify(isOn);
        }

        protected override void InteractableChanged(bool interactable) => _toggle.interactable = interactable;

        public override void ChangeState(bool isOn)
        {
            base.ChangeState(isOn);
            if (_toggle != null)
            {
                _toggle.onValueChanged.RemoveListener(OnToggleableValueChanged);
                _toggle.isOn = isOn;
                _toggle.onValueChanged.AddListener(OnToggleableValueChanged);
            }
        }

        public override void SetToggleGroup(ToggleGroup toggleGroup)
        {
            _toggleGroup = toggleGroup;
            if (_toggle != null)
                _toggle.group = toggleGroup;
        }
    }
}
