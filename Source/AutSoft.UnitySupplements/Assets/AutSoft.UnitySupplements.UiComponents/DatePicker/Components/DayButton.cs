#nullable enable
using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class DayButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _dayLabel = default!;
        [SerializeField] private Toggleable _dayButton = default!;

        private DateTimeOffset _currentDate;
        private DatePicker? _datePicker;
        private bool _isOtherMonth;

        private void Awake() => this.CheckSerializedFields();

        private void Start() => this.Bind(_dayButton.onValueChanged, DateSelected);

        public void SetupDayButton(DateTimeOffset currentDate, bool otherMonth, DatePicker datePicker, TMP_FontAsset _font, ToggleableGroup toggleGroup)
        {
            _currentDate = currentDate;
            _datePicker = datePicker;
            _dayLabel.font = _font;
            _dayButton.SetToggleGroup(toggleGroup);
            _dayLabel.text = currentDate.ToString("dd").TrimStart('0');
            _isOtherMonth = otherMonth;
            if (otherMonth)
            {
                _dayButton.Interactable = false;
                if(_dayButton.TryGetComponent<DateSelectionHighlighter>(out var highlighter))
                {
                    highlighter.SetColorsByState();
                }
            }
        }

        public void SelectDate() => _dayButton.IsOn = true;

        private void DateSelected(bool isOn)
        {
            if (!_isOtherMonth)
            {
                _dayButton.Interactable = !isOn;
            }
            if (!isOn) return;
            _datePicker.IsObjectNullThrow();
            _datePicker.SetDate(_currentDate);
        }
    }
}
