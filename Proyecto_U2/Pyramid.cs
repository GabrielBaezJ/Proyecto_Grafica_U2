namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D square pyramid centered at the local origin.
    /// Base vertices are at Y = -height/2, apex is at Y = +height/2.
    /// The square base is centered at X-Z plane.
    /// </summary>
    public class Pyramid : Object3D
    {
        private float baseSize;
        private float height;

        public Pyramid(string name = "Pyramid", float baseSize = 2f, float height = 2f)
            : base(name)
        {
            this.baseSize = baseSize;
            this.height = height;
            Material = new Material(Color3.Magenta);
            GenerateVertices();
        }

        /// <summary>
        /// Generate pyramid vertices centered at local origin (0, 0, 0).
        /// Base corners extend from -baseSize/2 to +baseSize/2 on X-Z plane at Y = -height/2.
        /// Apex is at (0, +height/2, 0).
        /// </summary>
        private void GenerateVertices()
        {
            vertices.Clear();
            indices.Clear();

            float s = baseSize / 2f;
            float h = height / 2f;

            // Base vertices (centered square at Y = -h)
            vertices.Add(new Vector3(-s, -h, -s)); // 0: Front-Left
            vertices.Add(new Vector3(s, -h, -s));  // 1: Front-Right
            vertices.Add(new Vector3(s, -h, s));   // 2: Back-Right
            vertices.Add(new Vector3(-s, -h, s));  // 3: Back-Left

            // Apex (top center)
            vertices.Add(new Vector3(0, h, 0));    // 4: Apex

            // Base face (square, counter-clockwise from below)
            indices.AddRange(new[] { 0, 2, 1, 0, 3, 2 });

            // Front face (from Y=-h to apex)
            indices.AddRange(new[] { 0, 1, 4 });

            // Right face
            indices.AddRange(new[] { 1, 2, 4 });

            // Back face
            indices.AddRange(new[] { 2, 3, 4 });

            // Left face
            indices.AddRange(new[] { 3, 0, 4 });
        }

        public override void Draw()
        {
            // Drawing logic will be implemented in the renderer
        }

        public float GetBaseSize()
        {
            return baseSize;
        }

        public float GetHeight()
        {
            return height;
        }

        public void SetBaseSize(float newSize)
        {
            baseSize = newSize;
            GenerateVertices();
        }

        public void SetHeight(float newHeight)
        {
            height = newHeight;
            GenerateVertices();
        }
    }
}
