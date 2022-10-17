#nullable enable
using System;

namespace AutSoft.UnitySupplements.UiComponents.Timeline
{
    public interface ITimelineCounter
    {
        /// <summary>
        /// Current time of the timeline
        /// </summary>
        DateTimeOffset CurrentTime { get; }

        /// <summary>
        /// Current playback multiplier for the timeline
        /// </summary>
        float PlaySpeed { get; }

        /// <summary>
        /// Initialize timeline with start, end time and a starting speed multiplier
        /// </summary>
        /// <param name="start">Start date of timeline</param>
        /// <param name="end">End date of timeline</param>
        /// <param name="speed">Starting speed multiplier of timeline</param>
        /// <param name="dateTimeOffset"></param>
        void Initialize(DateTimeOffset start, DateTimeOffset end, float speed, DateTimeOffset? currentTime);

        /// <summary>
        /// Called when the user stops scrubbing the timeline
        /// </summary>
        void OnDragEnd();

        /// <summary>
        /// Called when the user click in the timeline
        /// </summary>
        void OnDragPause();

        /// <summary>
        /// Pause the timeline
        /// </summary>
        void Pause();

        /// <summary>
        /// Start the timeline
        /// </summary>
        void Play();

        /// <summary>
        /// Set the current time called when the user moves the slider
        /// </summary>
        /// <param name="time"></param>
        void SetCurrentTime(DateTimeOffset time);

        /// <summary>
        /// Set the playback multiplier
        /// </summary>
        /// <param name="speed">Speed multiplier</param>
        void SetSpeed(float speed);

        /// <summary>
        /// Set the start and end date of the timeline
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        void SetTimeRange(DateTimeOffset start, DateTimeOffset end);

        /// <summary>
        /// Stop the playback and set current time to the start date
        /// </summary>
        void Stop();

        /// <summary>
        /// Switch from play to pause and vice versa
        /// </summary>
        void SwitchState();

        /// <summary>
        /// Move timeline forward with the given seconds
        /// </summary>
        /// <param name="elapsedSeconds"></param>
        void Update(float elapsedSeconds);
    }
}
