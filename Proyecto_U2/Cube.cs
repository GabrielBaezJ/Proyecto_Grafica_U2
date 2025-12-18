namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D cube centered at the local origin.
    /// Vertices range from -size/2 to +size/2 in each axis.
    /// </summary>
    public class Cube : Object3D
    {
        private float size;

        public Cube(string name = "Cube", float size = 1f) : base(name)
        {
            this.size = size;
            Material = new Material(Color3.Red);
            GenerateVertices();
        }

        /// <summary>
        /// Generate cube vertices centered at local origin (0, 0, 0).
        /// Vertices extend from -size/2 to +size/2 on each axis.
        /// </summary>
        private void GenerateVertices()
        {
            float s = size / 2f;
            
            vertices.Clear();
            
            // Define 8 vertices of the cube centered at origin
            // All coordinates range from -s to +s (centered around 0,0,0)
            vertices.Add(new Vector3(-s, -s, -s)); // 0: Bottom-Front-Left
            vertices.Add(new Vector3(s, -s, -s));  // 1: Bottom-Front-Right
            vertices.Add(new Vector3(s, s, -s));   // 2: Top-Front-Right
            vertices.Add(new Vector3(-s, s, -s));  // 3: Top-Front-Left
            vertices.Add(new Vector3(-s, -s, s));  // 4: Bottom-Back-Left
            vertices.Add(new Vector3(s, -s, s));   // 5: Bottom-Back-Right
            vertices.Add(new Vector3(s, s, s));    // 6: Top-Back-Right
            vertices.Add(new Vector3(-s, s, s));   // 7: Top-Back-Left

            indices.Clear();
            
            // Front face (Z = -s)
            indices.AddRange(new[] { 0, 1, 2, 0, 2, 3 });
            
            // Back face (Z = +s)
            indices.AddRange(new[] { 4, 6, 5, 4, 7, 6 });
            
            // Left face (X = -s)
            indices.AddRange(new[] { 0, 3, 7, 0, 7, 4 });
            
            // Right face (X = +s)
            indices.AddRange(new[] { 1, 5, 6, 1, 6, 2 });
            
            // Top face (Y = +s)
            indices.AddRange(new[] { 3, 2, 6, 3, 6, 7 });
            
            // Bottom face (Y = -s)
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
