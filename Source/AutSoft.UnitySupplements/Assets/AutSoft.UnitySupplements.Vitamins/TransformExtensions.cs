#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    /// <summary>
    /// Extensions for Unity <see cref="Transform"/>
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Destroy every child object of the transform
        /// </summary>
        public static void DestroyChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Get the direct children of the transform
        /// </summary>
        /// <returns>Enumerable of the child transforms</returns>
        public static IEnumerable<Transform> Children(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                yield return child;
            }
        }

        /// <summary>
        /// Get the children for the transform recursively
        /// </summary>
        /// <returns>Enumerable of all the child transforms</returns>
        public static IEnumerable<Transform> NestedChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                yield return child;

                foreach (var grandchild in child.NestedChildren())
                {
                    yield return grandchild;
                }
            }
        }

        /// <summary>
        /// Get all parent transforms recursively
        /// </summary>
        /// <returns>Enumerable of the parent transforms</returns>
        public static IEnumerable<Transform> AllParents(this Transform transform)
        {
            var parent = transform.parent;
            if (parent != null)
            {
                yield return parent;

                foreach (var grandParent in parent.AllParents())
                {
                    yield return grandParent;
                }
            }
        }

        /// <summary>
        /// Sets the <see cref="Transform"/> position properties coordinates
        /// </summary>
        /// <param name="transform">Transform</param>
        /// <param name="x">Unity coordinate</param>
        public static void SetPositionX(this Transform transform, float x)
        {
            var position = transform.position;
            position.x = x;
            transform.position = position;
        }

        /// <summary>
        /// Sets the <see cref="Transform"/> position properties coordinates
        /// </summary>
        /// <param name="transform">Transform</param>
        /// <param name="y">Unity coordinate</param>
        public static void SetPositionY(this Transform transform, float y)
        {
            var position = transform.position;
            position.y = y;
            transform.position = position;
        }

        /// <summary>
        /// Sets the <see cref="Transform"/> position properties coordinates
        /// </summary>
        /// <param name="transform">Transform</param>
        /// <param name="z">Unity coordinate</param>
        public static void SetPositionZ(this Transform transform, float z)
        {
            var position = transform.position;
            position.z = z;
            transform.position = position;
        }

        /// <summary>
        /// Sets the <see cref="Transform"/> position properties coordinates
        /// </summary>
        /// <param name="transform">Transform</param>
        /// <param name="x">Unity coordinate</param>
        /// <param name="y">Unity coordinate</param>
        public static void SetPositionXY(this Transform transform, float x, float y)
        {
            var position = transform.position;
            position.x = x;
            position.y = y;
            transform.position = position;
        }

        /// <summary>
        /// Sets the <see cref="Transform"/> position properties coordinates
        /// </summary>
        /// <param name="transform">Transform</param>
        /// <param name="x">Unity coordinate</param>
        /// <param name="z">Unity coordinate</param>
        public static void SetPositionXZ(this Transform transform, float x, float z)
        {
            var position = transform.position;
            position.x = x;
            position.z = z;
            transform.position = position;
        }

        /// <summary>
        /// Sets the <see cref="Transform"/> position properties coordinates
        /// </summary>
        /// <param name="transform">Transform</param>
        /// <param name="y">Unity coordinate</param>
        /// <param name="z">Unity coordinate</param>
        public static void SetPositionYZ(this Transform transform, float y, float z)
        {
            var position = transform.position;
            position.y = y;
            position.z = z;
            transform.position = position;
        }
    }
}
