using System;

namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D transformation (position, rotation, scale).
    /// </summary>
    public class Transform
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }  // Euler angles in degrees (X, Y, Z)
        public Vector3 Scale { get; set; }

        public Transform()
        {
            Position = new Vector3(0, 0, 0);
            Rotation = new Vector3(0, 0, 0);
            Scale = new Vector3(1, 1, 1);
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
            // Normalize angles to 0-360 range
            Rotation.X = Rotation.X % 360;
            Rotation.Y = Rotation.Y % 360;
            Rotation.Z = Rotation.Z % 360;
        }

        /// <summary>
        /// Sets rotation to specific Euler angles (in degrees).
        /// </summary>
        public void SetRotation(float x, float y, float z)
        {
            Rotation.X = x % 360;
            Rotation.Y = y % 360;
            Rotation.Z = z % 360;
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
        }

        /// <summary>
        /// Sets the scale to specific values.
        /// </summary>
        public void SetScale(float x, float y, float z)
        {
            Scale.X = Math.Max(0.1f, x);
            Scale.Y = Math.Max(0.1f, y);
            Scale.Z = Math.Max(0.1f, z);
        }

        public override string ToString()
        {
            return $"Transform - Pos: {Position}, Rot: {Rotation}, Scale: {Scale}";
        }
    }
}
