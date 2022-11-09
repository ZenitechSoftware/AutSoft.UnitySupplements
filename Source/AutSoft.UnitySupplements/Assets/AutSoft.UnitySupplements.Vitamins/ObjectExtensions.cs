#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using UnityObject = UnityEngine.Object;

namespace AutSoft.UnitySupplements.Vitamins
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Checks if object or unity object is null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if object is null, else return false</returns>
        public static bool IsObjectNull([NotNullWhen(false)] this object? obj) =>
            obj == null || (obj is UnityObject unityObject && unityObject.IsObjectNull());

        /// <summary>
        /// Checks if unity object is nulls
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if object is null, else return false</returns>
        public static bool IsObjectNull([NotNullWhen(false)] this UnityObject? obj) => obj == null;

        /// <summary>
        /// Checks if object or unity object is null
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="InvalidOperationException">Thrown when object is null</exception>
        public static void IsObjectNullThrow([NotNull] this object? obj)
        {
            if (obj.IsObjectNull())
            {
                throw new ArgumentNullException(nameof(obj), $"{nameof(obj)} was not initalized before accessing it");
            }
        }

        /// <summary>
        /// Checks if unity object is null
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when object is null</exception>
        public static void IsObjectNullThrow([NotNull] this UnityObject? obj)
        {
            if (obj.IsObjectNull())
            {
                throw new ArgumentNullException(nameof(obj), $"{nameof(obj)} was not initalized before accessing it");
            }
        }
    }
}
