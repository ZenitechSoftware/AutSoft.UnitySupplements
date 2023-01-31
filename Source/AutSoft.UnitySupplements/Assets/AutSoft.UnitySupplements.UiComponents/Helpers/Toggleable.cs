#nullable enable
using UnityEngine;
using UnityEngine.Events;

namespace AutSoft.UnitySupplements.UiComponents.Helpers
{
    /// <summary>
    /// Common base class to handle both XR / non-XR (regular) toggles.
    /// See <seealso cref="RegularToggle"/> and <seealso cref="XrToggleable"/> scripts.
    /// </summary>
    public abstract class Toggleable : MonoBehaviour
    {
        [SerializeField] protected bool _isOn = true;

        private bool _interactable;

        public UnityEvent<bool> onValueChanged { get; } = new();
        public UnityEvent<bool> onInteractibleChanged { get; } = new();

        private void OnDestroy()
        {
            onValueChanged.RemoveAllListeners();
            onInteractibleChanged.RemoveAllListeners();
        }

        public bool IsOn
        {
            get => _isOn;
            set
            {
                if(_isOn == value) return;
                _isOn = value;
                onValueChanged?.Invoke(value);
            }
        }

        public bool Interactable
        {
            get => _interactable;
            set
            {
                if(_interactable == value) return;
                _interactable = value;
                onInteractibleChanged?.Invoke(value);
            }
        }

        protected virtual void OnToggleableValueChanged(bool isOn) => IsOn = isOn;

        public virtual void SetIsOnWithoutNotify(bool isOn) => _isOn = isOn;

        public abstract void SetToggleGroup(ToggleableGroup toggleGroup);
        protected abstract void OnInteractableChanged(bool interactable);
    }
}
