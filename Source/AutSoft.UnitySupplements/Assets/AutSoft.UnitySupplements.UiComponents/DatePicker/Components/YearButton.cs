#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    [RequireComponent(typeof(Button))]
    public class YearButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _yearLabel = default!;

        private Button _yearButton = default!;
        private int _year;
        private MonthPicker? _monthPicker;
        private GameObject? _yearPickerObject;

        private void Awake()
        {
            this.CheckSerializedFields();
            _yearButton = GetComponent<Button>();
        }

        private void OnDestroy() => _yearButton.onClick.RemoveListener(YearClicked);

        public void SetupYearButton(int startYear, MonthPicker monthPicker, GameObject yearPickerObject)
        {
            _year = startYear;
            _monthPicker = monthPicker;
            _yearPickerObject = yearPickerObject;
            _yearLabel.text = startYear.ToString();
            _yearButton.onClick.AddListener(YearClicked);
        }

        private void YearClicked()
        {
            _yearPickerObject.IsObjectNullThrow();
            _monthPicker.IsObjectNullThrow();
            _monthPicker.InitYear(_year);
            _monthPicker.gameObject.SetActive(true);
            _yearPickerObject.SetActive(false);
        }
    }
}
