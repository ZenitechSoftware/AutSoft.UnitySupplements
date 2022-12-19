#nullable enable
using AutSoft.UnitySupplements.Vitamins.Bindings;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class TimePickerSelectionHighlighter : BaseSelectionHighlighter
    {
        [SerializeField] private TMP_InputField _currentField = default!;
        public override bool IsHighlighted
        {
            get => _currentField.isFocused;
            protected set
            {
                if (value)
                {
                    _currentField.Select();
                }
                SetColorsByState();
            }
        }

        public override bool Interactable => _currentField.interactable;

        private void Start()
        {
            this.Bind(_currentField.onSubmit, OnSubmittedTime);
            this.Bind(_currentField.onEndEdit, OnEditEnd);
        }

        private void OnSubmittedTime(string _) => EventSystem.current.SetSelectedGameObject(null!);
        private void OnEditEnd(string _) => IsHighlighted = false;
    }
}
