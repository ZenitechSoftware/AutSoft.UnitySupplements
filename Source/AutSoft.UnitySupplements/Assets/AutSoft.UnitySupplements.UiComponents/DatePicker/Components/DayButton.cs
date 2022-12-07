#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    [RequireComponent(typeof(Toggle))]
    public class DayButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _dayLabel = default!;
        [SerializeField] private Toggle _dayButton = default!;

        private DateTimeOffset _currentDate;
        private DatePicker? _datePicker;

        private void Awake() => this.CheckSerializedFields();

        private void Start() => this.Bind(_dayButton.onValueChanged, DateSelected);

        public void SetupDayButton(DateTimeOffset currentDate, bool otherMonth, DatePicker datePicker, TMP_FontAsset _font)
        {
            _currentDate = currentDate;
            _datePicker = datePicker;
            _dayLabel.font = _font;
            _dayLabel.text = currentDate.ToString("dd").TrimStart('0');
            if (otherMonth)
            {
                _dayButton.interactable = false;
                _dayButton.GetComponent<DateSelectionHighlighter>().SetColorsByState();
            }
        }

        private void DateSelected(bool isOn)
        {
            _dayButton.interactable = !isOn;
            if (!isOn) return;
            _datePicker.IsObjectNullThrow();
            _datePicker.SetDate(_currentDate);
        }
    }
}
