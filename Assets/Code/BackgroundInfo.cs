using System;
using UnityEngine;

namespace Car
{
    [Serializable]
    public class BackgroundInfo
    {
        public Transform backgroundTransform0;
        public Transform backgroundTransform1;
        public float speedMultiplier;

        private float leftBorder;
        private float rightBorder;
        private float width;

        public void UpdatePosition(float _leftBorder, float _rightBorder, float _width)
        {
            Vector3 position = backgroundTransform1.position;

            position.x += backgroundTransform1.GetComponent<SpriteRenderer>().size.x;
            backgroundTransform1.position = position;

            leftBorder = _leftBorder;
            rightBorder = _rightBorder;
            width = _width;
        }

        public void UpdateBackground(float speed)
        {
            Transform backgroundTransform;

            backgroundTransform0.position += new Vector3(speed*speedMultiplier, 0.0f, 0.0f);
            backgroundTransform1.position += new Vector3(speed*speedMultiplier, 0.0f, 0.0f);

            if(speed > 0)
            {
                if(backgroundTransform0.position.x > leftBorder)
                {
                    ChangeBackground(-width, backgroundTransform1);
                }
            }
            else if(speed < 0)
            {
                if(backgroundTransform1.position.x < rightBorder)
                {
                    ChangeBackground(width, backgroundTransform0);
                }
            }
        }

        private void ChangeBackground(float distance, Transform background)
        {
            Transform backgroundTransform;
            var position = background.position;

            position.x -= width;
            background.position = position;
            backgroundTransform = backgroundTransform0;
            backgroundTransform0 = backgroundTransform1;
            backgroundTransform1 = backgroundTransform;
        }
    }
}
