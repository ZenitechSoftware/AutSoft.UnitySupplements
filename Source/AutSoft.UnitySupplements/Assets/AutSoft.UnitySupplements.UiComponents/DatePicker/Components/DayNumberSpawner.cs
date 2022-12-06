#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class DayNumberSpawner : MonoBehaviour
    {
        private ToggleGroup? _toggleGroup;
        private DatePicker? _datePicker;

        private void SetupToggleGroup() => _toggleGroup = GetComponent<ToggleGroup>();

        public void SpawnDaysForMonth(DateTimeOffset firstDayOfMonth)
        {
            if (_toggleGroup.IsObjectNull()) SetupToggleGroup();
            _datePicker.IsObjectNullThrow();
            transform.DestroyChildren();
            var firstDayOfWeek = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            var currentDay = (int)firstDayOfMonth.DayOfWeek;
            DateTimeOffset startDate;
            if (firstDayOfWeek < currentDay)
            {
                startDate = firstDayOfMonth.AddDays(firstDayOfWeek - currentDay);
            }
            else if (currentDay < firstDayOfWeek)
            {
                startDate = firstDayOfMonth.AddDays(-(7 - (firstDayOfWeek - currentDay)));
            }
            else
            {
                startDate = firstDayOfMonth;
            }

            var endDate = startDate.AddDays(6 * 7);
            while (startDate < endDate)
            {
                var currentDate = Instantiate(Resources.Load<GameObject>("DayButton"), transform);
                currentDate.GetComponent<Toggle>().group = _toggleGroup;
                currentDate.GetComponent<DayButton>().SetupDayButton(startDate, startDate.Month != firstDayOfMonth.Month, _datePicker);
                startDate = startDate.AddDays(1);
            }
        }

        public void InitDays(DatePicker datePicker) => _datePicker = datePicker;
    }
}
