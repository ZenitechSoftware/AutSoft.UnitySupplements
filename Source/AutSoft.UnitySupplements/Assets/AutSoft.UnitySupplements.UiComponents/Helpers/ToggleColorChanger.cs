#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.Helpers
{
    [RequireComponent(typeof(Toggle), typeof(Image))]
    public class ToggleColorChanger : MonoBehaviour
    {
        [Header("Background colors")]
        [SerializeField] private Color _selectionBgColor = default!;
        [SerializeField] private Color _normalBgColor = default!;
        [SerializeField] private Color _disabledBgColor = default!;

        [Header("Foreground colors")]
        [SerializeField] private Color _selectionForegroundColor = default!;
        [SerializeField] private Color _normalForegroundColor = default!;
        [SerializeField] private Color _disabledForegroundColor = default!;

        [Header("Optional icon or text")]
        [SerializeField] private Image? _icon;
        [SerializeField] private TMP_Text? _text;

        private Toggle _toggle = default!;
        private Image _backGroundImage = default!;
        private bool _isToggled;
        private void Awake()
        {
            this.CheckSerializedFields(nameof(_icon), nameof(_text));
            _toggle = GetComponent<Toggle>();
            _backGroundImage = GetComponent<Image>();

            _toggle.onValueChanged.AddListener(OnToggleChanged);
            _isToggled = _toggle.isOn;
            UpdateVisuals();
        }

        private void Update()
        {
            if (_isToggled != _toggle.isOn)
            {
                UpdateVisuals();
            }
        }

        private void OnDestroy() => _toggle.onValueChanged.RemoveListener(OnToggleChanged);

        private void OnToggleChanged(bool isOn) => UpdateVisuals();

        private void UpdateVisuals()
        {
            if (_toggle.IsInteractable())
            {
                if (!_icon.IsObjectNull())
                {
                    _icon.color = _toggle.isOn ? _selectionForegroundColor : _normalForegroundColor;
                }
                if (!_text.IsObjectNull())
                {
                    _text.color = _toggle.isOn ? _selectionForegroundColor : _normalForegroundColor;
                }
                _backGroundImage.color = _toggle.isOn ? _selectionBgColor : _normalBgColor;
            }
            else
            {
                if (!_icon.IsObjectNull())
                {
                    _icon.color = _disabledForegroundColor;
                }
                if(!_text.IsObjectNull())
                {
                    _text.color = _disabledForegroundColor;
                }
                _backGroundImage.color = _disabledBgColor;
            }
            _isToggled = _toggle.isOn;
        }
    }
}
