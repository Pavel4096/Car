using UnityEngine.Localization.Settings;

namespace Car
{
    public class LanguageSelectorController : ControllerBase
    {
        private LanguageSelectorView _languageSelectorView;

        public LanguageSelectorController(LanguageSelectorView languageSelectorView)
        {
            _languageSelectorView = languageSelectorView;
            _languageSelectorView.RussianButton.onClick.AddListener(() => ChangeLanguage(1));
            _languageSelectorView.EnglishButton.onClick.AddListener(() => ChangeLanguage(0));
        }

        protected override void OnDispose()
        {
            _languageSelectorView.RussianButton.onClick.RemoveAllListeners();
            _languageSelectorView.EnglishButton.onClick.RemoveAllListeners();
        }

        private void ChangeLanguage(int id)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[id];
        }
    }
}
