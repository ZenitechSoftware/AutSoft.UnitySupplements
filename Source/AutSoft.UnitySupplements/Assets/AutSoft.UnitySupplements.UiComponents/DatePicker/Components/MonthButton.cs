#nullable enable
using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    [RequireComponent(typeof(Clickable))]
    public class MonthButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _buttonLabel = default!;

        private int _year;
        private YearMonthPicker? _yearMonthPicker;
        private int _monthNumber;

        private void Awake()
        {
            this.CheckSerializedFields();

            var monthButton = GetComponent<Clickable>();
            this.Bind(monthButton.onClick, SetYearMonth);
        }

        public void SetupYearButton(string monthname, int monthNumber, YearMonthPicker yearMonthPicker, int year, TMP_FontAsset font)
        {
            _yearMonthPicker = yearMonthPicker;
            _monthNumber = monthNumber;
            _buttonLabel.text = monthname;
            _buttonLabel.font = font;
            _year = year;
        }

        public void UpdateYear(int year) => _year = year;

        private void SetYearMonth()
        {
            _yearMonthPicker.IsObjectNullThrow();
            _yearMonthPicker.SetMonthYear(new DateTimeOffset(_year, _monthNumber, 1, 0, 0, 0, TimeSpan.Zero));
        }
    }
}
