using System;
using UnityEngine;

namespace Rewards
{
    public interface IRewardsView
    {
        event Action ButtonClicked;
        void Init(Reward[] rewards, GameObject item);
        void SetCurrent(int index);
        void SetTimer(TimeSpan time, float percent);
    }
}
