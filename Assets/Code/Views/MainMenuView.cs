using UnityEngine;
using UnityEngine.UI;

namespace Car
{
    internal class MainMenuView : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton;
        [SerializeField]
        private Button _rewardsButton;
        [SerializeField]
        private Button _fightButton;
        [SerializeField]
        private Button _exitButton;

        public Button StartButton => _startButton;
        public Button RewardsButton => _rewardsButton;
        public Button FightButton => _fightButton;
        public Button ExitButton => _exitButton;
    }
}
