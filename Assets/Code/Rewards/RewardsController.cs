using System;
using UnityEngine;

namespace Rewards
{
    public class RewardsController : IDisposable
    {
        private IRewardsView _rewardsView;
        private Reward[] _rewards;
        private IUserData _userData;

        private readonly int _timeToNext;
        private readonly int _timeToReset;

        public RewardsController(IRewardsView rewardsView, Reward[] rewards, IUserData userData, GameObject item, int timeToNext, int timeToReset)
        {
            _rewardsView = rewardsView;
            _rewards = rewards;
            _timeToNext = timeToNext;
            _timeToReset = timeToReset;
            _userData = userData;

            _rewardsView.Init(rewards, item);
            _rewardsView.SetCurrent(_userData.CurrentRewardIndex);
            _rewardsView.ButtonClicked += ProcessButtonClick;
        }

        public void UpdateTimer()
        {
            var timeToWait = _userData.LastTimeRewardTaken + new TimeSpan(0, 0, _timeToNext) - DateTime.UtcNow;
            var timeToWaitPercent = (float)timeToWait.TotalSeconds/_timeToNext;

            if(timeToWait.TotalSeconds < 0)
                timeToWait = new TimeSpan(0);
            _rewardsView.SetTimer(timeToWait, timeToWaitPercent);
        }

        public void Dispose()
        {
            _rewardsView.ButtonClicked -= ProcessButtonClick;
        }

        private void ProcessButtonClick()
        {
            var currentTime = DateTime.UtcNow;
            var timeSinceLastReward = currentTime - _userData.LastTimeRewardTaken;

            if(timeSinceLastReward.TotalSeconds >= _timeToNext)
            {
                _userData.RewardTaken(currentTime);
                _rewardsView.SetCurrent(_userData.CurrentRewardIndex);
            }
            else
            {
                _rewardsView.SetCurrent(_userData.CurrentRewardIndex);
            }
        }
    }
}
