using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Controller : IDisposable
{
    private UIView _uiview;
    private Text _uitext;
    private RectTransform _uitextTransform;

    private readonly Vector3 strength = new Vector3(10.0f, 0.0f, 0.0f);
    private readonly float duration = 0.5f;

    public Controller(UIView uiview)
    {
        _uiview = uiview;
        Setup();
    }

    public void Dispose()
    {
        _uiview.Slider.onValueChanged.RemoveAllListeners();
    }

    private void Setup()
    {
        _uitext = _uiview.Text;
        _uitextTransform = _uiview.Text.GetComponent<RectTransform>();
        _uiview.Slider.onValueChanged.AddListener(ValueChanged);
        _uitext.DOColor(new Color(1.0f, 1.0f, 1.0f, 0.0f), 1.0f).From();
    }

    private void ValueChanged(float value)
    {
        _uitext.text = $"Value: {value}";
        DOTween.Kill(_uitextTransform);
        _uitextTransform.DOShakeAnchorPos(duration, strength, 10, 25, false, false);
    }
}
