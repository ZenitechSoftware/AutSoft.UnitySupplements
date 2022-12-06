using AutSoft.UnitySupplements.Vitamins;
using System;
using System.Globalization;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class DatePicker : MonoBehaviour
    {
        [SerializeField] private YearMonthPicker _monthYearPicker = default!;
        [SerializeField] private WeekDaySpwaner _weekDaySpwaner = default!;
        [SerializeField] private DayNumberSpawner _dayNumberSpawner = default!;
        [SerializeField] private MonthStepper _monthStepper = default!;
        [SerializeField] private TimePickerHolder _timePicker = default!;

        private DateTimeOffset _pickedDate;

        private void Awake()
        {
            this.CheckSerializedFields();

            _pickedDate = DateTimeOffset.Now;
            _pickedDate = _pickedDate.AddSeconds(-_pickedDate.Second);

            //TODO: remove this
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            //CultureInfo.CurrentCulture = new CultureInfo("sv-SE");
            _dayNumberSpawner.InitDays(this);
            _timePicker.InitTimePicker(this, _pickedDate);
            _monthYearPicker.InitYearMonth(_pickedDate);
            _weekDaySpwaner.SpawnWeekDayLetters();
            _monthStepper.InitializeMonthStepper();
        }

        public void SetDate(DateTimeOffset pickedDate)
        {
            _pickedDate = _pickedDate.AddYears(pickedDate.Year - _pickedDate.Year).AddMonths(pickedDate.Month - _pickedDate.Month).AddDays(pickedDate.Day - _pickedDate.Day);
            Debug.Log($"Date changed: {_pickedDate}");
        }

        public void SetHour(int hour)
        {
            _pickedDate = _pickedDate.AddHours(hour - _pickedDate.Hour);
            Debug.Log($"Date changed: {_pickedDate}");
        }

        public void SetMinute(int minute)
        {
            _pickedDate = _pickedDate.AddMinutes(minute - _pickedDate.Minute);
            Debug.Log($"Date changed: {_pickedDate}");
        }

        public void AddHour(int hour) => _pickedDate = _pickedDate.AddHours(hour);
    }
}
