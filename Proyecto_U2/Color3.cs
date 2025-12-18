using System;
using System.Drawing;

namespace Proyecto_U2
{
    /// <summary>
    /// Represents a color with RGBA components (0.0 to 1.0).
    /// </summary>
    public class Color3
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }

        public Color3(float r = 1, float g = 1, float b = 1, float a = 1)
        {
            R = Math.Max(0, Math.Min(1, r));
            G = Math.Max(0, Math.Min(1, g));
            B = Math.Max(0, Math.Min(1, b));
            A = Math.Max(0, Math.Min(1, a));
        }

        public static Color3 FromSystemColor(System.Drawing.Color color)
        {
            return new Color3(
                color.R / 255f,
                color.G / 255f,
                color.B / 255f,
                color.A / 255f
            );
        }

        public System.Drawing.Color ToSystemColor()
        {
            return System.Drawing.Color.FromArgb(
                (int)(A * 255),
                (int)(R * 255),
                (int)(G * 255),
                (int)(B * 255)
            );
        }

        public static Color3 Red => new Color3(1, 0, 0);
        public static Color3 Green => new Color3(0, 1, 0);
        public static Color3 Blue => new Color3(0, 0, 1);
        public static Color3 White => new Color3(1, 1, 1);
        public static Color3 Black => new Color3(0, 0, 0);
        public static Color3 Yellow => new Color3(1, 1, 0);
        public static Color3 Cyan => new Color3(0, 1, 1);
        public static Color3 Magenta => new Color3(1, 0, 1);
        public static Color3 Gray => new Color3(0.5f, 0.5f, 0.5f);

        public override string ToString()
        {
            return $"RGBA({R:F2}, {G:F2}, {B:F2}, {A:F2})";
        }
    }
}
