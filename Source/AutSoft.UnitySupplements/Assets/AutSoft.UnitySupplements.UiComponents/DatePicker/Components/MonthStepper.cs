using System;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class MonthStepper : MonoBehaviour
    {
        [SerializeField] private YearMonthPicker _yearMonthPicker = default!;
        [SerializeField] private Button _nextMonth = default!;
        [SerializeField] private Button _previousMonth = default!;

        public void InitializeMonthStepper()
        {
            _nextMonth.onClick.AddListener(NextMonth);
            _previousMonth.onClick.AddListener(PreviousMonth);
        }

        private void OnDestroy()
        {
            _nextMonth.onClick?.RemoveListener(NextMonth);
            _previousMonth.onClick?.RemoveListener(PreviousMonth);
        }

        private void PreviousMonth() => SwitchMonth(_yearMonthPicker.CurrentMonth.AddMonths(-1));
        private void NextMonth() => SwitchMonth(_yearMonthPicker.CurrentMonth.AddMonths(1));

        public void SwitchMonth(DateTimeOffset month) => _yearMonthPicker.InitYearMonth(month);
    }
}
