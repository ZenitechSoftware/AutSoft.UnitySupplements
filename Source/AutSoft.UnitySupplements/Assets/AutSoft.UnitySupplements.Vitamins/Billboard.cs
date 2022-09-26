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
        [SerializeField] private PivotAxis _pivotAxis;
        [SerializeField] private bool _useCameraUpAxis;

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

        private void Update()
        {
            var direction = (_camera.transform.position - transform.position).normalized;

            direction.x = _pivotAxis.HasFlag(PivotAxis.X) ? direction.x : 0;
            direction.y = _pivotAxis.HasFlag(PivotAxis.Y) ? direction.y : 0;
            direction.z = _pivotAxis.HasFlag(PivotAxis.Z) ? direction.z : 0;

            transform.rotation = Quaternion.LookRotation(-direction, _useCameraUpAxis ? _camera.transform.up : Vector3.up);
        }

        public void SetCamera(Camera nextCamera)
        {
#pragma warning disable IDE0016 // Use 'throw' expression
            if (nextCamera == null) throw new ArgumentNullException(nameof(nextCamera));
#pragma warning restore IDE0016 // Use 'throw' expression
            _camera = nextCamera;
        }
    }
}
