using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngineInternal;

namespace Cainos.PixelArtTopDown_Basic
{
    public class FOV : MonoBehaviour
    {

        private static Vector3 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }

        private static float GetAngleFromVectorFloat(Vector3 dir)
        {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            n += 45;
            if (n < 0) n += 360;
            return n;
        }

        private Mesh mesh;
        private Vector3 origin;
        private float startingAngle;
        public MeshRenderer meshRenderer;
        public float fov;
        public float viewDistance;

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
            fov = 50f;
            origin = Vector3.zero;
            meshRenderer.sortingLayerName = "Layer 1";
        }
        private void LateUpdate()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            int rayCount = 50;
            float angle = startingAngle;
            float angleIncrease = fov / rayCount;
            viewDistance = 5f;

            Vector3[] vertices = new Vector3[rayCount + 1 + 1];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[rayCount * 3];
            vertices[0] = origin;
            int vertexIndex = 1;
            int triangleIndex = 0;
            GetComponent<SortingGroup>().sortingLayerName = meshRenderer.sortingLayerName;
            for (int i = 0; i <= rayCount; i++)
            {
                Vector3 vertex;
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
                vertices[vertexIndex] = vertex;

                if (i > 0)
                {
                    triangles[triangleIndex + 0] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;

                    triangleIndex += 3;
                }
                vertexIndex++;
                angle -= angleIncrease;
            }

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
        }

        public void setOrigin(Vector3 origin)
        {
            this.origin = origin;
        }

        public void SetAimDirection(Vector3 aimDirection)
        {
            this.startingAngle = GetAngleFromVectorFloat(aimDirection) - this.fov / 2f;
        }
    }
}