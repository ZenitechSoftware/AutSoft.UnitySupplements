using AutSoft.UnitySupplements.Vitamins;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class YearPicker : MonoBehaviour
    {
        [SerializeField] private Transform _buttonParent = default!;
        [SerializeField] private MonthPicker _monthPicker = default!;
        [SerializeField] private TMP_Text _yearMonthLabel = default!;

        private int _currentYearStart;

        private void Awake() => this.CheckSerializedFields();

        public void SpawnYears(int startYear)
        {
            _buttonParent.DestroyChildren();
            startYear -= startYear % 10;
            _currentYearStart = startYear;
            for (var i = 0; i < 20; i++)
            {
                var currentYear = Instantiate(Resources.Load<GameObject>("YearButton"), _buttonParent);
                currentYear.GetComponent<YearButton>().SetupYearButton(_currentYearStart + i, _monthPicker, gameObject);
                if(_currentYearStart +i == startYear)
                {
                    currentYear.GetComponent<YearSelectionHighlighter>().HighLightButton();
                }
            }
            _yearMonthLabel.text = $"{_currentYearStart} - {_currentYearStart + 19}";
        }

        public void StepYears(int step)
        {
            _currentYearStart += step;
            SpawnYears(_currentYearStart);
        }
    }
}
