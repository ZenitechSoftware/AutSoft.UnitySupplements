using UnityEngine;
using UnityEngine.Events;

namespace AutSoft.UnitySupplements.Vitamins.Bindings
{
    public static partial class Binder
    {
        /// <summary>
        /// Subscribe to a <see cref="UnityEvent"/> for the lifetime of the <see cref="GameObject"/>
        /// </summary>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="unityEvent">The event we subscribe to</param>
        /// <param name="action"><see cref="UnityAction"/> to run</param>
        public static void Bind(this MonoBehaviour lifetimeOwner, UnityEvent unityEvent, UnityAction action) =>
            lifetimeOwner.gameObject.Bind(unityEvent, action);

        /// <summary>
        /// Subscribe to a <see cref="UnityEvent"/> for the lifetime of the <see cref="GameObject"/>
        /// </summary>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="unityEvent">The event we subscribe to</param>
        /// <param name="action"><see cref="UnityAction"/> to run</param>
        public static void Bind(this GameObject lifetimeOwner, UnityEvent unityEvent, UnityAction action)
        {
            var destroyDetector = lifetimeOwner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }

        /// <summary>
        /// Subscribe to a <see cref="UnityEvent"/> for the lifetime of the <see cref="GameObject"/>
        /// </summary>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="unityEvent">The event we subscribe to</param>
        /// <param name="action"><see cref="UnityAction"/> to run</param>
        public static void Bind<T>(this MonoBehaviour lifetimeOwner, UnityEvent<T> unityEvent, UnityAction<T> action) =>
            lifetimeOwner.gameObject.Bind(unityEvent, action);

        /// <summary>
        /// Subscribe to a <see cref="UnityEvent"/> for the lifetime of the <see cref="GameObject"/>
        /// </summary>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="unityEvent">The event we subscribe to</param>
        /// <param name="action"><see cref="UnityAction"/> to run</param>
        public static void Bind<T>(this GameObject lifetimeOwner, UnityEvent<T> unityEvent, UnityAction<T> action)
        {
            var destroyDetector = lifetimeOwner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }

        /// <summary>
        /// Subscribe to a <see cref="UnityEvent"/> for the lifetime of the <see cref="GameObject"/>
        /// </summary>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="unityEvent">The event we subscribe to</param>
        /// <param name="action"><see cref="UnityAction"/> to run</param>
        public static void Bind<T0, T1>(this MonoBehaviour lifetimeOwner, UnityEvent<T0, T1> unityEvent, UnityAction<T0, T1> action) =>
            lifetimeOwner.gameObject.Bind(unityEvent, action);

        /// <summary>
        /// Subscribe to a <see cref="UnityEvent"/> for the lifetime of the <see cref="GameObject"/>
        /// </summary>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="unityEvent">The event we subscribe to</param>
        /// <param name="action"><see cref="UnityAction"/> to run</param>
        public static void Bind<T0, T1>(this GameObject lifetimeOwner, UnityEvent<T0, T1> unityEvent, UnityAction<T0, T1> action)
        {
            var destroyDetector = lifetimeOwner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }

        /// <summary>
        /// Subscribe to a <see cref="UnityEvent"/> for the lifetime of the <see cref="GameObject"/>
        /// </summary>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="unityEvent">The event we subscribe to</param>
        /// <param name="action"><see cref="UnityAction"/> to run</param>
        public static void Bind<T0, T1, T2>(this MonoBehaviour lifetimeOwner, UnityEvent<T0, T1, T2> unityEvent, UnityAction<T0, T1, T2> action) =>
            lifetimeOwner.gameObject.Bind(unityEvent, action);

        /// <summary>
        /// Subscribe to a <see cref="UnityEvent"/> for the lifetime of the <see cref="GameObject"/>
        /// </summary>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="unityEvent">The event we subscribe to</param>
        /// <param name="action"><see cref="UnityAction"/> to run</param>
        public static void Bind<T0, T1, T2>(this GameObject lifetimeOwner, UnityEvent<T0, T1, T2> unityEvent, UnityAction<T0, T1, T2> action)
        {
            var destroyDetector = lifetimeOwner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }

        /// <summary>
        /// Subscribe to a <see cref="UnityEvent"/> for the lifetime of the <see cref="GameObject"/>
        /// </summary>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="unityEvent">The event we subscribe to</param>
        /// <param name="action"><see cref="UnityAction"/> to run</param>
        public static void Bind<T0, T1, T2, T3>(this MonoBehaviour lifetimeOwner, UnityEvent<T0, T1, T2, T3> unityEvent, UnityAction<T0, T1, T2, T3> action) =>
            lifetimeOwner.gameObject.Bind(unityEvent, action);

        /// <summary>
        /// Subscribe to a <see cref="UnityEvent"/> for the lifetime of the <see cref="GameObject"/>
        /// </summary>
        /// <param name="lifetimeOwner">The binding is bound to the <see cref="GameObject"/>'s destruction</param>
        /// <param name="unityEvent">The event we subscribe to</param>
        /// <param name="action"><see cref="UnityAction"/> to run</param>
        public static void Bind<T0, T1, T2, T3>(this GameObject lifetimeOwner, UnityEvent<T0, T1, T2, T3> unityEvent, UnityAction<T0, T1, T2, T3> action)
        {
            var destroyDetector = lifetimeOwner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }
    }
}
