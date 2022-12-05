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

        private DateTimeOffset _pickedDate;

        private void Awake()
        {
            this.CheckSerializedFields();

            _pickedDate= DateTimeOffset.Now;

            //TODO: remove this
            //CultureInfo.CurrentCulture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = new CultureInfo("sv-SE");
            _monthYearPicker.SetYearMonthLabel(_pickedDate);
            _weekDaySpwaner.SpawnWeekDayLetters();
        }
    }
}
