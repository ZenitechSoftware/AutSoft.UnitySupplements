#nullable enable
using AutSoft.UnitySupplements.UiComponents.Helpers;
using AutSoft.UnitySupplements.Vitamins;
using AutSoft.UnitySupplements.Vitamins.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutSoft.UnitySupplements.MRTKExtras.Helpers
{
    public class XrToggleGroup : ToggleableGroup
    {
        [SerializeField] private bool _allowSwitchOff = false;

        [SerializeField] private List<XrToggleable> _toggles = default!;

        private XrToggleable? _currentOn;

        private void Awake()
        {
            foreach (var toggle in _toggles)
            {
                toggle.Bind(toggle.onValueChanged, OnInteractableClicked);
            }
            _currentOn = GetFirstActiveToggle();
        }
        private void Start() => EnsureValidState();

        public override bool AllowSwitchOff
        {
            get => _allowSwitchOff;
            set => _allowSwitchOff = value;
        }

        public override void AddToggleToGroup(Toggleable interactable) => interactable.SetToggleGroup(this);

        public void RegisterToggleToGroup(XrToggleable xrToggleable)
        {
            xrToggleable.Bind(xrToggleable.onValueChanged, OnInteractableClicked);
            if (!_toggles.Contains(xrToggleable))
            {
                _toggles.Add(xrToggleable);
            }
        }
        private void OnInteractableClicked(bool isOn)
        {
            var turnedOn = _toggles.Find(t => t.IsOn && t != _currentOn);
            if (isOn)
            {
                NotifyToggleOn(turnedOn);
                if (!AllowSwitchOff)
                {
                    if (!_currentOn.IsObjectNull())
                        _currentOn.Interactable = true;
                    turnedOn.Interactable = false;
                }
                _currentOn = turnedOn;
            }
            else if (AllowSwitchOff && turnedOn.IsObjectNull())
            {
                _currentOn = null;
            }
        }

        public void RemoveToggleFromGroup(XrToggleable interactable)
        {
            interactable.onValueChanged.RemoveListener(OnInteractableClicked);
            if (!_toggles.Contains(interactable))
                _toggles.Remove(interactable);
        }

        private void ValidateToggleIsInGroup(XrToggleable toggle)
        {
            if (toggle == null || !_toggles.Contains(toggle))
                throw new ArgumentException($"Toggle is not part of group or null {nameof(toggle)}");
        }

        public void NotifyToggleOn(XrToggleable toggle)
        {
            ValidateToggleIsInGroup(toggle);
            // disable all toggles in the group
            for (var i = 0; i < _toggles.Count; i++)
            {
                if (_toggles[i] == toggle)
                    continue;

                _toggles[i].IsOn = false;
            }
        }

        public bool AnyTogglesOn() => _toggles.Any(x => x.IsOn);

        public IEnumerable<XrToggleable> ActiveToggles() => _toggles.Where(x => x.IsOn);

        public XrToggleable GetFirstActiveToggle()
        {
            var activeToggles = ActiveToggles();
            return activeToggles.FirstOrDefault();
        }

        public void EnsureValidState()
        {
            if (!AllowSwitchOff && !AnyTogglesOn() && _toggles.Count != 0)
            {
                _toggles[0].IsOn = true;
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
                    toggle.IsOn = false;
                }
            }
        }

    }
}
