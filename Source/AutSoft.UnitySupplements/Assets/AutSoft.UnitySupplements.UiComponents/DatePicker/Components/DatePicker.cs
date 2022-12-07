using AutSoft.UnitySupplements.Vitamins;
using System;
using UnityEngine;
using UnityEngine.Events;

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

        public UnityEvent<DateTimeOffset> onTimePicked { get; } = new();

        public DateTimeOffset PickedDate
        {
            get => _pickedDate; set
            {
                _pickedDate = value;
                onTimePicked.Invoke(_pickedDate);
            }
        }

        private void Awake()
        {
            this.CheckSerializedFields();

            PickedDate = DateTimeOffset.Now;
            PickedDate = PickedDate.AddSeconds(-PickedDate.Second);

            //TODO: remove this
            //CultureInfo.CurrentCulture = new CultureInfo("en-US");
            //CultureInfo.CurrentCulture = new CultureInfo("sv-SE");
            _dayNumberSpawner.InitDays(this);
            _dayNumberSpawner.UpdatePickedDate(PickedDate);
            _timePicker.InitTimePicker(this, PickedDate);
            _monthYearPicker.InitYearMonth(PickedDate);
            _weekDaySpwaner.SpawnWeekDayLetters();
            _monthStepper.InitializeMonthStepper();
        }

        public void SetDate(DateTimeOffset pickedDate)
        {
            PickedDate = PickedDate.AddYears(pickedDate.Year - PickedDate.Year).AddMonths(pickedDate.Month - PickedDate.Month).AddDays(pickedDate.Day - PickedDate.Day);
            _dayNumberSpawner.UpdatePickedDate(PickedDate);
        }

        public void SetHour(int hour) => PickedDate = PickedDate.AddHours(hour - PickedDate.Hour);

        public void SetMinute(int minute) => PickedDate = PickedDate.AddMinutes(minute - PickedDate.Minute);

        public void AddHour(int hour) => PickedDate = PickedDate.AddHours(hour);
    }
}
