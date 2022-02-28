#nullable enable
using AutSoft.UnitySupplements.EventBus;
using System;

namespace AutSoft.UnitySupplements.Timeline
{
    public sealed class CurrentTimeChanged : IEvent
    {
        public CurrentTimeChanged(DateTimeOffset currentTime) => CurrentTime = currentTime;
        public DateTimeOffset CurrentTime { get; }
    }
}
