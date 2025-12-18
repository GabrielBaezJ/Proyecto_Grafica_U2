using System;

namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D camera with orbital, free, and fixed modes.
    /// </summary>
    public class Camera
    {
        public enum CameraMode
        {
            Orbital,  // Orbits around a target (origin)
            Free,     // Free movement with look-around
            Fixed     // Fixed position and direction
        }

        public Vector3 Position { get; set; }
        public Vector3 LookAt { get; set; }
        public Vector3 Up { get; set; }
        public CameraMode Mode { get; set; }

        // Orbital camera parameters
        private float orbitalDistance;
        private float orbitalAngleH;  // Ángulo horizontal (azimuth) en grados
        private float orbitalAngleV;  // Ángulo vertical (elevation) en grados
        private Vector3 orbitalTarget;

        // Field of view
        public float FOV { get; set; }
        public float AspectRatio { get; set; }
        public float NearPlane { get; set; }
        public float FarPlane { get; set; }

        // Propiedades públicas para los controles
        public float OrbitalAngleH
        {
            get { return orbitalAngleH; }
            set
            {
                orbitalAngleH = value % 360f;
                if (Mode == CameraMode.Orbital) UpdateOrbitalCamera();
            }
        }

        public float OrbitalAngleV
        {
            get { return orbitalAngleV; }
            set
            {
                // Limitar entre 5° y 85° para evitar gimbal lock
                orbitalAngleV = Math.Max(5f, Math.Min(85f, value));
                if (Mode == CameraMode.Orbital) UpdateOrbitalCamera();
            }
        }

        public float OrbitalDistance
        {
            get { return orbitalDistance; }
            set
            {
                orbitalDistance = Math.Max(2f, Math.Min(50f, value));
                if (Mode == CameraMode.Orbital) UpdateOrbitalCamera();
            }
        }

        public Camera()
        {
            Position = new Vector3(5, 5, 5);
            LookAt = new Vector3(0, 0, 0);
            Up = new Vector3(0, 1, 0);
            Mode = CameraMode.Orbital; // Iniciar en modo orbital
            FOV = 45f;
            AspectRatio = 16f / 9f;
            NearPlane = 0.1f;
            FarPlane = 1000f;

            // Configuración orbital inicial
            orbitalDistance = 8f;
            orbitalAngleH = 45f;   // 45° horizontal
            orbitalAngleV = 30f;   // 30° vertical (vista ligeramente elevada)
            orbitalTarget = new Vector3(0, 0, 0); // Siempre mirando al origen

            UpdateOrbitalCamera();
        }

        /// <summary>
        /// Update orbital camera position based on angles and distance.
        /// Usa coordenadas esféricas: el target siempre es el origen.
        /// </summary>
        private void UpdateOrbitalCamera()
        {
            float hRad = DegToRad(orbitalAngleH);
            float vRad = DegToRad(orbitalAngleV);

            // Convertir coordenadas esféricas a cartesianas
            float x = orbitalDistance * (float)Math.Cos(vRad) * (float)Math.Sin(hRad);
            float y = orbitalDistance * (float)Math.Sin(vRad);
            float z = orbitalDistance * (float)Math.Cos(vRad) * (float)Math.Cos(hRad);

            Position = new Vector3(
                orbitalTarget.X + x,
                orbitalTarget.Y + y,
                orbitalTarget.Z + z
            );
            LookAt = orbitalTarget;
            
            // Recalcular vector Up para evitar problemas en los polos
            Up = new Vector3(0, 1, 0);
        }

        /// <summary>
        /// Rotate orbital camera around the target using mouse delta.
        /// </summary>
        public void RotateOrbital(float deltaX, float deltaY)
        {
            if (Mode != CameraMode.Orbital) return;

            // deltaX controla rotación horizontal, deltaY controla elevación
            orbitalAngleH -= deltaX * 0.5f;
            orbitalAngleV += deltaY * 0.3f;

            // Normalizar ángulo horizontal
            if (orbitalAngleH < 0) orbitalAngleH += 360f;
            if (orbitalAngleH >= 360) orbitalAngleH -= 360f;

            // Limitar ángulo vertical
            orbitalAngleV = Math.Max(5f, Math.Min(85f, orbitalAngleV));

            UpdateOrbitalCamera();
        }

        /// <summary>
        /// Zoom orbital camera in/out.
        /// </summary>
        public void ZoomOrbital(float delta)
        {
            if (Mode != CameraMode.Orbital) return;

            orbitalDistance += delta;
            orbitalDistance = Math.Max(2f, Math.Min(50f, orbitalDistance));

            UpdateOrbitalCamera();
        }

        /// <summary>
        /// Pan orbital camera (move the target). 
        /// En el nuevo diseño, el target siempre es el origen.
        /// </summary>
        public void PanOrbital(Vector3 offset)
        {
            if (Mode != CameraMode.Orbital) return;
            
            // El target ahora es fijo en el origen, no se mueve
            // Esta función se mantiene por compatibilidad pero no hace nada
        }

        /// <summary>
        /// Reset orbital camera to default view.
        /// </summary>
        public void ResetOrbital()
        {
            orbitalDistance = 8f;
            orbitalAngleH = 45f;
            orbitalAngleV = 30f;
            orbitalTarget = new Vector3(0, 0, 0);
            UpdateOrbitalCamera();
        }

        /// <summary>
        /// Move free camera.
        /// </summary>
        public void MoveFree(Vector3 direction)
        {
            if (Mode != CameraMode.Free) return;

            Position = Position.Add(direction);
            LookAt = LookAt.Add(direction);
        }

        /// <summary>
        /// Rotate free camera (first-person style).
        /// </summary>
        public void RotateFree(float yawDegrees, float pitchDegrees)
        {
            if (Mode != CameraMode.Free) return;

            Vector3 dir = LookAt.Subtract(Position);
            float distance = dir.Magnitude();
            if (distance <= 0.0001f) distance = 0.0001f;

            float yaw = (float)Math.Atan2(dir.Z, dir.X);
            float pitch = (float)Math.Asin(dir.Y / distance);

            yaw += DegToRad(yawDegrees);
            pitch += DegToRad(pitchDegrees);

            float maxPitch = (float)(Math.PI / 2.0 - 0.01);
            pitch = Math.Max(-maxPitch, Math.Min(maxPitch, pitch));

            float cosPitch = (float)Math.Cos(pitch);
            Vector3 newDir = new Vector3(
                distance * cosPitch * (float)Math.Cos(yaw),
                distance * (float)Math.Sin(pitch),
                distance * cosPitch * (float)Math.Sin(yaw)
            );

            LookAt = Position.Add(newDir);
        }

        public void SetMode(CameraMode newMode)
        {
            Mode = newMode;
            if (Mode == CameraMode.Orbital)
            {
                UpdateOrbitalCamera();
            }
        }

        public Vector3 GetOrbitalTarget()
        {
            return orbitalTarget;
        }

        public void SetOrbitalTarget(Vector3 target)
        {
            orbitalTarget = target;
            UpdateOrbitalCamera();
        }

        public float GetOrbitalDistance()
        {
            return orbitalDistance;
        }

        public void SetOrbitalDistance(float distance)
        {
            orbitalDistance = Math.Max(2f, Math.Min(50f, distance));
            UpdateOrbitalCamera();
        }

        private float DegToRad(float degrees)
        {
            return (float)(degrees * Math.PI / 180.0);
        }

        public override string ToString()
        {
            return $"Camera - Mode: {Mode}, Pos: {Position}, LookAt: {LookAt}";
        }
    }
}
