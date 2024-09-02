using Gameplay;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(PlayerBuildsContainer))]
public class PlayerBuildsContainerEditor : Editor
{
    private PlayerBuildsContainer _container;

    private void OnEnable()
    {
        _container = target as PlayerBuildsContainer;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        InspectorElement.FillDefaultInspector(root, serializedObject, this);


        foreach (var obj in _container.GetBuildBehavior)
        {
            Label element = new Label();

            element.text = $"{obj.Value.GetBuildInfo.Title}";
            root.Add(element);
        }

        return root;
    }
}
