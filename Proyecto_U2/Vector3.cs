using System;

namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D vector with X, Y, and Z components.
    /// </summary>
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Get a zero vector (0, 0, 0).
        /// </summary>
        public static Vector3 Zero()
        {
            return new Vector3(0, 0, 0);
        }

        /// <summary>
        /// Get a unit vector pointing right (1, 0, 0).
        /// </summary>
        public static Vector3 Right()
        {
            return new Vector3(1, 0, 0);
        }

        /// <summary>
        /// Get a unit vector pointing up (0, 1, 0).
        /// </summary>
        public static Vector3 Up()
        {
            return new Vector3(0, 1, 0);
        }

        /// <summary>
        /// Get a unit vector pointing forward (0, 0, 1).
        /// </summary>
        public static Vector3 Forward()
        {
            return new Vector3(0, 0, 1);
        }

        public Vector3 Add(Vector3 other)
        {
            return new Vector3(X + other.X, Y + other.Y, Z + other.Z);
        }

        public Vector3 Subtract(Vector3 other)
        {
            return new Vector3(X - other.X, Y - other.Y, Z - other.Z);
        }

        public Vector3 Multiply(float scalar)
        {
            return new Vector3(X * scalar, Y * scalar, Z * scalar);
        }

        public float DotProduct(Vector3 other)
        {
            return X * other.X + Y * other.Y + Z * other.Z;
        }

        public Vector3 CrossProduct(Vector3 other)
        {
            return new Vector3(
                Y * other.Z - Z * other.Y,
                Z * other.X - X * other.Z,
                X * other.Y - Y * other.X
            );
        }

        public float Magnitude()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public Vector3 Normalize()
        {
            float mag = Magnitude();
            if (mag == 0) return new Vector3(0, 0, 0);
            return new Vector3(X / mag, Y / mag, Z / mag);
        }

        /// <summary>
        /// Calculate distance between two points.
        /// </summary>
        public float Distance(Vector3 other)
        {
            return Subtract(other).Magnitude();
        }

        public override string ToString()
        {
            return $"({X:F2}, {Y:F2}, {Z:F2})";
        }
    }
}
