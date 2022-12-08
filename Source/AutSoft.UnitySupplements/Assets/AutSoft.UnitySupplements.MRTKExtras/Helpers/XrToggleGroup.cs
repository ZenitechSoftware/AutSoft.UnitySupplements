#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using Injecter;
using Microsoft.Extensions.Logging;
using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutSoft.UnitySupplements.MRTKExtras.Helpers
{
    public class XrToggleGroup : MonoBehaviour
    {
        [Inject] private readonly ILogger<XrToggleGroup> _logger = default!;

        [SerializeField] private bool _allowSwitchOff = false;

        [SerializeField] private List<Interactable> _toggles = default!;

        private Interactable? _currentOn;

        private void Awake()
        {
            foreach (var toggle in _toggles)
            {
                if (toggle.TryGetComponent<XrToggleable>(out var xrToggleable))
                {
                    xrToggleable.Bind(xrToggleable.onValueChanged, OnInteractableClicked);
                }
            }
            _currentOn = GetFirstActiveToggle();
        }
        private void Start() => EnsureValidState();

        public bool allowSwitchOff
        {
            get => _allowSwitchOff;
            set => _allowSwitchOff = value;
        }

        public void AddToggleToGroup(Interactable interactable)
        {
            if (interactable.ButtonMode != SelectionModes.Toggle)
            {
                _logger.LogError("Cant add interactble to group it is not a toggle");
                return;
            }
            if (interactable.TryGetComponent<XrToggleable>(out var xrToggleable))
            {
                xrToggleable.Bind(xrToggleable.onValueChanged, OnInteractableClicked);
            }
            else
            {
                _logger.LogError("Cant add interactble to group it is only usable with xr toggleable");
                return;
            }
            if (!_toggles.Contains(interactable))
                _toggles.Add(interactable);
        }

        private void OnInteractableClicked(bool isOn)
        {
            if (isOn)
            {
                var turnedOn = _toggles.Find(t => t.IsToggled && t != _currentOn);
                NotifyToggleOn(turnedOn);
                if (!allowSwitchOff)
                {
                    if (!_currentOn.IsObjectNull())
                        _currentOn.IsEnabled = true;
                    turnedOn.IsEnabled = false;
                }
                _currentOn = turnedOn;
            }
        }

        public void RemoveToggleFromGroup(Interactable interactable)
        {
            if (interactable.ButtonMode != SelectionModes.Toggle)
            {
                _logger.LogError("Cant remove interactble from group it is not a toggle");
                return;
            }
            if (interactable.TryGetComponent<XrToggleable>(out var xrToggleable))
            {
                xrToggleable.onValueChanged.RemoveListener(OnInteractableClicked);
            }
            else
            {
                _logger.LogError("Cant add interactble to group it is only usable with xr toggleable");
                return;
            }
            if (!_toggles.Contains(interactable))
                _toggles.Remove(interactable);
        }

        private void ValidateToggleIsInGroup(Interactable toggle)
        {
            if (toggle == null || !_toggles.Contains(toggle))
                throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", new object[] { toggle, this }));
        }

        public void NotifyToggleOn(Interactable toggle)
        {
            ValidateToggleIsInGroup(toggle);
            // disable all toggles in the group
            for (var i = 0; i < _toggles.Count; i++)
            {
                if (_toggles[i] == toggle)
                    continue;

                _toggles[i].IsToggled = false;
            }
        }

        public bool AnyTogglesOn() => _toggles.Any(x => x.IsToggled);

        public IEnumerable<Interactable> ActiveToggles() => _toggles.Where(x => x.IsToggled);

        public Interactable GetFirstActiveToggle()
        {
            var activeToggles = ActiveToggles();
            return activeToggles.FirstOrDefault();
        }

        public void EnsureValidState()
        {
            if (!allowSwitchOff && !AnyTogglesOn() && _toggles.Count != 0)
            {
                _toggles[0].IsToggled = true;
                _currentOn = _toggles[0];
                NotifyToggleOn(_toggles[0]);
            }

            var activeToggles = ActiveToggles();

            if (activeToggles.Count() > 1)
            {
                var firstActive = GetFirstActiveToggle();

                foreach (var toggle in activeToggles)
                {
                    if (toggle == firstActive)
                    {
                        continue;
                    }
                    toggle.IsToggled = false;
                }
            }
        }

    }
}
