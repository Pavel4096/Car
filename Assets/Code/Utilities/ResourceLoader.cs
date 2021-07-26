using UnityEngine;

namespace Car.Utilities
{
    public static class ResourceLoader
    {
        public static GameObject Load(ResourcePath path)
        {
            return Resources.Load<GameObject>(path.resourcePath);
        }

        public static T Load<T>(ResourcePath path)
        {
            var loadedObject = Load(path);

            return loadedObject.GetComponent<T>();
        }
    }
}
