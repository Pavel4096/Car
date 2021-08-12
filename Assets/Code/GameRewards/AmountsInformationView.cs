using UnityEngine;
using TMPro;

namespace Car.Rewards
{
    public class AmountsInformationView : MonoBehaviour, IAmountsInformationView
    {
        [SerializeField]
        private TMP_Text _orangeCircles;
        [SerializeField]
        private TMP_Text _greenCircles;

        public TMP_Text OrangeCircles => _orangeCircles;
        public TMP_Text GreenCircles => _greenCircles;
    }
}
