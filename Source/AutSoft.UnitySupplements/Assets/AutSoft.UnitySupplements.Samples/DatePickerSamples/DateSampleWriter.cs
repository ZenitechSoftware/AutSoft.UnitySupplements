using AutSoft.UnitySupplements.UiComponents.DatePicker.Components;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.Samples.DatePickerSamples
{
    public class DateSampleWriter : MonoBehaviour
    {
        [SerializeField] private DatePicker _datePicker = default!;
        [SerializeField] private TMP_InputField _inputField = default!;
        [SerializeField] private DatePickerForcedCulture _forcedCulture = DatePickerForcedCulture.Local;

        private void Awake()
        {
            this.CheckSerializedFields();
            this.Bind(_datePicker.onTimePicked, date => _inputField.text = date.ToString("G"));

            switch (_forcedCulture)
            {
                case DatePickerForcedCulture.Hungarian:
                    CultureInfo.CurrentCulture = new CultureInfo("hu-HU");
                    break;
                case DatePickerForcedCulture.English:
                    CultureInfo.CurrentCulture = new CultureInfo("en-US");
                    break;
                case DatePickerForcedCulture.Swedish:
                    CultureInfo.CurrentCulture = new CultureInfo("sv-SE");
                    break;
            }
        }
    }

    public enum DatePickerForcedCulture
    {
        Local,
        Hungarian,
        English,
        Swedish,
    }
}
