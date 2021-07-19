using Car.Utilities;
using UnityEngine;

namespace Car
{
    internal sealed class Game : MonoBehaviour
    {
        public Transform menuRoot;
        
        private GameController gameController;

        private void Awake()
        {
            gameController = new GameController(this);
            //Debug.Log($"{Camera.main.orthographicSize*2*Camera.main.aspect} x {Camera.main.orthographicSize*2}");
        }

        void Update()
        {
            UpdateUtility.GameUpdate();
        }
    }
}
