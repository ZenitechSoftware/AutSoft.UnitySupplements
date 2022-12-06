using AutSoft.UnitySupplements.Vitamins;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class YearPicker : MonoBehaviour
    {
        [SerializeField] private Transform _buttonParent;

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
                currentYear.GetComponent<YearButton>().SetupYearButton(startYear + i);
            }
        }

        public void StepYears(int step)
        {
            _currentYearStart += step;
            SpawnYears(_currentYearStart);
        }
    }
}
