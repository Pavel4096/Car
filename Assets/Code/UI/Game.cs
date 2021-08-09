using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Game : MonoBehaviour
{
    [SerializeField]
    private UIView _uiview;

    private Controller _controller;

    private void Start()
    {
        _controller = new Controller(_uiview);
    }

    private void OnDestroy()
    {
        _controller.Dispose();
    }
}
