using Car.Utilities;
using System.Collections.Generic;
using UnityEngine;


using Car.Inventory;

namespace Car
{
    public class Game : MonoBehaviour
    {
        public Transform menuRoot;
        //public List<ItemUpgradeConfig> upgradeConfigs;
        public ItemUpgradeConfig[] upgradeConfigs;
        public AbilityConfig[] abilityConfigs;
        //public List<AbilityConfig> abilityConfigs;

        public ItemConfig[] configs;
        
        private GameController gameController;

        private void Awake()
        {
            gameController = new GameController(this);
        }

        private void Update()
        {
            UpdateUtility.GameUpdate();
        }
    }
}
