#nullable enable
using System;
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
        public static bool IsObjectNull(this object obj) => obj == null || ((obj is UnityObject) && ((UnityObject)obj) == null);

        /// <summary>
        /// Checks if unity object is null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True if object is null, else return false</returns>
        public static bool IsObjectNull(this UnityObject obj) => obj == null;

        /// <summary>
        /// Checks if object or unity object is null
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="InvalidOperationException">Thrown when object is null</exception>
        public static void IsObjectNullThrow(this object obj)
        {
            var check = obj as UnityEngine.Object;
            if (check != null)
            {
                check.IsObjectNullThrow();
            }
            else
            {
                if (obj == null)
                {
                    throw new InvalidOperationException($"{nameof(obj)} was not initalized before accessing it");
                }
            }
        }

        /// <summary>
        /// Checks if unity object is null
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when object is null</exception>
        public static void IsObjectNullThrow(this UnityObject obj)
        {
            if (obj == null)
            {
                throw new InvalidOperationException($"{nameof(obj)} was not initalized before accessing it");
            }
        }
    }
}
