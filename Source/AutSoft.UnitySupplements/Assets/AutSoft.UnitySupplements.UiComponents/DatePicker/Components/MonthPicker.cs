#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class MonthPicker : MonoBehaviour
    {
        [SerializeField] private Transform _buttonParent = default!;
        [SerializeField] private TMP_Text _yearMonthLabel = default!;
        [SerializeField] private YearMonthPicker _yearMonthPicker = default!;

        private int _currentYear;
        private TMP_FontAsset? _font;
        private int _currentMonth;
        private readonly List<MonthButton> _monthButtons = new();

        private void Awake() => this.CheckSerializedFields();

        private void CreateMonthButtons(TMP_FontAsset font)
        {
            _buttonParent.DestroyChildren();
            _monthButtons.Clear();
            var monthnames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
            for (var i = 0; i < monthnames.Count(m => m != string.Empty); i++)
            {
                var monthname = monthnames[i];
                var currentMonth = Instantiate(Resources.Load<GameObject>("MonthButton"), _buttonParent);
                var monthButton = currentMonth.GetComponent<MonthButton>();
                monthButton.SetupYearButton(monthname, i + 1, _yearMonthPicker, _currentYear, font);
                _monthButtons.Add(monthButton);
                monthButton.GetComponent<YearSelectionHighlighter>().Highlight(false);
            }

            if (_currentYear == _yearMonthPicker.CurrentDate.Year)
            {
                _monthButtons[_currentMonth - 1].GetComponent<YearSelectionHighlighter>().Highlight(true);
            }
        }

        public void InitYear(int year, TMP_FontAsset font)
        {
            _currentYear = year;
            _font = font;
            _yearMonthLabel.text = year.ToString();
            CreateMonthButtons(font);
        }

        public void StepYear(int step)
        {
            _font.IsObjectNullThrow();
            InitYear(_currentYear + step, _font);
        }

        public void SetCurrentMonth(int currentMonth) => _currentMonth = currentMonth;
    }
}
