#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class YearPicker : DateSpawner
    {
        [SerializeField] private Transform _buttonParent = default!;

        [Header("External")]
        [SerializeField] private MonthPicker _monthPicker = default!;
        [SerializeField] private TMP_Text _yearMonthLabel = default!;
        [SerializeField] private YearMonthPicker _yearMonthPicker = default!;
        [SerializeField] private YearButton _yearButtonPrefab = default!;

        private int _currentYearStart;
        private int _currentMonth;
        private TMP_FontAsset? _font;

        private void Awake() => this.CheckSerializedFields();

        public void SpawnYears(int startYear, int currentMonth, TMP_FontAsset font)
        {
            _currentMonth = currentMonth;
            _font = font;
            _buttonParent.DestroyChildren();
            _currentYearStart = startYear - (startYear % 10);
            for (var i = 0; i < 20; i++)
            {
                var currentYear = Instantiate(_yearButtonPrefab.gameObject, _buttonParent);
                currentYear.GetComponent<YearButton>().SetupYearButton(_currentYearStart + i, _monthPicker, gameObject, _font);
                if (_currentYearStart + i == _yearMonthPicker.CurrentDate.Year)
                {
                    if (currentYear.TryGetComponent<YearSelectionHighlighter>(out var highlighter))
                    {
                        highlighter.Highlight(true);
                    }
                    else
                    {
                        //TODO:: highlight xr button
                    }
                }
            }
            _yearMonthLabel.text = $"{_currentYearStart} - {_currentYearStart + 19}";
            _monthPicker.SetCurrentMonth(_currentMonth);
            onSpawned.Invoke();
        }

        public void StepYears(int step)
        {
            _font.IsObjectNullThrow();
            _currentYearStart += step;
            SpawnYears(_currentYearStart, _currentMonth, _font);
        }
    }
}
