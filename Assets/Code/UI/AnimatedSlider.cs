using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimatedSlider : Slider
{
    [SerializeField]
    private AnimationType _animationType;
    [SerializeField]
    private float _fadeInDuration;
    [SerializeField]
    private Vector2 _startPosition;
    [SerializeField]
    private float _moveDuration;

    private Image[] _sliderImages;

    public static readonly string SliderAnimationType = nameof(_animationType);
    public static readonly string FadeInDuration = nameof(_fadeInDuration);
    public static readonly string StartPosition = nameof(_startPosition);
    public static readonly string MoveDuration = nameof(_moveDuration);

    protected override void Awake()
    {
        base.Awake();
        _sliderImages = GetComponentsInChildren<Image>();
    }

    protected override void Start()
    {
        base.Start();

        if(Application.isPlaying)
        {
            switch(_animationType)
            {
                case AnimationType.FadeIn:
                    FadeIn();
                    break;
                case AnimationType.MoveIn:
                    MoveIn();
                    break;
            }
        }
    }

    private void FadeIn()
    {
        foreach(var image in _sliderImages)
        {
            image.DOColor(new Color(1.0f, 1.0f, 1.0f, 0.0f), _fadeInDuration).From();
        }
    }

    private void MoveIn()
    {
        var sliderTransform = GetComponent<RectTransform>();

        sliderTransform.DOAnchorPos(_startPosition, _moveDuration, false).From();
    }
}
