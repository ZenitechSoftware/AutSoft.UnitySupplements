using System.Globalization;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class AmPmSelector : MonoBehaviour
    {
        [SerializeField] private TMP_Text _amText = default!;
        [SerializeField] private TMP_Text _pmText = default!;
        private void Awake()
        {
            _amText.text = CultureInfo.CurrentCulture.DateTimeFormat.AMDesignator;
            _pmText.text = CultureInfo.CurrentCulture.DateTimeFormat.PMDesignator;
        }
    }
}
