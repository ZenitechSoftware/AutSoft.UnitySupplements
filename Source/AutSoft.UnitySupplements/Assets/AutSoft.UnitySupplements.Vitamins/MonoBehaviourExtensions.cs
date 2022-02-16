using System.Runtime.CompilerServices;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    public static class MonoBehaviourExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckSerializedField<T>(this MonoBehaviour component, T field, string fieldName) where T : Object
        {
            if (field == null)
            {
                var componentName = component.name;
                var gameObjectName = component.gameObject.name;
                throw new FieldNotSetException($"Field: {fieldName} is not set on Component: {componentName} on GameObject: {gameObjectName}", gameObjectName, componentName, fieldName);
            }
        }
    }
}
