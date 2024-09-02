using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Gameplay.Data;

[CustomEditor(typeof(PetDefinition))]
public class PetDefinitionEditor : Editor
{
    private PetDefinition _target;

    private void OnEnable()
    {
        _target = target as PetDefinition;
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