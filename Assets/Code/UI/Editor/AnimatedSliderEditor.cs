using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(AnimatedSlider))]
public class AnimatedSliderEditor : SliderEditor
{
    private SerializedProperty _animationType;
    private SerializedProperty _fadeInSetting;
    private SerializedProperty _startPosition;
    private SerializedProperty _moveDuration;
    private SerializedProperty _interactable;
    private SerializedProperty _minValue;
    private SerializedProperty _maxValue;
    private SerializedProperty _wholeNumbers;

    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        var animationType = new PropertyField(_animationType);
        var fadeInDuration = new PropertyField(_fadeInSetting);
        var startPosition = new PropertyField(_startPosition);
        var moveDuration = new PropertyField(_moveDuration);
        var interactable = new PropertyField(_interactable);
        var minValue = new PropertyField(_minValue);
        var maxValue = new PropertyField(_maxValue);
        var wholeNumbers = new PropertyField(_wholeNumbers);

        root.Add(animationType);
        root.Add(fadeInDuration);
        root.Add(startPosition);
        root.Add(moveDuration);
        root.Add(interactable);
        root.Add(minValue);
        root.Add(maxValue);
        root.Add(wholeNumbers);

        return root;
    }

    public override bool RequiresConstantRepaint()
    {
        return true;
    }

    protected override void OnEnable()
    {
        _animationType = serializedObject.FindProperty(AnimatedSlider.SliderAnimationType);
        _fadeInSetting = serializedObject.FindProperty(AnimatedSlider.FadeInDuration);
        _startPosition = serializedObject.FindProperty(AnimatedSlider.StartPosition);
        _moveDuration = serializedObject.FindProperty(AnimatedSlider.MoveDuration);
        _interactable = serializedObject.FindProperty("m_Interactable");
        _minValue = serializedObject.FindProperty("m_MinValue");
        _maxValue = serializedObject.FindProperty("m_MaxValue");
        _wholeNumbers = serializedObject.FindProperty("m_WholeNumbers");
    }
}
