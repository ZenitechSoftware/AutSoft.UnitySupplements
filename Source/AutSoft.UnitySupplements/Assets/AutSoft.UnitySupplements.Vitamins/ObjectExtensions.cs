#nullable enable
using System;

namespace AutSoft.UnitySupplements.Vitamins
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks if object or unity object is null
        /// </summary>
        /// <param name="currentObject"></param>
        /// <exception cref="InvalidOperationException">Thrown when object is null</exception>
        public static void IsObjectNull(this object currentObject)
        {
            var check = currentObject as UnityEngine.Object;
            if (check != null)
            {
                check.IsObjectNull();
            }
            else
            {
                if (currentObject == null)
                {
                    throw new InvalidOperationException($"{nameof(currentObject)} was not initalized before accessing it");
                }
            }
        }

        /// <summary>
        /// Checks if unity object is null
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
