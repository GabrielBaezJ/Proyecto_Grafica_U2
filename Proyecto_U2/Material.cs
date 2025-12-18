namespace Proyecto_U2
{
    /// <summary>
    /// Represents material properties of a 3D object (color, shininess, etc.).
    /// </summary>
    public class Material
    {
        public Color3 AmbientColor { get; set; }
        public Color3 DiffuseColor { get; set; }
        public Color3 SpecularColor { get; set; }
        public float Shininess { get; set; }

        public Material(Color3 color = null, float shininess = 32f)
        {
            if (color == null)
                color = Color3.White;

            AmbientColor = new Color3(color.R * 0.3f, color.G * 0.3f, color.B * 0.3f);
            DiffuseColor = color;
            SpecularColor = Color3.White;
            Shininess = shininess;
        }

        /// <summary>
        /// Update the diffuse color and automatically adjust ambient color.
        /// </summary>
        public void SetDiffuseColor(Color3 newColor)
        {
            if (newColor == null)
                return;

            DiffuseColor = newColor;
            AmbientColor = new Color3(
                newColor.R * 0.3f,
                newColor.G * 0.3f,
                newColor.B * 0.3f
            );
        }

        /// <summary>
        /// Update all color components.
        /// </summary>
        public void SetColors(Color3 ambient, Color3 diffuse, Color3 specular)
        {
            if (ambient != null)
                AmbientColor = ambient;
            if (diffuse != null)
                DiffuseColor = diffuse;
            if (specular != null)
                SpecularColor = specular;
        }

        /// <summary>
        /// Clone the material.
        /// </summary>
        public Material Clone()
        {
            var cloned = new Material(DiffuseColor, Shininess);
            cloned.AmbientColor = AmbientColor;
            cloned.SpecularColor = SpecularColor;
            return cloned;
        }

        public static Material RedPlastic()
        {
            return new Material(Color3.Red, 32f);
        }

        public static Material GreenPlastic()
        {
            return new Material(Color3.Green, 32f);
        }

        public static Material BluePlastic()
        {
            return new Material(Color3.Blue, 32f);
        }

        public static Material YellowPlastic()
        {
            return new Material(Color3.Yellow, 32f);
        }

        public static Material Gold()
        {
            var mat = new Material();
            mat.AmbientColor = new Color3(0.24725f, 0.1995f, 0.0745f);
            mat.DiffuseColor = new Color3(0.75164f, 0.60648f, 0.22648f);
            mat.SpecularColor = new Color3(0.628281f, 0.555802f, 0.366065f);
            mat.Shininess = 51.2f;
            return mat;
        }

        public static Material Silver()
        {
            var mat = new Material();
            mat.AmbientColor = new Color3(0.19225f, 0.19225f, 0.19225f);
            mat.DiffuseColor = new Color3(0.50754f, 0.50754f, 0.50754f);
            mat.SpecularColor = new Color3(0.508273f, 0.508273f, 0.508273f);
            mat.Shininess = 51.2f;
            return mat;
        }
    }
}