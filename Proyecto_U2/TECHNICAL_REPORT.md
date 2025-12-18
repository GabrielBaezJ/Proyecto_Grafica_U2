# Herramienta Interactiva de Computación Gráfica 3D
## Informe Técnico

### 1. Descripción General del Proyecto

Este proyecto implementa una herramienta interactiva de computación gráfica 3D que permite crear, visualizar y manipular figuras tridimensionales. La aplicación está desarrollada en C# utilizando Windows Forms y ofrece un entorno gráfico propio sin depender de librerías externas especializadas de gráficos.

### 2. Características Principales

#### 2.1 Figuras 3D Soportadas
- **Cubo (Cube)**: Prisma rectangular con 8 vértices y 12 triángulos
- **Esfera (Sphere)**: Generada mediante stacks y slices para crear una superficie suave
- **Cilindro (Cylinder)**: Cuerpo cilíndrico con dos bases circulares
- **Cono (Cone)**: Figura cónica con base circular y ápex
- **Pirámide (Pyramid)**: Pirámide cuadrangular regular

#### 2.2 Transformaciones 3D Fundamentales
- **Traslación (Translation)**: Movimiento en los ejes X, Y, Z
- **Rotación (Rotation)**: Ángulos de Euler para rotación en los tres ejes
- **Escalamiento (Scaling)**: Modificación del tamaño en cada eje de forma independiente

#### 2.3 Propiedades Visuales
- **Color**: Selección interactiva mediante diálogo de colores
- **Material**: Propiedades de brillo (shininess) ajustables
- **Iluminación básica**: Componentes ambiente, difusa y especular

#### 2.4 Sistema de Cámara
- **Modo Orbital**: La cámara orbita alrededor de un punto central, ideal para examinar objetos
- **Modo Libre (Free)**: Permite movimiento libre de la cámara (preparado para expansión futura)
- **Modo Fijo (Fixed)**: Cámara estática en posición fija

### 3. Arquitectura del Sistema

#### 3.1 Estructura de Clases

```
Vector3 - Representa vectores y puntos en 3D
Color3 - Gestiona colores en formato RGBA normalizado
Transform - Contiene posición, rotación y escala de objetos
Material - Define propiedades visuales del objeto
Object3D (Base) - Clase abstracta para objetos 3D
??? Cube
??? Sphere
??? Cylinder
??? Cone
??? Pyramid
Camera - Gestiona la vista 3D y proyección
Scene - Contiene todos los objetos de la escena
SoftwareRenderer - Renderiza la escena en 2D
Form1 - Interfaz gráfica y control de la aplicación
```

#### 3.2 Flujo de Ejecución

1. **Inicialización**: Se crea la escena, cámara y renderer al cargar el formulario
2. **Loop de Renderización**: Timer cada 16ms (~60 FPS)
   - Transformación de vértices (aplicar escala, rotación, traslación)
   - Proyección 3D a 2D (perspective projection)
   - Rasterización de triángulos
   - Dibujo en pantalla
3. **Interacción del Usuario**:
   - Selección de objetos desde lista
   - Modificación de transformaciones mediante controles numéricos
   - Manipulación de cámara con ratón
   - Edición de materiales

### 4. Algoritmos Implementados

#### 4.1 Proyección Perspectiva
La proyección transforma coordenadas 3D a 2D siguiendo la fórmula:
```
screenX = (width / 2) + (x / distance) * (width / 2)
screenY = (height / 2) - (y / distance) * (height / 2)
```

Donde `distance` es la distancia del punto a la cámara a lo largo del vector forward.

#### 4.2 Transformaciones de Vértices
Se aplican en orden: Escala ? Rotación ? Traslación

**Rotación mediante Ángulos de Euler (ZYX):**
```
Rot_X = Matriz de rotación en eje X
Rot_Y = Matriz de rotación en eje Y
Rot_Z = Matriz de rotación en eje Z

Transformación final = Rot_Z * Rot_Y * Rot_X * Vertex
```

#### 4.3 Sistema de Cámara Orbital
La posición de la cámara se calcula mediante:
```
x = distance * sin(angleX) * cos(angleY)
y = distance * cos(angleX)
z = distance * sin(angleX) * sin(angleY)
```

Esto permite orbitar alrededor del punto objetivo manteniendo distancia constante.

#### 4.4 Generación de Geometría

**Esfera**: Utiliza coordenadas esféricas con stacks (paralelos) y slices (meridianos)

**Cilindro**: Genera círculos para las tapas superior e inferior, conectadas por caras laterales

**Cono**: Crea un ápex único conectado a todos los puntos de la base circular

**Pirámide**: Define 5 vértices (4 de base + 1 ápex) y conecta con 6 triángulos

### 5. Interfaz de Usuario

#### 5.1 Panel de Renderización
- Vista 3D principal que muestra los objetos renderizados
- Fondo negro para mejor contraste
- Actualización en tiempo real

#### 5.2 Panel de Control (Derecha)
- **Grupo "Add Objects"**: Botones para crear nuevas figuras
- **Lista de Objetos**: Muestra todos los objetos en la escena
- **Botones de Gestión**: Eliminar objeto individual o limpiar escena
- **Transformaciones**: Controles numéricos para posición, rotación y escala
- **Material**: Selector de color y control de brillo
- **Cámara**: Selección de modo de cámara

#### 5.3 Interacción con el Ratón
- **Click izquierdo + Arrastrar**: Rota la cámara orbital
- **Click derecho + Arrastrar**: Pan (traslación) del objetivo
- **Rueda del Ratón**: Zoom in/out

### 6. Decisiones de Diseño

#### 6.1 Software Renderer
Se implementó un renderer de software en lugar de usar OpenGL porque:
- Mayor control sobre el proceso de renderización
- Educativo: permite comprender el pipeline gráfico completo
- Sin dependencias externas de librerías gráficas especializadas
- Compatible con .NET Framework 4.7.2

#### 6.2 Arquitectura Modular
- Separación clara entre modelo (objetos 3D), vista (renderer) y controlador (Form1)
- Cada tipo de figura tiene su propia clase
- Fácil de extender con nuevas formas o transformaciones

#### 6.3 Sistema de Transformaciones
Se utiliza orden ZYX para Euler angles por:
- Estabilidad numérica
- Intuitividad: primero se rota alrededor de Z, luego Y, luego X
- Compatibilidad con convenciones de gráficos 3D

#### 6.4 Interacción de Cámara
Modo orbital como predeterminado porque:
- Intuitivo para examinar objetos
- Previene que el usuario se pierda en la escena
- Fácil de implementar y controlar

### 7. Mejoras Futuras Sugeridas

1. **Rendering Avanzado**:
   - Implementar back-face culling para mejor rendimiento
   - Añadir z-buffer para oclusión correcta
   - Shading Gouraud o Phong

2. **Funcionalidades Adicionales**:
   - Guardar/cargar escenas
   - Importar modelos 3D (OBJ, STL)
   - Animaciones de objetos
   - Luz direccional y puntual

3. **Optimizaciones**:
   - Implementar frustum culling
   - Usar vertex buffers para mejor rendimiento
   - Multi-threading para renderización

4. **Interfaz**:
   - Vista 3D de jerarquía de objetos
   - Propiedades extendidas de materiales
   - Grid de ayuda en el espacio 3D

### 8. Conclusiones

Este proyecto implementa exitosamente una herramienta interactiva de gráficos 3D que demuestra conceptos fundamentales de computación gráfica como transformaciones 3D, proyección perspectiva y renderización. La arquitectura modular permite fácil expansión y experimentación con nuevas características.

La aplicación proporciona una base sólida para aprender sobre gráficos 3D y puede ser extendida con características más avanzadas como iluminación sofisticada, texturas y modelos complejos.

---

**Fecha**: 2025
**Autor**: Estudiante - Computación Gráfica
**Lenguaje**: C# (.NET Framework 4.7.2)
**Plataforma**: Windows Forms
