using System;

namespace AutSoft.UnitySupplements.Timeline
{
    public interface ITimelineCounter
    {
        DateTimeOffset CurrentTime { get; }
        float PlaySpeed { get; }

        void Initialize(DateTimeOffset start, DateTimeOffset end, float speed);
        void OnDragEnd();
        void OnDragPause();
        void Pause();
        void Play();
        void SetCurrentTime(DateTimeOffset time);
        void SetSpeed(float speed);
        void SetTimeRange(DateTimeOffset start, DateTimeOffset end);
        void Stop();
        void SwitchState();
        void Update(float elapsedSeconds);
    }
}
