#nullable enable
using System;
using System.Runtime.Serialization;

namespace AutSoft.UnitySupplements.Vitamins
{
    /// <summary>
    /// Exception used by <see cref="MonoBehaviourExtensions.CheckSerializedField{T}"/>
    /// </summary>
    [Serializable]
    public class FieldNotSetException : Exception
    {
        public FieldNotSetException() { }
        public FieldNotSetException(string message) : base(message) { }
        public FieldNotSetException(string message, Exception inner) : base(message, inner) { }
        public FieldNotSetException(string message, string gameObjectName, string componentName, string parameterName) : this(message)
        {
            GameObjectName = gameObjectName;
            ComponentName = componentName;
            ParameterName = parameterName;
        }
        protected FieldNotSetException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public string GameObjectName { get; } = string.Empty;
        public string ComponentName { get; } = string.Empty;
        public string ParameterName { get; } = string.Empty;
    }
}
