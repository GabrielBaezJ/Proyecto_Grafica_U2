namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D cube.
    /// </summary>
    public class Cube : Object3D
    {
        private float size;

        public Cube(string name = "Cube", float size = 1f) : base(name)
        {
            this.size = size;
            GenerateVertices();
            Material = new Material(Color3.Red);
        }

        private void GenerateVertices()
        {
            float s = size / 2f;
            
            // Define vertices for a cube
            vertices.Clear();
            vertices.Add(new Vector3(-s, -s, -s)); // 0
            vertices.Add(new Vector3(s, -s, -s));  // 1
            vertices.Add(new Vector3(s, s, -s));   // 2
            vertices.Add(new Vector3(-s, s, -s));  // 3
            vertices.Add(new Vector3(-s, -s, s));  // 4
            vertices.Add(new Vector3(s, -s, s));   // 5
            vertices.Add(new Vector3(s, s, s));    // 6
            vertices.Add(new Vector3(-s, s, s));   // 7

            // Define indices (two triangles per face)
            indices.Clear();
            
            // Front face
            indices.AddRange(new[] { 0, 1, 2, 0, 2, 3 });
            // Back face
            indices.AddRange(new[] { 4, 6, 5, 4, 7, 6 });
            // Left face
            indices.AddRange(new[] { 0, 3, 7, 0, 7, 4 });
            // Right face
            indices.AddRange(new[] { 1, 5, 6, 1, 6, 2 });
            // Top face
            indices.AddRange(new[] { 3, 2, 6, 3, 6, 7 });
            // Bottom face
            indices.AddRange(new[] { 0, 4, 5, 0, 5, 1 });
        }

        public override void Draw()
        {
            // Drawing logic will be implemented in the renderer
        }

        public float GetSize()
        {
            return size;
        }

        public void SetSize(float newSize)
        {
            size = newSize;
            GenerateVertices();
        }
    }
}
