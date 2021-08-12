using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rewards
{
    [Serializable]
    public class RewardsView : MonoBehaviour, IRewardsView
    {
        [SerializeField]
        private TMP_Text _timer;
        [SerializeField]
        private Image _bar;
        [SerializeField]
        private Button _takeRewardButton;
        [SerializeField]
        private Transform _itemsRoot;

        private List<IItemView> _itemViews = new List<IItemView>();
        private int currentIndex = 0;

        public event Action ButtonClicked;

        public void Init(Reward[] rewards, GameObject item)
        {
            foreach(var reward in rewards)
            {
                var nextItem = UnityEngine.Object.Instantiate(item, _itemsRoot, false);
                var nextItemView = nextItem.GetComponent<ItemView>();

                _itemViews.Add(nextItemView);
                nextItemView.Init(reward);
            }
            _takeRewardButton.onClick.AddListener(() => ProcessButtonClick());
        }

        public void SetCurrent(int index)
        {
            _itemViews[currentIndex].IsCurrent = false;
            if(index != -1)
            {
                _itemViews[index].IsCurrent = true;
                currentIndex = index;
                _takeRewardButton.enabled = true;
            }
        }

        public void SetTimer(TimeSpan time, float waitTimePercent)
        {
            if(time.TotalSeconds > 0)
                _timer.text = $"Wait time: {time.Days} days {time.Hours} hours {time.Minutes} minutes {time.Seconds} seconds";
            else
                _timer.text = "Wait time: 0";
            
            _bar.fillAmount = waitTimePercent;
        }

        private void ProcessButtonClick()
        {
            _takeRewardButton.enabled = false;
            ButtonClicked?.Invoke();
        }

        private void OnDestroy()
        {
            _takeRewardButton.onClick.RemoveAllListeners();
        }
    }
}
