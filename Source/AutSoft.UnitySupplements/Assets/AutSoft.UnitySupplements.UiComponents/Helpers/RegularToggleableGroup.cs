 #nullable enable
using AutSoft.UnitySupplements.Vitamins;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.Helpers
{
    [RequireComponent(typeof(ToggleGroup))]
    public class RegularToggleableGroup : ToggleableGroup
    {
        private ToggleGroup? _group;

        public ToggleGroup Group
        {
            get
            {
                if (_group.IsObjectNull())
                {
                    _group = GetComponent<ToggleGroup>();
                }
                return _group;
            }
            private set => _group = value;
        }

        private void Awake() => Group = GetComponent<ToggleGroup>();

        public override void AddToggleToGroup(Toggleable toggle) => toggle.SetToggleGroup(this);

        public override bool AllowSwitchOff { get => Group.allowSwitchOff; set => Group.allowSwitchOff = value; }
    }
}
