#nullable enable
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

// Made from https://gist.github.com/aholkner/214628a05b15f0bb169660945ac7923b

namespace AutSoft.UnitySupplements.ResourceGenerator.Editor.Extensions
{
    internal static class SerializedPropertyExtensions
    {
        /// <summary>
        /// (Extension) Set the value of the serialized property.
        /// </summary>
        public static void SetValue(this SerializedProperty property, object value)
        {
            Undo.RecordObject(property.serializedObject.targetObject, $"Set {property.name}");

            SetValueNoRecord(property, value);

            EditorUtility.SetDirty(property.serializedObject.targetObject);
            property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// (Extension) Set the value of the serialized property, but do not record the change.
        /// The change will not be persisted unless you call SetDirty and ApplyModifiedProperties.
        /// </summary>
        private static void SetValueNoRecord(this SerializedProperty property, object value)
        {
            var propertyPath = property.propertyPath;
            object container = property.serializedObject.targetObject;

            var i = 0;
            NextPathComponent(propertyPath, ref i, out var deferredToken);
            while (NextPathComponent(propertyPath, ref i, out var token))
            {
                container = GetPathComponentValue(container, deferredToken);
                deferredToken = token;
            }

            Debug.Assert(!container.GetType().IsValueType, $"Cannot use SerializedObject.SetValue on a struct object, as the result will be set on a temporary.  Either change {container.GetType().Name} to a class, or use SetValue with a parent member.");
            SetPathComponentValue(container, deferredToken, value);
        }

        // Union type representing either a property name or array element index.  The element
        // index is valid only if propertyName is null.
        private struct PropertyPathComponent
        {
            public string PropertyName;
            public int ElementIndex;
        }

        private static readonly Regex _arrayElementRegex = new(@"\GArray\.data\[(\d+)\]", RegexOptions.Compiled);

        private static bool NextPathComponent(string propertyPath, ref int index, out PropertyPathComponent component)
        {
            component = new PropertyPathComponent();

            if (index >= propertyPath.Length)
                return false;

            var arrayElementMatch = _arrayElementRegex.Match(propertyPath, index);
            if (arrayElementMatch.Success)
            {
                index += arrayElementMatch.Length + 1; // Skip past next '.'
                component.ElementIndex = int.Parse(arrayElementMatch.Groups[1].Value);
                return true;
            }

            var dot = propertyPath.IndexOf('.', index);
            if (dot == -1)
            {
                component.PropertyName = propertyPath[index..];
                index = propertyPath.Length;
            }
            else
            {
                component.PropertyName = propertyPath[index..dot];
                index = dot + 1; // Skip past next '.'
            }

            return true;
        }

        private static object GetPathComponentValue(object container, PropertyPathComponent component) =>
            component.PropertyName == null
                ? ((IList)container)[component.ElementIndex]
                : GetMemberValue(container, component.PropertyName);

        private static void SetPathComponentValue(object container, PropertyPathComponent component, object value)
        {
            if (component.PropertyName == null)
            {
                ((IList)container)[component.ElementIndex] = value;
            }
            else
            {
                SetMemberValue(container, component.PropertyName, value);
            }
        }

        private static object GetMemberValue(object container, string name)
        {
            if (container == null)
                return null;
            var type = container.GetType();
            foreach (var member in type.GetMember(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                switch (member)
                {
                    case FieldInfo field:
                        return field.GetValue(container);
                    case PropertyInfo property:
                        return property.GetValue(container);
                }
            }

            return null;
        }

        private static void SetMemberValue(object container, string name, object value)
        {
            var type = container.GetType();
            foreach (var member in type.GetMember(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                switch (member)
                {
                    case FieldInfo field:
                        field.SetValue(container, value);
                        return;
                    case PropertyInfo property:
                        property.SetValue(container, value);
                        return;
                }
            }

            Debug.Assert(false, $"Failed to set member {container}.{name} via reflection");
        }
    }
}
