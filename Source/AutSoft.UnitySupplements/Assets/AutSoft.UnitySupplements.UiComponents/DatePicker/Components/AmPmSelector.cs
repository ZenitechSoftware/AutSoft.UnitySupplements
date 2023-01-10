#nullable enable
using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class AmPmSelector : MonoBehaviour
    {
        [SerializeField] private TMP_Text _amText = default!;
        [SerializeField] private TMP_Text _pmText = default!;
        [SerializeField] private Toggleable _amToggle = default!;
        [SerializeField] private Toggleable _pmToggle = default!;

        private DatePicker? _datePicker;
        private bool _isPmInit;

        public bool IsAm => _amToggle.IsOn;

        private void Awake()
        {
            this.CheckSerializedFields();

            _amText.text = CultureInfo.CurrentCulture.DateTimeFormat.AMDesignator;
            _pmText.text = CultureInfo.CurrentCulture.DateTimeFormat.PMDesignator;
        }

        private void Start()
        {
            if (_isPmInit) _pmToggle.IsOn = true;

            this.Bind(_amToggle.onValueChanged, AmClicked);
            this.Bind(_pmToggle.onValueChanged, PmClicked);
        }

        private void PmClicked(bool clicked)
        {
            _datePicker.IsObjectNullThrow();
            if (clicked)
            {
                _datePicker.AddHour(12);
            }
        }

        private void AmClicked(bool clicked)
        {
            _datePicker.IsObjectNullThrow();
            if (clicked)
            {
                _datePicker.AddHour(-12);
            }
        }

        public void InitAmPmSelector(DatePicker datePicker, bool isPm, TMP_FontAsset font)
        {
            _datePicker = datePicker;
            _amText.font = font;
            _pmText.font = font;
            _isPmInit = isPm;
        }
    }
}
