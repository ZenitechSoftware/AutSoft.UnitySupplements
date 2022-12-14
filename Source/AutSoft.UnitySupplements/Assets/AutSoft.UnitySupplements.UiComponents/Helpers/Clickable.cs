#nullable enable
using UnityEngine;
using UnityEngine.Events;

namespace AutSoft.UnitySupplements.UiComponents.Helpers
{
    public abstract class Clickable : MonoBehaviour
    {
        public UnityEvent onClick { get;} = new();
        public UnityEvent onDeselect { get; } = new ();
        public UnityEvent onSelect { get; } = new ();
        public UnityEvent<bool> interactableChanged { get; } = new ();

        private void OnDestroy()
        {
            onClick.RemoveAllListeners();
            onDeselect.RemoveAllListeners();
            onSelect.RemoveAllListeners();
            interactableChanged.RemoveAllListeners();
        }

        private bool _interactable = true;

        public bool Interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;
                InteractableChanged(_interactable);
                interactableChanged.Invoke(value);
            }
        }

        protected void ClickHappened() => onClick?.Invoke();
        protected void SelectHappened() => onSelect?.Invoke();
        protected void DeselectHappened() => onDeselect?.Invoke();

        protected abstract void InteractableChanged(bool enabled);

        public abstract void SetColor(Color color);
        public abstract void SetIcon(char hexCode);
        public abstract void TriggerClick();
    }
}
