using UnityEngine;

namespace Car
{
    internal sealed class InputView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer circleSpriteRenderer;
        [SerializeField]
        private Transform circleTransform;
        [SerializeField]
        private Transform dotTransform;

        public float CircleCenterX => Screen.height/10*circleTransform.position.x;
        public float CircleCenterY => Screen.height/10*circleTransform.position.y;
        public float CircleRadiusInPixels => Screen.height/10*CircleRadius;
        public float CircleRadius => circleSpriteRenderer.sprite.bounds.extents.y;
        public Vector3 CirclePosition
        {
            get => circleTransform.position;
        }
        public Vector3 DotPosition
        {
            get => dotTransform.localPosition;
            set => dotTransform.localPosition = value;
        }
    }
}
