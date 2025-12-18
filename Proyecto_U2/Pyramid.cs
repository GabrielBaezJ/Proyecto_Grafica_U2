namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D pyramid (square base).
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
            GenerateVertices();
            Material = new Material(Color3.Magenta);
        }

        private void GenerateVertices()
        {
            vertices.Clear();
            indices.Clear();

            float s = baseSize / 2f;
            float h = height / 2f;

            // Base vertices (counter-clockwise when viewed from below)
            vertices.Add(new Vector3(-s, -h, -s)); // 0
            vertices.Add(new Vector3(s, -h, -s));  // 1
            vertices.Add(new Vector3(s, -h, s));   // 2
            vertices.Add(new Vector3(-s, -h, s));  // 3

            // Apex
            vertices.Add(new Vector3(0, h, 0));    // 4

            // Base (bottom face)
            indices.AddRange(new[] { 0, 2, 1, 0, 3, 2 });

            // Front face
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
