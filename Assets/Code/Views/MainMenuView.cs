using UnityEngine;
using UnityEngine.UI;

namespace Car
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField]
        private Button startButton;

        public Button StartButton => startButton;
    }
}
