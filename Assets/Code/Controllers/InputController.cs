using Car.Utilities;
using UnityEngine;

namespace Car
{
    internal sealed class InputController : ControllerBase
    {
        private InputView inputView;
        private Property<float> moveProperty;
        private Property garage;
        private Property abilities;
        private float circleCenterX;
        private float circleCenterY;
        private float circleRadiusInPixels;
        private float circleRadius;
        private float xValue, yValue;
        private float xOffset, yOffset;
        private readonly ResourcePath inputViewPath = new ResourcePath("Input");

        private readonly KeyCode garageKey = KeyCode.G;
        private readonly KeyCode abilitiesKey = KeyCode.A;

        public InputController(Property<float> _moveProperty, Property _garage, Property _abilities)
        {
            moveProperty = _moveProperty;
            garage = _garage;
            abilities = _abilities;
            inputView = LoadView();
            circleCenterX = inputView.CircleCenterX;
            circleCenterY = inputView.CircleCenterY;
            circleRadiusInPixels = inputView.CircleRadiusInPixels;
            circleRadius = inputView.CircleRadius;
            xOffset = Screen.width/2;
            yOffset = Screen.height/2;
            UpdateUtility.AddGameUpdate(GameUpdate);
        }

        protected override void OnDispose()
        {
            UpdateUtility.RemoveGameUpdate(GameUpdate);
        }

        private InputView LoadView()
        {
            var inputViewObject = Object.Instantiate(ResourceLoader.Load(inputViewPath));

            AddObject(inputViewObject);

            return inputViewObject.GetComponent<InputView>();
        }

        private void GameUpdate()
        {
            var position = inputView.DotPosition;
            var count = Input.touchCount;
            float touchX, touchY;
            bool done = false;

            for(var i = 0; i < count; i++)
            {
                var currentTouch = Input.GetTouch(i);

                touchX = (currentTouch.position.x - xOffset - circleCenterX)/circleRadiusInPixels;
                touchY = (currentTouch.position.y - yOffset - circleCenterY)/circleRadiusInPixels;

                if(touchX >= -1 && touchX <= 1 && touchY >= -1 && touchY <= 1)
                {
                    xValue = touchX;
                    yValue = touchY;
                    done = true;
                    break;
                }
            }
            if(!done)
            {
                xValue *= 0.95f;
                yValue *= 0.95f;
            }
            position.x = xValue*circleRadius;
            position.y = yValue*circleRadius;
            inputView.DotPosition = position;
            moveProperty.Value = -xValue;

            if(Input.GetKeyDown(garageKey))
                garage.Inform();
            if(Input.GetKeyDown(abilitiesKey))
                abilities.Inform();
        }
    }
}
