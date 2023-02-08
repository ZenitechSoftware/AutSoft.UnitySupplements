#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    /// <summary>
    /// Input field for time using hour and minute spinners and an AM/PM selector.
    /// </summary>
    /// <remarks>Requires initialization with a <see cref="DatePicker"/> component.</remarks>
    public class TimePickerHolder : MonoBehaviour
    {
        [SerializeField] private SpinnerInput _hourPicker = default!;
        [SerializeField] private SpinnerInput _minutePicker = default!;

        [Header("External")]
        [SerializeField] private AmPmSelector _amPmSelectorPrefab = default!;

        private DatePicker? _datePicker;
        private bool _isAmPm;
        private AmPmSelector? _amPmSelector;

        private void Awake() => this.CheckSerializedFields();

        private void Start()
        {
            this.Bind(_hourPicker.onValueChanged, SetHour);
            this.Bind(_minutePicker.onValueChanged, SetMinute);
        }

        public void InitTimePicker(DatePicker datePicker, DateTimeOffset initialTime, TMP_FontAsset font)
        {
            _datePicker = datePicker;
            if (CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern.Contains("tt"))
            {
                if (_amPmSelector.IsObjectNull())
                {
                    _amPmSelector = Instantiate(_amPmSelectorPrefab.gameObject, transform).GetComponent<AmPmSelector>();
                }
                _amPmSelector.InitAmPmSelector(datePicker, initialTime.Hour >= 12, font);
                _isAmPm = true;
                var currentHour = initialTime.Hour % 12 == 0 ? 12 : initialTime.Hour % 12;
                _hourPicker.Initialize(currentHour, 1, 12, font);
                _minutePicker.Initialize(initialTime.Minute, 0, 59, font);
            }
            else
            {
                _hourPicker.Initialize(initialTime.Hour, 0, 24, font);
                _minutePicker.Initialize(initialTime.Minute,0, 59, font);
            }
        }

        private void SetHour(int hour)
        {
            _datePicker.IsObjectNullThrow();
            if (_isAmPm)
            {
                _amPmSelector.IsObjectNullThrow();
                if (hour == 12)
                {
                    _datePicker.SetHour(_amPmSelector.IsAm ? 0 : 12);
                }
                else
                {
                    _datePicker.SetHour(hour + (_amPmSelector.IsAm ? 0 : 12));
                }
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
