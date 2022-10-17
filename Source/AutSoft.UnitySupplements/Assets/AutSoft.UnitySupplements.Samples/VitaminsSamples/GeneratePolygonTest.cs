#nullable enable
using AutSoft.UnitySupplements.Vitamins;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AutSoft.UnitySupplements.Samples.VitaminsSamples
{
    public class GeneratePolygonTest : MonoBehaviour
    {
        private MeshFilter _meshFilter = default!;

        private void Awake()
        {
            var vertices = new List<Vector3>();

            for (var i = 0; i < 10; i++)
            {
                vertices.Add(new Vector3(Random.value * 10, 0, Random.value * 10));
            }

            var mesh = new Mesh();
            PolygonHelper.GenerateTriangulatedMesh(mesh, vertices.ToArray());
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.mesh = mesh;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var vertices = new List<Vector3>();

                for (var i = 0; i < 10; i++)
                {
                    vertices.Add(new Vector3(Random.value * 10, 0, Random.value * 10));
                }

                PolygonHelper.GenerateTriangulatedMesh(_meshFilter.mesh, vertices.ToArray());
            }
        }
    }
}
