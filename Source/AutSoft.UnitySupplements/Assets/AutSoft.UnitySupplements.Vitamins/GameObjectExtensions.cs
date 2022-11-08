#nullable enable
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Get the given component or add it to the gameobject
        /// </summary>
        /// <typeparam name="T">Type of the component</typeparam>
        /// <returns>The given component</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (!gameObject.TryGetComponent<T>(out var component))
            {
                component = gameObject.AddComponent<T>();
            }

            component.gameObject.IsObjectNull();
            return component;
        }

        /// <summary>
        /// Checks if unity objects is null
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when object is null</exception>
        public static void IsObjectNull(this UnityEngine.Object? unityObject)
        {
            if (unityObject == null)
            {
                throw new InvalidOperationException($"{nameof(unityObject)} was not initalized before accessing it");
            }
        }
    }
}
