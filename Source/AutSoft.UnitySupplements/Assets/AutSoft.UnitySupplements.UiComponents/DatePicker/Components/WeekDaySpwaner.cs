using AutSoft.UnitySupplements.Vitamins;
using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class WeekDaySpwaner : DateSpawner
    {
        [SerializeField] private WeekLetter _weekDayPrefab = default!;

        private readonly Dictionary<DayOfWeek, string> _dayNames = new();

        private void InitDayNames()
        {
            _dayNames.Clear();
            for (var i = 0; i < 7; i++)
            {
                _dayNames.Add((DayOfWeek)i, CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[i]);
            }
        }
        public void SpawnWeekDayLetters(TMP_FontAsset font)
        {
            InitDayNames();
            transform.DestroyChildren();
            var firstDayOfWeek = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            for (var i = firstDayOfWeek; i < firstDayOfWeek + 7; i++)
            {
                var currentLetter = Instantiate(_weekDayPrefab.gameObject, transform).GetComponent<WeekLetter>();
                currentLetter.SetWeekText(_dayNames[(DayOfWeek)(i % 7)], font);
            }
            onSpawned.Invoke();
        }
    }
}
