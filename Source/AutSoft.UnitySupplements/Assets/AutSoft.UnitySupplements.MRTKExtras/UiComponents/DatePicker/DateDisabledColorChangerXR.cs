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
        private Toggleable _toggle;

        private void Awake()
        {
            this.CheckSerializedFields();
            _toggle = GetComponent<Toggleable>();
            this.Bind(_toggle.onInteractibleChanged, onStateChangedDisabledColor);
            _originalColor = _text.color;
        }

        private void onStateChangedDisabledColor(bool isOn)
        {
            if (_toggle.IsOn)
            {
                _text.color = _originalColor;
                return;
            }
            _text.color = isOn ? _originalColor : _disabledColor;
        }
    }
}
