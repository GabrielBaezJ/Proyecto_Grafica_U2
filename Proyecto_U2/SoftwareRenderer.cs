using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Proyecto_U2
{
    /// <summary>
    /// Software 3D renderer using 2D graphics.
    /// Performs 3D to 2D projection and renders wireframe with simple shading.
    /// </summary>
    public class SoftwareRenderer
    {
        private int width;
        private int height;
        private float[] depthBuffer;
        private Bitmap backBuffer;
        private Graphics graphics;

        // ========== CONFIGURACIÓN DEL GRID =========
        private bool showGrid = true;
        private bool showAxes = true;
        private float gridSize = 20f;        // Tamaño total del grid aumentado
        private float gridSpacing = 1f;      // Espacio entre líneas
        private Color gridColor = Color.FromArgb(60, 60, 60);
        private Color gridSubColor = Color.FromArgb(45, 45, 45);  // Líneas secundarias

        public bool ShowGrid
        {
            get { return showGrid; }
            set { showGrid = value; }
        }

        public bool ShowAxes
        {
            get { return showAxes; }
            set { showAxes = value; }
        }

        public SoftwareRenderer(int width, int height)
        {
            this.width = width;
            this.height = height;
            depthBuffer = new float[width * height];
            backBuffer = new Bitmap(width, height);
            graphics = Graphics.FromImage(backBuffer);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        /// <summary>
        /// Clear the buffer with a background color.
        /// </summary>
        public void Clear(Color3 color)
        {
            graphics.Clear(color.ToSystemColor());
            for (int i = 0; i < depthBuffer.Length; i++)
            {
                depthBuffer[i] = float.MaxValue;
            }
        }

        /// <summary>
        /// Project a 3D point to 2D screen coordinates.
        /// </summary>
        private Vector3 Project(Vector3 point, Camera camera)
        {
            Vector3 toCamera = point.Subtract(camera.Position);
            Vector3 forward = camera.LookAt.Subtract(camera.Position).Normalize();
            Vector3 right = forward.CrossProduct(camera.Up).Normalize();
            Vector3 up = right.CrossProduct(forward);

            float distance = toCamera.DotProduct(forward);
            if (distance <= 0.1f)
                return new Vector3(-9999, -9999, -1); // Behind camera

            float x = toCamera.DotProduct(right);
            float y = toCamera.DotProduct(up);

            float fovFactor = 1.0f;
            float screenX = (width / 2f) + (x / distance) * (width / 2f) * fovFactor;
            float screenY = (height / 2f) - (y / distance) * (height / 2f) * fovFactor;

            return new Vector3(screenX, screenY, distance);
        }

        /// <summary>
        /// Transform a vertex using the object's transform.
        /// </summary>
        private Vector3 TransformVertex(Vector3 vertex, Transform transform)
        {
            vertex = new Vector3(
                vertex.X * transform.Scale.X,
                vertex.Y * transform.Scale.Y,
                vertex.Z * transform.Scale.Z
            );

            vertex = RotateX(vertex, transform.Rotation.X);
            vertex = RotateY(vertex, transform.Rotation.Y);
            vertex = RotateZ(vertex, transform.Rotation.Z);

            vertex = vertex.Add(transform.Position);

            return vertex;
        }

        private Vector3 RotateX(Vector3 v, float angle)
        {
            float rad = (float)(angle * Math.PI / 180.0);
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);
            return new Vector3(v.X, v.Y * cos - v.Z * sin, v.Y * sin + v.Z * cos);
        }

        private Vector3 RotateY(Vector3 v, float angle)
        {
            float rad = (float)(angle * Math.PI / 180.0);
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);
            return new Vector3(v.X * cos + v.Z * sin, v.Y, -v.X * sin + v.Z * cos);
        }

        private Vector3 RotateZ(Vector3 v, float angle)
        {
            float rad = (float)(angle * Math.PI / 180.0);
            float cos = (float)Math.Cos(rad);
            float sin = (float)Math.Sin(rad);
            return new Vector3(v.X * cos - v.Y * sin, v.X * sin + v.Y * cos, v.Z);
        }

        /// <summary>
        /// Render the ground reference grid as a fixed plane on XZ axis (Y=0).
        /// The grid is defined in world space coordinates and only affected by 
        /// perspective projection - it does NOT rotate or transform with the camera.
        /// This provides a stable spatial reference similar to Blender's grid behavior.
        /// </summary>
        private void RenderGroundGrid(Camera camera)
        {
            if (!showGrid) return;

            // El grid se define en coordenadas del mundo 3D con Y=0 (plano XZ fijo)
            // Solo se proyecta a pantalla - NO se transforma con la cámara

            // Líneas paralelas al eje Z (varían en X)
            for (float x = -gridSize; x <= gridSize; x += gridSpacing)
            {
                Color lineColor = gridColor;
                float lineWidth = 1f;

                // Cada 5 unidades, línea más visible
                if (Math.Abs(x % 5) < 0.01f && Math.Abs(x) > 0.01f)
                {
                    lineColor = Color.FromArgb(80, 80, 80);
                    lineWidth = 1f;
                }

                // No dibujar en el centro (ahí van los ejes)
                if (Math.Abs(x) > 0.01f)
                {
                    // Coordenadas fijas en el mundo: Y siempre es 0
                    Vector3 start = new Vector3(x, 0, -gridSize);
                    Vector3 end = new Vector3(x, 0, gridSize);
                    DrawLine3D(start, end, lineColor, lineWidth, camera);
                }
            }

            // Líneas paralelas al eje X (varían en Z)
            for (float z = -gridSize; z <= gridSize; z += gridSpacing)
            {
                Color lineColor = gridColor;
                float lineWidth = 1f;

                if (Math.Abs(z % 5) < 0.01f && Math.Abs(z) > 0.01f)
                {
                    lineColor = Color.FromArgb(80, 80, 80);
                    lineWidth = 1f;
                }

                if (Math.Abs(z) > 0.01f)
                {
                    // Coordenadas fijas en el mundo: Y siempre es 0
                    Vector3 start = new Vector3(-gridSize, 0, z);
                    Vector3 end = new Vector3(gridSize, 0, z);
                    DrawLine3D(start, end, lineColor, lineWidth, camera);
                }
            }
        }

        /// <summary>
        /// Render XYZ axes as fixed world reference (like Blender).
        /// X axis (red) and Z axis (blue) are on the grid plane (Y=0).
        /// Y axis (green) points upward, perpendicular to the grid.
        /// </summary>
        private void RenderAxes(Camera camera)
        {
            if (!showAxes) return;

            float axisLength = gridSize;

            // Eje X - ROJO (hacia la derecha)
            DrawLine3D(new Vector3(-axisLength, 0, 0), new Vector3(axisLength, 0, 0), 
                Color.FromArgb(200, 60, 60), 2f, camera);
            
            // Eje Y - VERDE (hacia arriba)
            DrawLine3D(new Vector3(0, 0, 0), new Vector3(0, axisLength * 0.5f, 0), 
                Color.FromArgb(60, 200, 60), 2f, camera);
            
            // Eje Z - AZUL (hacia el frente)
            DrawLine3D(new Vector3(0, 0, -axisLength), new Vector3(0, 0, axisLength), 
                Color.FromArgb(60, 60, 200), 2f, camera);

            // Dibujar indicadores de ejes en pantalla (esquina inferior izquierda)
            DrawAxisIndicator(camera);
        }

        /// <summary>
        /// Draw axis indicator in corner (like Blender)
        /// </summary>
        private void DrawAxisIndicator(Camera camera)
        {
            int indicatorSize = 40;
            int margin = 60;
            int centerX = margin;
            int centerY = height - margin;

            // Calcular direcciones de los ejes en espacio de pantalla
            Vector3 origin = new Vector3(0, 0, 0);
            Vector3 xDir = new Vector3(1, 0, 0);
            Vector3 yDir = new Vector3(0, 1, 0);
            Vector3 zDir = new Vector3(0, 0, 1);

            // Proyectar direcciones
            Vector3 forward = camera.LookAt.Subtract(camera.Position).Normalize();
            Vector3 right = forward.CrossProduct(camera.Up).Normalize();
            Vector3 up = right.CrossProduct(forward);

            // Calcular posiciones 2D de los ejes
            float xScreenX = xDir.DotProduct(right);
            float xScreenY = -xDir.DotProduct(up);
            float yScreenX = yDir.DotProduct(right);
            float yScreenY = -yDir.DotProduct(up);
            float zScreenX = zDir.DotProduct(right);
            float zScreenY = -zDir.DotProduct(up);

            using (Pen penX = new Pen(Color.FromArgb(220, 80, 80), 2))
            using (Pen penY = new Pen(Color.FromArgb(80, 220, 80), 2))
            using (Pen penZ = new Pen(Color.FromArgb(80, 80, 220), 2))
            using (Font font = new Font("Arial", 10, FontStyle.Bold))
            {
                // Eje X
                int xEndX = centerX + (int)(xScreenX * indicatorSize);
                int xEndY = centerY + (int)(xScreenY * indicatorSize);
                graphics.DrawLine(penX, centerX, centerY, xEndX, xEndY);
                graphics.DrawString("X", font, Brushes.Red, xEndX - 5, xEndY - 15);

                // Eje Y
                int yEndX = centerX + (int)(yScreenX * indicatorSize);
                int yEndY = centerY + (int)(yScreenY * indicatorSize);
                graphics.DrawLine(penY, centerX, centerY, yEndX, yEndY);
                graphics.DrawString("Y", font, Brushes.LimeGreen, yEndX - 5, yEndY - 15);

                // Eje Z
                int zEndX = centerX + (int)(zScreenX * indicatorSize);
                int zEndY = centerY + (int)(zScreenY * indicatorSize);
                graphics.DrawLine(penZ, centerX, centerY, zEndX, zEndY);
                graphics.DrawString("Z", font, Brushes.DodgerBlue, zEndX - 5, zEndY - 15);
            }
        }

        /// <summary>
        /// Draw a 3D line projected to 2D with proper near-plane clipping.
        /// Clipping is done in 3D space before projection to avoid distortion.
        /// </summary>
        private void DrawLine3D(Vector3 start, Vector3 end, Color color, float lineWidth, Camera camera)
        {
            // Calcular vectores de la cámara
            Vector3 forward = camera.LookAt.Subtract(camera.Position).Normalize();
            
            // Calcular distancia de cada punto al plano near de la cámara
            Vector3 toStart = start.Subtract(camera.Position);
            Vector3 toEnd = end.Subtract(camera.Position);
            
            float distStart = toStart.DotProduct(forward);
            float distEnd = toEnd.DotProduct(forward);
            
            float nearPlane = 0.5f; // Plano de corte cercano
            
            // Ambos puntos detrás del plano near
            if (distStart < nearPlane && distEnd < nearPlane)
                return;
            
            // Clipping en espacio 3D (antes de proyección)
            Vector3 clippedStart = start;
            Vector3 clippedEnd = end;
            
            if (distStart < nearPlane)
            {
                // Interpolar en 3D para encontrar el punto de intersección con el plano near
                float t = (nearPlane - distStart) / (distEnd - distStart);
                clippedStart = new Vector3(
                    start.X + t * (end.X - start.X),
                    start.Y + t * (end.Y - start.Y),
                    start.Z + t * (end.Z - start.Z)
                );
            }
            
            if (distEnd < nearPlane)
            {
                float t = (nearPlane - distEnd) / (distStart - distEnd);
                clippedEnd = new Vector3(
                    end.X + t * (start.X - end.X),
                    end.Y + t * (start.Y - end.Y),
                    end.Z + t * (start.Z - end.Z)
                );
            }
            
            // Ahora proyectar los puntos ya recortados
            Vector3 p1 = Project(clippedStart, camera);
            Vector3 p2 = Project(clippedEnd, camera);
            
            // Verificar que la proyección sea válida
            if (p1.Z < 0.1f || p2.Z < 0.1f)
                return;

            // Verificar límites de pantalla con margen
            int margin = 5000;
            if ((p1.X < -margin && p2.X < -margin) || (p1.X > width + margin && p2.X > width + margin))
                return;
            if ((p1.Y < -margin && p2.Y < -margin) || (p1.Y > height + margin && p2.Y > height + margin))
                return;

            try
            {
                using (Pen pen = new Pen(color, lineWidth))
                {
                    graphics.DrawLine(pen, (float)p1.X, (float)p1.Y, (float)p2.X, (float)p2.Y);
                }
            }
            catch { }
        }

        /// <summary>
        /// Render an object in the scene.
        /// </summary>
        public void RenderObject(Object3D obj, Camera camera)
        {
            List<Vector3> vertices = obj.GetVertices();
            List<int> indices = obj.GetIndices();

            if (vertices.Count == 0 || indices.Count == 0)
                return;

            List<Vector3> transformedVertices = new List<Vector3>();
            List<Vector3> projectedVertices = new List<Vector3>();

            foreach (Vector3 vertex in vertices)
            {
                Vector3 transformed = TransformVertex(vertex, obj.Transform);
                transformedVertices.Add(transformed);

                Vector3 projected = Project(transformed, camera);
                projectedVertices.Add(projected);
            }

            Color penColor = obj.Material.DiffuseColor.ToSystemColor();
            if (obj.IsSelected)
            {
                penColor = Color.FromArgb(255, 200, 0); // Amarillo/naranja para selección
            }

            using (Pen wirePen = new Pen(penColor, obj.IsSelected ? 3 : 2))
            using (Brush fillBrush = new SolidBrush(Color.FromArgb(180, penColor)))
            {
                for (int i = 0; i < indices.Count; i += 3)
                {
                    int i0 = indices[i];
                    int i1 = indices[i + 1];
                    int i2 = indices[i + 2];

                    if (i0 >= projectedVertices.Count || i1 >= projectedVertices.Count || i2 >= projectedVertices.Count)
                        continue;

                    Vector3 p0 = projectedVertices[i0];
                    Vector3 p1 = projectedVertices[i1];
                    Vector3 p2 = projectedVertices[i2];

                    if (p0.Z < 0.1f || p1.Z < 0.1f || p2.Z < 0.1f)
                        continue;

                    Point[] triangle = new Point[]
                    {
                        new Point((int)p0.X, (int)p0.Y),
                        new Point((int)p1.X, (int)p1.Y),
                        new Point((int)p2.X, (int)p2.Y)
                    };

                    try
                    {
                        graphics.FillPolygon(fillBrush, triangle);
                        graphics.DrawPolygon(wirePen, triangle);
                    }
                    catch { }
                }
            }

            // Dibujar gizmo de transformación si está seleccionado
            if (obj.IsSelected)
            {
                DrawTransformGizmo(obj.Transform.Position, camera);
            }
        }

        /// <summary>
        /// Draw transform gizmo for selected object
        /// </summary>
        private void DrawTransformGizmo(Vector3 position, Camera camera)
        {
            float gizmoSize = 1.5f;

            // Eje X del gizmo - Rojo
            DrawLine3D(position, position.Add(new Vector3(gizmoSize, 0, 0)), 
                Color.FromArgb(255, 80, 80), 3f, camera);
            
            // Eje Y del gizmo - Verde
            DrawLine3D(position, position.Add(new Vector3(0, gizmoSize, 0)), 
                Color.FromArgb(80, 255, 80), 3f, camera);
            
            // Eje Z del gizmo - Azul
            DrawLine3D(position, position.Add(new Vector3(0, 0, gizmoSize)), 
                Color.FromArgb(80, 80, 255), 3f, camera);
        }

        /// <summary>
        /// Render the entire scene.
        /// </summary>
        public void RenderScene(Scene scene)
        {
            Clear(scene.BackgroundColor);

            // 1. Grid (fondo)
            RenderGroundGrid(scene.Camera);

            // 2. Ejes principales
            RenderAxes(scene.Camera);

            // 3. Objetos de la escena
            foreach (Object3D obj in scene.Objects)
            {
                RenderObject(obj, scene.Camera);
            }
        }

        /// <summary>
        /// Get the rendered image.
        /// </summary>
        public Bitmap GetBackBuffer()
        {
            return backBuffer;
        }

        /// <summary>
        /// Resize the renderer.
        /// </summary>
        public void Resize(int newWidth, int newHeight)
        {
            if (newWidth <= 0 || newHeight <= 0) return;

            width = newWidth;
            height = newHeight;
            depthBuffer = new float[width * height];
            backBuffer?.Dispose();
            graphics?.Dispose();
            backBuffer = new Bitmap(width, height);
            graphics = Graphics.FromImage(backBuffer);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        public void SetGridProperties(float size, float spacing)
        {
            gridSize = size;
            gridSpacing = spacing;
        }

        public void Dispose()
        {
            graphics?.Dispose();
            backBuffer?.Dispose();
        }
    }
}
