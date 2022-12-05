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
        [SerializeField] private GameObject _yearMonthObject = default!;
        [SerializeField] private GameObject _daySelectorObject = default!;
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
            _yearMonthObject.SetActive(!_yearMonthObject.activeInHierarchy);
            _daySelectorObject.SetActive(!_daySelectorObject.activeInHierarchy);
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
            SetYearMonthLabel(CurrentMonth);
        }

        private void SetYearMonthLabel(DateTimeOffset currentDate)
        {
            if (_daySelectorObject.activeInHierarchy)
            {
                _yearMonthLabel.text = currentDate.ToString("Y");
            }
            else
            {
                var yearStart = currentDate.Year - (currentDate.Year % 10);
                _yearMonthLabel.text = $"{yearStart} - {yearStart + 19}";
            }
        }
    }
}
