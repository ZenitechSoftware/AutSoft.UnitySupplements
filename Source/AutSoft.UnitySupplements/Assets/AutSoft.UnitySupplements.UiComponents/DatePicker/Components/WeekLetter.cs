using AutSoft.UnitySupplements.Vitamins;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class WeekLetter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _weekText = default!;

        private void Awake() => this.CheckSerializedFields();

        public void SetWeekText(string weekText, TMP_FontAsset font)
        {
            _weekText.text = weekText;
            _weekText.font = font;
        }
    }
}
