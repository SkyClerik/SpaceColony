using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using Gameplay.UI;

[CustomEditor(typeof(DungeonPage))]
public class DungeonPageEditor : Editor
{
    private DungeonPage _dungeon;

    private void OnEnable()
    {
        _dungeon = target as DungeonPage;
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        InspectorElement.FillDefaultInspector(root, serializedObject, this);

        Button element = new Button();
        element.style.width = 100;
        element.style.height = 30;
        element.text = "DepthOfField";
        element.clicked += Element_clicked;
        root.Add(element);

        return root;
    }

    private void Element_clicked()
    {
        if (_dungeon.GetVolumeManager.TryGetDepthOfField(out DepthOfField depthOfField))
        {
            depthOfField.active = !depthOfField.active;
            EditorUtility.SetDirty(_dungeon);
        }
    }
}