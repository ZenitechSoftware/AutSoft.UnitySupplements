﻿#nullable enable
using AutSoft.UnitySupplements.EventBus;
using AutSoft.UnitySupplements.Vitamins;
using Injecter;
using Microsoft.Extensions.Logging;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.Timeline
{
    public class BasicTimelinePlayer : MonoBehaviour
    {
        [Inject] private readonly IEventBus _eventBus = default!;
        [Inject] private readonly ITimelineCounter _timeLine = default!;
        [Inject] private readonly ILogger<BasicTimelinePlayer> _logger = default!;

        [Header("Controls")]
        [SerializeField] private Slider _timeSlider = default!;
        [SerializeField] private EventTrigger _sliderEvents = default!;

        [SerializeField] private Button _playPauseButton = default!;
        [SerializeField] private Button _stopButton = default!;
        [SerializeField] private TMP_Dropdown _speedDropdown = default!;
        [SerializeField] private TMP_Text _currentTimeLabel = default!;
        [SerializeField] private Button _startDateButton = default!;
        [SerializeField] private TMP_Text _endDateText = default!;
        [Header("External")]
        [SerializeField] private DatePickerDialog _datePickerDialog = default!;
        [SerializeField] private bool _isTimeUserEditable = true;

        private long _flightDurationInTicks;
        public DateTimeOffset StartTime { get; private set; }
        public DateTimeOffset EndTime { get; private set; }
        public DateTimeOffset CurrentTime => _timeLine.CurrentTime;

        private void Start()
        {
            this.CheckSerializedField(_timeSlider, nameof(_timeSlider));
            this.CheckSerializedField(_sliderEvents, nameof(_sliderEvents));
            this.CheckSerializedField(_playPauseButton, nameof(_playPauseButton));
            this.CheckSerializedField(_stopButton, nameof(_stopButton));
            this.CheckSerializedField(_speedDropdown, nameof(_speedDropdown));
            this.CheckSerializedField(_currentTimeLabel, nameof(_currentTimeLabel));
            this.CheckSerializedField(_startDateButton, nameof(_startDateButton));
            this.CheckSerializedField(_endDateText, nameof(_endDateText));

            AddEventTrigger(EventTriggerType.Drag, SetCurrentTimeFromSliderValue);
            AddEventTrigger(EventTriggerType.PointerDown, ClickedOnTimeline);
            AddEventTrigger(EventTriggerType.PointerUp, MouseUpOnTimeline);

            _eventBus.Subscribe<CurrentTimeChanged>(OnCurrentTimeChanged);
            _eventBus.Subscribe<TimeRangeChanged>(OnTimeRangeChanged);

            _playPauseButton.onClick.AddListener(OnPlayPauseClicked);
            _stopButton.onClick.AddListener(OnStopClicked);
            _speedDropdown.onValueChanged.AddListener(OnSpeedChanged);

            if (_isTimeUserEditable)
            {
                this.CheckSerializedField(_datePickerDialog, nameof(_datePickerDialog));
                _startDateButton.onClick.AddListener(OnStartDateClicked);
            }
            else
            {
                _startDateButton.interactable = false;
            }
        }

        private void Update()
        {
            _timeLine.Update(Time.deltaTime);
            _currentTimeLabel.text = $"{CurrentTime:G}";
        }

        private void OnDestroy()
        {
            _eventBus.UnSubscribe<CurrentTimeChanged>(OnCurrentTimeChanged);
            _eventBus.UnSubscribe<TimeRangeChanged>(OnTimeRangeChanged);

            _playPauseButton.onClick.RemoveListener(OnPlayPauseClicked);
            _stopButton.onClick.RemoveListener(OnStopClicked);
            _speedDropdown.onValueChanged.RemoveListener(OnSpeedChanged);
            if (_isTimeUserEditable)
            {
                _startDateButton.onClick.RemoveListener(OnStartDateClicked);
            }

            _sliderEvents.triggers.ForEach(t => t.callback.RemoveAllListeners());
            _sliderEvents.triggers.Clear();
        }

        public void Initialize(DateTimeOffset start, DateTimeOffset end)
        {
            _timeLine.Initialize(start, end, ParseSpeed());

            SetDuration(start, end);

            _eventBus.Invoke(new CurrentTimeChanged(CurrentTime));
        }

        public void ChangeCurrentTimeRange(DateTimeOffset start, DateTimeOffset end) => SetDuration(start, end);

        private void OnTimeRangeChanged(TimeRangeChanged message)
        {
            SetSliderValue(_timeLine.CurrentTime.Ticks);

            if (StartTime == message.Start && EndTime == message.End) return;

            SetDuration(message.Start, message.End);
        }

        private void SetDuration(DateTimeOffset start, DateTimeOffset end)
        {
            StartTime = start;
            EndTime = end;
            _flightDurationInTicks = end.Ticks - start.Ticks;
            _startDateButton.GetComponentInChildren<TMP_Text>().text = $"{start:g}";
            _endDateText.text = $"{end:g}";
            _timeLine.SetTimeRange(start, end);
        }

        private void SetSliderValue(long currentTimeInTicks)
        {
            if (_flightDurationInTicks == 0) return;
            var elapsedTicks = currentTimeInTicks - StartTime.Ticks;
            _timeSlider.value = elapsedTicks == 0
                ? 0
                : (float)(elapsedTicks / (decimal)_flightDurationInTicks);
        }

        private void OnSpeedChanged(int _) => _timeLine.SetSpeed(ParseSpeed());
        private void OnPlayPauseClicked() => _timeLine.SwitchState();
        private void OnStopClicked() => _timeLine.Stop();

        private void OnCurrentTimeChanged(CurrentTimeChanged timeUpdated) => SetSliderValue(timeUpdated.CurrentTime.Ticks);

        private async void OnStartDateClicked()
        {
            try
            {
                var date = await _datePickerDialog.ShowDialog("Mission start date", StartTime);
                _eventBus.Invoke(new TimeRangeChanged(date, EndTime));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed setting new time range");
            }
        }

        private void AddEventTrigger(EventTriggerType eventId, Action action)
        {
            var onBeginDrag = new EventTrigger.Entry { eventID = eventId };
            onBeginDrag.callback.AddListener(_ => action());
            _sliderEvents.triggers.Add(onBeginDrag);
        }

        private void SetCurrentTimeFromSliderValue()
        {
            var valueInDecimal = (decimal)_timeSlider.value;
            _timeLine.SetCurrentTime(new DateTimeOffset((long)(valueInDecimal * _flightDurationInTicks) + StartTime.Ticks, _timeLine.CurrentTime.Offset));
        }

        private void ClickedOnTimeline()
        {
            _timeLine.OnDragPause();
            SetCurrentTimeFromSliderValue();
        }

        private void MouseUpOnTimeline() => _timeLine.OnDragEnd();

        private float ParseSpeed() => float.Parse(_speedDropdown.options[_speedDropdown.value].text);
    }
}