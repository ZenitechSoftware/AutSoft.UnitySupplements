using AutSoft.UnitySupplements.Vitamins;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class YearMonthPicker : MonoBehaviour
    {
        [SerializeField] private TMP_Text _yearMonthLabel = default!;
        [SerializeField] private Image _arrowImage = default!;
        [SerializeField] private Button _arrowButton = default!;
        [SerializeField] private GameObject _yearsObject = default!;
        [SerializeField] private GameObject _daySelectorObject = default!;
        [SerializeField] private GameObject _monthSelectorObject = default!;
        [SerializeField] private DayNumberSpawner _dayNumberSpawner = default!;
        [SerializeField] private YearPicker _yearPicker = default!;

        public DateTimeOffset CurrentMonth { get; private set; }

        private void Awake()
        {
            this.CheckSerializedFields();

            _arrowButton.onClick.AddListener(ToggleYearMonthPanel);
        }

        private void ToggleYearMonthPanel()
        {
            _daySelectorObject.SetActive(!_daySelectorObject.activeInHierarchy);
            if (_yearsObject.activeInHierarchy || _monthSelectorObject.activeInHierarchy)
            {
                _yearsObject.SetActive(false);
                _monthSelectorObject.SetActive(false);
            }
            else
            {
                _yearsObject.SetActive(true);
            }

            if (_daySelectorObject.activeInHierarchy)
            {
                _dayNumberSpawner.SpawnDaysForMonth(CurrentMonth);
            }
            else
            {
                _yearPicker.SpawnYears(CurrentMonth.Year);
            }
            _arrowImage.transform.Rotate(new Vector3(0, 0, 180));
            SetYearMonthLabel(CurrentMonth);
        }

        public void InitYearMonth(DateTimeOffset currentDate)
        {
            CurrentMonth = new DateTimeOffset(currentDate.Year, currentDate.Month, 1, 0, 0, 0, TimeSpan.Zero);
            _dayNumberSpawner.SpawnDaysForMonth(CurrentMonth);
            SetYearMonthLabel(CurrentMonth);
        }

        private void SetYearMonthLabel(DateTimeOffset currentDate)
        {
            if (_daySelectorObject.activeInHierarchy)
            {
                _yearMonthLabel.text = currentDate.ToString("Y");
            }
        }

        public void SetMonthYear(DateTimeOffset dateTimeOffset)
        {
            InitYearMonth(dateTimeOffset);
            ToggleYearMonthPanel();
        }
    }
}
