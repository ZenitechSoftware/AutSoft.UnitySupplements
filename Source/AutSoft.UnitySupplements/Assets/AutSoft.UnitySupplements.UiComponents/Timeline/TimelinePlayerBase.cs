#nullable enable
using AutSoft.UnitySupplements.EventBus;
using AutSoft.UnitySupplements.Vitamins;
using Injecter;
using Injecter.Unity;
using System;
using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.Timeline
{
    public abstract class TimelinePlayerBase : MonoBehaviourInjected
    {
        [Inject] private readonly IEventBus _eventBus = default!;
        [Inject] protected readonly ITimelineCounter _timeLine = default!;

        [SerializeField] private TMP_Dropdown _speedDropdown = default!;
        [SerializeField] private TMP_Text _currentTimeLabel = default!;
        [SerializeField] private TMP_Text _startDateText = default!;
        [SerializeField] private TMP_Text _endDateText = default!;

        protected long _flightDurationInTicks;

        public DateTimeOffset StartTime { get; private set; }
        public DateTimeOffset EndTime { get; private set; }
        public DateTimeOffset CurrentTime => _timeLine.CurrentTime;

        protected override void Awake()
        {
            base.Awake();

            this.CheckSerializedFields();

            _eventBus.Subscribe<CurrentTimeChanged>(OnCurrentTimeChanged);
            _eventBus.Subscribe<TimeRangeChanged>(OnTimeRangeChanged);

            _speedDropdown.onValueChanged.AddListener(OnSpeedChanged);
        }

        private void Update()
        {
            _timeLine.Update(Time.deltaTime);
            _currentTimeLabel.text = $"{CurrentTime:G}";
        }

        protected override void OnDestroy()
        {
            _eventBus.UnSubscribe<CurrentTimeChanged>(OnCurrentTimeChanged);
            _eventBus.UnSubscribe<TimeRangeChanged>(OnTimeRangeChanged);

            _speedDropdown.onValueChanged.RemoveListener(OnSpeedChanged);

            base.OnDestroy();
        }

        public void Initialize(DateTimeOffset start, DateTimeOffset end, DateTimeOffset? currentTime = null)
        {
            _timeLine.Initialize(start, end, ParseSpeed(), currentTime);

            SetDuration(start, end);

            _eventBus.Invoke(new CurrentTimeChanged(CurrentTime));
        }

        public void ChangeCurrentTimeRange(DateTimeOffset start, DateTimeOffset end) => SetDuration(start, end);

        protected void OnTimeRangeChanged(TimeRangeChanged message)
        {
            SetSliderValue(_timeLine.CurrentTime.Ticks);

            if (StartTime == message.Start && EndTime == message.End) return;

            SetDuration(message.Start, message.End);
        }

        protected void SetDuration(DateTimeOffset start, DateTimeOffset end)
        {
            StartTime = start;
            EndTime = end;
            _flightDurationInTicks = end.Ticks - start.Ticks;
            _startDateText.text = $"{start:g}";
            _endDateText.text = $"{end:g}";
            _timeLine.SetTimeRange(start, end);
        }

        protected abstract void SetSliderValue(long currentTimeInTicks);
        protected abstract void SetCurrentTimeFromSliderValue();

        private void OnSpeedChanged(int _) => _timeLine.SetSpeed(ParseSpeed());
        protected void OnPlayPauseClicked() => _timeLine.SwitchState();
        protected void OnStopClicked() => _timeLine.Stop();

        private void OnCurrentTimeChanged(CurrentTimeChanged timeUpdated) => SetSliderValue(timeUpdated.CurrentTime.Ticks);

        protected void ClickedOnTimeline()
        {
            _timeLine.OnDragPause();
            SetCurrentTimeFromSliderValue();
        }

        protected void DragEndOnTimeline() => _timeLine.OnDragEnd();

        private float ParseSpeed() => float.Parse(_speedDropdown.options[_speedDropdown.value].text);
    }
}
