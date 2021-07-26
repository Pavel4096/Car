using System;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    internal abstract class ControllerBase : IDisposable
    {
        private List<ControllerBase> controllers = new List<ControllerBase>();
        private List<GameObject> objects = new List<GameObject>();
        private bool isDisposed;

        public void Dispose()
        {
            if(isDisposed)
                return;
            
            foreach(var controller in controllers)
                controller.Dispose();
            controllers.Clear();

            foreach(var currentObject in objects)
                UnityEngine.Object.Destroy(currentObject);
            objects.Clear();

            OnDispose();
            isDisposed = true;
        }

        public void AddController(ControllerBase controller)
        {
            if(controller != null)
                controllers.Add(controller);
        }

        protected void AddObject(GameObject newObject)
        {
            objects.Add(newObject);
        }

        protected virtual void OnDispose()
        {
        }
    }
}
