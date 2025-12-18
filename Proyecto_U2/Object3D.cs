using System;
using System.Collections.Generic;

namespace Proyecto_U2
{
    /// <summary>
    /// Base class for all 3D objects.
    /// Transformation order: Scale ? Rotation ? Translation
    /// All vertices are centered at the local origin (0,0,0) in model space.
    /// </summary>
    public abstract class Object3D
    {
        public string Name { get; set; }
        public Transform Transform { get; set; }
        public Material Material { get; set; }
        public bool IsSelected { get; set; }

        protected List<Vector3> vertices;
        protected List<int> indices;

        public Object3D(string name = "Object3D")
        {
            Name = name;
            Transform = new Transform();  // Identity transformation at (0,0,0)
            Material = new Material(Color3.White);
            IsSelected = false;
            vertices = new List<Vector3>();
            indices = new List<int>();
        }

        /// <summary>
        /// Get the vertices of the object in local space (model coordinates).
        /// Vertices should be centered at local origin (0,0,0).
        /// </summary>
        public List<Vector3> GetVertices()
        {
            return vertices;
        }

        /// <summary>
        /// Get the indices that define the faces of the object.
        /// Each set of 3 consecutive indices forms a triangle.
        /// </summary>
        public List<int> GetIndices()
        {
            return indices;
        }

        /// <summary>
        /// Abstract method for rendering (implemented by derived classes).
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Reset object to default state: origin position, no rotation, unit scale.
        /// </summary>
        public virtual void ResetToDefault()
        {
            Transform.ResetToIdentity();
            Material = new Material(Color3.White);
            IsSelected = false;
        }

        /// <summary>
        /// Verify that all vertices are centered around the local origin.
        /// Used for debugging.
        /// </summary>
        public Vector3 GetGeometricCenter()
        {
            if (vertices.Count == 0)
                return Vector3.Zero();

            float centerX = 0, centerY = 0, centerZ = 0;
            foreach (var vertex in vertices)
            {
                centerX += vertex.X;
                centerY += vertex.Y;
                centerZ += vertex.Z;
            }
            int count = vertices.Count;
            return new Vector3(centerX / count, centerY / count, centerZ / count);
        }

        public override string ToString()
        {
            return $"{Name} - {Transform}";
        }
    }
}
