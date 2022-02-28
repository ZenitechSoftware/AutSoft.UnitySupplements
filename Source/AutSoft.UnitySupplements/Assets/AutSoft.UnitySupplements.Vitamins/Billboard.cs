#nullable enable
using System;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    /// <summary>
    /// Sets the transform rotation to match the camera's rotation
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        [SerializeField] private Camera _camera = default!;

        private void Awake() => this.CheckSerializedField(_camera, nameof(_camera));

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
