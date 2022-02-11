using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    public static class GameObjectExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (!gameObject.TryGetComponent<T>(out var component))
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
    }
}
