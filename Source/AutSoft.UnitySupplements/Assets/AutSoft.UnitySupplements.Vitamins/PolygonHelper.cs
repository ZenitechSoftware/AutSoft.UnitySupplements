#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    /// <summary>
    /// Class for generating triangulated mesh
    /// </summary>
    public static class PolygonHelper
    {
        public static Mesh GenerateTriangulatedMesh(Vector3[] vertices)
        {
            var triangles = Triangulate(vertices);

            var normals = new Vector3[vertices.Length];
            Array.Fill(normals, Vector3.up);

            return new Mesh
            {
                vertices = vertices,
                triangles = triangles,
                normals = normals,
            };
        }

        private static int[] Triangulate(Vector3[] vertices)
        {
            var indices = new List<int>();

            var n = vertices.Length;
            if (n < 3)
            {
                return indices.ToArray();
            }

            var V = new int[n];
            if (Area(vertices) > 0)
            {
                for (var v = 0; v < n; v++)
                {
                    V[v] = v;
                }
            }
            else
            {
                for (var v = 0; v < n; v++)
                {
                    V[v] = n - 1 - v;
                }
            }

            var nv = n;
            var count = 2 * nv;
            for (var v = nv - 1; nv > 2;)
            {
                if (count-- <= 0)
                    return indices.ToArray();

                var u = v;
                if (nv <= u)
                    u = 0;
                v = u + 1;
                if (nv <= v)
                    v = 0;
                var w = v + 1;
                if (nv <= w)
                    w = 0;

                if (Snip(vertices, u, v, w, nv, V))
                {
                    int a, b, c, s, t;
                    a = V[u];
                    b = V[v];
                    c = V[w];
                    indices.Add(a);
                    indices.Add(b);
                    indices.Add(c);
                    for (s = v, t = v + 1; t < nv; s++, t++)
                        V[s] = V[t];
                    nv--;
                    count = 2 * nv;
                }
            }

            indices.Reverse();
            return indices.ToArray();
        }

        private static float Area(Vector3[] vertices)
        {
            var n = vertices.Length;
            var A = 0.0f;
            for (int p = n - 1, q = 0; q < n; p = q++)
            {
                var pval = vertices[p];
                var qval = vertices[q];
                A += (pval.x * qval.z) - (qval.x * pval.z);
            }
            return A * 0.5f;
        }

        private static bool Snip(Vector3[] vertices, int u, int v, int w, int n, int[] V)
        {
            int p;
            var A = vertices[V[u]];
            var B = vertices[V[v]];
            var C = vertices[V[w]];
            if (Mathf.Epsilon > ((B.x - A.x) * (C.z - A.z)) - ((B.z - A.z) * (C.x - A.x)))
                return false;
            for (p = 0; p < n; p++)
            {
                if (p == u || p == v || p == w)
                    continue;
                var P = vertices[V[p]];
                if (InsideTriangle(A, B, C, P))
                    return false;
            }
            return true;
        }

        private static bool InsideTriangle(Vector3 A, Vector3 B, Vector3 C, Vector3 P)
        {
            float ax, ay, bx, by, cx, cy, apx, apy, bpx, bpy, cpx, cpy;
            float cCROSSap, bCROSScp, aCROSSbp;

            ax = C.x - B.x; ay = C.z - B.z;
            bx = A.x - C.x; by = A.z - C.z;
            cx = B.x - A.x; cy = B.z - A.z;
            apx = P.x - A.x; apy = P.z - A.z;
            bpx = P.x - B.x; bpy = P.z - B.z;
            cpx = P.x - C.x; cpy = P.z - C.z;

            aCROSSbp = (ax * bpy) - (ay * bpx);
            cCROSSap = (cx * apy) - (cy * apx);
            bCROSScp = (bx * cpy) - (by * cpx);

            return aCROSSbp >= 0.0f && bCROSScp >= 0.0f && cCROSSap >= 0.0f;
        }
    }
}
