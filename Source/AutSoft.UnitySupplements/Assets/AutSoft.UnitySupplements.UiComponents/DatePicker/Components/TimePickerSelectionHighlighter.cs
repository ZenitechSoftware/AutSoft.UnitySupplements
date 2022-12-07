using AutSoft.UnitySupplements.Vitamins.Bindings;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class TimePickerSelectionHighlighter : BaseSelectionHighlighter
    {
        [SerializeField] private TMP_InputField _currentField = default!;

        private void Start() => this.Bind(_currentField.onEndEdit, OnEditEnd);

        private void OnEditEnd(string _) => IsHighlighted = false;

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
    }
}
