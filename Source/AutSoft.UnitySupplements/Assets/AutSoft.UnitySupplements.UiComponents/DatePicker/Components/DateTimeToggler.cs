using AutSoft.UnitySupplements.Vitamins;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{

    public class DateTimeToggler : MonoBehaviour
    {
        [SerializeField] private GameObject _datePicker = default!;
        [SerializeField] private GameObject _timePicker = default!;
        [SerializeField] private Toggle _timeToggle = default! ;
        [SerializeField] private Toggle _dateToggle = default! ;

        private void Awake()
        {
            this.CheckSerializedFields();

            _timeToggle.onValueChanged.AddListener(OnTimeToggled);
            _dateToggle.onValueChanged.AddListener(OnDateToggled);
        }

        private void OnTimeToggled(bool isOn) => _timePicker.SetActive(isOn);
        private void OnDateToggled(bool isOn) => _datePicker.SetActive(isOn);
    }
}
