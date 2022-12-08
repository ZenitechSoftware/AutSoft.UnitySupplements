using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using Injecter;
using Microsoft.Extensions.Logging;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.MRTKExtras.Helpers
{
    [RequireComponent(typeof(Interactable))]
    [DefaultExecutionOrder(-2)]
    public class XrToggle : Toggleable
    {
        [Inject] private readonly ILogger<XrToggle> _logger = default!;

        private Interactable _toggle = default!;

        private void Awake()
        {
            _toggle = GetComponent<Interactable>();

            _toggle.CurrentDimension = (int)SelectionModes.Toggle;
            _toggle.IsToggled = IsOn;

            Interactable = _toggle.IsEnabled;
            this.Bind(onInteractibleChanged, OnInteractableChanged);
            Interactable = _toggle.IsEnabled;
            this.Bind(onValueChanged, (isOn) =>
            {
                _toggle.IsToggled = isOn;
            });
            this.Bind(_toggle.OnClick, () =>
            {
                if (IsOn == _toggle.IsToggled) return;
                IsOn = !_toggle.IsToggled;
            });
        }

        public override void SetIsOnWithoutNotify(bool isOn)
        {
            base.SetIsOnWithoutNotify(isOn);
            _toggle.IsToggled = isOn;
        }

        protected override void OnInteractableChanged(bool interactable) => _toggle.IsEnabled = interactable;

        public override void SetToggleGroup(ToggleGroup toggleGroup) =>
            _logger.LogWarning("MRTK doesn't have a suitable ToggleGroup to use");
    }
}
