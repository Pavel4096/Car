using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    internal sealed class BackgroundView : MonoBehaviour
    {
        [SerializeField]
        private List<BackgroundInfo> backgrounds;

        public List<BackgroundInfo> Backgrounds => backgrounds;
    }
}
