using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[AddComponentMenu("UI/Animated Slider")]
public class AnimatedSlider : Slider
{
    [SerializeField]
    private float _fadeInDuration;

    private Image[] _sliderImages;

    public static readonly string FadeInDuration = nameof(_fadeInDuration);

    protected override void Awake()
    {
        base.Awake();
        _sliderImages = GetComponentsInChildren<Image>();
    }

    protected override void Start()
    {
        base.Start();

        foreach(var image in _sliderImages)
        {
            image.DOColor(new Color(1.0f, 1.0f, 1.0f, 0.0f), _fadeInDuration).From();
        }
    }
}
