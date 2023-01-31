#nullable enable
using AutSoft.UnitySupplements.EventBus;
using System;

namespace AutSoft.UnitySupplements.UiComponents.Timeline
{
    /// <summary>
    /// EventBus event invoked by the timeline to signal time change.
    /// </summary>
    public sealed class CurrentTimeChanged : IEvent
    {
        public CurrentTimeChanged(DateTimeOffset currentTime) => CurrentTime = currentTime;

        /// <summary>
        /// The current date and time selected on the timeline.
        /// </summary>
        public DateTimeOffset CurrentTime { get; }
    }
}
