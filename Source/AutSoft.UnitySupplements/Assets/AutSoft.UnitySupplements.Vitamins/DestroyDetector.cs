#nullable enable
using System;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    /// <summary>
    /// Invokes the <see cref="Destroyed"/> event on destroy and unsubscribes subscribers
    /// </summary>
    public sealed class DestroyDetector : MonoBehaviour
    {
        public event Action? Destroyed;

        private void OnDestroy()
        {
            Destroyed?.Invoke();
            Destroyed = null;
        }
    }
}
