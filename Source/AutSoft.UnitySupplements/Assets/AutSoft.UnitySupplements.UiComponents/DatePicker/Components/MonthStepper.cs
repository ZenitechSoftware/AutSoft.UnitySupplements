using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class MonthStepper : MonoBehaviour
    {
        [SerializeField] private Clickable _previousMonth = default!;
        [SerializeField] private Clickable _nextMonth = default!;

        [Header("External")]
        [SerializeField] private YearMonthPicker _yearMonthPicker = default!;

        public void InitializeMonthStepper()
        {
            this.Bind(_nextMonth.onClick, NextMonth);
            this.Bind(_previousMonth.onClick, PreviousMonth);
        }

        private void PreviousMonth() => SwitchMonth(_yearMonthPicker.CurrentDate.AddMonths(-1));

        private void NextMonth() => SwitchMonth(_yearMonthPicker.CurrentDate.AddMonths(1));

        public void SwitchMonth(DateTimeOffset month) => _yearMonthPicker.SetYearMonth(month);
    }
}
