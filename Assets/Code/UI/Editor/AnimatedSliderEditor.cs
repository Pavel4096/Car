using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(AnimatedSlider))]
public class AnimatedSliderEditor : SliderEditor
{
    private SerializedProperty fadeInSetting;
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        var fadeInDuration = new PropertyField(fadeInSetting);

        root.Add(fadeInDuration);

        return root;
    }

    protected override void OnEnable()
    {
        fadeInSetting = serializedObject.FindProperty(AnimatedSlider.FadeInDuration);
    }
}
