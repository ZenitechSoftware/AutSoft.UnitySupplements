using AutSoft.UnitySupplements.Vitamins;
using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class WeekDaySpwaner : MonoBehaviour
    {
        private readonly Dictionary<DayOfWeek, string> _dayNames = new();
        private void InitDayNames()
        {
            _dayNames.Clear();
            for (var i = 0; i < 7; i++)
            {
                _dayNames.Add((DayOfWeek)i, CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[i]);
            }
        }
        public void SpawnWeekDayLetters()
        {
            InitDayNames();
            transform.DestroyChildren();
            var firstDayOfWeek = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            for (var i = firstDayOfWeek; i < firstDayOfWeek + 7; i++)
            {
                var currentLetter = Instantiate(Resources.Load<GameObject>("WeekLetter"), transform);
                currentLetter.GetComponent<TMP_Text>().text = _dayNames[(DayOfWeek)(i % 7)];
            }
        }
    }
}
