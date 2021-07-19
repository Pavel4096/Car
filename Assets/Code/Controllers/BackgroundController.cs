using Car.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace Car
{
    internal sealed class BackgroundController : ControllerBase
    {
        private BackgroundView backgroundView;
        private PlayerProfile playerProfile;
        private IReadOnlyProperty<float> horizontalMoveProperty;
        private float width, height;
        private readonly ResourcePath backgroundViewPath = new ResourcePath("Background");
        private const float maxSizeScale = 1.25f;

        public BackgroundController(PlayerProfile _playerProfile, IReadOnlyProperty<float> _horizontalMoveProperty)
        {
            var camera = Camera.main;

            playerProfile = _playerProfile;
            horizontalMoveProperty = _horizontalMoveProperty;
            horizontalMoveProperty.Subscribe(MoveBackgroundHorizontally);
            height = camera.orthographicSize * 2;
            width = height * camera.aspect;
            backgroundView = LoadView();
            UpdateSizes();
        }

        protected override void OnDispose()
        {
            horizontalMoveProperty.Unsubscribe(MoveBackgroundHorizontally);
        }

        private BackgroundView LoadView()
        {
            var backgroundViewObject = Object.Instantiate(ResourceLoader.Load(backgroundViewPath));

            AddObject(backgroundViewObject);

            return backgroundViewObject.GetComponent<BackgroundView>();
        }

        private void MoveBackgroundHorizontally(float direction)
        {
            foreach(var background in backgroundView.Backgrounds)
            {
                background.UpdateBackground(direction);
            }
        }

        private void UpdateSizes()
        {
            foreach(var background in backgroundView.Backgrounds)
            {
                var background0Size = background.backgroundTransform0.GetComponent<SpriteRenderer>().size;
                var background1Size = background.backgroundTransform1.GetComponent<SpriteRenderer>().size;

                background0Size.x = width*maxSizeScale;
                background0Size.y = height*maxSizeScale;
                background1Size.x = width*maxSizeScale;
                background1Size.y = height*maxSizeScale;

                background.backgroundTransform0.GetComponent<SpriteRenderer>().size = background0Size;
                background.backgroundTransform1.GetComponent<SpriteRenderer>().size = background1Size;

                background.UpdatePosition( -width/2 - (background0Size.x - width)/2, width/2 + (background0Size.x - width)/2, background0Size.x);
            }
        }
    }
}
