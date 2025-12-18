# Guía de Usuario - Editor de Gráficos 3D

## Inicio Rápido

### 1. Crear tu Primer Objeto

1. **Ejecuta la aplicación**: Abre `Proyecto_U2.exe`
2. **Añade una forma**: Haz clic en uno de los botones en el grupo "Add Objects":
   - Cube
   - Sphere
   - Cylinder
   - Cone
   - Pyramid

3. Verás el objeto renderizado en el área de visualización central y aparecerá en la lista de objetos.

### 2. Seleccionar y Manipular Objetos

1. **Selecciona un objeto** de la lista en el panel derecho
2. El objeto se resaltará con color amarillo en la vista 3D
3. Los controles de transformación se actualizarán automáticamente

### 3. Transformaciones

#### Posición
- Modifica los valores **Position (X, Y, Z)** para mover el objeto en los tres ejes
- Valores positivos: derecha, arriba, adelante
- Valores negativos: izquierda, abajo, atrás

#### Rotación
- Cambia **Rotation (X, Y, Z)** para girar el objeto
- Valores en grados (-360 a 360)
- X: Rotación alrededor del eje horizontal
- Y: Rotación alrededor del eje vertical
- Z: Rotación alrededor del eje profundo

#### Escala
- Ajusta **Scale (X, Y, Z)** para redimensionar
- Mínimo: 0.1 (muy pequeño)
- Máximo: 10 (muy grande)
- Valor 1 = tamaño original

### 4. Materiales y Color

1. **Cambiar color**: Haz clic en el botón "Pick Color"
2. Selecciona el color deseado en el diálogo
3. El objeto se actualizará inmediatamente

#### Brillo (Shininess)
- Usa el slider "Shininess" (1-128)
- Valores altos: objeto más brillante
- Valores bajos: objeto más mate

### 5. Control de Cámara

#### Modo Orbital (Recomendado)
- **Selecciona "Orbital"** en el combo "Camera Mode"
- **Click izquierdo + Arrastrar**: Rota alrededor del objeto
- **Click derecho + Arrastrar**: Traslada el centro de rotación
- **Rueda del Ratón**: Zoom in/out (acercar/alejar)

#### Modo Libre
- Para futuras expansiones (navegación tipo FPS)

#### Modo Fijo
- Cámara estática en posición predefinida

### 6. Gestión de Objetos

**Eliminar un objeto:**
1. Selecciona el objeto de la lista
2. Haz clic en "Delete"

**Limpiar la escena:**
- Haz clic en "Clear Scene" para eliminar todos los objetos

**Ver lista de objetos:**
- La lista en el panel derecho muestra todos los objetos
- Haz click en cualquiera para seleccionarlo

## Ejemplos de Uso

### Crear una Escena Simple

1. Añade un **Cube** (centro)
2. Añade una **Sphere** a la derecha (Position X: 3)
3. Añade un **Cylinder** a la izquierda (Position X: -3)
4. Rota la vista con el ratón para verlos desde diferentes ángulos

### Práctica de Transformaciones

1. Crea un **Pyramid**
2. Aumenta su escala (Scale: 2, 2, 2)
3. Rótalo 45 grados en Y
4. Muévelo hacia arriba (Position Y: 3)
5. Cambia su color a rojo

### Exploración de Geometría

1. Crea una **Sphere**
2. Escálala (Scale: 2, 1, 0.5) para hacerla elipsoide
3. Rótala mientras observas desde diferentes ángulos
4. Prueba con diferentes valores de shininess

## Consejos y Trucos

1. **Mejor visualización**: Aumenta la escala de los objetos para verlos mejor
2. **Múltiples objetos**: Mantén los objetos con escalas moderadas para evitar superposición
3. **Experimento**: Intenta rotaciones combinadas (ej: X=45, Y=45, Z=45)
4. **Zoom**: Si un objeto sale de pantalla, usa la rueda del ratón para alejar la cámara
5. **Precisión**: Los valores numéricos permiten transformaciones exactas

## Atajos Efectivos

| Acción | Método |
|--------|--------|
| Rotar vista | Click izquierdo + Arrastrar |
| Mover escena | Click derecho + Arrastrar |
| Zoom | Rueda del Ratón |
| Cambiar objeto | Lista + Click |
| Nueva forma | Botones en grupo "Add Objects" |

## Solución de Problemas

**Problema**: Los objetos son muy pequeños
- **Solución**: Aumenta la escala o acerca la cámara con la rueda del ratón

**Problema**: No veo los cambios
- **Solución**: Verifica que el objeto esté seleccionado (resaltado en amarillo)

**Problema**: La cámara está fuera de control
- **Solución**: Cambia a "Orbital" mode y usa el botón derecho para restablecer la posición

**Problema**: Rendimiento lento
- **Solución**: Reduce la cantidad de objetos o utiliza figuras con menos detalle

## Especificaciones Técnicas

- **Resolución**: Adaptive (responde a cambios de ventana)
- **FPS**: ~60 FPS
- **Objetos Máximos**: Sin límite teórico (limitado por rendimiento)
- **Formato Color**: RGBA (0.0 - 1.0)
- **Rango Ángulos**: 0 - 360 grados

---

**Versión**: 1.0
**Plataforma**: Windows
**Requisitos**: .NET Framework 4.7.2
