using AutSoft.UnitySupplements.Vitamins;
using System;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class DatePicker : MonoBehaviour
    {
        [SerializeField] private YearMonthPicker _monthYearPicker = default!;
        [SerializeField] private WeekDaySpwaner _weekDaySpwaner = default!;
        [SerializeField] private DayNumberSpawner _dayNumberSpawner = default!;
        [SerializeField] private MonthStepper _monthStepper = default!;

        private DateTimeOffset _pickedDate;

        private void Awake()
        {
            this.CheckSerializedFields();

            _pickedDate = DateTimeOffset.Now;

            //TODO: remove this
            //CultureInfo.CurrentCulture = new CultureInfo("en-US");
            //CultureInfo.CurrentCulture = new CultureInfo("sv-SE");
            _dayNumberSpawner.InitDays(this);
            _monthYearPicker.InitYearMonth(_pickedDate);
            _weekDaySpwaner.SpawnWeekDayLetters();
            _monthStepper.InitializeMonthStepper();
        }

        public void SetDate(DateTimeOffset pickedDate)
        {
            Debug.Log($"Date changed: {pickedDate}");
            _pickedDate = pickedDate;
        }
    }
}
