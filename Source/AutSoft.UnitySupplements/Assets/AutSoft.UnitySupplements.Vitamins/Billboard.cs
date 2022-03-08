#nullable enable
using System;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    /// <summary>
    /// Sets the transform rotation to match the given camera's rotation, uses main camera by default
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private Camera _camera = default!;

        private void Awake()
        {
            const string fieldName = nameof(_camera);
            var gameObjectName = gameObject.name;
            _camera = _camera == null && Camera.main != null
                ? Camera.main
                : throw new FieldNotSetException(
                    $"Field: {fieldName} is not set on Component: {name} on GameObject: {gameObjectName}",
                    gameObjectName,
                    name,
                    fieldName);
        }

        private void Update() => transform.rotation = _camera.transform.rotation;

        public void SetCamera(Camera nextCamera)
        {
#pragma warning disable IDE0016 // Use 'throw' expression
            if (nextCamera == null) throw new ArgumentNullException(nameof(nextCamera));
#pragma warning restore IDE0016 // Use 'throw' expression
            _camera = nextCamera;
        }
    }
}
