#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System;
using System.Globalization;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class TimePickerHolder : MonoBehaviour
    {
        [SerializeField] private TimePicker _hourPicker = default!;
        [SerializeField] private TimePicker _minutePicker = default!;

        private DatePicker? _datePicker;
        private bool _isAmPm;
        private AmPmSelector? _amPmSelector;

        private void Awake() => this.CheckSerializedFields();

        private void Start()
        {
            this.Bind(_hourPicker.onTimeChanged, SetHour);
            this.Bind(_minutePicker.onTimeChanged, SetMinute);
        }

        public void InitTimePicker(DatePicker datePicker, DateTimeOffset initialTime)
        {
            _datePicker = datePicker;
            if (CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern.Contains("tt"))
            {
                _amPmSelector = Instantiate(Resources.Load<GameObject>("AmPmSelector"), transform).GetComponent<AmPmSelector>();
                _amPmSelector.InitAmPmSelector(datePicker, initialTime.Hour > 12);
                _isAmPm = true;
                _hourPicker.InitTimePicker(initialTime.Hour % 12, 12);
            }
            else
            {
                _hourPicker.InitTimePicker(initialTime.Hour, 24);
            }
            _minutePicker.InitTimePicker(initialTime.Minute, 60);
        }
        private void SetHour(int hour)
        {
            _datePicker.IsObjectNullThrow();
            if (_isAmPm)
            {
                _amPmSelector.IsObjectNullThrow();
                _datePicker.SetHour(hour + (_amPmSelector.IsAm ? 0 : 12));
            }
            else
            {
                _datePicker.SetHour(hour);
            }
        }

        private void SetMinute(int minute)
        {
            _datePicker.IsObjectNullThrow();
            _datePicker.SetMinute(minute);
        }
    }
}
