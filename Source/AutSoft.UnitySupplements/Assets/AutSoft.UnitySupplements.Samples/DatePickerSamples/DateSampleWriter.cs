using AutSoft.UnitySupplements.UiComponents.DatePicker.Components;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.Samples.DatePickerSamples
{
    public class DateSampleWriter : MonoBehaviour
    {
        [SerializeField] private DatePicker _datePicker = default!;
        [SerializeField] private TMP_InputField _inputField = default!;

        private void Awake()
        {
            this.CheckSerializedFields();
            this.Bind(_datePicker.onTimePicked, date => _inputField.text = date.ToString("G"));
        }
    }
}
