using Car.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Car.Abilities
{
    public class AbilitiesView : MonoBehaviour, IAbilitiesView
    {
        public event Action<IAbility> Selected;

        [SerializeField]
        private List<Button> buttons;
        [SerializeField]
        private List<Text> texts;

        public void Display(IReadOnlyList<IItem> items, IAbilityRepository abilityRepository)
        {
            var abilities = abilityRepository.Abilities;
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
            }
        }

        private void ProcessClick(IAbility ability)
        {
            Selected?.Invoke(ability);
        }
    }
}
