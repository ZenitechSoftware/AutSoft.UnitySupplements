using UnityEngine;
using UnityEngine.Events;

namespace AutSoft.UnitySupplements.Vitamins.Bindings
{
    public static partial class Binder
    {
        public static void Bind(this UnityEvent unityEvent, GameObject owner, UnityAction action)
        {
            var destroyDetector = owner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }

        public static void Bind<T>(this UnityEvent<T> unityEvent, GameObject owner, UnityAction<T> action)
        {
            var destroyDetector = owner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }

        public static void Bind<T0, T1>(this UnityEvent<T0, T1> unityEvent, GameObject owner, UnityAction<T0, T1> action)
        {
            var destroyDetector = owner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }

        public static void Bind<T0, T1, T2>(this UnityEvent<T0, T1, T2> unityEvent, GameObject owner, UnityAction<T0, T1, T2> action)
        {
            var destroyDetector = owner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }

        public static void Bind<T0, T1, T2, T3>(this UnityEvent<T0, T1, T2, T3> unityEvent, GameObject owner, UnityAction<T0, T1, T2, T3> action)
        {
            var destroyDetector = owner.GetOrAddComponent<DestroyDetector>();
            unityEvent.AddListener(action);
            destroyDetector.Destroyed += () => unityEvent.RemoveListener(action);
        }
    }
}
