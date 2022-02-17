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
