#nullable enable
using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class DayNumberSpawner : DateSpawner
    {
        [SerializeField] private DayButton _dayButtonPrefab = default!;
        [SerializeField] private ToggleableGroup _toggleGroup = default!;

        private DatePicker? _datePicker;
        private DateTimeOffset _pickedDate;
        private TMP_FontAsset? _font;

        private void Awake() => this.CheckSerializedFields();

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
                var currentDate = Instantiate(_dayButtonPrefab, transform);
                currentDate.GetComponent<DayButton>()
                    .SetupDayButton(startDate, startDate.Month != firstDayOfMonth.Month, _datePicker, _font, _toggleGroup);
                if (startDate.Date == _pickedDate.Date)
                {
                    if(currentDate.TryGetComponent<DateSelectionHighlighter>(out var highlighter))
                    {
                        highlighter.Highlight(true);
                    }
                    else
                    {
                        currentDate.SelectDate();
                    }
                    inSelectedMonth = true;
                }
                startDate = startDate.AddDays(1);
            }

            _toggleGroup.AllowSwitchOff = !inSelectedMonth;
            onSpawned.Invoke();
        }

        public void InitDays(DatePicker datePicker, TMP_FontAsset font)
        {
            _datePicker = datePicker;
            _font = font;
        }

        public void UpdatePickedDate(DateTimeOffset pickedDate) => _pickedDate = pickedDate;
    }
}
