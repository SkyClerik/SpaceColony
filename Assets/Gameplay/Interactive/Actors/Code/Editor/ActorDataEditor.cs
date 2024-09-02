using Gameplay.Data;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(ActorDefinition))]
public class ActorDataEditor : Editor
{
    private ActorDefinition _target;

    private void OnEnable()
    {
        _target = target as ActorDefinition;
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