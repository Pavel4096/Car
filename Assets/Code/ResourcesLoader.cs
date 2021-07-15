using UnityEngine;

namespace Car
{
    internal static class ResourcesLoader
    {
        public static GameObject Load(ResourcePath path)
        {
            return Resources.Load<GameObject>(path.resourcePath);
        }
    }
}
