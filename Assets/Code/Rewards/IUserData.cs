using System;

namespace Rewards
{
    public interface IUserData
    {
        int OrangeCircles { get; }
        int GreenCircles { get; }
        void AddOrangeCircles(int amount);
        void AddGreenCircles(int amount);
        DateTime LastTimeRewardTaken { get; }
        int CurrentRewardIndex { get; }
        void RewardTaken(DateTime time);
        void SetMaxRewardIndex(int index);
        void ResetRewards(DateTime time);
    }
}
