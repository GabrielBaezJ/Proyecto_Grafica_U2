using System;
using System.Collections.Generic;

namespace Proyecto_U2
{
    /// <summary>
    /// Base class for all 3D objects.
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
            Transform = new Transform();
            Material = new Material(Color3.White);
            IsSelected = false;
            vertices = new List<Vector3>();
            indices = new List<int>();
        }

        /// <summary>
        /// Get the vertices of the object.
        /// </summary>
        public List<Vector3> GetVertices()
        {
            return vertices;
        }

        /// <summary>
        /// Get the indices that define the faces of the object.
        /// </summary>
        public List<int> GetIndices()
        {
            return indices;
        }

        public abstract void Draw();

        public override string ToString()
        {
            return $"{Name} - {Transform}";
        }
    }
}
