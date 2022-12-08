#nullable enable
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.Helpers
{
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

        public abstract void SetToggleGroup(ToggleGroup toggleGroup);
        protected abstract void OnInteractableChanged(bool interactable);
    }
}
