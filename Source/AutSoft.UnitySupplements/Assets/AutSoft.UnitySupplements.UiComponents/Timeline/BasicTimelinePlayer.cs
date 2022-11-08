#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AutSoft.UnitySupplements.UiComponents.Timeline
{
    public class BasicTimelinePlayer : TimelinePlayerBase
    {
        [Header("Controls")]
        [SerializeField] private Slider _timeSlider = default!;
        [SerializeField] private EventTrigger _sliderEvents = default!;

        [SerializeField] private Button _playPauseButton = default!;
        [SerializeField] private Button _stopButton = default!;

        protected override void Awake()
        {
            base.Awake();

            this.CheckSerializedFields();

            AddEventTrigger(EventTriggerType.Drag, SetCurrentTimeFromSliderValue);
            AddEventTrigger(EventTriggerType.PointerDown, ClickedOnTimeline);
            AddEventTrigger(EventTriggerType.PointerUp, DragEndOnTimeline);

            _playPauseButton.onClick.AddListener(OnPlayPauseClicked);
            _stopButton.onClick.AddListener(OnStopClicked);
        }

        protected override void OnDestroy()
        {
            _playPauseButton.onClick.RemoveListener(OnPlayPauseClicked);
            _stopButton.onClick.RemoveListener(OnStopClicked);

            _sliderEvents.triggers.ForEach(t => t.callback.RemoveAllListeners());
            _sliderEvents.triggers.Clear();
            base.OnDestroy();
        }

        protected override void SetSliderValue(long currentTimeInTicks)
        {
            if (_flightDurationInTicks == 0) return;
            var elapsedTicks = currentTimeInTicks - StartTime.Ticks;
            _timeSlider.value = elapsedTicks == 0
                ? 0
                : (float)(elapsedTicks / (decimal)_flightDurationInTicks);
        }

        private void AddEventTrigger(EventTriggerType eventId, Action action)
        {
            var onBeginDrag = new EventTrigger.Entry { eventID = eventId };
            onBeginDrag.callback.AddListener(_ => action());
            _sliderEvents.triggers.Add(onBeginDrag);
        }

        protected override void SetCurrentTimeFromSliderValue()
        {
            var valueInDecimal = (decimal)_timeSlider.value;
            _timeLine.SetCurrentTime(new DateTimeOffset((long)(valueInDecimal * _flightDurationInTicks) + StartTime.Ticks, _timeLine.CurrentTime.Offset));
        }
    }
}
