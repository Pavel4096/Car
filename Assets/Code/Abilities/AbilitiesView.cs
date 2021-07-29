using Car.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Linq;

namespace Car.Abilities
{
    public class AbilitiesView : MonoBehaviour, IAbilitiesView
    {
        public event Action<IAbility> Selected;

        [SerializeField]
        private List<Button> buttons;
        [SerializeField]
        private List<Text> texts;

        public bool IsDisplayed { get; private set; }

        public void Display(IReadOnlyList<IItem> items, IRepository<int, IAbility> abilityRepository)
        {
            var abilities = abilityRepository.Items;
            var displayedCount = 0;

            Init();
            foreach(var item in items)
            {
                if(abilities.TryGetValue(item.Id, out var ability))
                {
                    texts[displayedCount].text = item.Info.Title;
                    buttons[displayedCount].onClick.AddListener(() => ProcessClick(ability));
                    buttons[displayedCount].gameObject.SetActive(true);
                    displayedCount++;
                    if(displayedCount == buttons.Count)
                        break;
                }
            }

            gameObject.SetActive(true);
            IsDisplayed = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsDisplayed = false;
        }

        public void Dispose()
        {
            foreach(var button in buttons)
            {
                button.onClick.RemoveAllListeners();
            }
        }

        private void Init()
        {
            foreach(var button in buttons)
            {
                button.onClick.RemoveAllListeners();
                button.gameObject.SetActive(false);
            }
        }

        private void ProcessClick(IAbility ability)
        {
            Selected?.Invoke(ability);
        }
    }
}
