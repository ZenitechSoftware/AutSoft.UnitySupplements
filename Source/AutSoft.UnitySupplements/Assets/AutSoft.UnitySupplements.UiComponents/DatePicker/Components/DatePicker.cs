using AutSoft.UnitySupplements.Vitamins;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    /// <summary>
    /// Simple date and time picker. Initializes to <see cref="DateTimeOffset.Now"/> by default.
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
        /// Invoked on <see cref="PickedDate"/> change.
        /// </summary>
        public UnityEvent<DateTimeOffset> onTimePicked { get; } = new();

        /// <summary>
        /// Gets the currently selected date and time.
        /// Use <see cref="SetDate"/> to set value.
        /// </summary>
        public DateTimeOffset PickedDate
        {
            get => _pickedDate;
            private set
            {
                _pickedDate = value;
                onTimePicked.Invoke(_pickedDate);
            }
        }

        private void Awake()
        {
            this.CheckSerializedFields();
            InitWithDate(DateTimeOffset.Now);
        }

        public void InitWithDate(DateTimeOffset pickedDate)
        {
            PickedDate = pickedDate;
            PickedDate = PickedDate.AddSeconds(-PickedDate.Second);

            _monthYearPicker.ActivateDefault();
            _dateTimeToggler.ActivateDefault();
            _dayNumberSpawner.InitDays(this, _font);
            _dayNumberSpawner.UpdatePickedDate(PickedDate);
            _timePicker.InitTimePicker(this, PickedDate, _font);
            _monthYearPicker.InitYearMonth(PickedDate, _font);
            _weekDaySpwaner.SpawnWeekDayLetters(_font);
        }

        private void OnValidate()
        {
            foreach (var font in GetComponentsInChildren<TMP_Text>())
            {
                font.font = _font;
            }
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
