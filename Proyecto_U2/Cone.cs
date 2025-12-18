using System;

namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D cone centered at the local origin.
    /// Apex is at Y = +height/2, base is at Y = -height/2.
    /// The circular base is centered at X-Z plane.
    /// </summary>
    public class Cone : Object3D
    {
        private float radius;
        private float height;
        private int sides;

        public Cone(string name = "Cone", float radius = 1f, float height = 2f, int sides = 20)
            : base(name)
        {
            this.radius = radius;
            this.height = height;
            this.sides = sides;
            Material = new Material(Color3.Yellow);
            GenerateVertices();
        }

        /// <summary>
        /// Generate cone vertices centered at local origin (0, 0, 0).
        /// Apex at (0, +height/2, 0), base at Y = -height/2.
        /// </summary>
        private void GenerateVertices()
        {
            vertices.Clear();
            indices.Clear();

            float halfHeight = height / 2f;

            // Add apex at the top center
            vertices.Add(new Vector3(0, halfHeight, 0));
            int apexIdx = 0;

            // Add base circle vertices at Y = -halfHeight
            for (int i = 0; i < sides; i++)
            {
                float angle = (float)(2 * Math.PI * i / sides);
                float x = (float)(radius * Math.Cos(angle));
                float z = (float)(radius * Math.Sin(angle));
                vertices.Add(new Vector3(x, -halfHeight, z));
            }

            // Add base center point
            int baseCenterIdx = vertices.Count;
            vertices.Add(new Vector3(0, -halfHeight, 0));

            // Generate side faces (triangles from apex to base edge)
            for (int i = 0; i < sides; i++)
            {
                int next = (i + 1) % sides;
                int v1 = 1 + i;
                int v2 = 1 + next;

                indices.Add(apexIdx);
                indices.Add(v2);
                indices.Add(v1);
            }

            // Generate base cap (fan triangles from center)
            for (int i = 0; i < sides; i++)
            {
                int next = (i + 1) % sides;
                indices.Add(baseCenterIdx);
                indices.Add(1 + next);
                indices.Add(1 + i);
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
