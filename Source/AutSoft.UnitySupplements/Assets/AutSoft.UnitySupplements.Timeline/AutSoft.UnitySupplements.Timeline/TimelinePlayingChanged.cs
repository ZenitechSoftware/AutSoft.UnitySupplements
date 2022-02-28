#nullable enable
using AutSoft.UnitySupplements.EventBus;

namespace AutSoft.UnitySupplements.Timeline
{
    public sealed class TimelinePlayingChanged : IEvent
    {
        public TimelinePlayingChanged(bool isPlaying) => IsPlaying = isPlaying;

        public bool IsPlaying { get; }
    }
}
