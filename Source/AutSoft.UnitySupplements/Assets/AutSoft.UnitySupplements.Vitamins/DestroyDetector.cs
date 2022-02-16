#nullable enable
using System;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
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
