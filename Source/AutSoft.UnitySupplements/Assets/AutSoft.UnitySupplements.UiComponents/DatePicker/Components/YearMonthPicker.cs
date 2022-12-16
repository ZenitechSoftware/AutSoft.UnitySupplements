#nullable enable
using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class YearMonthPicker : MonoBehaviour
    {
        [SerializeField] private TMP_Text _yearMonthLabel = default!;
        [SerializeField] private Transform _arrowTransform = default!;
        [SerializeField] private Clickable _arrowButton = default!;

        [Header("External")]
        [SerializeField] private GameObject _yearsObject = default!;
        [SerializeField] private GameObject _daySelectorObject = default!;
        [SerializeField] private GameObject _monthSelectorObject = default!;
        [SerializeField] private DayNumberSpawner _dayNumberSpawner = default!;
        [SerializeField] private YearPicker _yearPicker = default!;

        private TMP_FontAsset? _font;

        public DateTimeOffset CurrentDate { get; private set; }

        private void Awake() => this.CheckSerializedFields();

        private void Start() => this.Bind(_arrowButton.onClick, ToggleYearMonthPanel);

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
                _dayNumberSpawner.SpawnDaysForMonth(CurrentDate);
            }
            else
            {
                _font.IsObjectNullThrow();
                _yearPicker.SpawnYears(CurrentDate.Year, CurrentDate.Month, _font);
            }
            _arrowTransform.Rotate(new Vector3(0, 0, 180));
            SetYearMonthLabel(CurrentDate);
        }

        public void InitYearMonth(DateTimeOffset currentDate, TMP_FontAsset font)
        {
            _font = font;
            SetYearMonth(currentDate);
        }

        public void SetYearMonth(DateTimeOffset currentDate)
        {
            CurrentDate = new DateTimeOffset(currentDate.Year, currentDate.Month, 1, 0, 0, 0, TimeSpan.Zero);
            _dayNumberSpawner.SpawnDaysForMonth(CurrentDate);
            SetYearMonthLabel(CurrentDate);
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
            SetYearMonth(dateTimeOffset);
            ToggleYearMonthPanel();
        }

        public void ActivateDefault()
        {
            _daySelectorObject.SetActive(true);
            _yearsObject.SetActive(false);
            _monthSelectorObject.SetActive(false);
        }
    }
}
