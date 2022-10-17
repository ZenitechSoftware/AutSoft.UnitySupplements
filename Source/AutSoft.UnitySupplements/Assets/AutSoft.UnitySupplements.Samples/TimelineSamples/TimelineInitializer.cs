#nullable enable
using AutSoft.UnitySupplements.UiComponents.Timeline;
using AutSoft.UnitySupplements.Vitamins;
using System;
using UnityEngine;

namespace AutSoft.UnitySupplements.Samples.TimelineSamples
{
    public class TimelineInitializer : MonoBehaviour
    {
        [Header("External")]
        [SerializeField] private BasicTimelinePlayer _timeline = default!;

        private void Awake() => this.CheckSerializedFields();

        private void Start() => _timeline.Initialize(DateTimeOffset.Now, DateTimeOffset.Now.AddHours(1));
    }
}
