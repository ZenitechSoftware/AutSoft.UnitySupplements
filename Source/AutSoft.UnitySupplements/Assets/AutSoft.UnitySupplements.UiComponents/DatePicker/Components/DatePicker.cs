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

        private DateTimeOffset _pickedDate;

        private void Awake()
        {
            this.CheckSerializedFields();

            _pickedDate = DateTimeOffset.Now;

            //TODO: remove this
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            //CultureInfo.CurrentCulture = new CultureInfo("sv-SE");
            _monthYearPicker.InitYearMonth(_pickedDate);
            _weekDaySpwaner.SpawnWeekDayLetters();
            _dayNumberSpawner.SpawnDaysForMonth(new DateTimeOffset(_pickedDate.Year, _pickedDate.Month, 1, 0, 0, 0, TimeSpan.Zero));
            _monthStepper.InitializeMonthStepper();
        }
    }
}
