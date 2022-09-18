#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace AutSoft.UnitySupplements.Vitamins.Bindings
{
    /// <summary>
    /// Provide "WPF-like" data binding methods, which connect properties of binding target objects and data sources
    /// </summary>
    public static partial class Binder
    {
        private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> _properties = new();

        /// <summary>
        /// One-way
        /// </summary>
        public static void Bind<TSource, TBindingSource>
        (
            this TBindingSource source,
            GameObject gameObject,
            Expression<Func<TBindingSource, TSource>> memberExpression,
            Action<TSource> update
        )
            where TBindingSource : INotifyPropertyChanged =>
            BindSourceToTarget(source, gameObject, GetMemberName(memberExpression), update);

        /// <summary>
        /// Two-way
        /// </summary>
        public static void Bind<TSource, TTarget, TBindingSource>
        (
            this TBindingSource source,
            GameObject gameObject,
            UnityEvent<TTarget> unityEvent,
            Expression<Func<TBindingSource, TSource>> memberExpression,
            Action<TSource> updateTarget,
            Func<TTarget, TSource> updateSource
        )
            where TBindingSource : INotifyPropertyChanged
        {
            var propertyName = GetMemberName(memberExpression);
            var (sourceType, destroyDetector) = BindSourceToTarget(source, gameObject, propertyName, updateTarget);
            BindTargetToSource(source, unityEvent, propertyName, updateSource, sourceType, destroyDetector);
        }

        private static string GetMemberName<T, R>(Expression<Func<T, R>> memberExpression) => ((MemberExpression)memberExpression.Body).Member.Name;

        private static (Type sourceType, DestroyDetector destroyDetector) BindSourceToTarget<T>
        (
            INotifyPropertyChanged source,
            GameObject gameObject,
            string propertyName,
            Action<T> update
        )
        {
            var sourceType = source.GetType();
            void UpdateBinding(object _, PropertyChangedEventArgs args)
            {
                if (args.PropertyName != propertyName) return;
                var property = _properties[sourceType][propertyName];
                update((T)property.GetValue(source));
            }
            source.PropertyChanged += UpdateBinding;
            var destroyDetector = gameObject.GetOrAddComponent<DestroyDetector>();
            destroyDetector.Destroyed += () => source.PropertyChanged -= UpdateBinding;
            SetValueFirstTime(source, propertyName, update, sourceType);
            return (sourceType, destroyDetector);
        }

        private static void BindTargetToSource<TSource, TTarget>
        (
            INotifyPropertyChanged source,
            UnityEvent<TTarget> unityEvent,
            string propertyName,
            Func<TTarget, TSource> updateSource,
            Type sourceType,
            DestroyDetector destroyDetector
        )
        {
            void UpdateProperty(TTarget value)
            {
                var property = _properties[sourceType][propertyName];
                var nextValue = updateSource(value);
                property.SetValue(source, nextValue);
            }
            unityEvent.AddListener(UpdateProperty);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(UpdateProperty);
        }

        private static void SetValueFirstTime<T>
        (
            INotifyPropertyChanged source,
            string propertyName,
            Action<T> update,
            Type sourceType
        )
        {
            var property = GetProperty(propertyName, sourceType);
            update((T)property.GetValue(source));
        }

        private static PropertyInfo GetProperty(string propertyName, Type sourceType)
        {
            PropertyInfo property;
            if (!_properties.TryGetValue(sourceType, out var properties))
            {
                property = sourceType.GetProperty(propertyName);
                _properties.Add(sourceType, new Dictionary<string, PropertyInfo> { { propertyName, property } });
            }
            else if (!properties.TryGetValue(propertyName, out property))
            {
                property = sourceType.GetProperty(propertyName);
                properties.Add(propertyName, property);
            }
            return property;
        }
    }
}
