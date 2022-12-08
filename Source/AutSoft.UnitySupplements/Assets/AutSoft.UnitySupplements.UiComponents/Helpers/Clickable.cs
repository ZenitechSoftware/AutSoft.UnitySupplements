#nullable enable
using System;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.Helpers
{
    public abstract class Clickable : MonoBehaviour
    {
        public event Action? onClick;
        public event Action? onDeselect;
        public event Action? onSelect;
        public event Action? interactableChanged;

        private void OnDestroy()
        {
            onClick = null;
            onDeselect= null;
            onSelect = null;
            interactableChanged = null;
        }

        private bool _interactable = true;

        public bool Interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;
                InteractableChanged(_interactable);
                interactableChanged?.Invoke();
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
