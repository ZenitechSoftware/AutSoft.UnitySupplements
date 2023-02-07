using AutSoft.UnitySupplements.Vitamins;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    /// <summary>
    /// Simple date and time picker. Initializes to <see cref="DateTimeOffset.Now"/> on Awake() by default.
    /// Used on the DatePicker and DatePickerXR prefabs.
    /// </summary>
    public class DatePicker : MonoBehaviour
    {
        [SerializeField] private YearMonthPicker _monthYearPicker = default!;
        [SerializeField] private WeekDaySpwaner _weekDaySpwaner = default!;
        [SerializeField] private DayNumberSpawner _dayNumberSpawner = default!;
        [SerializeField] private TimePickerHolder _timePicker = default!;
        [SerializeField] private DateTimeToggler _dateTimeToggler = default!;
        [SerializeField] private TMP_FontAsset _font = default!;
        private DateTimeOffset _pickedDate;

        /// <summary>
        /// Invoked on <see cref="PickedDateTime"/> change.
        /// </summary>
        public UnityEvent<DateTimeOffset> onDateTimePicked { get; } = new();

        /// <summary>
        /// Gets the currently selected date and time.
        /// Use the setter methods or <see cref="InitWithDate"/> to set value.
        /// </summary>
        public DateTimeOffset PickedDateTime
        {
            get => _pickedDate;
            private set
            {
                _pickedDate = value;
                onDateTimePicked.Invoke(_pickedDate);
            }
        }

        private void Awake()
        {
            this.CheckSerializedFields();
            InitWithDate(DateTimeOffset.Now);
        }

        public void InitWithDate(DateTimeOffset pickedDate)
        {
            PickedDateTime = pickedDate;
            PickedDateTime = PickedDateTime.AddSeconds(-PickedDateTime.Second);

            _monthYearPicker.ActivateDefault();
            _dateTimeToggler.ActivateDefault();
            _dayNumberSpawner.InitDays(this, _font);
            _dayNumberSpawner.UpdatePickedDate(PickedDateTime);
            _timePicker.InitTimePicker(this, PickedDateTime, _font);
            _monthYearPicker.InitYearMonth(PickedDateTime, _font);
            _weekDaySpwaner.SpawnWeekDayLetters(_font);
        }

        private void OnValidate()
        {
            foreach (var text in GetComponentsInChildren<TMP_Text>())
            {
                text.font = _font;
            }
        }

        /// <summary>
        /// Sets the current date without changing time of the day.
        /// </summary>
        public void SetDate(DateTimeOffset pickedDate)
        {
            PickedDateTime = PickedDateTime.AddYears(pickedDate.Year - PickedDateTime.Year).AddMonths(pickedDate.Month - PickedDateTime.Month).AddDays(pickedDate.Day - PickedDateTime.Day);
            _dayNumberSpawner.UpdatePickedDate(PickedDateTime);
        }

        public void SetHour(int hour) => PickedDateTime = PickedDateTime.AddHours(hour - PickedDateTime.Hour);

        public void SetMinute(int minute) => PickedDateTime = PickedDateTime.AddMinutes(minute - PickedDateTime.Minute);

        public void AddHour(int hour) => PickedDateTime = PickedDateTime.AddHours(hour);
    }
}
