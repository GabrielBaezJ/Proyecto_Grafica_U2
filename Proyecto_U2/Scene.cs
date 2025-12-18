using System;
using System.Collections.Generic;

namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D scene containing objects, camera, and lighting.
    /// </summary>
    public class Scene
    {
        public List<Object3D> Objects { get; set; }
        public Camera Camera { get; set; }
        public Color3 BackgroundColor { get; set; }
        public Vector3 AmbientLight { get; set; }

        private Object3D selectedObject;

        public Scene()
        {
            Objects = new List<Object3D>();
            Camera = new Camera();
            BackgroundColor = new Color3(0.2f, 0.2f, 0.2f);
            AmbientLight = new Vector3(0.3f, 0.3f, 0.3f);
            selectedObject = null;
        }

        /// <summary>
        /// Add an object to the scene.
        /// </summary>
        public void AddObject(Object3D obj)
        {
            Objects.Add(obj);
        }

        /// <summary>
        /// Remove an object from the scene.
        /// </summary>
        public void RemoveObject(Object3D obj)
        {
            Objects.Remove(obj);
            if (selectedObject == obj)
                selectedObject = null;
        }

        /// <summary>
        /// Clear all objects from the scene.
        /// </summary>
        public void Clear()
        {
            Objects.Clear();
            selectedObject = null;
        }

        /// <summary>
        /// Select an object in the scene.
        /// </summary>
        public void SelectObject(Object3D obj)
        {
            if (selectedObject != null)
                selectedObject.IsSelected = false;

            selectedObject = obj;
            if (selectedObject != null)
                selectedObject.IsSelected = true;
        }

        /// <summary>
        /// Get the currently selected object.
        /// </summary>
        public Object3D GetSelectedObject()
        {
            return selectedObject;
        }

        /// <summary>
        /// Deselect the current object.
        /// </summary>
        public void DeselectObject()
        {
            if (selectedObject != null)
                selectedObject.IsSelected = false;
            selectedObject = null;
        }

        /// <summary>
        /// Get object count.
        /// </summary>
        public int GetObjectCount()
        {
            return Objects.Count;
        }

        public override string ToString()
        {
            return $"Scene - Objects: {Objects.Count}, Selected: {selectedObject?.Name ?? "None"}";
        }
    }
}
