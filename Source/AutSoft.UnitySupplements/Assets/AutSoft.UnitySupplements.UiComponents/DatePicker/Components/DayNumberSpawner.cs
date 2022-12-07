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
        [SerializeField] private ToggleGroup _toggleGroup = default!;

        private DatePicker? _datePicker;
        private DateTimeOffset _pickedDate;


        public void SpawnDaysForMonth(DateTimeOffset firstDayOfMonth)
        {
            _datePicker.IsObjectNullThrow();
            transform.DestroyChildren();
            var inSelectedMonth = false;
            var firstDayOfWeek = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            var currentDay = (int)firstDayOfMonth.DayOfWeek;
            DateTimeOffset startDate;
            if (firstDayOfWeek < currentDay)
            {
                startDate = firstDayOfMonth.AddDays(firstDayOfWeek - currentDay);
            }
            else
            {
                startDate = currentDay < firstDayOfWeek
                    ? firstDayOfMonth.AddDays(-(7 - (firstDayOfWeek - currentDay))) 
                    : firstDayOfMonth;
            }

            var endDate = startDate.AddDays(6 * 7);
            while (startDate < endDate)
            {
                var currentDate = Instantiate(Resources.Load<GameObject>("DayButton"), transform);
                currentDate.GetComponent<Toggle>().group = _toggleGroup;
                currentDate.GetComponent<DayButton>().SetupDayButton(startDate, startDate.Month != firstDayOfMonth.Month, _datePicker);
                if (startDate.Date == _pickedDate.Date)
                {
                    currentDate.GetComponent<DateSelectionHighlighter>().HighlightDate();
                    inSelectedMonth = true;
                }
                startDate = startDate.AddDays(1);
            }

            _toggleGroup.allowSwitchOff = !inSelectedMonth;
        }

        public void InitDays(DatePicker datePicker) => _datePicker = datePicker;

        public void UpdatePickedDate(DateTimeOffset pickedDate) => _pickedDate = pickedDate;
    }
}
