using AutSoft.UnitySupplements.Vitamins;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class YearBlockStepper : MonoBehaviour
    {
        [SerializeField] private YearPicker _yearPicker = default!;
        [SerializeField] private Button _previousButton = default!;
        [SerializeField] private Button _nextButton = default!;

        private void Awake()
        {
            this.CheckSerializedFields();
            _nextButton.onClick.AddListener(StepForward);
            _previousButton.onClick.AddListener(StepBack);
        }

        private void OnDestroy()
        {
            _nextButton.onClick.RemoveListener(StepForward);
            _previousButton.onClick.RemoveListener(StepBack);
        }

        private void StepForward() => _yearPicker.StepYears(20);

        private void StepBack() => _yearPicker.StepYears(-20);
    }
}
