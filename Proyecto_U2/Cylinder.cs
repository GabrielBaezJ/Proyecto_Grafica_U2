using System;

namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D cylinder centered at the local origin.
    /// Cylinder extends from -height/2 to +height/2 along Y axis.
    /// The circular cross-section is centered at X-Z plane.
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
            Material = new Material(Color3.Blue);
            GenerateVertices();
        }

        /// <summary>
        /// Generate cylinder vertices centered at local origin (0, 0, 0).
        /// Cylinder extends from -height/2 to +height/2 along Y axis.
        /// </summary>
        private void GenerateVertices()
        {
            vertices.Clear();
            indices.Clear();

            float halfHeight = height / 2f;

            // Generate vertices for the top and bottom circles
            // All coordinates are centered at origin
            for (int i = 0; i < sides; i++)
            {
                float angle = (float)(2 * Math.PI * i / sides);
                float x = (float)(radius * Math.Cos(angle));
                float z = (float)(radius * Math.Sin(angle));

                // Bottom circle (Y = -halfHeight)
                vertices.Add(new Vector3(x, -halfHeight, z));
                // Top circle (Y = +halfHeight)
                vertices.Add(new Vector3(x, halfHeight, z));
            }

            // Add center points for caps
            int bottomCenterIdx = vertices.Count;
            vertices.Add(new Vector3(0, -halfHeight, 0));  // Center of bottom cap
            int topCenterIdx = vertices.Count;
            vertices.Add(new Vector3(0, halfHeight, 0));   // Center of top cap

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

            // Generate bottom cap (fan triangles from center)
            for (int i = 0; i < sides; i++)
            {
                int next = (i + 1) % sides;
                indices.Add(bottomCenterIdx);
                indices.Add(next * 2);
                indices.Add(i * 2);
            }

            // Generate top cap (fan triangles from center)
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
