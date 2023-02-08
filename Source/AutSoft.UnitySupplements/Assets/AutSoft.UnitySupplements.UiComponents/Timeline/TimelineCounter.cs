#nullable enable
using AutSoft.UnitySupplements.EventBus;
using System;

namespace AutSoft.UnitySupplements.UiComponents.Timeline
{
    /// <summary>
    /// The default implementation of <see cref="ITimelineCounter"/>
    /// that uses <see cref="IEventBus"/> to signal time and playback state changes.
    /// </summary>
    public sealed class TimelineCounter : ITimelineCounter
    {
        private readonly IEventBus _eventBus;

        private bool _isPlaying;

        private DateTimeOffset _startTime;
        private DateTimeOffset _endTime;
        private DateTimeOffset _currentTime;

        public float PlaySpeed { get; private set; } = 8;

        private bool _dragState;

        public TimelineCounter(IEventBus eventBus) => _eventBus = eventBus;

        public DateTimeOffset CurrentTime
        {
            get => _currentTime;
            private set
            {
                if (value == _currentTime) return;

                if (value > _endTime)
                {
                    _currentTime = _endTime;
                    IsPlaying = false;
                    return;
                }

                _currentTime = value;
                _eventBus.Invoke(new CurrentTimeChanged(CurrentTime));
            }
        }

        private bool IsPlaying
        {
            get => _isPlaying;
            set
            {
                _isPlaying = value;
                _eventBus.Invoke(new TimelinePlayingChanged(_isPlaying));
            }
        }

        public void Update(float elapsedSeconds)
        {
            if (!IsPlaying) return;

            CurrentTime = CurrentTime.AddSeconds(elapsedSeconds * PlaySpeed);
        }

        public void OnDragPause()
        {
            _dragState = IsPlaying;
            IsPlaying = false;
        }

        public void OnDragEnd() => IsPlaying = _dragState;

        public void Stop()
        {
            IsPlaying = false;
            CurrentTime = _startTime;
        }

        public void Play() => IsPlaying = true;
        public void Pause() => IsPlaying = false;
        public void SwitchState() => IsPlaying = !IsPlaying;

        public void Initialize(DateTimeOffset start, DateTimeOffset end, float speed, DateTimeOffset? currentTime)
        {
            PlaySpeed = speed;

            SetTimeRange(start, end);

            CurrentTime = currentTime ?? _startTime;
        }

        public void SetSpeed(float speed) => PlaySpeed = speed;

        public void SetCurrentTime(DateTimeOffset time) => CurrentTime = time;

        public void SetTimeRange(DateTimeOffset start, DateTimeOffset end)
        {
            if (_startTime == start && _endTime == end) return;

            _startTime = start;
            _endTime = end;
            _eventBus.Invoke(new TimeRangeChanged(_startTime, _endTime));
        }
    }
}
