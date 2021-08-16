using System;
using UnityEngine;

namespace Car.Rewards
{
    [Serializable]
    public class Reward
    {
        public RewardType type;
        public Sprite image;
        public string name;
        public int amount;
    }
}
