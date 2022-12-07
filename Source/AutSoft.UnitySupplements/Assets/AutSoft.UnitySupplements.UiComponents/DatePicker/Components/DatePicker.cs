using AutSoft.UnitySupplements.Vitamins;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class DatePicker : MonoBehaviour
    {
        [SerializeField] private YearMonthPicker _monthYearPicker = default!;
        [SerializeField] private WeekDaySpwaner _weekDaySpwaner = default!;
        [SerializeField] private DayNumberSpawner _dayNumberSpawner = default!;
        [SerializeField] private MonthStepper _monthStepper = default!;
        [SerializeField] private TimePickerHolder _timePicker = default!;
        [SerializeField] private TMP_FontAsset _font = default!;
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
            _dayNumberSpawner.InitDays(this, _font);
            _dayNumberSpawner.UpdatePickedDate(PickedDate);
            _timePicker.InitTimePicker(this, PickedDate, _font);
            _monthYearPicker.InitYearMonth(PickedDate, _font);
            _weekDaySpwaner.SpawnWeekDayLetters(_font);
            _monthStepper.InitializeMonthStepper();
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
