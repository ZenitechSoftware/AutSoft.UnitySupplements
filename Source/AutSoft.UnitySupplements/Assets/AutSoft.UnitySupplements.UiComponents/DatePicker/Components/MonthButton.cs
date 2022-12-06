#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    [RequireComponent(typeof(Button))]
    public class MonthButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _buttonLabel = default!;

        private int _year;
        private YearMonthPicker? _yearMonthPicker;
        private int _monthNumber;
        private Button _monthButton = default!;

        private void Awake()
        {
            this.CheckSerializedFields();

            _monthButton = GetComponent<Button>();
            _monthButton.onClick.AddListener(SetYearMonth);
        }

        private void OnDestroy() => _monthButton.onClick.RemoveListener(SetYearMonth);

        public void SetupYearButton(string monthname, int monthNumber, YearMonthPicker yearMonthPicker, int year)
        {
            _yearMonthPicker = yearMonthPicker;
            _monthNumber = monthNumber;
            _buttonLabel.text = monthname;
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
