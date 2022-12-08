using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class YearBlockStepper : MonoBehaviour
    {
        [SerializeField] private Clickable _previousButton = default!;
        [SerializeField] private Clickable _nextButton = default!;

        [Header("External")]
        [SerializeField] private YearPicker _yearPicker = default!;
        private void Awake()
        {
            this.CheckSerializedFields();
            this.Bind(_previousButton.onClick, StepBack);
            this.Bind(_nextButton.onClick, StepForward);
        }

        private void StepForward() => _yearPicker.StepYears(20);

        private void StepBack() => _yearPicker.StepYears(-20);
    }
}
