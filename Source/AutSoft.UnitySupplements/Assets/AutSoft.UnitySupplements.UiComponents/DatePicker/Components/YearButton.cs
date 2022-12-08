#nullable enable
using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    [RequireComponent(typeof(Clickable))]
    public class YearButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _yearLabel = default!;

        private Clickable _yearButton = default!;
        private int _year;
        private TMP_FontAsset? _font;
        private MonthPicker? _monthPicker;
        private GameObject? _yearPickerObject;

        private void Awake()
        {
            this.CheckSerializedFields();
            _yearButton = GetComponent<Clickable>();
            this.Bind(_yearButton.onClick, YearClicked);
        }

        public void SetupYearButton(int startYear, MonthPicker monthPicker, GameObject yearPickerObject, TMP_FontAsset font)
        {
            _year = startYear;
            _font = font;
            _monthPicker = monthPicker;
            _yearPickerObject = yearPickerObject;
            _yearLabel.font = font;
            _yearLabel.text = startYear.ToString();
        }

        private void YearClicked()
        {
            _yearPickerObject.IsObjectNullThrow();
            _monthPicker.IsObjectNullThrow();
            _font.IsObjectNullThrow();
            _monthPicker.InitYear(_year, _font);
            _monthPicker.gameObject.SetActive(true);
            _yearPickerObject.SetActive(false);
        }
    }
}
