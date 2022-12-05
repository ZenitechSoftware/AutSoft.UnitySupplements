using AutSoft.UnitySupplements.Vitamins;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class YearMonthPicker : MonoBehaviour
    {
        [SerializeField] private TMP_Text _yearMonthLabel = default!;
        [SerializeField] private Image _arrowImage = default!;
        [SerializeField] private Button _arrowButton = default!;
        [SerializeField] private GameObject _yearMonthObject = default!;
        [SerializeField] private GameObject _daySelectorObject = default!;

        private void Awake()
        {
            this.CheckSerializedFields();

            _arrowButton.onClick.AddListener(ToggleYearMonthPanel);
        }

        private void ToggleYearMonthPanel()
        {
            _yearMonthObject.SetActive(!_yearMonthObject.activeInHierarchy);
            _daySelectorObject.SetActive(!_daySelectorObject.activeInHierarchy);
            _arrowImage.transform.Rotate(new Vector3(0, 0, 180));
        }

        public void SetYearMonthLabel(DateTimeOffset pickedDate)
        {
            _yearMonthLabel.text = pickedDate.ToString("Y");
        }
    }
}
