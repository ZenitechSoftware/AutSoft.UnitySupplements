#nullable enable
using AutSoft.UnitySupplements.Vitamins;
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
        private void Awake()
        {
            this.CheckSerializedFields();

            _hourPicker.onTimeChanged += SetHour;
            _minutePicker.onTimeChanged += SetMinute;
        }

        private void OnDestroy()
        {
            _hourPicker.onTimeChanged -= SetHour;
            _minutePicker.onTimeChanged -= SetMinute;
        }

        public void InitTimePicker(DatePicker datePicker, DateTimeOffset initialTime)
        {
            _datePicker = datePicker;
            if (CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern.Contains("tt"))
            {
                Instantiate(Resources.Load<GameObject>("AmPmSelector"), transform);
                _hourPicker.InitTimePicker(initialTime.Hour, 12);
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
            _datePicker.SetHour(hour);
        }

        private void SetMinute(int minute)
        {
            _datePicker.IsObjectNullThrow();
            _datePicker.SetMinute(minute);
        }


    }
}
