using System;
using System.Collections.Generic;

namespace Proyecto_U2
{
    /// <summary>
    /// Represents a 3D scene containing objects, camera, and lighting.
    /// Supports single and multiple object selection.
    /// </summary>
    public class Scene
    {
        public List<Object3D> Objects { get; set; }
        public Camera Camera { get; set; }
        public Color3 BackgroundColor { get; set; }
        public Vector3 AmbientLight { get; set; }

        private Object3D selectedObject;
        private List<Object3D> selectedObjects;
        private bool multiSelectMode;

        public Scene()
        {
            Objects = new List<Object3D>();
            Camera = new Camera();
            BackgroundColor = new Color3(0.2f, 0.2f, 0.2f);
            AmbientLight = new Vector3(0.3f, 0.3f, 0.3f);
            selectedObject = null;
            selectedObjects = new List<Object3D>();
            multiSelectMode = false;
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
            selectedObjects.Remove(obj);
        }

        /// <summary>
        /// Clear all objects from the scene.
        /// </summary>
        public void Clear()
        {
            Objects.Clear();
            selectedObject = null;
            selectedObjects.Clear();
        }

        /// <summary>
        /// Select a single object in the scene (deselects all others).
        /// </summary>
        public void SelectObject(Object3D obj)
        {
            if (selectedObject != null)
                selectedObject.IsSelected = false;

            foreach (var sel in selectedObjects)
            {
                sel.IsSelected = false;
            }
            selectedObjects.Clear();

            selectedObject = obj;
            if (selectedObject != null)
            {
                selectedObject.IsSelected = true;
                selectedObjects.Add(selectedObject);
            }
            multiSelectMode = false;
        }

        /// <summary>
        /// Add object to selection (for multi-select).
        /// </summary>
        public void AddToSelection(Object3D obj)
        {
            if (obj == null)
                return;

            if (!selectedObjects.Contains(obj))
            {
                selectedObjects.Add(obj);
                obj.IsSelected = true;

                if (selectedObject == null)
                {
                    selectedObject = obj;
                }
            }
            multiSelectMode = true;
        }

        /// <summary>
        /// Remove object from selection (for multi-select).
        /// </summary>
        public void RemoveFromSelection(Object3D obj)
        {
            if (obj != null)
            {
                selectedObjects.Remove(obj);
                obj.IsSelected = false;

                if (selectedObject == obj)
                {
                    selectedObject = selectedObjects.Count > 0 ? selectedObjects[0] : null;
                }
            }
        }

        /// <summary>
        /// Get the currently selected object (primary selection in multi-select mode).
        /// </summary>
        public Object3D GetSelectedObject()
        {
            return selectedObject;
        }

        /// <summary>
        /// Get all selected objects.
        /// </summary>
        public List<Object3D> GetAllSelectedObjects()
        {
            return new List<Object3D>(selectedObjects);
        }

        /// <summary>
        /// Check if multi-select mode is active.
        /// </summary>
        public bool IsMultiSelectMode()
        {
            return multiSelectMode;
        }

        /// <summary>
        /// Get count of selected objects.
        /// </summary>
        public int GetSelectedObjectCount()
        {
            return selectedObjects.Count;
        }

        /// <summary>
        /// Deselect all objects.
        /// </summary>
        public void DeselectObject()
        {
            if (selectedObject != null)
                selectedObject.IsSelected = false;
            selectedObject = null;

            foreach (var obj in selectedObjects)
            {
                obj.IsSelected = false;
            }
            selectedObjects.Clear();
            multiSelectMode = false;
        }

        /// <summary>
        /// Deselect all objects except one.
        /// </summary>
        public void DeselectAllExcept(Object3D obj)
        {
            foreach (var sel in selectedObjects)
            {
                if (sel != obj)
                {
                    sel.IsSelected = false;
                }
            }
            selectedObjects.RemoveAll(o => o != obj);

            if (selectedObject != obj && obj != null)
            {
                selectedObject = obj;
            }
            multiSelectMode = false;
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
            return $"Scene - Objects: {Objects.Count}, Selected: {selectedObject?.Name ?? "None"}, MultiSelect: {multiSelectMode}";
        }
    }
}