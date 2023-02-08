#nullable enable
using AutSoft.UnitySupplements.EventBus;

namespace AutSoft.UnitySupplements.UiComponents.Timeline
{
    /// <summary>
    /// EventBus event invoked by the timeline to signal playback state change.
    /// </summary>
    public sealed class TimelinePlayingChanged : IEvent
    {
        public TimelinePlayingChanged(bool isPlaying) => IsPlaying = isPlaying;

        public bool IsPlaying { get; }
    }
}
