using System.Collections.Generic;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    public static class TransformExtensions
    {
        public static void DestroyChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                Object.Destroy(child.gameObject);
            }
        }

        public static IEnumerable<Transform> Children(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                yield return child;
            }
        }

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
    }
}
