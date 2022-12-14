#nullable enable
using UnityEngine;
using UnityEngine.Events;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public abstract class DateSpawner : MonoBehaviour
    {
        public UnityEvent onSpawned { get; } = new();
    }
}
