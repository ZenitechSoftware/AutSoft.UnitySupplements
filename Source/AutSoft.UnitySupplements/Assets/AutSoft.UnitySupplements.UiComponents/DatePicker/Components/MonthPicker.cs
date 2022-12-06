using AutSoft.UnitySupplements.Vitamins;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class MonthPicker : MonoBehaviour
    {
        [SerializeField] private Transform _buttonParent = default!;
        [SerializeField] private TMP_Text _yearMonthLabel = default!;
        [SerializeField] private YearMonthPicker _yearMonthPicker = default!;

        private int _year;
        private readonly List<MonthButton> _monthButtons = new();

        private void Awake()
        {
            this.CheckSerializedFields();
            _buttonParent.DestroyChildren();
            var monthnames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames;
            for (var i = 0; i < monthnames.Length; i++)
            {
                var monthname = monthnames[i];
                var currentMonth = Instantiate(Resources.Load<GameObject>("MonthButton"), _buttonParent);
                var monthButton = currentMonth.GetComponent<MonthButton>();
                monthButton.SetupYearButton(monthname, i + 1, _yearMonthPicker, _year);
                _monthButtons.Add(monthButton);
            }
        }

        public void InitYear(int year)
        {
            _year = year;
            _yearMonthLabel.text = year.ToString();
            foreach (var monthButton in _monthButtons)
            {
                monthButton.UpdateYear(year);
            }
        }
    }
}
