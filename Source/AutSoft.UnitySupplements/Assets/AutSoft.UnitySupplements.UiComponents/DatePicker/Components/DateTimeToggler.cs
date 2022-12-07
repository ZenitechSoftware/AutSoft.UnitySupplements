using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{

    public class DateTimeToggler : MonoBehaviour
    {
        [SerializeField] private GameObject _datePicker = default!;
        [SerializeField] private GameObject _timePicker = default!;
        [SerializeField] private Toggle _timeToggle = default!;
        [SerializeField] private Toggle _dateToggle = default!;

        private void Awake() => this.CheckSerializedFields();

        private void Start()
        {
            this.Bind(_timeToggle.onValueChanged, OnTimeToggled);
            this.Bind(_dateToggle.onValueChanged, OnDateToggled);
        }

        private void OnTimeToggled(bool isOn) => _timePicker.SetActive(isOn);
        private void OnDateToggled(bool isOn) => _datePicker.SetActive(isOn);
    }
}
