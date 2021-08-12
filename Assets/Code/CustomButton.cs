using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CustomButton : Button
{
    private Transform _buttonTransform;
    private const float _scaleDuration = 1.0f;
    private readonly Vector3 _scaleBigValue = new Vector3(1.5f, 1.5f, 1.0f);
    private readonly Vector3 _scaleSmallValue = new Vector3(1.0f, 1.0f, 1.0f);

    protected override void Awake()
    {
        _buttonTransform = GetComponent<Transform>();
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        base.OnPointerEnter(data);
        _buttonTransform.DOScale(_scaleBigValue, _scaleDuration);
    }

    public override void OnPointerExit(PointerEventData data)
    {
        base.OnPointerExit(data);
        _buttonTransform.DOScale(_scaleSmallValue, _scaleDuration);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        DOTween.KillAll();
    }
}
