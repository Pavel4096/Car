using System;

namespace Car.Rewards
{
    [Serializable]
    public class UserData : IUserData
    {
        public int OrangeCircles { get; set; }
        public int GreenCircles { get; set; }
        public DateTime LastTimeRewardTaken { get; set; }
        public int CurrentRewardIndex { get; set; }

        private int _maxRewardIndex = -1;

        public void AddOrangeCircles(int amount)
        {
            OrangeCircles += amount;
        }

        public void AddGreenCircles(int amount)
        {
            GreenCircles += amount;
        }

        public void RewardTaken(DateTime time)
        {
            LastTimeRewardTaken = time;
            if(CurrentRewardIndex < _maxRewardIndex)
                CurrentRewardIndex++;
            else
                CurrentRewardIndex = -1;
        }

        public void SetMaxRewardIndex(int index)
        {
            if(_maxRewardIndex == -1)
                _maxRewardIndex = index;
        }

        public void ResetRewards(DateTime time)
        {
            LastTimeRewardTaken = time;
            CurrentRewardIndex = 0;
        }
    }
}
