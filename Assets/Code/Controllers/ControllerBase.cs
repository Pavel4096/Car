using System;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    public abstract class ControllerBase : IControllerBase
    {
        private List<IControllerBase> controllers = new List<IControllerBase>();
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

        public void AddController(IControllerBase controller)
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
