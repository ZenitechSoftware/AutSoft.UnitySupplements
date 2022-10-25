#nullable enable
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// Check if fields marked by the <see cref="SerializeField"/> are set. Only runs validations when run inside the Unity Editor
        /// </summary>
        /// <param name="self">Self</param>
        /// <param name="exceptions">Which fields not to check. Use the nameof() expression</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckSerializedFields(this MonoBehaviour self, params string[] exceptions)
        {
#if UNITY_EDITOR
            foreach (var field in GetFields(self)
                .Where(f => !exceptions.Contains(f.Name) && FilterField(f)))
            {
                CheckField(self, field);
            }
#endif
        }

        /// <summary>
        /// Check if fields marked by the <see cref="SerializeField"/> are set. Only runs validations when run inside the Unity Editor
        /// </summary>
        /// <param name="self">Self</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckSerializedFields(this MonoBehaviour self)
        {
#if UNITY_EDITOR
            foreach (var field in GetFields(self)
                .Where(f => FilterField(f)))
            {
                CheckField(self, field);
            }
#endif
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields", Justification = "We need this")]
        private static FieldInfo[] GetFields(MonoBehaviour self) => self
            .GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        private static bool FilterField(FieldInfo f) => f.FieldType.IsSubclassOf(typeof(Component)) && f.GetCustomAttribute<SerializeField>() != null;

        private static void CheckField(MonoBehaviour self, FieldInfo field)
        {
            var fieldValue = field.GetValue(self) as UnityEngine.Object;

            if (fieldValue == null) Debug.LogError($"Field {field.Name} is not set", self);
        }
    }
}
