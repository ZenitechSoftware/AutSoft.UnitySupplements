#nullable enable
using AutSoft.UnitySupplements.EventBus;
using System;

namespace AutSoft.UnitySupplements.UiComponents.Timeline
{
    /// <summary>
    /// EventBus event invoked by the timeline to signal time range change.
    /// </summary>
    public sealed class TimeRangeChanged : IEvent
    {
        public TimeRangeChanged(DateTimeOffset start, DateTimeOffset end)
        {
            Start = start;
            End = end;
        }

        public DateTimeOffset Start { get; }
        public DateTimeOffset End { get; }
    }
}
