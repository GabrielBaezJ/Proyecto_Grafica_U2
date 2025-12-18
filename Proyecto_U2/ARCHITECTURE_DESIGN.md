# Documento de Diseño de Arquitectura
## 3D Graphics Interactive Tool

### 1. Visión General

El proyecto implementa un editor de gráficos 3D interactivo que permite visualizar y manipular figuras tridimensionales. La arquitectura está diseñada siguiendo principios de separación de responsabilidades y modularidad.

### 2. Componentes Principales

#### 2.1 Núcleo Matemático

**Vector3.cs**
- Representa puntos y vectores en 3D
- Operaciones: suma, resta, multiplicación escalar, producto punto, producto cruz
- Normalización y cálculo de magnitud
- Responsabilidad: Cálculos vectoriales básicos

**Color3.cs**
- Almacena componentes RGBA normalizados (0.0 - 1.0)
- Conversión con System.Drawing.Color
- Colores predefinidos (Red, Green, Blue, etc.)
- Responsabilidad: Gestión de colores

#### 2.2 Transformaciones

**Transform.cs**
- Posición: Vector3 en coordenadas mundiales
- Rotación: Ángulos de Euler (grados) en orden X, Y, Z
- Escala: Factor de escala por eje
- Métodos: Translate, Rotate, SetRotation, ScaleBy, SetScale
- Responsabilidad: Estado y operaciones de transformación

#### 2.3 Propiedades Visuales

**Material.cs**
- Componente ambiente (iluminación ambiental)
- Componente difusa (color principal)
- Componente especular (reflejos)
- Shininess (exponente de Phong)
- Materiales predefinidos: Plásticos, oro, plata
- Responsabilidad: Propiedades de apariencia

#### 2.4 Objetos 3D

**Object3D.cs** (Clase Base Abstracta)
```csharp
public abstract class Object3D
{
    public string Name { get; set; }
    public Transform Transform { get; set; }
    public Material Material { get; set; }
    public bool IsSelected { get; set; }
    
    protected List<Vector3> vertices;
    protected List<int> indices;
    
    public abstract void Draw();
}
```

Proporciona:
- Interfaz común para todas las formas
- Gestión de vértices e índices
- Propiedades de transformación y material
- Estado de selección

**Implementaciones Concretas:**

1. **Cube.cs**
   - 8 vértices, 12 triángulos (2 por cara)
   - Método: GenerateVertices() crea geometría en tiempo de diseño
   - Puede modificar tamaño con SetSize()

2. **Sphere.cs**
   - Generada mediante stacks (paralelos) y slices (meridianos)
   - Parámetros: radius, stacks=20, slices=20
   - Algoritmo: Coordenadas esféricas
   ```
   x = radius * sin(stackAngle) * cos(sliceAngle)
   y = radius * sin(stackAngle) * sin(sliceAngle)
   z = radius * cos(stackAngle)
   ```

3. **Cylinder.cs**
   - Círculos superior e inferior + caras laterales
   - Parámetros: radius, height, sides=20
   - Genera tapa superior, tapa inferior, y lado lateral

4. **Cone.cs**
   - Ápex + base circular
   - Parámetros: radius, height, sides=20
   - Triángulos desde ápex hacia perímetro base

5. **Pyramid.cs**
   - Base cuadrada regular + 4 triángulos laterales
   - Parámetros: baseSize, height
   - Total 5 vértices, 6 triángulos

#### 2.5 Sistema de Cámara

**Camera.cs**
```csharp
public enum CameraMode
{
    Orbital,  // Orbita alrededor del objetivo
    Free,     // Libre (futuro)
    Fixed     // Fija
}
```

Propiedades:
- Position: Posición actual en 3D
- LookAt: Punto de enfoque
- Up: Vector "arriba" para orientación

Modo Orbital:
```
x = distance * sin(angleX) * cos(angleY)
y = distance * cos(angleX)
z = distance * sin(angleX) * sin(angleY)
```

Métodos:
- RotateOrbital(deltaX, deltaY): Cambiar ángulos
- ZoomOrbital(delta): Cambiar distancia
- PanOrbital(offset): Mover objetivo

#### 2.6 Gestión de Escena

**Scene.cs**
- Colección de Object3D
- Referencia a Camera
- Color de fondo
- Luz ambiental
- Objeto seleccionado actual

Responsabilidades:
- Mantener lista de objetos
- Gestionar selección
- Coordinar renderización

#### 2.7 Renderización

**SoftwareRenderer.cs**

Pipeline de renderización:
```
1. Clear buffer (color de fondo)
2. Para cada objeto:
   a) Transformar vértices (escala ? rotación ? traslación)
   b) Proyectar a 2D (proyección perspectiva)
   c) Rasterizar triángulos
   d) Dibujar en pantalla
```

Métodos clave:
- `TransformVertex()`: Aplica transformaciones del objeto
- `Project()`: Proyección perspectiva 3D?2D
- `RenderObject()`: Renderiza un objeto individual
- `RenderScene()`: Renderiza toda la escena

Transformación de Vértices:
```csharp
Vector3 vertex = originalVertex;

// 1. Escala
vertex = new Vector3(
    vertex.X * transform.Scale.X,
    vertex.Y * transform.Scale.Y,
    vertex.Z * transform.Scale.Z
);

// 2. Rotación (Euler angles)
vertex = RotateX(vertex, transform.Rotation.X);
vertex = RotateY(vertex, transform.Rotation.Y);
vertex = RotateZ(vertex, transform.Rotation.Z);

// 3. Traslación
vertex = vertex.Add(transform.Position);
```

Proyección Perspectiva:
```csharp
// Construir base de cámara
Vector3 forward = camera.LookAt.Subtract(camera.Position).Normalize();
Vector3 right = forward.CrossProduct(camera.Up).Normalize();
Vector3 up = right.CrossProduct(forward);

// Proyectar
float distance = toCamera.DotProduct(forward);
float x = toCamera.DotProduct(right);
float y = toCamera.DotProduct(up);

float screenX = (width / 2f) + (x / distance) * (width / 2f);
float screenY = (height / 2f) - (y / distance) * (height / 2f);
```

#### 2.8 Interfaz de Usuario

**Form1.cs** y **Form1.Designer.cs**

Estructura:
```
SplitContainer
??? Panel Izquierdo: Viewport 3D
??? Panel Derecho: Controles
    ??? GroupBox "Add Objects"
    ??? ListBox Objetos
    ??? GroupBox "Transform"
    ?   ??? NumericUpDown Posición X, Y, Z
    ?   ??? NumericUpDown Rotación X, Y, Z
    ?   ??? NumericUpDown Escala X, Y, Z
    ??? GroupBox "Material"
    ?   ??? Button Selector Color
    ?   ??? TrackBar Shininess
    ??? GroupBox "Camera"
        ??? ComboBox Modo Cámara
```

### 3. Flujo de Datos

#### 3.1 Inicialización
```
Form1_Load()
??? new Scene()
??? new SoftwareRenderer(width, height)
??? Timer.Start() ? RenderTimer_Tick
```

#### 3.2 Loop Principal
```
RenderTimer_Tick (cada 16ms)
??? Render()
?   ??? renderer.RenderScene(scene)
?   ?   ??? Para cada objeto:
?   ?       ??? Transformar vértices
?   ?       ??? Proyectar a 2D
?   ?       ??? Rasterizar
?   ??? Graphics.DrawImage(backBuffer)
```

#### 3.3 Interacción del Usuario
```
Seleccionar Objeto
??? LstObjects_SelectedIndexChanged()
    ??? scene.SelectObject(obj)
    ??? Actualizar NumericUpDown (posición, rotación, escala)
    ??? Actualizar TrackBar (shininess)

Modificar Transformación
??? Transform_ValueChanged()
    ??? selectedObject.Transform ? actualizar

Manipular Cámara
??? RenderPanel_MouseMove()
?   ??? Si Left: RotateOrbital()
?   ??? Si Right: PanOrbital()
??? RenderPanel_MouseWheel()
    ??? ZoomOrbital()
```

### 4. Patrones de Diseño Utilizados

#### 4.1 Pattern: Model-View-Controller (MVC)
- **Model**: Scene, Object3D, Transform, Material
- **View**: SoftwareRenderer, Form1
- **Controller**: Manejadores de eventos en Form1

#### 4.2 Pattern: Strategy
- Camera modes (Orbital, Free, Fixed)
- Different shape implementations

#### 4.3 Pattern: Object Pool (potencial)
- Reutilizar objetos gráficos
- Evitar asignaciones continuas

#### 4.4 Pattern: Observer (Timer-based)
- Timer notifica cada frame
- Renderización reactiva

### 5. Características de Extensibilidad

#### 5.1 Agregar Nueva Forma
```csharp
public class Torus : Object3D
{
    public Torus(string name, float radius, float thickness) : base(name)
    {
        GenerateVertices();
        Material = new Material(Color3.White);
    }
    
    private void GenerateVertices()
    {
        // Implementar generación de geometría
    }
    
    public override void Draw() { }
}
```

Luego en Form1:
```csharp
private void BtnAddTorus_Click(object sender, EventArgs e)
{
    scene.AddObject(new Torus($"Torus_{objectCounter++}"));
    UpdateObjectList();
}
```

#### 5.2 Agregar Nueva Transformación
- En Transform.cs: Agregar método (ej: Shear, Perspective)
- En SoftwareRenderer.cs: Actualizar TransformVertex()

#### 5.3 Mejorar Iluminación
- Agregar luz direccional en Scene.cs
- Calcular normal en cada triángulo
- Aplicar modelo de iluminación (Phong, PBR)

### 6. Consideraciones de Rendimiento

#### 6.1 Cuellos de Botella
1. **Transformación de vértices**: O(n) por objeto, cada frame
2. **Proyección**: O(n) por objeto
3. **Rasterización**: O(triángulos)
4. **Dibujo en pantalla**: Overhead de Graphics

#### 6.2 Optimizaciones Posibles
1. **Vertex Caching**: Cachear vértices transformados
2. **Frustum Culling**: Descartar objetos fuera de vista
3. **Back-face Culling**: No dibujar caras traseras
4. **Level of Detail (LOD)**: Reducir detalle lejos
5. **Batching**: Dibujar múltiples triángulos en una llamada

#### 6.3 Límites Actuales
- Esfera: ~400-800 triángulos (20x20)
- Cilindro: ~120 triángulos (20 lados)
- Sin oclusión (overdraw)
- Sin frustum culling

### 7. Testing y Validación

#### 7.1 Casos de Prueba Sugeridos

**Transformaciones:**
- Posición: Mover objeto a (-10, 10, 10)
- Rotación: 45° en cada eje
- Escala: Combinar escala no uniforme

**Cámara:**
- Orbitar alrededor de objeto
- Zoom hasta límite mínimo/máximo
- Pan en diferentes ángulos

**Interacción:**
- Agregar/eliminar múltiples objetos
- Cambiar propiedades mientras se renderiza
- Cambiar modos de cámara

#### 7.2 Validación Geométrica
- Verificar que vértices sean correctos
- Comprobar índices sin errores
- Validar normales (futuro)

### 8. Conclusión

La arquitectura está diseñada para ser:
- **Modular**: Fácil agregar nuevas formas
- **Extensible**: Base para características avanzadas
- **Mantenible**: Código limpio y separación de responsabilidades
- **Educativo**: Implementa conceptos gráficos fundamentales
- **Performante**: Optimizado dentro de las limitaciones de software renderer

---

**Documento Versión**: 1.0  
**Compatible con**: .NET Framework 4.7.2  
**Lenguaje**: C# 7.3
