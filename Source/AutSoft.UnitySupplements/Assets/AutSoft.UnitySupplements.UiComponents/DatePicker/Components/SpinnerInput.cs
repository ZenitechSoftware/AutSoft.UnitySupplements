#nullable enable
using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class SpinnerInput : MonoBehaviour
    {
        [SerializeField] private Clickable _incrementValue = default!;
        [SerializeField] private Clickable _decrementValue = default!;
        [SerializeField] private TMP_Text _labelText = default!;

        [Header("Optional")]
        [SerializeField] private TMP_InputField? _textInput;

        private int _upperLimit;
        private int _lowerLimit;
        private int _currentValue;

        public UnityEvent<int> onValueChanged { get; } = new();

        private void Awake() => this.CheckSerializedFields(nameof(_textInput));

        private void Start()
        {
            this.Bind(_incrementValue.onClick, IncrementValue);
            this.Bind(_decrementValue.onClick, DecrementValue);
            if (!_textInput.IsObjectNull())
            {
                this.Bind(_textInput.onEndEdit, UpdateValue);
            }
        }

        private void UpdateValue(string input)
        {
            _currentValue = int.Parse(input);
            CheckLimits();
        }

        private void DecrementValue()
        {
            _currentValue -= 1;
            CheckLimits();
        }

        private void IncrementValue()
        {
            _currentValue += 1;
            CheckLimits();
        }

        private void CheckLimits()
        {
            if (_currentValue < _lowerLimit)
            {
                _currentValue = _upperLimit;
            }
            if (_currentValue > _upperLimit)
            {
                _currentValue = _lowerLimit;
            }
            UpdateText();
            onValueChanged.Invoke(_currentValue);
        }

        private void UpdateText()
        {
            if (!_textInput.IsObjectNull())
            {
                _textInput.text = _currentValue.ToString("00");
            }
            else
            {
                _labelText.text = _currentValue.ToString("00");
            }
        }

        public void Initialize(int value, int lowerLimit, int upperLimit, TMP_FontAsset font)
        {
            _upperLimit = upperLimit;
            _lowerLimit = lowerLimit;
            _currentValue = value;
            if (!_textInput.IsObjectNull())
            {
                _textInput.textComponent.font = font;
            }
            else
            {
                _labelText.font = font;
            }
            UpdateText();
        }
    }
}
