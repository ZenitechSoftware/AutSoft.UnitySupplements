using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.Helpers
{
    public abstract class ToggleableGroup : MonoBehaviour
    {
        public abstract bool AllowSwitchOff { get; set; }

        public abstract void AddToggleToGroup(Toggleable toggle);
    }
}
