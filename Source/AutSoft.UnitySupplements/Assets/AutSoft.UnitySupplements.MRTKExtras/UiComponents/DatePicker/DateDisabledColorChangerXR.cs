using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.MRTKExtras.UiComponents.DatePicker
{
    [RequireComponent(typeof(Toggleable))]
    public class DateDisabledColorChangerXR : MonoBehaviour
    {
        [SerializeField] private Color _disabledColor;
        [SerializeField] private TMP_Text _text = default!;

        private Color _originalColor;

        private void Awake()
        {
            this.CheckSerializedFields();
            this.Bind(GetComponent<Toggleable>().onInteractibleChanged, onStateChangedDisabledColor);
            _originalColor = _text.color;
        }

        private void onStateChangedDisabledColor(bool isOn) => _text.color = isOn ? _originalColor : _disabledColor;
    }
}
