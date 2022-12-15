using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

namespace AutSoft.UnitySupplements.MRTKExtras.Helpers
{
    [DefaultExecutionOrder(-1)]
    [RequireComponent(typeof(Interactable))]
    public class XrClickable : Clickable
    {
        private Interactable _button = default!;

        private void Awake()
        {
            _button = GetComponent<Interactable>();

            this.Bind(_button.OnClick, ClickHappened);
        }

        protected override void InteractableChanged(bool enabled) => _button.IsEnabled = enabled;

        public override void SetColor(Color color)
        {
            //TODO: What happens here? Set BackPlateColor
        }

        public override void SetIcon(char unicodeChar)
        {
            var helper = _button.GetComponent<ButtonConfigHelper>();
            helper.SetCharIcon(unicodeChar, helper.IconSet.CharIconFont);
        }

        public override void TriggerClick() => _button.OnClick.Invoke();
    }
}
