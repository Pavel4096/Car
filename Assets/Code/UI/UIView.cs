using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Slider _slider;

    public Slider Slider => _slider;
    public Text Text => _text;
}
