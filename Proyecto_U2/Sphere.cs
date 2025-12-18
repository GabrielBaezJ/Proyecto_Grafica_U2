using System;

namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D sphere centered at the local origin.
    /// Generated using spherical coordinates (stacks and slices parametrization).
    /// </summary>
    public class Sphere : Object3D
    {
        private float radius;
        private int stacks;
        private int slices;

        public Sphere(string name = "Sphere", float radius = 1f, int stacks = 20, int slices = 20)
            : base(name)
        {
            this.radius = radius;
            this.stacks = stacks;
            this.slices = slices;
            Material = new Material(Color3.Green);
            GenerateVertices();
        }

        /// <summary>
        /// Generate sphere vertices centered at local origin (0, 0, 0).
        /// Uses parametric spherical coordinates with stacks (latitude) and slices (longitude).
        /// </summary>
        private void GenerateVertices()
        {
            vertices.Clear();
            indices.Clear();

            // Generate vertices using spherical coordinates
            // Each vertex is centered around the origin within radius
            for (int i = 0; i <= stacks; i++)
            {
                float stackAngle = (float)(Math.PI / stacks * i);
                float xy = (float)(radius * Math.Sin(stackAngle));
                float z = (float)(radius * Math.Cos(stackAngle));
                
                // Adjust z to be centered: range from -radius to +radius
                z = (float)(radius * Math.Cos(stackAngle) - radius * Math.Cos(0));

                for (int j = 0; j <= slices; j++)
                {
                    float sliceAngle = (float)(2 * Math.PI / slices * j);
                    float x = (float)(xy * Math.Cos(sliceAngle));
                    float y = (float)(xy * Math.Sin(sliceAngle));

                    vertices.Add(new Vector3(x, y, z));
                }
            }

            // Generate triangle indices
            for (int i = 0; i < stacks; i++)
            {
                int k1 = i * (slices + 1);
                int k2 = k1 + slices + 1;

                for (int j = 0; j < slices; j++)
                {
                    // First triangle of quad
                    if (i != 0)
                    {
                        indices.Add(k1);
                        indices.Add(k2);
                        indices.Add(k1 + 1);
                    }

                    // Second triangle of quad
                    if (i != (stacks - 1))
                    {
                        indices.Add(k1 + 1);
                        indices.Add(k2);
                        indices.Add(k2 + 1);
                    }

                    k1++;
                    k2++;
                }
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

        public void SetRadius(float newRadius)
        {
            radius = Math.Max(0.1f, newRadius);
            GenerateVertices();
        }
    }
}
