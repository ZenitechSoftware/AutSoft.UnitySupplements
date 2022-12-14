using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.Helpers
{
    [RequireComponent(typeof(ToggleGroup))]
    public class RegularToggleableGroup : ToggleableGroup
    {
        public ToggleGroup Group { get; private set; } = default!;

        private void Awake() => Group = GetComponent<ToggleGroup>();

        public override void AddToggleToGroup(Toggleable toggle) => toggle.SetToggleGroup(this);

        public override bool AllowSwitchOff { get => Group.allowSwitchOff; set => Group.allowSwitchOff = value; }
    }
}
