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
        /// <typeparam name="TSource">Type of the property we are binding</typeparam>
        /// <typeparam name="TBindingSource">Type of the class we are bindig to</typeparam>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="source">The binding source</param>
        /// <param name="sourcePropertyExpression">Defines the property we want to bind to</param>
        /// <param name="sourceToTarget">This method will run when the source property is changed</param>
        public static void Bind<TSource, TBindingSource>
        (
            this MonoBehaviour lifetimeOwner,
            TBindingSource source,
            Expression<Func<TBindingSource, TSource>> sourcePropertyExpression,
            Action<TSource> sourceToTarget
        )
            where TBindingSource : INotifyPropertyChanged =>
            lifetimeOwner.gameObject.Bind(source, sourcePropertyExpression, sourceToTarget);

        /// <summary>
        /// One-way
        /// </summary>
        /// <typeparam name="TSource">Type of the property we are binding</typeparam>
        /// <typeparam name="TBindingSource">Type of the class we are bindig to</typeparam>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="source">The binding source</param>
        /// <param name="sourcePropertyExpression">Defines the property we want to bind to</param>
        /// <param name="sourceToTarget">This method will run when the source property is changed</param>
        public static void Bind<TSource, TBindingSource>
        (
            this GameObject lifetimeOwner,
            TBindingSource source,
            Expression<Func<TBindingSource, TSource>> sourcePropertyExpression,
            Action<TSource> sourceToTarget
        )
            where TBindingSource : INotifyPropertyChanged =>
            BindSourceToTarget(source, lifetimeOwner, GetMemberName(sourcePropertyExpression), sourceToTarget);

        /// <summary>
        /// Two-way
        /// </summary>
        /// <typeparam name="TSource">Type of the property we are binding</typeparam>
        /// <typeparam name="TTarget">Type of the event data on <see cref="UnityEvent"/></typeparam>
        /// <typeparam name="TBindingSource">Type of the class we are bindig to</typeparam>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="source">The binding source</param>
        /// <param name="sourcePropertyExpression">Defines the property we want to bind to</param>
        /// <param name="sourceToTarget">This method will run when the source property is changed</param>
        /// <param name="targetEvent">The event which updateds the source property</param>
        /// <param name="targetToSource">Converts the unity event data for the source property</param>
        public static void Bind<TSource, TTarget, TBindingSource>
        (
            this MonoBehaviour lifetimeOwner,
            TBindingSource source,
            Expression<Func<TBindingSource, TSource>> sourcePropertyExpression,
            Action<TSource> sourceToTarget,
            UnityEvent<TTarget> targetEvent,
            Func<TTarget, TSource> targetToSource
        )
            where TBindingSource : INotifyPropertyChanged =>
            lifetimeOwner.gameObject.Bind(source, sourcePropertyExpression, sourceToTarget, targetEvent, targetToSource);

        /// <summary>
        /// Two-way
        /// </summary>
        /// <typeparam name="TSource">Type of the property we are binding</typeparam>
        /// <typeparam name="TTarget">Type of the event data on <see cref="UnityEvent"/></typeparam>
        /// <typeparam name="TBindingSource">Type of the class we are bindig to</typeparam>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="source">The binding source</param>
        /// <param name="sourcePropertyExpression">Defines the property we want to bind to</param>
        /// <param name="sourceToTarget">This method will run when the source property is changed</param>
        /// <param name="targetEvent">The event which updateds the source property</param>
        /// <param name="targetToSource">Converts the unity event data for the source property</param>
        public static void Bind<TSource, TTarget, TBindingSource>
        (
            this GameObject lifetimeOwner,
            TBindingSource source,
            Expression<Func<TBindingSource, TSource>> sourcePropertyExpression,
            Action<TSource> sourceToTarget,
            UnityEvent<TTarget> targetEvent,
            Func<TTarget, TSource> targetToSource
        )
            where TBindingSource : INotifyPropertyChanged
        {
            var propertyName = GetMemberName(sourcePropertyExpression);
            var (sourceType, destroyDetector) = BindSourceToTarget(source, lifetimeOwner, propertyName, sourceToTarget);
            BindTargetToSource(source, targetEvent, propertyName, targetToSource, sourceType, destroyDetector);
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
