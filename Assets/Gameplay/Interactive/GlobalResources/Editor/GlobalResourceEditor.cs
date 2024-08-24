using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine.UIElements;
using Gameplay.Data;

[CustomEditor(typeof(GlobalResource))]
public class GlobalResourceEditor : Editor
{
    private GlobalResource _target;

    private void OnEnable()
    {
        _target = target as GlobalResource;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        InspectorElement.FillDefaultInspector(root, serializedObject, this);

        VisualElement element = new VisualElement();
        element.style.backgroundImage = new StyleBackground(_target.Icon);
        element.style.width = 100;
        element.style.height = 100;
        root.Add(element);

        return root;
    }
}