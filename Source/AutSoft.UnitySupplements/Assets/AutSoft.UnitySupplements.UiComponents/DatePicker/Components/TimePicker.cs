#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class TimePicker : MonoBehaviour
    {
        [SerializeField] private Button _incrementTime = default!;
        [SerializeField] private Button _reduceTime = default!;
        [SerializeField] private TMP_InputField _timeInput = default!;

        private int _limit;
        private int _currentTime;

        public UnityEvent<int> onTimeChanged {get;} = new();

        private void Awake() => this.CheckSerializedFields();

        private void Start()
        {
            this.Bind(_incrementTime.onClick, IncrementTime);
            this.Bind(_reduceTime.onClick, ReduceTime);
            this.Bind(_timeInput.onEndEdit, UpdateTime);
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
            if (_currentTime < 0)
            {
                _currentTime = _limit - 1;
            }
            if (_currentTime >= _limit)
            {
                _currentTime = 0;
            }
            UpdateText();
            onTimeChanged.Invoke(_currentTime);
        }

        private void UpdateText() => _timeInput.text = _currentTime.ToString("00");

        public void InitTimePicker(int currentTime, int limit)
        {
            _limit = limit;
            _currentTime = currentTime;
            UpdateText();
        }
    }
}
