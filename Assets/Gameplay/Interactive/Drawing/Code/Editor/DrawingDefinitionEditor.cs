using Gameplay.Data;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(DrawingDefinition))]
public class DrawingDefinitionEditor : Editor
{
    private DrawingDefinition _target;

    private void OnEnable()
    {
        _target = target as DrawingDefinition;
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