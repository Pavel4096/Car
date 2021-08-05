namespace Rewards
{
    public interface IItemView
    {
        bool IsCurrent { set; }
        void Init(Reward reward);
    }
}
