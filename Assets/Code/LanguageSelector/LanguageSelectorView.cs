using UnityEngine;
using UnityEngine.UI;

namespace Car
{
    public class LanguageSelectorView : MonoBehaviour
    {
        [SerializeField]
        private Button _russianButton;
        [SerializeField]
        private Button _englishButton;

        public Button RussianButton => _russianButton;
        public Button EnglishButton => _englishButton;
    }
}
