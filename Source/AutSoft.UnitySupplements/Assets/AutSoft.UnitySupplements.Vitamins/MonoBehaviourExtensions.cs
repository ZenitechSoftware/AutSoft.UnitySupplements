#nullable enable
using System.Linq.Expressions;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AutSoft.UnitySupplements.Vitamins
{
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// Check if the given field is set on the component, if not throws an exception. Only runs in the Unity Editor
        /// <exception cref="FieldNotSetException"></exception>
        /// </summary>
        /// <typeparam name="T">Type of the field</typeparam>
        /// <param name="component">Owner of the field</param>
        /// <param name="field">Field to check</param>
        /// <param name="fieldName">The name of the field. Use the <see cref="nameof"/> expression</param>
        /// <exception cref="FieldNotSetException">The field in question was not set</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckSerializedField<T>(this MonoBehaviour component, T field, string fieldName) where T : Object
        {
#if UNITY_EDITOR
            if (field == null)
            {
                var componentName = component.name;
                var gameObjectName = component.gameObject.name;
                throw new FieldNotSetException($"Field: {fieldName} is not set on Component: {componentName} on GameObject: {gameObjectName}", gameObjectName, componentName, fieldName);
            }
#endif
        }

        /// <summary>
        /// Check if the given field is set on the component, if not throws an exception. Only runs in the Unity Editor
        /// </summary>
        /// <typeparam name="TField">Type of the field</typeparam>
        /// <typeparam name="TComponent">Type of the component</typeparam>
        /// <param name="component">Owner of the field</param>
        /// <param name="fieldExpression">Expression which will return the member to check.</param>
        /// <exception cref="FieldNotSetException">The field in question was not set</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckSerializedField<TField, TComponent>(this TComponent component, Expression<Func<TComponent, TField>> fieldExpression) where TField : Object where TComponent : MonoBehaviour
        {
#if UNITY_EDITOR
            var field = fieldExpression.Compile()(component);

            if (field == null)
            {
                var fieldName = ((MemberExpression)fieldExpression.Body).Member.Name;

                var componentName = component.name;
                var gameObjectName = component.gameObject.name;
                throw new FieldNotSetException($"Field: {fieldName} is not set on Component: {componentName} on GameObject: {gameObjectName}", gameObjectName, componentName, fieldName);
            }
#endif
        }
    }
}
