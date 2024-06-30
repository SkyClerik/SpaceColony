using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemDefinition))]
public class ItemDefinitionEditor : Editor
{
    private ItemDefinition _itemDefinition;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _itemDefinition = (ItemDefinition)target;
        GUILayout.Box(_itemDefinition.Icon.texture);
    }
}