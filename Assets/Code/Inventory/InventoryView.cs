using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Car.Inventory
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        public event Action<IItem> Selected;
        public event Action<IItem> Deselected;
        public event Action Exit;

        [SerializeField]
        private List<Button> buttons;
        [SerializeField]
        private List<Text> texts;
        [SerializeField]
        private Button exitButton;

        public void Init()
        {
            exitButton.onClick.AddListener(() => Exit?.Invoke());
        }

        public void Display(IReadOnlyDictionary<int, IItem> items)
        {
            var displayedCount = 0;

            Reset();
            foreach(var item in items)
            {
                var currentIndex = displayedCount;

                texts[displayedCount].text = item.Value.Info.Title;
                buttons[displayedCount].onClick.AddListener(() => ChangeButton(displayedCount, item.Value));
                buttons[displayedCount].gameObject.SetActive(true);
                displayedCount++;
                if(displayedCount == buttons.Count)
                    break;
            }
        }

        public void Dispose()
        {
            Reset();
            exitButton.onClick.RemoveAllListeners();
        }

        private void ChangeButton(int textIndex, IItem item)
        {
            if(texts[textIndex].text.EndsWith("(Selected)"))
            {
                texts[textIndex].text = item.Info.Title;
                Deselected?.Invoke(item);
            }
            else
            {
                texts[textIndex].text = item.Info.Title + " (Selected)";
                Selected?.Invoke(item);
            }
        }

        private void Reset()
        {
            foreach(var button in buttons)
            {
                button.onClick.RemoveAllListeners();
                button.gameObject.SetActive(false);
            }
        }
    }
}
