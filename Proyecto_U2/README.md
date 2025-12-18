# 3D Graphics Interactive Tool
## Herramienta Interactiva de Computación Gráfica 3D

A comprehensive 3D graphics editor built with C# and Windows Forms, demonstrating fundamental computer graphics concepts without external graphics libraries.

### Features

? **5 Basic 3D Shapes**
- Cube (Cubo)
- Sphere (Esfera)
- Cylinder (Cilindro)
- Cone (Cono)
- Pyramid (Pirámide)

?? **Essential 3D Transformations**
- Translation (Traslación) - Move in X, Y, Z axes
- Rotation (Rotación) - Euler angles in degrees
- Scaling (Escalamiento) - Independent axis scaling

?? **Visual Properties**
- Color Selection with System Color Picker
- Adjustable Material Shininess (1-128)
- Ambient, Diffuse, and Specular Components
- Real-time Material Updates

?? **Camera System**
- **Orbital Mode**: Rotate around objects smoothly
- **Free Mode**: For future FPS-style navigation
- **Fixed Mode**: Static viewpoint
- Mouse-based Controls: Pan, Rotate, Zoom

??? **Intuitive Interface**
- Real-time 3D Rendering (~60 FPS)
- Object Management and Selection
- Transform Controls via Numeric Inputs
- Interactive List View

### Technical Details

**Language**: C# 7.3  
**Framework**: .NET Framework 4.7.2  
**UI Framework**: Windows Forms  
**Graphics**: Custom Software Renderer (No DirectX/OpenGL dependency)

### Project Structure

```
Proyecto_U2/
??? Vector3.cs                    # 3D vector math utilities
??? Color3.cs                     # Color management (RGBA)
??? Transform.cs                  # Position, Rotation, Scale
??? Material.cs                   # Visual material properties
??? Object3D.cs                   # Base class for 3D objects
??? Cube.cs                       # Cube implementation
??? Sphere.cs                     # Sphere implementation
??? Cylinder.cs                   # Cylinder implementation
??? Cone.cs                       # Cone implementation
??? Pyramid.cs                    # Pyramid implementation
??? Camera.cs                     # Camera with orbital mode
??? Scene.cs                      # Scene management
??? SoftwareRenderer.cs           # 3D to 2D projection & rendering
??? Form1.cs                      # Main UI and logic
??? Form1.Designer.cs             # UI components
??? TECHNICAL_REPORT.md           # Detailed technical documentation
??? USER_GUIDE.md                 # User manual
??? README.md                     # This file
```

### Key Algorithms

**3D to 2D Projection**
- Perspective projection using dot products
- Depth sorting for visibility
- Vector transformation via camera basis

**Transformations**
- Scale ? Rotation ? Translation order
- Euler angles (X, Y, Z rotation)
- Matrix-free implementation for clarity

**Geometry Generation**
- Sphere: Stacks and slices parametric surface
- Cylinder: Base circles + lateral faces
- Cone: Apex + base triangulation
- Pyramid: Regular square base

### Quick Start

1. **Open the Application**
   ```
   Proyecto_U2.exe
   ```

2. **Create Objects**
   - Click buttons in "Add Objects" group
   - Objects appear in the list and 3D view

3. **Manipulate Objects**
   - Select from list
   - Adjust Position, Rotation, Scale
   - Change color with "Pick Color"
   - Adjust material shininess

4. **Control Camera**
   - **Left Mouse + Drag**: Orbit around
   - **Right Mouse + Drag**: Pan
   - **Mouse Wheel**: Zoom in/out
   - Select camera mode: Orbital, Free, Fixed

### Usage Examples

**Create a rotating scene:**
1. Add multiple objects at different positions
2. Select orbital camera mode
3. Drag to orbit and observe all objects
4. Adjust individual transforms for artistic arrangement

**Learn transformations:**
1. Create a cube
2. Modify position: Move to different coordinates
3. Modify rotation: Apply Euler angles
4. Modify scale: Create different sizes

**Experiment with materials:**
1. Create a sphere
2. Pick different colors
3. Adjust shininess to see material effects

### Performance

- **Target FPS**: ~60 FPS (16ms per frame)
- **Resolution**: Scales with window size
- **Rendering**: Software-based (CPU only)
- **Maximum Objects**: Limited by available RAM

### Requirements

- Windows 7 or later
- .NET Framework 4.7.2
- ~50 MB disk space

### Architecture Decisions

1. **Software Renderer**: Educational approach to understand the graphics pipeline
2. **No External Graphics Libraries**: Pure C# implementation for portability
3. **Modular Design**: Each shape in its own class for extensibility
4. **Real-time Updates**: Timer-based rendering loop for smooth interaction

### Extension Possibilities

- Add more primitive shapes (Torus, Tetrahedron, etc.)
- Implement texturing
- Add per-light objects
- Save/Load scene files
- Import external 3D models
- Implement selection-by-clicking
- Add animation system

### Documentation Files

- **TECHNICAL_REPORT.md**: Complete technical documentation in Spanish
  - System architecture
  - Algorithm descriptions
  - Design decisions
  - Future improvements

- **USER_GUIDE.md**: User manual in Spanish
  - Quick start guide
  - Feature documentation
  - Usage examples
  - Troubleshooting

### Author Notes

This project demonstrates:
- Vector mathematics and transformations
- 3D projection and perspective
- Object-oriented design in graphics
- Real-time rendering concepts
- Interactive UI programming in Windows Forms

It serves as both a functional tool and an educational resource for learning computer graphics fundamentals.

### License

Educational Project - Universidad ESPE
Semestre 7 - Computación Gráfica
Parcial 2

---

**Version**: 1.0  
**Build Date**: 2025  
**Status**: Complete and Functional
