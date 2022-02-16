using AutSoft.UnitySupplements.Vitamins;
using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.Timeline
{
    public class DatePickerDialog : MonoBehaviour
    {
        [SerializeField] private GameObject _dialogPanel = default!;
        [SerializeField] private TMP_Text _messageLabel = default!;
        [SerializeField] private TMP_InputField _dateInput = default!;
        [SerializeField] private Button _cancelButton = default!;
        [SerializeField] private Button _confirmButton = default!;

        private DateTimeOffset _originalValue = default!;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "False positive")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S1450:Private fields only used as local variables in methods should become local variables", Justification = "False positive")]
        private DateTimeOffset _currentValue = default!;
        private UniTaskCompletionSource _tcs = new();

        private void Awake()
        {
            this.CheckSerializedField(_dialogPanel, nameof(_dialogPanel));
            this.CheckSerializedField(_messageLabel, nameof(_messageLabel));
            this.CheckSerializedField(_dateInput, nameof(_dateInput));
            this.CheckSerializedField(_cancelButton, nameof(_cancelButton));
            this.CheckSerializedField(_confirmButton, nameof(_confirmButton));

            _dateInput.onValueChanged.AddListener(OnDateInputValueChanged);
            _cancelButton.onClick.AddListener(Cancel);
            _confirmButton.onClick.AddListener(Confirm);
        }

        private void OnDestroy()
        {
            _dateInput.onValueChanged.RemoveListener(OnDateInputValueChanged);
            _cancelButton.onClick.RemoveListener(Cancel);
            _confirmButton.onClick.RemoveListener(Confirm);
        }

        public async UniTask<DateTimeOffset> ShowDialog(string message, DateTimeOffset defaultValue)
        {
            _tcs.TrySetResult();
            _tcs = new UniTaskCompletionSource();

            _originalValue = defaultValue;
            _currentValue = _originalValue;

            _messageLabel.text = message;
            _dateInput.text = _currentValue.ToString();
            _dialogPanel.SetActive(true);

            await _tcs.Task;

            return _currentValue;
        }

        private void Confirm()
        {
            if (DateTimeOffset.TryParse(_dateInput.text, out var date))
            {
                _currentValue = date;
            }

            _dialogPanel.SetActive(false);

            _tcs.TrySetResult();
        }

        private void Cancel()
        {
            _currentValue = _originalValue;

            _dialogPanel.SetActive(false);

            _tcs.TrySetResult();
        }

        private void OnDateInputValueChanged(string arg0) => _confirmButton.enabled = DateTimeOffset.TryParse(_dateInput.text, out _);
    }
}
