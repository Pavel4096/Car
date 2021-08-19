using Car.Utilities;
using Car.Notifications;
using System;
using UnityEngine;

namespace Car.Rewards
{
    public class RewardsController : ControllerBase, IDisposable
    {
        private IRewardsView _rewardsView;
        private Reward[] _rewards;
        private IUserData _userData;
        private IAmountsInformationController _amountsInformationController;
        private PlayerProfile _playerProfile;
        private int? _currentNotification;

        private readonly int _timeToNext;
        private readonly int _timeToReset;

        public RewardsController(IRewardsView rewardsView, IAmountsInformationController amountsInformationController, Reward[] rewards, IUserData userData, GameObject item, int timeToNext, int timeToReset, PlayerProfile playerProfile)
        {
            _rewardsView = rewardsView;
            _rewards = rewards;
            _timeToNext = timeToNext;
            _timeToReset = timeToReset;
            _userData = userData;
            _amountsInformationController = amountsInformationController;
            _playerProfile = playerProfile;

            UpdateUtility.AddGameUpdate(UpdateTimer);
            _rewardsView.Init(rewards, item);
            _rewardsView.SetCurrent(_userData.CurrentRewardIndex);
            _rewardsView.ButtonClicked += ProcessButtonClick;
            _rewardsView.ExitButtonClicked += QuitRewards;

            ShowNotification();
        }

        public void UpdateTimer()
        {
            var timeToWait = _userData.LastTimeRewardTaken + new TimeSpan(0, 0, _timeToNext) - DateTime.UtcNow;

            if(timeToWait.TotalSeconds < 0 && Math.Abs(timeToWait.TotalSeconds) >= _timeToReset)
            {
                ResetRewards();
            }
            else
            {
                if(timeToWait.TotalSeconds <= 0)
                    timeToWait = new TimeSpan(0);
                var timeToWaitPercent = (float)timeToWait.TotalSeconds/_timeToNext;
                _rewardsView.SetTimer(timeToWait, timeToWaitPercent);
            }
        }

        protected override void OnDispose()
        {
            UpdateUtility.RemoveGameUpdate(UpdateTimer);
            _rewardsView.ButtonClicked -= ProcessButtonClick;
            _rewardsView.ExitButtonClicked -= QuitRewards;
        }

        private void ProcessButtonClick()
        {
            var currentTime = DateTime.UtcNow;
            var timeSinceLastReward = currentTime - _userData.LastTimeRewardTaken;

            if(timeSinceLastReward.TotalSeconds >= _timeToNext)
            {
                ProcessRewardConfirmationResult(true, currentTime);
            }
            else
            {
                _rewardsView.SetCurrent(_userData.CurrentRewardIndex);
            }
        }

        private void ProcessRewardConfirmationResult(bool rewardConfirmed, DateTime time)
        {
            if(rewardConfirmed)
            {
                AddReward();
                _userData.RewardTaken(time);
                _amountsInformationController.UpdateData();
            }
            _rewardsView.SetCurrent(_userData.CurrentRewardIndex);
            ShowNotification();
        }

        private void AddReward()
        {
            var currentReward = _rewards[_userData.CurrentRewardIndex];

            switch(currentReward.type)
            {
                case RewardType.OrangeCircle:
                    _userData.AddOrangeCircles(currentReward.amount);
                    break;
                case RewardType.GreenCircle:
                    _userData.AddGreenCircles(currentReward.amount);
                    break;
            }
        }

        private void ResetRewards()
        {
            _userData.ResetRewards(DateTime.UtcNow);
            _rewardsView.SetCurrent(_userData.CurrentRewardIndex);
        }

        private void QuitRewards()
        {
            _playerProfile.GameState.Value = GameState.MainMenu;
        }

        private void ShowNotification()
        {
            if(_playerProfile.CurrentRewardsNotificationIdentifier.HasValue)
                _playerProfile.NotificationUtility.RemoveNotification(_playerProfile.CurrentRewardsNotificationIdentifier.Value);
            
            if(_userData.CurrentRewardIndex < _rewards.Length)
            {
                var timeToWait = _userData.LastTimeRewardTaken + new TimeSpan(0, 0, _timeToNext) - DateTime.UtcNow;
                if(timeToWait.TotalMilliseconds < 0)
                    timeToWait = new TimeSpan(0);
                var date = DateTime.Now + timeToWait;
                _playerProfile.CurrentRewardsNotificationIdentifier = _playerProfile.NotificationUtility.Send(NotificationType.Ordinary, "Rewards", "You can get reward now", date);
            }
        }
    }
}
