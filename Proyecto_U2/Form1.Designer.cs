namespace Proyecto_U2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // ========== PANELES PRINCIPALES ==========
        private System.Windows.Forms.Panel panelTop;           // Panel superior (toolbar/título)
        private System.Windows.Forms.Panel panelBottom;        // Panel inferior (estado)
        private System.Windows.Forms.Panel panelLeft;          // Panel izquierdo (crear/seleccionar objetos)
        private System.Windows.Forms.Panel panelRight;         // Panel derecho (propiedades)
        private System.Windows.Forms.Panel renderPanel;        // Panel central (viewport 3D)

        // ========== CONTROLES PANEL SUPERIOR ==========
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnAddCube;
        private System.Windows.Forms.Button btnAddSphere;
        private System.Windows.Forms.Button btnAddCylinder;
        private System.Windows.Forms.Button btnAddCone;
        private System.Windows.Forms.Button btnAddPyramid;
        private System.Windows.Forms.CheckBox chkShowGrid;
        private System.Windows.Forms.ToolTip toolTip1;

        // ========== CONTROLES PANEL IZQUIERDO ==========
        private System.Windows.Forms.GroupBox grpSceneObjects;
        private System.Windows.Forms.ListBox lstObjects;
        private System.Windows.Forms.Button btnDeleteObject;
        private System.Windows.Forms.Button btnDuplicateObject;
        private System.Windows.Forms.Button btnFocusObject;
        private System.Windows.Forms.Button btnResetTransform;
        private System.Windows.Forms.Button btnClearScene;

        // ========== CONTROLES PANEL DERECHO ==========
        private System.Windows.Forms.GroupBox transformGroup;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.NumericUpDown nudPosX;
        private System.Windows.Forms.NumericUpDown nudPosY;
        private System.Windows.Forms.NumericUpDown nudPosZ;
        private System.Windows.Forms.Label lblRotation;
        private System.Windows.Forms.NumericUpDown nudRotX;
        private System.Windows.Forms.NumericUpDown nudRotY;
        private System.Windows.Forms.NumericUpDown nudRotZ;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.NumericUpDown nudScaleX;
        private System.Windows.Forms.NumericUpDown nudScaleY;
        private System.Windows.Forms.NumericUpDown nudScaleZ;

        private System.Windows.Forms.GroupBox materialGroup;
        private System.Windows.Forms.Button btnColorPicker;
        private System.Windows.Forms.Label lblShininessValue;
        private System.Windows.Forms.TrackBar trkShininess;

        private System.Windows.Forms.GroupBox cameraGroup;
        private System.Windows.Forms.ComboBox cmbCameraMode;
        private System.Windows.Forms.Label lblCameraMode;
        private System.Windows.Forms.TrackBar trkOrbitalDistance;
        private System.Windows.Forms.TrackBar trkOrbitalAngleH;
        private System.Windows.Forms.TrackBar trkOrbitalAngleV;
        private System.Windows.Forms.Label lblOrbitalDistance;
        private System.Windows.Forms.Label lblOrbitalAngleH;
        private System.Windows.Forms.Label lblOrbitalAngleV;
        private System.Windows.Forms.Label lblOrbitalDistanceValue;
        private System.Windows.Forms.Label lblOrbitalAngleHValue;
        private System.Windows.Forms.Label lblOrbitalAngleVValue;
        private System.Windows.Forms.Button btnResetCamera;

        // ========== CONTROLES PANEL INFERIOR ==========
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSelectedObject;
        private System.Windows.Forms.Label lblCameraPos;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ==========================================
            // INSTANCIAR PANELES PRINCIPALES
            // ==========================================
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.renderPanel = new System.Windows.Forms.Panel();

            // ==========================================
            // INSTANCIAR CONTROLES PANEL SUPERIOR
            // ==========================================
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnAddCube = new System.Windows.Forms.Button();
            this.btnAddSphere = new System.Windows.Forms.Button();
            this.btnAddCylinder = new System.Windows.Forms.Button();
            this.btnAddCone = new System.Windows.Forms.Button();
            this.btnAddPyramid = new System.Windows.Forms.Button();
            this.chkShowGrid = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);

            // ==========================================
            // INSTANCIAR CONTROLES PANEL IZQUIERDO
            // ==========================================
            this.grpSceneObjects = new System.Windows.Forms.GroupBox();
            this.lstObjects = new System.Windows.Forms.ListBox();
            this.btnDeleteObject = new System.Windows.Forms.Button();
            this.btnDuplicateObject = new System.Windows.Forms.Button();
            this.btnFocusObject = new System.Windows.Forms.Button();
            this.btnResetTransform = new System.Windows.Forms.Button();
            this.btnClearScene = new System.Windows.Forms.Button();

            // ==========================================
            // INSTANCIAR CONTROLES PANEL DERECHO
            // ==========================================
            this.transformGroup = new System.Windows.Forms.GroupBox();
            this.lblPosition = new System.Windows.Forms.Label();
            this.nudPosX = new System.Windows.Forms.NumericUpDown();
            this.nudPosY = new System.Windows.Forms.NumericUpDown();
            this.nudPosZ = new System.Windows.Forms.NumericUpDown();
            this.lblRotation = new System.Windows.Forms.Label();
            this.nudRotX = new System.Windows.Forms.NumericUpDown();
            this.nudRotY = new System.Windows.Forms.NumericUpDown();
            this.nudRotZ = new System.Windows.Forms.NumericUpDown();
            this.lblScale = new System.Windows.Forms.Label();
            this.nudScaleX = new System.Windows.Forms.NumericUpDown();
            this.nudScaleY = new System.Windows.Forms.NumericUpDown();
            this.nudScaleZ = new System.Windows.Forms.NumericUpDown();

            this.materialGroup = new System.Windows.Forms.GroupBox();
            this.btnColorPicker = new System.Windows.Forms.Button();
            this.trkShininess = new System.Windows.Forms.TrackBar();
            this.lblShininessValue = new System.Windows.Forms.Label();

            this.cameraGroup = new System.Windows.Forms.GroupBox();
            this.lblCameraMode = new System.Windows.Forms.Label();
            this.cmbCameraMode = new System.Windows.Forms.ComboBox();
            this.lblOrbitalDistance = new System.Windows.Forms.Label();
            this.trkOrbitalDistance = new System.Windows.Forms.TrackBar();
            this.lblOrbitalDistanceValue = new System.Windows.Forms.Label();
            this.lblOrbitalAngleH = new System.Windows.Forms.Label();
            this.trkOrbitalAngleH = new System.Windows.Forms.TrackBar();
            this.lblOrbitalAngleHValue = new System.Windows.Forms.Label();
            this.lblOrbitalAngleV = new System.Windows.Forms.Label();
            this.trkOrbitalAngleV = new System.Windows.Forms.TrackBar();
            this.lblOrbitalAngleVValue = new System.Windows.Forms.Label();
            this.btnResetCamera = new System.Windows.Forms.Button();

            // ==========================================
            // INSTANCIAR CONTROLES PANEL INFERIOR
            // ==========================================
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSelectedObject = new System.Windows.Forms.Label();
            this.lblCameraPos = new System.Windows.Forms.Label();

            // ==========================================
            // SUSPENDER LAYOUT
            // ==========================================
            this.panelTop.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.grpSceneObjects.SuspendLayout();
            this.transformGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleZ)).BeginInit();
            this.materialGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkShininess)).BeginInit();
            this.cameraGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkOrbitalDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkOrbitalAngleH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkOrbitalAngleV)).BeginInit();
            this.SuspendLayout();

            // ==========================================
            // PANEL SUPERIOR (TOOLBAR)
            // ==========================================
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 50;
            this.panelTop.Name = "panelTop";
            this.panelTop.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.btnAddCube);
            this.panelTop.Controls.Add(this.btnAddSphere);
            this.panelTop.Controls.Add(this.btnAddCylinder);
            this.panelTop.Controls.Add(this.btnAddCone);
            this.panelTop.Controls.Add(this.btnAddPyramid);
            this.panelTop.Controls.Add(this.chkShowGrid);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(10, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Editor 3D";

            // btnAddCube
            this.btnAddCube.BackColor = System.Drawing.Color.FromArgb(62, 62, 66);
            this.btnAddCube.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCube.ForeColor = System.Drawing.Color.White;
            this.btnAddCube.Location = new System.Drawing.Point(150, 10);
            this.btnAddCube.Name = "btnAddCube";
            this.btnAddCube.Size = new System.Drawing.Size(70, 30);
            this.btnAddCube.Text = "Cubo";
            this.btnAddCube.Click += new System.EventHandler(this.BtnAddCube_Click);
            this.toolTip1.SetToolTip(this.btnAddCube, "Agregar un cubo a la escena");

            // btnAddSphere
            this.btnAddSphere.BackColor = System.Drawing.Color.FromArgb(62, 62, 66);
            this.btnAddSphere.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSphere.ForeColor = System.Drawing.Color.White;
            this.btnAddSphere.Location = new System.Drawing.Point(230, 10);
            this.btnAddSphere.Name = "btnAddSphere";
            this.btnAddSphere.Size = new System.Drawing.Size(70, 30);
            this.btnAddSphere.Text = "Esfera";
            this.btnAddSphere.Click += new System.EventHandler(this.BtnAddSphere_Click);
            this.toolTip1.SetToolTip(this.btnAddSphere, "Agregar una esfera a la escena");

            // btnAddCylinder
            this.btnAddCylinder.BackColor = System.Drawing.Color.FromArgb(62, 62, 66);
            this.btnAddCylinder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCylinder.ForeColor = System.Drawing.Color.White;
            this.btnAddCylinder.Location = new System.Drawing.Point(310, 10);
            this.btnAddCylinder.Name = "btnAddCylinder";
            this.btnAddCylinder.Size = new System.Drawing.Size(70, 30);
            this.btnAddCylinder.Text = "Cilindro";
            this.btnAddCylinder.Click += new System.EventHandler(this.BtnAddCylinder_Click);
            this.toolTip1.SetToolTip(this.btnAddCylinder, "Agregar un cilindro a la escena");

            // btnAddCone
            this.btnAddCone.BackColor = System.Drawing.Color.FromArgb(62, 62, 66);
            this.btnAddCone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCone.ForeColor = System.Drawing.Color.White;
            this.btnAddCone.Location = new System.Drawing.Point(390, 10);
            this.btnAddCone.Name = "btnAddCone";
            this.btnAddCone.Size = new System.Drawing.Size(70, 30);
            this.btnAddCone.Text = "Cono";
            this.btnAddCone.Click += new System.EventHandler(this.BtnAddCone_Click);
            this.toolTip1.SetToolTip(this.btnAddCone, "Agregar un cono a la escena");

            // btnAddPyramid
            this.btnAddPyramid.BackColor = System.Drawing.Color.FromArgb(62, 62, 66);
            this.btnAddPyramid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPyramid.ForeColor = System.Drawing.Color.White;
            this.btnAddPyramid.Location = new System.Drawing.Point(470, 10);
            this.btnAddPyramid.Name = "btnAddPyramid";
            this.btnAddPyramid.Size = new System.Drawing.Size(80, 30);
            this.btnAddPyramid.Text = "Pirámide";
            this.btnAddPyramid.Click += new System.EventHandler(this.BtnAddPyramid_Click);
            this.toolTip1.SetToolTip(this.btnAddPyramid, "Agregar una pirámide a la escena");

            // chkShowGrid
            this.chkShowGrid.AutoSize = true;
            this.chkShowGrid.Checked = true;
            this.chkShowGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShowGrid.ForeColor = System.Drawing.Color.White;
            this.chkShowGrid.Location = new System.Drawing.Point(570, 15);
            this.chkShowGrid.Name = "chkShowGrid";
            this.chkShowGrid.Size = new System.Drawing.Size(85, 17);
            this.chkShowGrid.Text = "Mostrar Grid";
            this.chkShowGrid.UseVisualStyleBackColor = true;
            this.chkShowGrid.CheckedChanged += new System.EventHandler(this.ChkShowGrid_CheckedChanged);

            // ==========================================
            // PANEL INFERIOR (ESTADO)
            // ==========================================
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Height = 30;
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Controls.Add(this.lblSelectedObject);
            this.panelBottom.Controls.Add(this.lblCameraPos);

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.LightGray;
            this.lblStatus.Location = new System.Drawing.Point(10, 8);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "Listo";

            // lblSelectedObject
            this.lblSelectedObject.AutoSize = true;
            this.lblSelectedObject.ForeColor = System.Drawing.Color.LightGray;
            this.lblSelectedObject.Location = new System.Drawing.Point(200, 8);
            this.lblSelectedObject.Name = "lblSelectedObject";
            this.lblSelectedObject.Text = "Objeto: Ninguno";

            // lblCameraPos
            this.lblCameraPos.AutoSize = true;
            this.lblCameraPos.ForeColor = System.Drawing.Color.LightGray;
            this.lblCameraPos.Location = new System.Drawing.Point(400, 8);
            this.lblCameraPos.Name = "lblCameraPos";
            this.lblCameraPos.Text = "Cámara: (0, 0, 5)";

            // ==========================================
            // PANEL IZQUIERDO (JERARQUÍA DE ESCENA)
            // ==========================================
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Width = 200;
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(5);
            this.panelLeft.AutoScroll = true;
            this.panelLeft.Controls.Add(this.grpSceneObjects);

            // grpSceneObjects
            this.grpSceneObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSceneObjects.ForeColor = System.Drawing.Color.White;
            this.grpSceneObjects.Name = "grpSceneObjects";
            this.grpSceneObjects.Padding = new System.Windows.Forms.Padding(5);
            this.grpSceneObjects.Text = "Objetos de la Escena";
            this.grpSceneObjects.Controls.Add(this.btnClearScene);
            this.grpSceneObjects.Controls.Add(this.btnResetTransform);
            this.grpSceneObjects.Controls.Add(this.btnFocusObject);
            this.grpSceneObjects.Controls.Add(this.btnDuplicateObject);
            this.grpSceneObjects.Controls.Add(this.btnDeleteObject);
            this.grpSceneObjects.Controls.Add(this.lstObjects);

            // lstObjects - Lista de objetos (parte superior, se ajusta al espacio disponible)
            this.lstObjects.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lstObjects.ForeColor = System.Drawing.Color.White;
            this.lstObjects.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstObjects.Location = new System.Drawing.Point(8, 20);
            this.lstObjects.Name = "lstObjects";
            this.lstObjects.Size = new System.Drawing.Size(174, 200);
            this.lstObjects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstObjects.SelectedIndexChanged += new System.EventHandler(this.LstObjects_SelectedIndexChanged);

            // btnDeleteObject
            this.btnDeleteObject.BackColor = System.Drawing.Color.FromArgb(62, 62, 66);
            this.btnDeleteObject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteObject.ForeColor = System.Drawing.Color.White;
            this.btnDeleteObject.Location = new System.Drawing.Point(8, 230);
            this.btnDeleteObject.Name = "btnDeleteObject";
            this.btnDeleteObject.Size = new System.Drawing.Size(174, 28);
            this.btnDeleteObject.Text = "🗑 Eliminar (Supr)";
            this.btnDeleteObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteObject.Click += new System.EventHandler(this.BtnDeleteObject_Click);
            this.toolTip1.SetToolTip(this.btnDeleteObject, "Eliminar el objeto seleccionado de la escena (Supr)");

            // btnDuplicateObject
            this.btnDuplicateObject.BackColor = System.Drawing.Color.FromArgb(62, 62, 66);
            this.btnDuplicateObject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuplicateObject.ForeColor = System.Drawing.Color.White;
            this.btnDuplicateObject.Location = new System.Drawing.Point(8, 263);
            this.btnDuplicateObject.Name = "btnDuplicateObject";
            this.btnDuplicateObject.Size = new System.Drawing.Size(174, 28);
            this.btnDuplicateObject.Text = "📋 Duplicar (Ctrl+D)";
            this.btnDuplicateObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDuplicateObject.Click += new System.EventHandler(this.BtnDuplicateObject_Click);
            this.toolTip1.SetToolTip(this.btnDuplicateObject, "Crear una copia del objeto seleccionado (Ctrl+D)");

            // btnFocusObject
            this.btnFocusObject.BackColor = System.Drawing.Color.FromArgb(62, 62, 66);
            this.btnFocusObject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFocusObject.ForeColor = System.Drawing.Color.White;
            this.btnFocusObject.Location = new System.Drawing.Point(8, 296);
            this.btnFocusObject.Name = "btnFocusObject";
            this.btnFocusObject.Size = new System.Drawing.Size(174, 28);
            this.btnFocusObject.Text = "🎯 Centrar Cámara (F)";
            this.btnFocusObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFocusObject.Click += new System.EventHandler(this.BtnFocusObject_Click);
            this.toolTip1.SetToolTip(this.btnFocusObject, "Centrar la cámara en el objeto seleccionado (F)");

            // btnResetTransform
            this.btnResetTransform.BackColor = System.Drawing.Color.FromArgb(62, 62, 66);
            this.btnResetTransform.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetTransform.ForeColor = System.Drawing.Color.White;
            this.btnResetTransform.Location = new System.Drawing.Point(8, 329);
            this.btnResetTransform.Name = "btnResetTransform";
            this.btnResetTransform.Size = new System.Drawing.Size(174, 28);
            this.btnResetTransform.Text = "↺ Reset Transform";
            this.btnResetTransform.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetTransform.Click += new System.EventHandler(this.BtnResetTransform_Click);
            this.toolTip1.SetToolTip(this.btnResetTransform, "Restaurar posición, rotación y escala del objeto (Ctrl+R)");

            // btnClearScene
            this.btnClearScene.BackColor = System.Drawing.Color.FromArgb(120, 50, 50);
            this.btnClearScene.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearScene.ForeColor = System.Drawing.Color.White;
            this.btnClearScene.Location = new System.Drawing.Point(8, 367);
            this.btnClearScene.Name = "btnClearScene";
            this.btnClearScene.Size = new System.Drawing.Size(174, 28);
            this.btnClearScene.Text = "🧹 Limpiar Escena";
            this.btnClearScene.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearScene.Click += new System.EventHandler(this.BtnClearScene_Click);
            this.toolTip1.SetToolTip(this.btnClearScene, "Eliminar todos los objetos de la escena");

            // ==========================================
            // PANEL DERECHO (PROPIEDADES)
            // ==========================================
            this.panelRight.AutoScroll = true;
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Width = 280;
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(5);
            this.panelRight.Controls.Add(this.transformGroup);
            this.panelRight.Controls.Add(this.materialGroup);
            this.panelRight.Controls.Add(this.cameraGroup);

            // ==========================================
            // GRUPO TRANSFORMACIÓN
            // ==========================================
            this.transformGroup.ForeColor = System.Drawing.Color.White;
            this.transformGroup.Location = new System.Drawing.Point(5, 5);
            this.transformGroup.Name = "transformGroup";
            this.transformGroup.Size = new System.Drawing.Size(260, 145);
            this.transformGroup.TabStop = false;
            this.transformGroup.Text = "Transformación";
            this.transformGroup.Controls.Add(this.lblPosition);
            this.transformGroup.Controls.Add(this.nudPosX);
            this.transformGroup.Controls.Add(this.nudPosY);
            this.transformGroup.Controls.Add(this.nudPosZ);
            this.transformGroup.Controls.Add(this.lblRotation);
            this.transformGroup.Controls.Add(this.nudRotX);
            this.transformGroup.Controls.Add(this.nudRotY);
            this.transformGroup.Controls.Add(this.nudRotZ);
            this.transformGroup.Controls.Add(this.lblScale);
            this.transformGroup.Controls.Add(this.nudScaleX);
            this.transformGroup.Controls.Add(this.nudScaleY);
            this.transformGroup.Controls.Add(this.nudScaleZ);

            // lblPosition
            this.lblPosition.Location = new System.Drawing.Point(10, 22);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(55, 20);
            this.lblPosition.Text = "Posición:";

            // nudPosX, nudPosY, nudPosZ
            this.nudPosX.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.nudPosX.ForeColor = System.Drawing.Color.White;
            this.nudPosX.Location = new System.Drawing.Point(70, 20);
            this.nudPosX.Minimum = new decimal(-100);
            this.nudPosX.Maximum = new decimal(100);
            this.nudPosX.DecimalPlaces = 1;
            this.nudPosX.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.nudPosX.Name = "nudPosX";
            this.nudPosX.Size = new System.Drawing.Size(55, 20);
            this.nudPosX.ValueChanged += new System.EventHandler(this.Transform_ValueChanged);

            this.nudPosY.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.nudPosY.ForeColor = System.Drawing.Color.White;
            this.nudPosY.Location = new System.Drawing.Point(135, 20);
            this.nudPosY.Minimum = new decimal(-100);
            this.nudPosY.Maximum = new decimal(100);
            this.nudPosY.DecimalPlaces = 1;
            this.nudPosY.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.nudPosY.Name = "nudPosY";
            this.nudPosY.Size = new System.Drawing.Size(55, 20);
            this.nudPosY.ValueChanged += new System.EventHandler(this.Transform_ValueChanged);

            this.nudPosZ.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.nudPosZ.ForeColor = System.Drawing.Color.White;
            this.nudPosZ.Location = new System.Drawing.Point(200, 20);
            this.nudPosZ.Minimum = new decimal(-100);
            this.nudPosZ.Maximum = new decimal(100);
            this.nudPosZ.DecimalPlaces = 1;
            this.nudPosZ.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.nudPosZ.Name = "nudPosZ";
            this.nudPosZ.Size = new System.Drawing.Size(55, 20);
            this.nudPosZ.ValueChanged += new System.EventHandler(this.Transform_ValueChanged);

            // lblRotation
            this.lblRotation.Location = new System.Drawing.Point(10, 57);
            this.lblRotation.Name = "lblRotation";
            this.lblRotation.Size = new System.Drawing.Size(55, 20);
            this.lblRotation.Text = "Rotación:";

            // nudRotX, nudRotY, nudRotZ
            this.nudRotX.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.nudRotX.ForeColor = System.Drawing.Color.White;
            this.nudRotX.Location = new System.Drawing.Point(70, 55);
            this.nudRotX.Minimum = new decimal(-360);
            this.nudRotX.Maximum = new decimal(360);
            this.nudRotX.Name = "nudRotX";
            this.nudRotX.Size = new System.Drawing.Size(55, 20);
            this.nudRotX.ValueChanged += new System.EventHandler(this.Transform_ValueChanged);

            this.nudRotY.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.nudRotY.ForeColor = System.Drawing.Color.White;
            this.nudRotY.Location = new System.Drawing.Point(135, 55);
            this.nudRotY.Minimum = new decimal(-360);
            this.nudRotY.Maximum = new decimal(360);
            this.nudRotY.Name = "nudRotY";
            this.nudRotY.Size = new System.Drawing.Size(55, 20);
            this.nudRotY.ValueChanged += new System.EventHandler(this.Transform_ValueChanged);

            this.nudRotZ.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.nudRotZ.ForeColor = System.Drawing.Color.White;
            this.nudRotZ.Location = new System.Drawing.Point(200, 55);
            this.nudRotZ.Minimum = new decimal(-360);
            this.nudRotZ.Maximum = new decimal(360);
            this.nudRotZ.Name = "nudRotZ";
            this.nudRotZ.Size = new System.Drawing.Size(55, 20);
            this.nudRotZ.ValueChanged += new System.EventHandler(this.Transform_ValueChanged);

            // lblScale
            this.lblScale.Location = new System.Drawing.Point(10, 92);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(55, 20);
            this.lblScale.Text = "Escala:";

            // nudScaleX, nudScaleY, nudScaleZ
            this.nudScaleX.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.nudScaleX.ForeColor = System.Drawing.Color.White;
            this.nudScaleX.Location = new System.Drawing.Point(70, 90);
            this.nudScaleX.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            this.nudScaleX.Maximum = new decimal(10);
            this.nudScaleX.DecimalPlaces = 1;
            this.nudScaleX.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.nudScaleX.Value = new decimal(1);
            this.nudScaleX.Name = "nudScaleX";
            this.nudScaleX.Size = new System.Drawing.Size(55, 20);
            this.nudScaleX.ValueChanged += new System.EventHandler(this.Transform_ValueChanged);

            this.nudScaleY.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.nudScaleY.ForeColor = System.Drawing.Color.White;
            this.nudScaleY.Location = new System.Drawing.Point(135, 90);
            this.nudScaleY.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            this.nudScaleY.Maximum = new decimal(10);
            this.nudScaleY.DecimalPlaces = 1;
            this.nudScaleY.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.nudScaleY.Value = new decimal(1);
            this.nudScaleY.Name = "nudScaleY";
            this.nudScaleY.Size = new System.Drawing.Size(55, 20);
            this.nudScaleY.ValueChanged += new System.EventHandler(this.Transform_ValueChanged);

            this.nudScaleZ.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.nudScaleZ.ForeColor = System.Drawing.Color.White;
            this.nudScaleZ.Location = new System.Drawing.Point(200, 90);
            this.nudScaleZ.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            this.nudScaleZ.Maximum = new decimal(10);
            this.nudScaleZ.DecimalPlaces = 1;
            this.nudScaleZ.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.nudScaleZ.Value = new decimal(1);
            this.nudScaleZ.Name = "nudScaleZ";
            this.nudScaleZ.Size = new System.Drawing.Size(55, 20);
            this.nudScaleZ.ValueChanged += new System.EventHandler(this.Transform_ValueChanged);

            // ==========================================
            // GRUPO MATERIAL
            // ==========================================
            this.materialGroup.ForeColor = System.Drawing.Color.White;
            this.materialGroup.Location = new System.Drawing.Point(5, 155);
            this.materialGroup.Name = "materialGroup";
            this.materialGroup.Size = new System.Drawing.Size(260, 100);
            this.materialGroup.TabStop = false;
            this.materialGroup.Text = "Material";
            this.materialGroup.Controls.Add(this.btnColorPicker);
            this.materialGroup.Controls.Add(this.trkShininess);
            this.materialGroup.Controls.Add(this.lblShininessValue);

            // btnColorPicker
            this.btnColorPicker.BackColor = System.Drawing.Color.FromArgb(62, 62, 66);
            this.btnColorPicker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColorPicker.ForeColor = System.Drawing.Color.White;
            this.btnColorPicker.Location = new System.Drawing.Point(10, 25);
            this.btnColorPicker.Name = "btnColorPicker";
            this.btnColorPicker.Size = new System.Drawing.Size(100, 28);
            this.btnColorPicker.Text = "Elegir Color";
            this.btnColorPicker.Click += new System.EventHandler(this.BtnColorPicker_Click);

            // lblShininessValue
            this.lblShininessValue.Location = new System.Drawing.Point(10, 65);
            this.lblShininessValue.Name = "lblShininessValue";
            this.lblShininessValue.Size = new System.Drawing.Size(70, 20);
            this.lblShininessValue.Text = "Brillo: 32";

            // trkShininess
            this.trkShininess.Location = new System.Drawing.Point(80, 60);
            this.trkShininess.Maximum = 128;
            this.trkShininess.Minimum = 1;
            this.trkShininess.Name = "trkShininess";
            this.trkShininess.Size = new System.Drawing.Size(170, 45);
            this.trkShininess.Value = 32;
            this.trkShininess.ValueChanged += new System.EventHandler(this.TrkShininess_ValueChanged);

            // ==========================================
            // GRUPO CÁMARA
            // ==========================================
            this.cameraGroup.ForeColor = System.Drawing.Color.White;
            this.cameraGroup.Location = new System.Drawing.Point(5, 260);
            this.cameraGroup.Name = "cameraGroup";
            this.cameraGroup.Size = new System.Drawing.Size(260, 310);
            this.cameraGroup.TabStop = false;
            this.cameraGroup.Text = "Cámara Orbital";
            this.cameraGroup.Controls.Add(this.lblCameraMode);
            this.cameraGroup.Controls.Add(this.cmbCameraMode);
            this.cameraGroup.Controls.Add(this.lblOrbitalDistance);
            this.cameraGroup.Controls.Add(this.trkOrbitalDistance);
            this.cameraGroup.Controls.Add(this.lblOrbitalDistanceValue);
            this.cameraGroup.Controls.Add(this.lblOrbitalAngleH);
            this.cameraGroup.Controls.Add(this.trkOrbitalAngleH);
            this.cameraGroup.Controls.Add(this.lblOrbitalAngleHValue);
            this.cameraGroup.Controls.Add(this.lblOrbitalAngleV);
            this.cameraGroup.Controls.Add(this.trkOrbitalAngleV);
            this.cameraGroup.Controls.Add(this.lblOrbitalAngleVValue);
            this.cameraGroup.Controls.Add(this.btnResetCamera);

            // lblCameraMode
            this.lblCameraMode.Location = new System.Drawing.Point(10, 25);
            this.lblCameraMode.Name = "lblCameraMode";
            this.lblCameraMode.Size = new System.Drawing.Size(45, 20);
            this.lblCameraMode.Text = "Modo:";

            // cmbCameraMode
            this.cmbCameraMode.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.cmbCameraMode.ForeColor = System.Drawing.Color.White;
            this.cmbCameraMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCameraMode.Items.AddRange(new object[] { "Orbital", "Libre", "Fija" });
            this.cmbCameraMode.Location = new System.Drawing.Point(60, 22);
            this.cmbCameraMode.Name = "cmbCameraMode";
            this.cmbCameraMode.Size = new System.Drawing.Size(190, 21);
            this.cmbCameraMode.SelectedIndexChanged += new System.EventHandler(this.CmbCameraMode_SelectedIndexChanged);

            // lblOrbitalDistance
            this.lblOrbitalDistance.Location = new System.Drawing.Point(10, 55);
            this.lblOrbitalDistance.Name = "lblOrbitalDistance";
            this.lblOrbitalDistance.Size = new System.Drawing.Size(55, 20);
            this.lblOrbitalDistance.Text = "Distancia:";

            // trkOrbitalDistance
            this.trkOrbitalDistance.Location = new System.Drawing.Point(10, 75);
            this.trkOrbitalDistance.Minimum = 2;
            this.trkOrbitalDistance.Maximum = 50;
            this.trkOrbitalDistance.Name = "trkOrbitalDistance";
            this.trkOrbitalDistance.Size = new System.Drawing.Size(200, 45);
            this.trkOrbitalDistance.TickFrequency = 5;
            this.trkOrbitalDistance.Value = 8;
            this.trkOrbitalDistance.Scroll += new System.EventHandler(this.TrkOrbitalDistance_Scroll);

            // lblOrbitalDistanceValue
            this.lblOrbitalDistanceValue.Location = new System.Drawing.Point(215, 75);
            this.lblOrbitalDistanceValue.Name = "lblOrbitalDistanceValue";
            this.lblOrbitalDistanceValue.Size = new System.Drawing.Size(40, 20);
            this.lblOrbitalDistanceValue.Text = "8.0";

            // lblOrbitalAngleH
            this.lblOrbitalAngleH.Location = new System.Drawing.Point(10, 120);
            this.lblOrbitalAngleH.Name = "lblOrbitalAngleH";
            this.lblOrbitalAngleH.Size = new System.Drawing.Size(80, 20);
            this.lblOrbitalAngleH.Text = "Rotación H:";

            // trkOrbitalAngleH
            this.trkOrbitalAngleH.Location = new System.Drawing.Point(10, 140);
            this.trkOrbitalAngleH.Minimum = 0;
            this.trkOrbitalAngleH.Maximum = 360;
            this.trkOrbitalAngleH.Name = "trkOrbitalAngleH";
            this.trkOrbitalAngleH.Size = new System.Drawing.Size(200, 45);
            this.trkOrbitalAngleH.TickFrequency = 45;
            this.trkOrbitalAngleH.Value = 45;
            this.trkOrbitalAngleH.Scroll += new System.EventHandler(this.TrkOrbitalAngleH_Scroll);

            // lblOrbitalAngleHValue
            this.lblOrbitalAngleHValue.Location = new System.Drawing.Point(215, 140);
            this.lblOrbitalAngleHValue.Name = "lblOrbitalAngleHValue";
            this.lblOrbitalAngleHValue.Size = new System.Drawing.Size(40, 20);
            this.lblOrbitalAngleHValue.Text = "45°";

            // lblOrbitalAngleV
            this.lblOrbitalAngleV.Location = new System.Drawing.Point(10, 185);
            this.lblOrbitalAngleV.Name = "lblOrbitalAngleV";
            this.lblOrbitalAngleV.Size = new System.Drawing.Size(80, 20);
            this.lblOrbitalAngleV.Text = "Elevación:";

            // trkOrbitalAngleV
            this.trkOrbitalAngleV.Location = new System.Drawing.Point(10, 205);
            this.trkOrbitalAngleV.Minimum = 5;
            this.trkOrbitalAngleV.Maximum = 85;
            this.trkOrbitalAngleV.Name = "trkOrbitalAngleV";
            this.trkOrbitalAngleV.Size = new System.Drawing.Size(200, 45);
            this.trkOrbitalAngleV.TickFrequency = 10;
            this.trkOrbitalAngleV.Value = 30;
            this.trkOrbitalAngleV.Scroll += new System.EventHandler(this.TrkOrbitalAngleV_Scroll);

            // lblOrbitalAngleVValue
            this.lblOrbitalAngleVValue.Location = new System.Drawing.Point(215, 205);
            this.lblOrbitalAngleVValue.Name = "lblOrbitalAngleVValue";
            this.lblOrbitalAngleVValue.Size = new System.Drawing.Size(40, 20);
            this.lblOrbitalAngleVValue.Text = "30°";

            // btnResetCamera
            this.btnResetCamera.BackColor = System.Drawing.Color.FromArgb(62, 62, 66);
            this.btnResetCamera.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetCamera.ForeColor = System.Drawing.Color.White;
            this.btnResetCamera.Location = new System.Drawing.Point(10, 260);
            this.btnResetCamera.Name = "btnResetCamera";
            this.btnResetCamera.Size = new System.Drawing.Size(240, 30);
            this.btnResetCamera.Text = "Reiniciar Cámara";
            this.btnResetCamera.Click += new System.EventHandler(this.BtnResetCamera_Click);

            // ==========================================
            // PANEL CENTRAL (VIEWPORT 3D)
            // ==========================================
            this.renderPanel.BackColor = System.Drawing.Color.Black;
            this.renderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderPanel.Name = "renderPanel";
            this.renderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RenderPanel_MouseMove);
            this.renderPanel.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.RenderPanel_MouseWheel);

            // ==========================================
            // CONFIGURACIÓN DEL FORM
            // ==========================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "Form1";
            this.Text = "Editor 3D - Proyecto Gráfica U2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            // IMPORTANTE: El orden de agregar controles con Dock afecta el layout
            // Primero los paneles con Dock específico, luego el Fill
            this.Controls.Add(this.renderPanel);      // Fill (debe agregarse primero para que ocupe el espacio restante)
            this.Controls.Add(this.panelRight);       // Right
            this.Controls.Add(this.panelLeft);        // Left
            this.Controls.Add(this.panelBottom);      // Bottom
            this.Controls.Add(this.panelTop);         // Top

            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);

            // ==========================================
            // REANUDAR LAYOUT
            // ==========================================
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.grpSceneObjects.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.transformGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPosZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleZ)).EndInit();
            this.materialGroup.ResumeLayout(false);
            this.materialGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkShininess)).EndInit();
            this.cameraGroup.ResumeLayout(false);
            this.cameraGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkOrbitalDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkOrbitalAngleH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkOrbitalAngleV)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}

