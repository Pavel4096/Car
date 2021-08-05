using System;

namespace Rewards
{
    public interface IUserData
    {
        DateTime LastTimeRewardTaken { get; }
        int CurrentRewardIndex { get; }
        void RewardTaken(DateTime time);
    }
}
