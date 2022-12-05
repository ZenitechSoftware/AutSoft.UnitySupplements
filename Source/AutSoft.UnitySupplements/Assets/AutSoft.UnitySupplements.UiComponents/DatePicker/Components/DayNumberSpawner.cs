#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class DayNumberSpawner : MonoBehaviour
    {
        [SerializeField] private Color _otherMonth;

        private ToggleGroup? _toggleGroup;
        private void SetupToggleGroup() => _toggleGroup = GetComponent<ToggleGroup>();

        public void SpawnDaysForMonth(DateTimeOffset firstDayOfMonth)
        {
            if (_toggleGroup.IsObjectNull()) SetupToggleGroup();
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

            var endDate = new DateTimeOffset(
                firstDayOfMonth.Year,
                firstDayOfMonth.Month,
                DateTime.DaysInMonth(firstDayOfMonth.Year, firstDayOfMonth.Month), 0, 0, 0, TimeSpan.Zero);
            var lastDayOfWeek = startDate.AddDays(6).DayOfWeek;
            while (endDate.DayOfWeek != lastDayOfWeek)
            {
                endDate = endDate.AddDays(1);
            }

            while (startDate <= endDate)
            {
                var currentDate = Instantiate(Resources.Load<GameObject>("MonthNumber"), transform);
                currentDate.GetComponent<Toggle>().group = _toggleGroup;
                var monthNumber = currentDate.GetComponent<TMP_Text>();
                monthNumber.text = startDate.ToString("dd").TrimStart('0');
                if (startDate.Month != firstDayOfMonth.Month)
                {
                    monthNumber.color = _otherMonth;
                }
                startDate = startDate.AddDays(1);
            }
        }
    }
}
