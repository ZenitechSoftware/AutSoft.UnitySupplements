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
        [field: SerializeField] public Camera Camera { get; set; } = default!;
        [field: SerializeField] public PivotAxis PivotAxis { get; set; }
        [field: SerializeField] public bool UseCameraUpAxis { get; set; } = true;

        private void Awake()
        {
            const string fieldName = nameof(Camera);
            var gameObjectName = gameObject.name;
            Camera = Camera == null && Camera.main != null
                ? Camera.main
                : throw new FieldNotSetException(
                    $"Field: {fieldName} is not set on Component: {name} on GameObject: {gameObjectName}",
                    gameObjectName,
                    name,
                    fieldName);
        }

        private void Update()
        {
            var direction = (Camera.transform.position - transform.position).normalized;

            direction.x = PivotAxis.HasFlag(PivotAxis.X) ? direction.x : 0;
            direction.y = PivotAxis.HasFlag(PivotAxis.Y) ? direction.y : 0;
            direction.z = PivotAxis.HasFlag(PivotAxis.Z) ? direction.z : 0;

            transform.rotation = Quaternion.LookRotation(-direction, UseCameraUpAxis ? Camera.transform.up : Vector3.up);
        }

        public void SetCamera(Camera nextCamera)
        {
#pragma warning disable IDE0016 // Use 'throw' expression
            if (nextCamera == null) throw new ArgumentNullException(nameof(nextCamera));
#pragma warning restore IDE0016 // Use 'throw' expression
            Camera = nextCamera;
        }
    }
}
