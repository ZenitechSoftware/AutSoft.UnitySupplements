using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class YearStepper : MonoBehaviour
    {
        [SerializeField] private Clickable _previousYear = default!;
        [SerializeField] private Clickable _nextYear = default!;

        [Header("External")]
        [SerializeField] private MonthPicker _monthPicker = default!;

        private void Awake()
        {
            this.CheckSerializedFields();

            this.Bind(_nextYear.onClick, NextMonth);
            this.Bind(_previousYear.onClick, PreviousMonth);
        }

        private void PreviousMonth() => _monthPicker.StepYear(-1);

        private void NextMonth() => _monthPicker.StepYear(1);

    }
}
