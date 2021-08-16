using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Car.Rewards
{
    public class ItemView : MonoBehaviour, IItemView
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _name;
        [SerializeField]
        private TMP_Text _amount;
        [SerializeField]
        private GameObject _backgroundCurrent;

        public bool IsCurrent
        {
            set => _backgroundCurrent.SetActive(value);
        }

        public void Init(Reward reward)
        {
            _icon.sprite = reward.image;
            _name.text = reward.name;
            _amount.text = reward.amount.ToString();
        }
    }
}
