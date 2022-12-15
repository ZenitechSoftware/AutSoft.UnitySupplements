using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class DateTimeToggler : MonoBehaviour
    {
        [SerializeField] private Toggleable _dateToggle = default!;
        [SerializeField] private Toggleable _timeToggle = default!;

        [Header("External")]
        [SerializeField] private GameObject _datePicker = default!;
        [SerializeField] private GameObject _timePicker = default!;

        private void Awake() => this.CheckSerializedFields();

        private void Start()
        {
            this.Bind(_timeToggle.onValueChanged, OnTimeToggled);
            this.Bind(_dateToggle.onValueChanged, OnDateToggled);
        }

        private void OnTimeToggled(bool isOn) => _timePicker.SetActive(isOn);
        private void OnDateToggled(bool isOn) => _datePicker.SetActive(isOn);

        public void ActivateDefault() => _dateToggle.IsOn = true;
    }
}
