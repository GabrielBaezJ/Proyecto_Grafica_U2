using System;

namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D transformation (position, rotation, scale).
    /// Initialization Order: Scale ? Rotation ? Translation
    /// </summary>
    public class Transform
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }  // Euler angles in degrees (X, Y, Z)
        public Vector3 Scale { get; set; }

        public Transform()
        {
            // Initialize at world origin with identity transformation
            Position = new Vector3(0f, 0f, 0f);      // Origin position
            Rotation = new Vector3(0f, 0f, 0f);      // No rotation
            Scale = new Vector3(1f, 1f, 1f);         // Identity scale
        }

        /// <summary>
        /// Create a Transform with specific initial values.
        /// </summary>
        public Transform(float posX, float posY, float posZ)
        {
            Position = new Vector3(posX, posY, posZ);
            Rotation = new Vector3(0f, 0f, 0f);
            Scale = new Vector3(1f, 1f, 1f);
        }

        /// <summary>
        /// Translates the object by the given offset.
        /// </summary>
        public void Translate(Vector3 offset)
        {
            Position = Position.Add(offset);
        }

        /// <summary>
        /// Rotates the object by the given Euler angles (in degrees).
        /// </summary>
        public void Rotate(Vector3 eulerAngles)
        {
            Rotation = Rotation.Add(eulerAngles);
            NormalizeRotation();
        }

        /// <summary>
        /// Sets rotation to specific Euler angles (in degrees).
        /// </summary>
        public void SetRotation(float x, float y, float z)
        {
            Rotation = new Vector3(x, y, z);
            NormalizeRotation();
        }

        /// <summary>
        /// Normalize rotation angles to 0-360 range.
        /// </summary>
        private void NormalizeRotation()
        {
            Rotation.X = ((Rotation.X % 360) + 360) % 360;
            Rotation.Y = ((Rotation.Y % 360) + 360) % 360;
            Rotation.Z = ((Rotation.Z % 360) + 360) % 360;
        }

        /// <summary>
        /// Scales the object by multiplying the current scale.
        /// </summary>
        public void ScaleBy(Vector3 scaleFactor)
        {
            Scale = new Vector3(
                Scale.X * scaleFactor.X,
                Scale.Y * scaleFactor.Y,
                Scale.Z * scaleFactor.Z
            );
            ClampScale();
        }

        /// <summary>
        /// Sets the scale to specific values (minimum 0.1 to prevent degeneration).
        /// </summary>
        public void SetScale(float x, float y, float z)
        {
            Scale = new Vector3(
                Math.Max(0.1f, x),
                Math.Max(0.1f, y),
                Math.Max(0.1f, z)
            );
            ClampScale();
        }

        /// <summary>
        /// Ensure scale values are within valid range [0.1, 10].
        /// </summary>
        private void ClampScale()
        {
            Scale.X = Math.Max(0.1f, Math.Min(10f, Scale.X));
            Scale.Y = Math.Max(0.1f, Math.Min(10f, Scale.Y));
            Scale.Z = Math.Max(0.1f, Math.Min(10f, Scale.Z));
        }

        /// <summary>
        /// Reset to identity transformation (origin, no rotation, unit scale).
        /// </summary>
        public void ResetToIdentity()
        {
            Position = new Vector3(0f, 0f, 0f);
            Rotation = new Vector3(0f, 0f, 0f);
            Scale = new Vector3(1f, 1f, 1f);
        }

        public override string ToString()
        {
            return $"Transform - Pos: {Position}, Rot: {Rotation}, Scale: {Scale}";
        }
    }
}
