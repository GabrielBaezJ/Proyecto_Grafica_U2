using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto_U2
{
    public partial class Form1 : Form
    {
        private Scene scene;
        private SoftwareRenderer renderer;
        private System.Windows.Forms.Timer renderTimer;
        private Point lastMousePosition;
        private bool isMouseDragging;
        private int objectCounter;
        private bool isUpdatingUI; // Evitar eventos recursivos

        public Form1()
        {
            InitializeComponent();
            objectCounter = 0;
            isUpdatingUI = false;
            
            // Habilitar captura de teclas
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize the scene
            scene = new Scene();

            // Initialize the renderer
            int renderWidth = renderPanel.Width > 0 ? renderPanel.Width : 800;
            int renderHeight = renderPanel.Height > 0 ? renderPanel.Height : 600;
            renderer = new SoftwareRenderer(renderWidth, renderHeight);
            scene.Camera.AspectRatio = (float)renderWidth / renderHeight;

            // Setup render timer
            renderTimer = new System.Windows.Forms.Timer();
            renderTimer.Interval = 16; // ~60 FPS
            renderTimer.Tick += RenderTimer_Tick;
            renderTimer.Start();

            // Mouse events
            renderPanel.MouseDown += RenderPanel_MouseDown;
            renderPanel.MouseUp += RenderPanel_MouseUp;

            // Configurar cámara en modo orbital por defecto
            if (scene?.Camera != null)
            {
                scene.Camera.SetMode(Camera.CameraMode.Orbital);
                scene.Camera.ResetOrbital();
                cmbCameraMode.SelectedIndex = 0; // Orbital
            }

            // Inicializar controles de cámara orbital
            InitializeCameraControls();

            // Actualizar estado inicial
            UpdateStatusBar();
        }

        private void InitializeCameraControls()
        {
            if (scene?.Camera == null) return;

            isUpdatingUI = true;

            // Configurar valores iniciales de los trackbars
            trkOrbitalDistance.Minimum = 2;
            trkOrbitalDistance.Maximum = 50;
            trkOrbitalDistance.Value = (int)scene.Camera.OrbitalDistance;
            lblOrbitalDistanceValue.Text = scene.Camera.OrbitalDistance.ToString("F1");

            trkOrbitalAngleH.Minimum = 0;
            trkOrbitalAngleH.Maximum = 360;
            trkOrbitalAngleH.Value = (int)scene.Camera.OrbitalAngleH;
            lblOrbitalAngleHValue.Text = $"{scene.Camera.OrbitalAngleH:F0}°";

            trkOrbitalAngleV.Minimum = 5;
            trkOrbitalAngleV.Maximum = 85;
            trkOrbitalAngleV.Value = (int)scene.Camera.OrbitalAngleV;
            lblOrbitalAngleVValue.Text = $"{scene.Camera.OrbitalAngleV:F0}°";

            isUpdatingUI = false;
        }

        private void UpdateCameraControlsFromCamera()
        {
            if (scene?.Camera == null || isUpdatingUI) return;

            isUpdatingUI = true;

            try
            {
                // Actualizar trackbars con los valores actuales de la cámara
                int distance = (int)Math.Max(trkOrbitalDistance.Minimum, 
                    Math.Min(trkOrbitalDistance.Maximum, scene.Camera.OrbitalDistance));
                trkOrbitalDistance.Value = distance;
                lblOrbitalDistanceValue.Text = scene.Camera.OrbitalDistance.ToString("F1");

                int angleH = (int)scene.Camera.OrbitalAngleH;
                if (angleH < 0) angleH += 360;
                angleH = Math.Max(0, Math.Min(360, angleH));
                trkOrbitalAngleH.Value = angleH;
                lblOrbitalAngleHValue.Text = $"{scene.Camera.OrbitalAngleH:F0}°";

                int angleV = (int)Math.Max(trkOrbitalAngleV.Minimum,
                    Math.Min(trkOrbitalAngleV.Maximum, scene.Camera.OrbitalAngleV));
                trkOrbitalAngleV.Value = angleV;
                lblOrbitalAngleVValue.Text = $"{scene.Camera.OrbitalAngleV:F0}°";
            }
            catch { }

            isUpdatingUI = false;
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            Render();
            UpdateStatusBar();
        }

        private void Render()
        {
            if (renderer == null || renderPanel == null) return;

            // Render the scene
            renderer.RenderScene(scene);

            // Display on the panel
            Bitmap backBuffer = renderer.GetBackBuffer();
            using (Graphics g = Graphics.FromHwnd(renderPanel.Handle))
            {
                g.DrawImage(backBuffer, 0, 0);
            }
        }

        #region Status Bar

        private void UpdateStatusBar()
        {
            if (scene == null) return;

            // Actualizar objeto seleccionado
            Object3D selected = scene.GetSelectedObject();
            if (selected != null)
            {
                lblSelectedObject.Text = $"Objeto: {selected.Name}";
            }
            else
            {
                lblSelectedObject.Text = "Objeto: Ninguno";
            }

            // Actualizar información de cámara
            if (scene.Camera != null)
            {
                if (scene.Camera.Mode == Camera.CameraMode.Orbital)
                {
                    lblCameraPos.Text = $"Orbital: Dist={scene.Camera.OrbitalDistance:F1} H={scene.Camera.OrbitalAngleH:F0}° V={scene.Camera.OrbitalAngleV:F0}°";
                }
                else
                {
                    var pos = scene.Camera.Position;
                    lblCameraPos.Text = $"Cámara: ({pos.X:F1}, {pos.Y:F1}, {pos.Z:F1})";
                }
            }

            // Actualizar estado
            lblStatus.Text = $"Objetos en escena: {scene.Objects.Count}";
        }

        #endregion

        #region Object List Management

        private void RefreshObjectList()
        {
            if (lstObjects == null || scene == null) return;

            isUpdatingUI = true;
            lstObjects.Items.Clear();
            foreach (var obj in scene.Objects)
            {
                lstObjects.Items.Add(obj.Name);
            }

            // Seleccionar el objeto actual si existe
            Object3D selected = scene.GetSelectedObject();
            if (selected != null)
            {
                int index = lstObjects.Items.IndexOf(selected.Name);
                if (index >= 0)
                    lstObjects.SelectedIndex = index;
            }
            else
            {
                // No hay objeto seleccionado, asegurar que la lista tampoco tenga selección
                lstObjects.SelectedIndex = -1;
            }
            isUpdatingUI = false;
        }

        private void LstObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingUI || scene == null) return;

            // Si no hay selección en la lista, deseleccionar el objeto en la escena
            if (lstObjects.SelectedIndex < 0)
            {
                scene.DeselectObject();
                return;
            }

            string selectedName = lstObjects.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedName))
            {
                // Buscar el objeto por nombre en la lista de objetos
                Object3D objToSelect = null;
                foreach (var obj in scene.Objects)
                {
                    if (obj.Name == selectedName)
                    {
                        objToSelect = obj;
                        break;
                    }
                }
                
                if (objToSelect != null)
                {
                    scene.SelectObject(objToSelect);
                    UpdateTransformUI();
                }
            }
        }

        private void UpdateTransformUI()
        {
            Object3D selected = scene.GetSelectedObject();
            if (selected == null) return;

            isUpdatingUI = true;
            try
            {
                nudPosX.Value = (decimal)selected.Transform.Position.X;
                nudPosY.Value = (decimal)selected.Transform.Position.Y;
                nudPosZ.Value = (decimal)selected.Transform.Position.Z;

                nudRotX.Value = (decimal)selected.Transform.Rotation.X;
                nudRotY.Value = (decimal)selected.Transform.Rotation.Y;
                nudRotZ.Value = (decimal)selected.Transform.Rotation.Z;

                nudScaleX.Value = (decimal)selected.Transform.Scale.X;
                nudScaleY.Value = (decimal)selected.Transform.Scale.Y;
                nudScaleZ.Value = (decimal)selected.Transform.Scale.Z;

                trkShininess.Value = (int)selected.Material.Shininess;
                lblShininessValue.Text = $"Brillo: {trkShininess.Value}";
            }
            catch { }
            isUpdatingUI = false;
        }

        #endregion

        #region Object Management

        private void BtnAddCube_Click(object sender, EventArgs e)
        {
            Cube cube = new Cube($"Cube_{objectCounter++}", 1f);
            cube.Material = new Material(Color3.Red);
            cube.Transform.Position = new Vector3(0, 0, 0);
            scene.AddObject(cube);
            scene.SelectObject(cube);
            RefreshObjectList();
            UpdateTransformUI();
        }

        private void BtnAddSphere_Click(object sender, EventArgs e)
        {
            Sphere sphere = new Sphere($"Sphere_{objectCounter++}", 0.5f, 20, 20);
            sphere.Material = new Material(Color3.Green);
            sphere.Transform.Position = new Vector3(0, 0, 0);
            scene.AddObject(sphere);
            scene.SelectObject(sphere);
            RefreshObjectList();
            UpdateTransformUI();
        }

        private void BtnAddCylinder_Click(object sender, EventArgs e)
        {
            Cylinder cylinder = new Cylinder($"Cylinder_{objectCounter++}", 0.5f, 1.5f, 20);
            cylinder.Material = new Material(Color3.Blue);
            cylinder.Transform.Position = new Vector3(0, 0, 0);
            scene.AddObject(cylinder);
            scene.SelectObject(cylinder);
            RefreshObjectList();
            UpdateTransformUI();
        }

        private void BtnAddCone_Click(object sender, EventArgs e)
        {
            Cone cone = new Cone($"Cone_{objectCounter++}", 0.5f, 1.5f, 20);
            cone.Material = new Material(Color3.Yellow);
            cone.Transform.Position = new Vector3(0, 0, 0);
            scene.AddObject(cone);
            scene.SelectObject(cone);
            RefreshObjectList();
            UpdateTransformUI();
        }

        private void BtnAddPyramid_Click(object sender, EventArgs e)
        {
            Pyramid pyramid = new Pyramid($"Pyramid_{objectCounter++}", 1f, 1.5f);
            pyramid.Material = new Material(Color3.Magenta);
            pyramid.Transform.Position = new Vector3(0, 0, 0);
            scene.AddObject(pyramid);
            scene.SelectObject(pyramid);
            RefreshObjectList();
            UpdateTransformUI();
        }

        private Vector3 GetRandomPosition()
        {
            Random rnd = new Random();
            return new Vector3(
                (float)(rnd.NextDouble() * 4 - 2),  // -2 a 2
                0.5f,                                 // Ligeramente elevado del suelo
                (float)(rnd.NextDouble() * 4 - 2)   // -2 a 2
            );
        }

        private void BtnDeleteObject_Click(object sender, EventArgs e)
        {
            Object3D selected = scene.GetSelectedObject();
            if (selected != null)
            {
                scene.RemoveObject(selected);
                RefreshObjectList();
            }
            else
            {
                MessageBox.Show("Seleccione un objeto primero.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDuplicateObject_Click(object sender, EventArgs e)
        {
            Object3D selected = scene.GetSelectedObject();
            if (selected == null)
            {
                MessageBox.Show("Seleccione un objeto para duplicar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Object3D duplicate = null;

            // Crear copia según el tipo de objeto
            if (selected is Cube)
            {
                duplicate = new Cube($"{selected.Name}_copia", 1f);
            }
            else if (selected is Sphere)
            {
                duplicate = new Sphere($"{selected.Name}_copia", 0.5f, 20, 20);
            }
            else if (selected is Cylinder)
            {
                duplicate = new Cylinder($"{selected.Name}_copia", 0.5f, 1.5f, 20);
            }
            else if (selected is Cone)
            {
                duplicate = new Cone($"{selected.Name}_copia", 0.5f, 1.5f, 20);
            }
            else if (selected is Pyramid)
            {
                duplicate = new Pyramid($"{selected.Name}_copia", 1f, 1.5f);
            }

            if (duplicate != null)
            {
                // Copiar transformaciones con un pequeño desplazamiento
                duplicate.Transform.Position = new Vector3(
                    selected.Transform.Position.X + 1f,
                    selected.Transform.Position.Y,
                    selected.Transform.Position.Z + 1f
                );
                duplicate.Transform.SetRotation(
                    selected.Transform.Rotation.X,
                    selected.Transform.Rotation.Y,
                    selected.Transform.Rotation.Z
                );
                duplicate.Transform.SetScale(
                    selected.Transform.Scale.X,
                    selected.Transform.Scale.Y,
                    selected.Transform.Scale.Z
                );

                // Copiar material
                duplicate.Material = new Material(selected.Material.DiffuseColor);

                scene.AddObject(duplicate);
                scene.SelectObject(duplicate);
                RefreshObjectList();
                UpdateTransformUI();
            }
        }

        private void BtnFocusObject_Click(object sender, EventArgs e)
        {
            Object3D selected = scene.GetSelectedObject();
            if (selected == null)
            {
                MessageBox.Show("Seleccione un objeto para centrar la cámara.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (scene?.Camera != null)
            {
                // Centrar la cámara orbital en el objeto seleccionado
                scene.Camera.SetOrbitalTarget(selected.Transform.Position);
                scene.Camera.SetOrbitalDistance(5f); // Distancia cercana para ver el objeto
                UpdateCameraControlsFromCamera();
            }
        }

        private void BtnResetTransform_Click(object sender, EventArgs e)
        {
            Object3D selected = scene.GetSelectedObject();
            if (selected == null)
            {
                MessageBox.Show("Seleccione un objeto para resetear sus transformaciones.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Resetear posición a origen
            selected.Transform.Position = new Vector3(0, 0, 0);
            
            // Resetear rotación
            selected.Transform.SetRotation(0, 0, 0);
            
            // Resetear escala a 1
            selected.Transform.SetScale(1, 1, 1);

            UpdateTransformUI();
        }

        private void BtnClearScene_Click(object sender, EventArgs e)
        {
            if (scene.Objects.Count == 0)
            {
                MessageBox.Show("La escena ya está vacía.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                "¿Está seguro de que desea eliminar todos los objetos de la escena?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                scene.Clear();
                RefreshObjectList();
            }
        }

        #endregion

        #region Object Selection and Transformation

        private void Transform_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdatingUI) return;

            Object3D selectedObject = scene.GetSelectedObject();
            if (selectedObject != null)
            {
                // Actualizar posición - TRASLACIÓN LIBRE EN X, Y, Z
                selectedObject.Transform.Position = new Vector3(
                    (float)nudPosX.Value,
                    (float)nudPosY.Value,
                    (float)nudPosZ.Value
                );

                // Actualizar rotación
                selectedObject.Transform.SetRotation(
                    (float)nudRotX.Value,
                    (float)nudRotY.Value,
                    (float)nudRotZ.Value
                );

                // Actualizar escala
                selectedObject.Transform.SetScale(
                    (float)nudScaleX.Value,
                    (float)nudScaleY.Value,
                    (float)nudScaleZ.Value
                );
            }
        }

        #endregion

        #region Material

        private void BtnColorPicker_Click(object sender, EventArgs e)
        {
            Object3D selectedObject = scene.GetSelectedObject();
            if (selectedObject != null)
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        System.Drawing.Color color = colorDialog.Color;
                        selectedObject.Material.DiffuseColor = Color3.FromSystemColor(color);
                        selectedObject.Material.AmbientColor = new Color3(
                            selectedObject.Material.DiffuseColor.R * 0.3f,
                            selectedObject.Material.DiffuseColor.G * 0.3f,
                            selectedObject.Material.DiffuseColor.B * 0.3f
                        );
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un objeto primero.", "Aviso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TrkShininess_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdatingUI) return;

            Object3D selectedObject = scene.GetSelectedObject();
            if (selectedObject != null)
            {
                selectedObject.Material.Shininess = trkShininess.Value;
            }
            lblShininessValue.Text = $"Brillo: {trkShininess.Value}";
        }

        #endregion

        #region Camera

        private void CmbCameraMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (scene?.Camera == null) return;
            Camera.CameraMode mode = (Camera.CameraMode)cmbCameraMode.SelectedIndex;
            scene.Camera.SetMode(mode);

            // Habilitar/deshabilitar controles según el modo
            bool isOrbital = (mode == Camera.CameraMode.Orbital);
            trkOrbitalDistance.Enabled = isOrbital;
            trkOrbitalAngleH.Enabled = isOrbital;
            trkOrbitalAngleV.Enabled = isOrbital;
            btnResetCamera.Enabled = isOrbital;

            if (isOrbital)
            {
                UpdateCameraControlsFromCamera();
            }
        }

        // Manejador para mostrar/ocultar el grid
        private void ChkShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            if (renderer != null)
            {
                renderer.ShowGrid = chkShowGrid.Checked;
            }
        }

        // Manejadores para controles orbitales
        private void TrkOrbitalDistance_Scroll(object sender, EventArgs e)
        {
            if (isUpdatingUI || scene?.Camera == null) return;

            scene.Camera.OrbitalDistance = trkOrbitalDistance.Value;
            lblOrbitalDistanceValue.Text = trkOrbitalDistance.Value.ToString("F1");
        }

        private void TrkOrbitalAngleH_Scroll(object sender, EventArgs e)
        {
            if (isUpdatingUI || scene?.Camera == null) return;

            scene.Camera.OrbitalAngleH = trkOrbitalAngleH.Value;
            lblOrbitalAngleHValue.Text = $"{trkOrbitalAngleH.Value}°";
        }

        private void TrkOrbitalAngleV_Scroll(object sender, EventArgs e)
        {
            if (isUpdatingUI || scene?.Camera == null) return;

            scene.Camera.OrbitalAngleV = trkOrbitalAngleV.Value;
            lblOrbitalAngleVValue.Text = $"{trkOrbitalAngleV.Value}°";
        }

        private void BtnResetCamera_Click(object sender, EventArgs e)
        {
            if (scene?.Camera == null) return;

            scene.Camera.ResetOrbital();
            UpdateCameraControlsFromCamera();
        }

        private void RenderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDragging = true;
            lastMousePosition = e.Location;
            renderPanel.Focus(); // Para capturar eventos de scroll
        }

        private void RenderPanel_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDragging = false;
        }

        private void RenderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDragging || scene?.Camera == null)
                return;

            int deltaX = e.X - lastMousePosition.X;
            int deltaY = e.Y - lastMousePosition.Y;

            var cam = scene.Camera;

            if (e.Button == MouseButtons.Left)
            {
                if (cam.Mode == Camera.CameraMode.Free)
                {
                    float yawDeg = deltaX * 0.2f;
                    float pitchDeg = -deltaY * 0.2f;
                    cam.RotateFree(yawDeg, pitchDeg);
                }
                else if (cam.Mode == Camera.CameraMode.Orbital)
                {
                    // Rotación orbital con mouse
                    cam.RotateOrbital(deltaX, deltaY);
                    UpdateCameraControlsFromCamera();
                }
            }
            else if (e.Button == MouseButtons.Right && cam.Mode == Camera.CameraMode.Free)
            {
                // Pan solo en modo libre
                Vector3 dir = cam.LookAt.Subtract(cam.Position).Normalize();
                Vector3 right = dir.CrossProduct(cam.Up).Normalize();
                Vector3 up = cam.Up.Normalize();

                Vector3 panOffset = right.Multiply(-deltaX * 0.01f).Add(up.Multiply(deltaY * 0.01f));
                cam.MoveFree(panOffset);
            }

            lastMousePosition = e.Location;
        }

        private void RenderPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (scene?.Camera == null) return;

            var cam = scene.Camera;
            if (cam.Mode == Camera.CameraMode.Free)
            {
                Vector3 dir = cam.LookAt.Subtract(cam.Position).Normalize();
                float zoom = e.Delta > 0 ? 0.5f : -0.5f;
                cam.MoveFree(dir.Multiply(zoom));
            }
            else if (cam.Mode == Camera.CameraMode.Orbital)
            {
                // Zoom orbital con scroll
                cam.ZoomOrbital(e.Delta > 0 ? -1f : 1f);
                UpdateCameraControlsFromCamera();
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                renderTimer?.Stop();
                renderTimer?.Dispose();
                renderer?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (renderer != null && renderPanel != null && renderPanel.Width > 0 && renderPanel.Height > 0)
            {
                renderer.Resize(renderPanel.Width, renderPanel.Height);
                if (scene != null)
                {
                    scene.Camera.AspectRatio = (float)renderPanel.Width / renderPanel.Height;
                }
            }
        }

        #region Keyboard Shortcuts

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Supr/Delete - Eliminar objeto seleccionado
            if (e.KeyCode == Keys.Delete)
            {
                BtnDeleteObject_Click(sender, e);
                e.Handled = true;
            }
            // Ctrl+D - Duplicar objeto
            else if (e.Control && e.KeyCode == Keys.D)
            {
                BtnDuplicateObject_Click(sender, e);
                e.Handled = true;
            }
            // F - Centrar cámara en objeto seleccionado
            else if (e.KeyCode == Keys.F && !e.Control && !e.Alt && !e.Shift)
            {
                BtnFocusObject_Click(sender, e);
                e.Handled = true;
            }
            // Ctrl+R - Resetear transformaciones del objeto
            else if (e.Control && e.KeyCode == Keys.R)
            {
                BtnResetTransform_Click(sender, e);
                e.Handled = true;
            }
            // Escape - Deseleccionar objeto
            else if (e.KeyCode == Keys.Escape)
            {
                if (scene != null)
                {
                    scene.DeselectObject();
                    RefreshObjectList();
                }
                e.Handled = true;
            }
        }

        #endregion
    }
}
