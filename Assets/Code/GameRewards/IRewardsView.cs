using System;
using UnityEngine;

namespace Car.Rewards
{
    public interface IRewardsView
    {
        event Action ButtonClicked;
        event Action ExitButtonClicked;
        void Init(Reward[] rewards, GameObject item);
        void SetCurrent(int index);
        void SetTimer(TimeSpan time, float percent);
    }
}
