using System;

namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D cylinder.
    /// </summary>
    public class Cylinder : Object3D
    {
        private float radius;
        private float height;
        private int sides;

        public Cylinder(string name = "Cylinder", float radius = 1f, float height = 2f, int sides = 20)
            : base(name)
        {
            this.radius = radius;
            this.height = height;
            this.sides = sides;
            GenerateVertices();
            Material = new Material(Color3.Blue);
        }

        private void GenerateVertices()
        {
            vertices.Clear();
            indices.Clear();

            float halfHeight = height / 2f;

            // Generate vertices for the top and bottom circles
            for (int i = 0; i < sides; i++)
            {
                float angle = (float)(2 * Math.PI * i / sides);
                float x = (float)(radius * Math.Cos(angle));
                float z = (float)(radius * Math.Sin(angle));

                // Bottom circle
                vertices.Add(new Vector3(x, -halfHeight, z));
                // Top circle
                vertices.Add(new Vector3(x, halfHeight, z));
            }

            // Add center points for caps
            int bottomCenterIdx = vertices.Count;
            vertices.Add(new Vector3(0, -halfHeight, 0));
            int topCenterIdx = vertices.Count;
            vertices.Add(new Vector3(0, halfHeight, 0));

            // Generate side faces
            for (int i = 0; i < sides; i++)
            {
                int next = (i + 1) % sides;
                int v0 = i * 2;
                int v1 = v0 + 1;
                int v2 = next * 2;
                int v3 = v2 + 1;

                // Side quad as two triangles
                indices.Add(v0);
                indices.Add(v2);
                indices.Add(v1);

                indices.Add(v1);
                indices.Add(v2);
                indices.Add(v3);
            }

            // Generate bottom cap
            for (int i = 0; i < sides; i++)
            {
                int next = (i + 1) % sides;
                indices.Add(bottomCenterIdx);
                indices.Add(next * 2);
                indices.Add(i * 2);
            }

            // Generate top cap
            for (int i = 0; i < sides; i++)
            {
                int next = (i + 1) % sides;
                indices.Add(topCenterIdx);
                indices.Add(i * 2 + 1);
                indices.Add(next * 2 + 1);
            }
        }

        public override void Draw()
        {
            // Drawing logic will be implemented in the renderer
        }

        public float GetRadius()
        {
            return radius;
        }

        public float GetHeight()
        {
            return height;
        }

        public void SetRadius(float newRadius)
        {
            radius = Math.Max(0.1f, newRadius);
            GenerateVertices();
        }

        public void SetHeight(float newHeight)
        {
            height = Math.Max(0.1f, newHeight);
            GenerateVertices();
        }
    }
}
