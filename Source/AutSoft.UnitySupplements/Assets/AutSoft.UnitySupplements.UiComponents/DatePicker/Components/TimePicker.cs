#nullable enable
using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class TimePicker : MonoBehaviour
    {
        [SerializeField] private Clickable _incrementTime = default!;
        [SerializeField] private Clickable _reduceTime = default!;
        [SerializeField] private TMP_Text _timePickerText = default!;

        [Header("Optional")]
        [SerializeField] private TMP_InputField? _timeInput;

        private int _upperLimit;
        private int _lowerLimit;
        private int _currentTime;

        public UnityEvent<int> onTimeChanged { get; } = new();

        private void Awake() => this.CheckSerializedFields(nameof(_timeInput));

        private void Start()
        {
            this.Bind(_incrementTime.onClick, IncrementTime);
            this.Bind(_reduceTime.onClick, ReduceTime);
            if (!_timeInput.IsObjectNull())
            {
                this.Bind(_timeInput.onEndEdit, UpdateTime);
            }
        }

        private void UpdateTime(string input)
        {
            _currentTime = int.Parse(input);
            CheckLimits();
        }

        private void ReduceTime()
        {
            _currentTime -= 1;
            CheckLimits();
        }

        private void IncrementTime()
        {
            _currentTime += 1;
            CheckLimits();
        }

        private void CheckLimits()
        {
            if (_currentTime < _lowerLimit)
            {
                _currentTime = _upperLimit;
            }
            if (_currentTime > _upperLimit)
            {
                _currentTime = _lowerLimit;
            }
            UpdateText();
            onTimeChanged.Invoke(_currentTime);
        }

        private void UpdateText()
        {
            if (!_timeInput.IsObjectNull())
            {
                _timeInput.text = _currentTime.ToString("00");
            }
            else
            {
                _timePickerText.text = _currentTime.ToString("00");
            }
        }

        public void InitTimePicker(int currentTime, int lowerLimit, int upperLimit, TMP_FontAsset font)
        {
            _upperLimit = upperLimit;
            _lowerLimit = lowerLimit;
            _currentTime = currentTime;
            if (!_timeInput.IsObjectNull())
            {
                _timeInput.textComponent.font = font;
            }
            else
            {
                _timePickerText.font = font;
            }
            UpdateText();
        }
    }
}
