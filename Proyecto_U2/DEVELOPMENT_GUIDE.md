# Guía de Desarrollo y Extensión

## Cómo Compilar y Ejecutar

### Requisitos Previos
- Visual Studio 2015 o posterior
- .NET Framework 4.7.2
- C# 7.3

### Compilación

**Desde Visual Studio:**
1. Abre `Proyecto_U2.sln`
2. Presiona `Ctrl + Shift + B` para compilar
3. O usa `Build > Build Solution`

**Desde Línea de Comandos:**
```bash
cd Proyecto_U2
msbuild Proyecto_U2.csproj
```

### Ejecución

**Desde Visual Studio:**
- Presiona `F5` para ejecutar con debug
- O `Ctrl + F5` para ejecutar sin debug

**Desde Ejecutable:**
```bash
Proyecto_U2\bin\Debug\Proyecto_U2.exe
```

## Estructura de Carpetas

```
Proyecto_U2/
??? bin/                          # Archivos compilados
?   ??? Debug/
?   ??? Release/
??? obj/                          # Archivos objeto
??? Properties/                   # Configuración de proyecto
??? *.cs                          # Código fuente
??? Proyecto_U2.csproj           # Archivo de proyecto
??? Proyecto_U2.sln              # Solución Visual Studio
```

## Extender con Nueva Forma 3D

### Paso 1: Crear Clase de Forma

```csharp
// NuevaForma.cs
using System;

namespace Proyecto_U2
{
    public class NuevaForma : Object3D
    {
        private float parametro1;
        private float parametro2;

        public NuevaForma(string name = "NuevaForma", float p1 = 1f, float p2 = 2f)
            : base(name)
        {
            this.parametro1 = p1;
            this.parametro2 = p2;
            GenerateVertices();
            Material = new Material(Color3.White);
        }

        private void GenerateVertices()
        {
            vertices.Clear();
            indices.Clear();

            // Generar vértices
            // vertices.Add(new Vector3(x, y, z));

            // Definir índices (triángulos)
            // indices.AddRange(new[] { 0, 1, 2 });  // Triángulo 0-1-2
        }

        public override void Draw()
        {
            // Implementación si es necesaria
        }
    }
}
```

### Paso 2: Agregar Botón en Form1.Designer.cs

En `InitializeComponent()`:

```csharp
private System.Windows.Forms.Button btnAddNuevaForma;

// En la sección de inicialización:
this.btnAddNuevaForma = new System.Windows.Forms.Button();
this.btnAddNuevaForma.Location = new System.Drawing.Point(90, 65);
this.btnAddNuevaForma.Size = new System.Drawing.Size(70, 30);
this.btnAddNuevaForma.Text = "Nueva Forma";
this.btnAddNuevaForma.Click += new System.EventHandler(this.BtnAddNuevaForma_Click);
this.addObjectGroup.Controls.Add(this.btnAddNuevaForma);
```

### Paso 3: Agregar Manejador de Evento en Form1.cs

```csharp
private void BtnAddNuevaForma_Click(object sender, EventArgs e)
{
    NuevaForma forma = new NuevaForma($"NuevaForma_{objectCounter++}", 1.5f, 2.0f);
    forma.Material = new Material(Color3.Cyan);
    scene.AddObject(forma);
    UpdateObjectList();
}
```

### Paso 4: Compilar y Probar

```bash
Ctrl + Shift + B  # Compilar
F5                # Ejecutar
```

## Generar Geometría Común

### Generar Malla Icosfera

Para una esfera mejor optimizada:

```csharp
private void GenerateIcosphere(int subdivisions)
{
    const float t = (1.0f + (float)Math.Sqrt(5.0f)) / 2.0f;

    // Vértices iniciales del icosaedro
    vertices.Clear();
    vertices.Add(new Vector3(-1,  t, -1).Normalize());
    vertices.Add(new Vector3( 1,  t, -1).Normalize());
    vertices.Add(new Vector3(-1,  t,  1).Normalize());
    vertices.Add(new Vector3( 1,  t,  1).Normalize());
    // ... más vértices ...

    // Procedimiento de subdivisión (opcional)
}
```

### Generar Malla de Revolución

Para formas simétricas:

```csharp
private List<Vector3> GenerateRevolution(List<Vector3> profile, int slices)
{
    List<Vector3> mesh = new List<Vector3>();
    
    for (int i = 0; i < slices; i++)
    {
        float angle = 2.0f * (float)Math.PI * i / slices;
        float cos = (float)Math.Cos(angle);
        float sin = (float)Math.Sin(angle);
        
        foreach (Vector3 point in profile)
        {
            float x = point.X * cos - point.Z * sin;
            float z = point.X * sin + point.Z * cos;
            mesh.Add(new Vector3(x, point.Y, z));
        }
    }
    
    return mesh;
}
```

## Implementar Nuevas Transformaciones

### Agregar Shear Transform

```csharp
// En Transform.cs
public void ShearXY(float shearX, float shearY)
{
    // Implementar matriz de shear
}

// En SoftwareRenderer.cs - TransformVertex()
// Aplicar después de escala y rotación
vertex = new Vector3(
    vertex.X + vertex.Y * shearX,
    vertex.Y + vertex.X * shearY,
    vertex.Z
);
```

## Mejorar Iluminación

### Implementar Normals

```csharp
// En Object3D.cs
protected List<Vector3> normals;

public List<Vector3> GetNormals()
{
    return normals;
}

private void CalculateNormals()
{
    normals.Clear();
    normals.Resize(vertices.Count, Vector3.Zero);
    
    for (int i = 0; i < indices.Count; i += 3)
    {
        Vector3 v0 = vertices[indices[i]];
        Vector3 v1 = vertices[indices[i + 1]];
        Vector3 v2 = vertices[indices[i + 2]];
        
        Vector3 edge1 = v1.Subtract(v0);
        Vector3 edge2 = v2.Subtract(v0);
        Vector3 normal = edge1.CrossProduct(edge2).Normalize();
        
        normals[indices[i]] = normals[indices[i]].Add(normal);
        normals[indices[i + 1]] = normals[indices[i + 1]].Add(normal);
        normals[indices[i + 2]] = normals[indices[i + 2]].Add(normal);
    }
    
    // Normalizar
    for (int i = 0; i < normals.Count; i++)
        normals[i] = normals[i].Normalize();
}
```

### Aplicar Iluminación Phong

```csharp
// En SoftwareRenderer.cs
private float CalculateLighting(Vector3 normal, Vector3 lightDir, Material material)
{
    float ambientStrength = 0.1f;
    float diffuseStrength = Math.Max(0, normal.DotProduct(lightDir));
    float specularStrength = 0.5f;
    
    float ambient = ambientStrength;
    float diffuse = diffuseStrength;
    float specular = specularStrength * (float)Math.Pow(Math.Max(0, diffuse), material.Shininess / 4.0f);
    
    return Math.Min(1.0f, ambient + diffuse + specular);
}
```

## Optimizar Rendimiento

### Implementar Frustum Culling

```csharp
// En SoftwareRenderer.cs
private bool IsInFrustum(Vector3 point, Camera camera)
{
    Vector3 toCamera = point.Subtract(camera.Position);
    float distance = toCamera.DotProduct(camera.LookAt.Subtract(camera.Position).Normalize());
    
    // Verificar si está frente a la cámara
    if (distance < camera.NearPlane) return false;
    if (distance > camera.FarPlane) return false;
    
    return true;
}

// En RenderObject()
if (!IsInFrustum(transformedVertices[i0], camera)) continue;
```

### Implementar Back-face Culling

```csharp
// En RenderObject()
for (int i = 0; i < indices.Count; i += 3)
{
    Vector3 v0 = transformedVertices[indices[i]];
    Vector3 v1 = transformedVertices[indices[i + 1]];
    Vector3 v2 = transformedVertices[indices[i + 2]];
    
    Vector3 edge1 = v1.Subtract(v0);
    Vector3 edge2 = v2.Subtract(v0);
    Vector3 normal = edge1.CrossProduct(edge2);
    
    // Cullear si la normal apunta hacia atrás
    Vector3 toCamera = scene.Camera.Position.Subtract(v0);
    if (normal.DotProduct(toCamera) < 0) continue;
    
    // Dibujar triángulo
}
```

## Debug y Troubleshooting

### Verificar Geometría

```csharp
// Imprimir información de forma
Debug.WriteLine($"Object: {obj.Name}");
Debug.WriteLine($"Vertices: {obj.GetVertices().Count}");
Debug.WriteLine($"Indices: {obj.GetIndices().Count}");
Debug.WriteLine($"Triangles: {obj.GetIndices().Count / 3}");
```

### Verificar Transformaciones

```csharp
// En Form1.cs - Debug
if (selectedObject != null)
{
    Debug.WriteLine(selectedObject.Transform.ToString());
    Debug.WriteLine($"Scale: {selectedObject.Transform.Scale}");
}
```

### Validar Proyección

```csharp
// En SoftwareRenderer.cs
Vector3 projected = Project(vertex, camera);
if (projected.X < 0 || projected.X >= width || 
    projected.Y < 0 || projected.Y >= height)
{
    // Fuera de pantalla
}
```

## Release Build

### Crear Ejecutable de Distribución

```bash
# Compilar en Release
msbuild Proyecto_U2.csproj /p:Configuration=Release

# El ejecutable estará en:
# Proyecto_U2\bin\Release\Proyecto_U2.exe
```

### Empaquetar para Distribución

1. Copiar:
   - Proyecto_U2.exe
   - README.md
   - USER_GUIDE.md
   - TECHNICAL_REPORT.md

2. Crear carpeta: `3DGraphicsEditor_v1.0`
3. Comprimir como ZIP

## Referencias Matemáticas

### Matrices de Rotación

```
Rotación en X (?):
[1,    0,        0    ]
[0,  cos(?), -sin(?) ]
[0,  sin(?),  cos(?) ]

Rotación en Y (?):
[ cos(?), 0, sin(?) ]
[   0,    1,   0    ]
[-sin(?), 0, cos(?) ]

Rotación en Z (?):
[ cos(?), -sin(?), 0 ]
[ sin(?),  cos(?), 0 ]
[   0,       0,    1 ]
```

### Proyección Perspectiva

```
screenX = centerX + (x / distance) * fov
screenY = centerY - (y / distance) * fov
```

### Producto Punto (Dot Product)

```
a · b = a.x*b.x + a.y*b.y + a.z*b.z
Uso: Determinar ángulo entre vectores
```

### Producto Cruz (Cross Product)

```
a × b = (a.y*b.z - a.z*b.y,
         a.z*b.x - a.x*b.z,
         a.x*b.y - a.y*b.x)
Uso: Calcular normales a superficies
```

---

**Versión**: 1.0  
**Última Actualización**: 2025
