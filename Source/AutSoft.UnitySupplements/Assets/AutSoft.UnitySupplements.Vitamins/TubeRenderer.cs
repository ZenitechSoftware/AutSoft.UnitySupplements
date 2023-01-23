using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    /// <summary>
    /// Works similarly to Unity's <see cref="LineRenderer"/> component but the segments are made of a mesh of connected tubes.
    /// </summary>
    /// <remarks>
    /// The mesh is generated in the next LateUpdate() after properties have changed.
    /// </remarks>
    [RequireComponent(typeof(MeshFilter))]
    [ExecuteInEditMode]
    public class TubeRenderer : MonoBehaviour
    {
        [Min(0)]
        [SerializeField] private int _segments = 8;
        [SerializeField] private List<Vector3> _positions = new();
        [Min(0)]
        [SerializeField] private float _startWidth = 1f;
        [Min(0)]
        [SerializeField] private float _endWidth = 1f;
        [SerializeField] private bool _showNodesInEditor;
        [SerializeField] private Vector2 _uvScale = Vector2.one;
        [SerializeField] private bool _inside;
        [SerializeField] private MeshCollider _collider = default!;

        private Mesh _mesh = default!;
        private int _lastUpdate;

        public int PositionCount => _positions.Count;

        private void Awake()
        {
            this.CheckSerializedFields();

            var meshFilter = GetComponent<MeshFilter>();
            if (_mesh == null) _mesh = new Mesh();
            meshFilter.sharedMesh = _mesh;
        }

        private void LateUpdate()
        {
            var currentHash = PropHashCode();
            if (_lastUpdate != currentHash)
            {
                CreateMesh(currentHash);
            }
        }

        public Vector3 GetPosition(float f)
        {
            var a = Math.Max(0, Math.Min(_positions.Count, Mathf.FloorToInt(f)));
            var b = Math.Max(0, Math.Min(_positions.Count, Mathf.CeilToInt(f)));
            var t = f - a;
            return Vector3.Lerp(_positions[a], _positions[b], t);
        }

        public Vector3 GetPosition(int index) => _positions[index];

        public void SetPositions(List<Vector3> positions) => _positions = positions;

        public void SetPosition(int index, Vector3 position)
        {
            if (index >= _positions.Count)
            {
                _positions.Add(position);
            }
            else
            {
                _positions[index] = position;
            }
        }

        private void CreateMesh(int currentHash)
        {
            if (_positions == null || _positions.Count < 2) return;

            var theta = Mathf.PI * 2 / _segments;

            var size = (_positions.Count - 1) * _segments * 2;
            var rectangels = ((_positions.Count - 1) * _segments) + ((_positions.Count - 2) * _segments);
            var endcapTris = _segments - 2;
            var numberOfTris = 2 * 3 * (rectangels + endcapTris);
            using var verts = ArrayPool<Vector3>.Shared.RentDisposable(size);
            using var uvs = ArrayPool<Vector2>.Shared.RentDisposable(size);
            using var normals = ArrayPool<Vector3>.Shared.RentDisposable(size);
            using var tris = ArrayPool<int>.Shared.RentDisposable(numberOfTris);

            for (var i = 0; i < _positions.Count - 1; i++)
            {
                var dia = Mathf.Lerp(_startWidth, _endWidth, (float)i / _positions.Count);

                var localForward = GetVertexFwd(i);
                var localUp = Vector3.Cross(localForward, Vector3.up);
                var localRight = Vector3.Cross(localForward, localUp);

                for (var j = 0; j < _segments; ++j)
                {
                    var t = theta * j;
                    var vert = _positions[i] + (dia * Mathf.Sin(t) * localUp) + (dia * Mathf.Cos(t) * localRight);
                    var vert2 = _positions[i + 1] + (dia * Mathf.Sin(t) * localUp) + (dia * Mathf.Cos(t) * localRight);
                    var trix = (i * _segments) + j;

                    var x = (i * _segments * 2) + j;

                    verts[x] = vert;
                    verts[x + _segments] = vert2;
                    uvs[x] = _uvScale * new Vector2(t / (Mathf.PI * 2), (float)i * _positions.Count);

                    normals[x] = (vert - _positions[i]).normalized;
                    normals[x + _segments] = (vert - _positions[i + 1]).normalized;

                    if (i >= _positions.Count - 1) continue;

                    if (_inside) normals[x] = -normals[x];
                    if (_inside)
                    {
                        tris[trix * 6] = x;
                        tris[(trix * 6) + 1] = x + _segments;
                        tris[(trix * 6) + 2] = x + 1;

                        tris[(trix * 6) + 3] = x;
                        tris[(trix * 6) + 4] = x + _segments - 1;
                        tris[(trix * 6) + 5] = x + _segments;
                    }
                    else
                    {
                        tris[trix * 6] = x + 1;
                        tris[(trix * 6) + 1] = x + _segments;
                        tris[(trix * 6) + 2] = x;

                        tris[(trix * 6) + 3] = x + _segments;
                        tris[(trix * 6) + 4] = x + _segments - 1;
                        tris[(trix * 6) + 5] = x;
                    }
                }
            }

            //Connect tubes to each other
            var firstTri = (_positions.Count - 1) * _segments * 2 * 3;
            for (var i = 0; i < _positions.Count - 2; i++)
            {
                for (var j = 0; j < _segments; j++)
                {
                    var x = (((i * 2) + 1) * _segments) + j;
                    tris[firstTri++] = x + 1;
                    tris[firstTri++] = x + _segments;
                    tris[firstTri++] = x;

                    tris[firstTri++] = x + _segments;
                    tris[firstTri++] = x + _segments - 1;
                    tris[firstTri++] = x;
                }
            }

            //Add endcaps
            for (var j = 1; j < _segments - 1; j++)
            {
                tris[firstTri++] = j + 1;
                tris[firstTri++] = j;
                tris[firstTri++] = 0;

                tris[firstTri++] = size - _segments;
                tris[firstTri++] = size - _segments + j;
                tris[firstTri++] = size - _segments + j + 1;
            }

            _mesh.Clear();
            _mesh.SetVertices(verts.Values, 0, size);
            _mesh.SetUVs(0, uvs.Values, 0, size);
            _mesh.SetNormals(normals.Values, 0, size);
            _mesh.SetTriangles(tris.Values, 0, numberOfTris, 0);
            _mesh.RecalculateBounds();
            _collider.sharedMesh = _mesh;
            _lastUpdate = currentHash;
        }

        private Vector3 GetVertexFwd(int i) => (_positions[i] - _positions[i + 1]).normalized;

        private void OnDrawGizmos()
        {
            if (_showNodesInEditor)
            {
                Gizmos.color = Color.red;
                for (var i = 0; i < _positions.Count; ++i)
                {
                    var dia = Mathf.Lerp(_startWidth, _endWidth, (float)i / _positions.Count);
                    Gizmos.DrawSphere(transform.position + _positions[i], dia);
                }
            }
        }

        private int PropHashCode() =>
            _positions.Aggregate(0, (total, it) => total ^ it.GetHashCode()) ^ _segments.GetHashCode() ^ _startWidth.GetHashCode() ^ _endWidth.GetHashCode();
    }
}
