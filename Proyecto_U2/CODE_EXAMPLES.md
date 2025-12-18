# Ejemplos de Uso y Snippets de Código

## Ejemplos de Programación

### Ejemplo 1: Crear Escena Programáticamente

```csharp
// Crear escena
Scene myScene = new Scene();

// Agregar objetos
Cube cube = new Cube("MyCube", 2.0f);
cube.Material = new Material(Color3.Red);
cube.Transform.Position = new Vector3(0, 0, 0);
myScene.AddObject(cube);

Sphere sphere = new Sphere("MySphere", 1.5f);
sphere.Material = new Material(Color3.Blue);
sphere.Transform.Position = new Vector3(3, 0, 0);
myScene.AddObject(sphere);

// Configurar cámara
myScene.Camera.SetMode(Camera.CameraMode.Orbital);
myScene.Camera.SetOrbitalDistance(15f);

// Renderizar
SoftwareRenderer renderer = new SoftwareRenderer(800, 600);
renderer.RenderScene(myScene);
```

### Ejemplo 2: Transformaciones Complejas

```csharp
Object3D obj = scene.Objects[0];

// Traslación
obj.Transform.Translate(new Vector3(1, 2, 3));

// Rotación incremental
obj.Transform.Rotate(new Vector3(10, 20, 30));

// Escala no uniforme
obj.Transform.SetScale(2, 1, 0.5f);

// Combinar transformaciones
obj.Transform.Position = new Vector3(5, 0, 5);
obj.Transform.Rotate(new Vector3(45, 45, 0));
obj.Transform.SetScale(1.5f, 1.5f, 1.5f);
```

### Ejemplo 3: Operaciones con Vectores

```csharp
// Crear vectores
Vector3 v1 = new Vector3(1, 0, 0);
Vector3 v2 = new Vector3(0, 1, 0);

// Operaciones básicas
Vector3 suma = v1.Add(v2);           // (1, 1, 0)
Vector3 resta = v1.Subtract(v2);     // (1, -1, 0)
Vector3 escala = v1.Multiply(2);     // (2, 0, 0)

// Operaciones avanzadas
float puntoDot = v1.DotProduct(v2);  // 0 (perpendiculares)
Vector3 cruz = v1.CrossProduct(v2);  // (0, 0, 1)
float magnitud = v1.Magnitude();     // 1
Vector3 normalizada = v1.Normalize();// Normalizar
```

### Ejemplo 4: Crear Material Personalizado

```csharp
// Material predefinido
Material gold = Material.Gold();

// Material personalizado
Material custom = new Material();
custom.AmbientColor = new Color3(0.2f, 0.2f, 0.2f);
custom.DiffuseColor = new Color3(1.0f, 0.5f, 0.0f);  // Naranja
custom.SpecularColor = new Color3(1.0f, 1.0f, 1.0f);
custom.Shininess = 64.0f;

obj.Material = custom;
```

### Ejemplo 5: Control de Cámara Programático

```csharp
// Modo orbital
scene.Camera.SetMode(Camera.CameraMode.Orbital);
scene.Camera.SetOrbitalTarget(new Vector3(0, 0, 0));
scene.Camera.SetOrbitalDistance(10f);
scene.Camera.RotateOrbital(45, 30);

// Zoom
scene.Camera.ZoomOrbital(-2);  // Alejar
scene.Camera.ZoomOrbital(2);   // Acercar

// Pan
Vector3 offset = new Vector3(2, 0, 0);
scene.Camera.PanOrbital(offset);
```

### Ejemplo 6: Recorrer Todos los Objetos

```csharp
// Obtener información de todos los objetos
foreach (Object3D obj in scene.Objects)
{
    Console.WriteLine($"Nombre: {obj.Name}");
    Console.WriteLine($"Posición: {obj.Transform.Position}");
    Console.WriteLine($"Rotación: {obj.Transform.Rotation}");
    Console.WriteLine($"Escala: {obj.Transform.Scale}");
    Console.WriteLine($"Vértices: {obj.GetVertices().Count}");
    Console.WriteLine("---");
}

// Transformar todos los objetos
foreach (Object3D obj in scene.Objects)
{
    obj.Transform.Translate(new Vector3(0.1f, 0, 0));
}
```

### Ejemplo 7: Crear Escena Artística

```csharp
// Configurar escena
Scene scene = new Scene();
scene.BackgroundColor = new Color3(0.1f, 0.1f, 0.15f);

// Crear composición
Pyramid pyramid = new Pyramid("CentralPyramid", 2, 3);
pyramid.Material = new Material(Color3.Red);
pyramid.Transform.Position = new Vector3(0, 0, 0);
scene.AddObject(pyramid);

// Satélites
for (int i = 0; i < 4; i++)
{
    float angle = (float)(Math.PI * 2 * i / 4);
    Sphere satellite = new Sphere($"Satellite_{i}", 0.3f);
    satellite.Material = new Material(Color3.Green);
    satellite.Transform.Position = new Vector3(
        (float)Math.Cos(angle) * 5,
        2,
        (float)Math.Sin(angle) * 5
    );
    scene.AddObject(satellite);
}

// Base
Cylinder base_obj = new Cylinder("Base", 3, 0.5f);
base_obj.Material = new Material(Color3.Blue);
base_obj.Transform.Position = new Vector3(0, -2, 0);
scene.AddObject(base_obj);
```

### Ejemplo 8: Animación Manual (Frame-by-Frame)

```csharp
// En un loop de rendering
float angle = 0;

public void AnimateFrame()
{
    angle += 2;  // Incrementar ángulo cada frame
    
    // Rotar objeto
    Object3D obj = scene.Objects[0];
    obj.Transform.SetRotation(angle, angle * 0.5f, 0);
    
    // Orbitar alrededor del origen
    float x = (float)Math.Cos(angle * Math.PI / 180) * 5;
    float z = (float)Math.Sin(angle * Math.PI / 180) * 5;
    obj.Transform.Position = new Vector3(x, 0, z);
}
```

### Ejemplo 9: Interacción Avanzada

```csharp
// Seleccionar objeto por proximidad
Object3D GetObjectAtScreenPoint(Point screenPoint, Camera camera)
{
    float minDistance = float.MaxValue;
    Object3D closest = null;
    
    foreach (Object3D obj in scene.Objects)
    {
        Vector3 screenPos = Project(obj.Transform.Position, camera);
        float distance = Math.Sqrt(
            Math.Pow(screenPos.X - screenPoint.X, 2) +
            Math.Pow(screenPos.Y - screenPoint.Y, 2)
        );
        
        if (distance < minDistance)
        {
            minDistance = distance;
            closest = obj;
        }
    }
    
    return minDistance < 50 ? closest : null;
}

// Uso
if (e.Button == MouseButtons.Left)
{
    Object3D clicked = GetObjectAtScreenPoint(e.Location, scene.Camera);
    if (clicked != null)
    {
        scene.SelectObject(clicked);
    }
}
```

### Ejemplo 10: Validación de Datos

```csharp
// Validar valores de transformación
public bool ValidateTransform(Transform transform)
{
    // Verificar escala mínima
    if (transform.Scale.X < 0.1f || transform.Scale.Y < 0.1f || 
        transform.Scale.Z < 0.1f)
        return false;
    
    // Verificar posición
    if (Math.Abs(transform.Position.X) > 1000 ||
        Math.Abs(transform.Position.Y) > 1000 ||
        Math.Abs(transform.Position.Z) > 1000)
        return false;
    
    return true;
}

// Usar
if (ValidateTransform(obj.Transform))
{
    // Proceder
}
else
{
    MessageBox.Show("Valores inválidos", "Error");
}
```

## Snippets Reutilizables

### Vector Math Helper

```csharp
public static class VectorHelpers
{
    public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        return new Vector3(
            a.X + (b.X - a.X) * t,
            a.Y + (b.Y - a.Y) * t,
            a.Z + (b.Z - a.Z) * t
        );
    }
    
    public static float Distance(Vector3 a, Vector3 b)
    {
        return a.Subtract(b).Magnitude();
    }
    
    public static Vector3 RotateAroundAxis(Vector3 point, Vector3 axis, float angle)
    {
        // Implementar rotación de Rodrigues
        axis = axis.Normalize();
        angle = (float)(angle * Math.PI / 180.0);
        
        float cos = (float)Math.Cos(angle);
        float sin = (float)Math.Sin(angle);
        
        Vector3 cross = axis.CrossProduct(point);
        Vector3 dot = axis.Multiply(axis.DotProduct(point));
        
        return point.Multiply(cos)
            .Add(cross.Multiply(sin))
            .Add(dot.Multiply(1 - cos));
    }
}
```

### Utilidades de Color

```csharp
public static class ColorHelpers
{
    public static Color3 Lerp(Color3 a, Color3 b, float t)
    {
        return new Color3(
            a.R + (b.R - a.R) * t,
            a.G + (b.G - a.G) * t,
            a.B + (b.B - a.B) * t,
            a.A + (b.A - a.A) * t
        );
    }
    
    public static Color3 RandomColor()
    {
        Random rand = new Random();
        return new Color3(
            (float)rand.NextDouble(),
            (float)rand.NextDouble(),
            (float)rand.NextDouble()
        );
    }
    
    public static Color3 HSVtoRGB(float h, float s, float v)
    {
        float c = v * s;
        float x = c * (1 - Math.Abs((h / 60) % 2 - 1));
        float m = v - c;
        
        float r, g, b;
        if (h < 60) { r = c; g = x; b = 0; }
        else if (h < 120) { r = x; g = c; b = 0; }
        else if (h < 180) { r = 0; g = c; b = x; }
        else if (h < 240) { r = 0; g = x; b = c; }
        else if (h < 300) { r = x; g = 0; b = c; }
        else { r = c; g = 0; b = x; }
        
        return new Color3(r + m, g + m, b + m);
    }
}
```

### Logging y Debug

```csharp
public static class DebugHelper
{
    public static void LogObject(Object3D obj)
    {
        Debug.WriteLine($"=== {obj.Name} ===");
        Debug.WriteLine($"Type: {obj.GetType().Name}");
        Debug.WriteLine($"Position: {obj.Transform.Position}");
        Debug.WriteLine($"Rotation: {obj.Transform.Rotation}");
        Debug.WriteLine($"Scale: {obj.Transform.Scale}");
        Debug.WriteLine($"Vertices: {obj.GetVertices().Count}");
        Debug.WriteLine($"Indices: {obj.GetIndices().Count}");
        Debug.WriteLine($"Material: {obj.Material.DiffuseColor}");
        Debug.WriteLine($"Selected: {obj.IsSelected}");
    }
    
    public static void LogScene(Scene scene)
    {
        Debug.WriteLine("=== SCENE ===");
        Debug.WriteLine($"Objects: {scene.Objects.Count}");
        Debug.WriteLine($"Camera Mode: {scene.Camera.Mode}");
        Debug.WriteLine($"Camera Position: {scene.Camera.Position}");
        foreach (Object3D obj in scene.Objects)
        {
            LogObject(obj);
        }
    }
}
```

## Casos de Uso Comunes

### Crear Patrón Grid

```csharp
public void CreateGrid(Scene scene, int gridSize, float spacing)
{
    for (int x = 0; x < gridSize; x++)
    {
        for (int z = 0; z < gridSize; z++)
        {
            Sphere sphere = new Sphere($"Sphere_{x}_{z}", 0.2f);
            sphere.Transform.Position = new Vector3(
                x * spacing - (gridSize - 1) * spacing / 2,
                0,
                z * spacing - (gridSize - 1) * spacing / 2
            );
            scene.AddObject(sphere);
        }
    }
}
```

### Crear Pirámide de Objetos

```csharp
public void CreatePyramid(Scene scene, int levels)
{
    for (int level = 0; level < levels; level++)
    {
        for (int i = 0; i < (levels - level); i++)
        {
            Cube cube = new Cube($"Cube_L{level}_I{i}", 0.8f);
            cube.Transform.Position = new Vector3(
                i - (levels - level - 1) / 2f,
                level,
                0
            );
            scene.AddObject(cube);
        }
    }
}
```

### Rotar Todos los Objetos

```csharp
public void RotateAllObjects(Scene scene, Vector3 rotation)
{
    foreach (Object3D obj in scene.Objects)
    {
        obj.Transform.Rotate(rotation);
    }
}
```

### Escalar Todos los Objetos Proporcionalmente

```csharp
public void ScaleAllObjects(Scene scene, float factor)
{
    foreach (Object3D obj in scene.Objects)
    {
        obj.Transform.ScaleBy(new Vector3(factor, factor, factor));
    }
}
```

---

**Colección de Ejemplos**: 1.0  
**Última Actualización**: 2025
