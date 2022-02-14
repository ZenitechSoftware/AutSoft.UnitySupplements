using System;
using UnityEngine;

namespace AutSoft.UnitySupplements.Timeline.Sample
{
    public class TimelineInitializer : MonoBehaviour
    {
        [Header("External")]
        [SerializeField] private BasicTimelinePlayer _timeline;

        private void Start() => _timeline.Initialize(DateTimeOffset.Now, DateTimeOffset.Now.AddHours(1));
    }
}
