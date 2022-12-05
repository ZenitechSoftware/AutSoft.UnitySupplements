using AutSoft.UnitySupplements.Vitamins;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class YearButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _yearLabel = default!;
        private void Awake()
        {
            this.CheckSerializedFields();
        }
        public void SetupYearButton(int startYear)
        {
            _yearLabel.text = startYear.ToString();
        }
    }
}
