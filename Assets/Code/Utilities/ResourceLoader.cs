using UnityEngine;

namespace Car.Utilities
{
    public static class ResourceLoader
    {
        public static GameObject Load(ResourcePath path)
        {
            return Resources.Load<GameObject>(path.resourcePath);
        }
    }
}
