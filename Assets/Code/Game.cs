using Car.Utilities;
using UnityEngine;

namespace Car
{
    public class Game : MonoBehaviour
    {
        public Transform menuRoot;
        
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
