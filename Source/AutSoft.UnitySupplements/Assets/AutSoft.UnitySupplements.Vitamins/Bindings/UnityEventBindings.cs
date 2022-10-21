using UnityEngine;
using UnityEngine.Events;

namespace AutSoft.UnitySupplements.Vitamins.Bindings
{
    public static partial class Binder
    {
        public static void Bind(this MonoBehaviour owner, UnityEvent unityEvent, UnityAction action) =>
            owner.gameObject.Bind(unityEvent, action);

        public static void Bind(this GameObject owner, UnityEvent unityEvent, UnityAction action)
        {
            var destroyDetector = owner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }

        public static void Bind<T>(this MonoBehaviour owner, UnityEvent<T> unityEvent, UnityAction<T> action) =>
            owner.gameObject.Bind(unityEvent, action);

        public static void Bind<T>(this GameObject owner, UnityEvent<T> unityEvent, UnityAction<T> action)
        {
            var destroyDetector = owner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }

        public static void Bind<T0, T1>(this MonoBehaviour owner, UnityEvent<T0, T1> unityEvent, UnityAction<T0, T1> action) =>
            owner.gameObject.Bind(unityEvent, action);

        public static void Bind<T0, T1>(this GameObject owner, UnityEvent<T0, T1> unityEvent, UnityAction<T0, T1> action)
        {
            var destroyDetector = owner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }

        public static void Bind<T0, T1, T2>(this MonoBehaviour owner, UnityEvent<T0, T1, T2> unityEvent, UnityAction<T0, T1, T2> action) =>
            owner.gameObject.Bind(unityEvent, action);

        public static void Bind<T0, T1, T2>(this GameObject owner, UnityEvent<T0, T1, T2> unityEvent, UnityAction<T0, T1, T2> action)
        {
            var destroyDetector = owner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }

        public static void Bind<T0, T1, T2, T3>(this MonoBehaviour owner, UnityEvent<T0, T1, T2, T3> unityEvent, UnityAction<T0, T1, T2, T3> action) =>
            owner.gameObject.Bind(unityEvent, action);

        public static void Bind<T0, T1, T2, T3>(this GameObject owner, UnityEvent<T0, T1, T2, T3> unityEvent, UnityAction<T0, T1, T2, T3> action)
        {
            var destroyDetector = owner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }
    }
}
