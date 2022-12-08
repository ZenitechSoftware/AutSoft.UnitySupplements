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
            protected set
            {
                _isOn = value;
                onValueChanged?.Invoke(value);
            }
        }

        public bool Interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;
                InteractableChanged(_interactable);
                onInteractibleChanged?.Invoke(value);
            }
        }

        protected abstract void InteractableChanged(bool interactable);
        protected virtual void OnToggleableValueChanged(bool isOn) => IsOn = isOn;

        public virtual void ChangeState(bool isOn)
        {
            if (IsOn == isOn) return;
            IsOn = isOn;
        }

        public virtual void ChangeStateWithoutNotifiy(bool isOn) => _isOn = isOn;

        public abstract void SetToggleGroup(ToggleGroup toggleGroup);
    }
}
