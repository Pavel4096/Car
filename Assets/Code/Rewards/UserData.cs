using System;

namespace Rewards
{
    [Serializable]
    public class UserData : IUserData
    {
        public DateTime LastTimeRewardTaken { get; set; }
        public int CurrentRewardIndex { get; set; }

        //public UserData()
        //{}

        public void RewardTaken(DateTime time)
        {
            LastTimeRewardTaken = time;
            CurrentRewardIndex++;
        }
    }
}
