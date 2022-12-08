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
        [SerializeField] private ToggleGroup _toggleGroup = default!;

        private DatePicker? _datePicker;
        private DateTimeOffset _pickedDate;
        private TMP_FontAsset? _font;

        public void SpawnDaysForMonth(DateTimeOffset firstDayOfMonth)
        {
            _datePicker.IsObjectNullThrow();
            _font.IsObjectNullThrow();
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
                currentDate.GetComponent<DayButton>()
                    .SetupDayButton(startDate, startDate.Month != firstDayOfMonth.Month, _datePicker, _font, _toggleGroup);
                if (startDate.Date == _pickedDate.Date)
                {
                    currentDate.GetComponent<DateSelectionHighlighter>().Highlight(true);
                    inSelectedMonth = true;
                }
                startDate = startDate.AddDays(1);
            }

            _toggleGroup.allowSwitchOff = !inSelectedMonth;
        }

        public void InitDays(DatePicker datePicker, TMP_FontAsset font)
        {
            _datePicker = datePicker;
            _font = font;
        }

        public void UpdatePickedDate(DateTimeOffset pickedDate) => _pickedDate = pickedDate;
    }
}
