using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnumBuilder))]
public class EnumBuilderEditor : Editor
{
    private EnumBuilder _target;

    private string _allText;
    private string _stateNames;


    private void OnEnable()
    {
        _target = target as EnumBuilder;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Обновить перечисление"))
        {
            _stateNames = "";

            for (int i = 0; i < _target.States.Count; i++)
            {
                _stateNames += $"{_target.States[i].name} = {i},\n";
            }
            _allText = $"public enum AvatarStateID : byte\r\n{{\n{_stateNames}}}";

            string filePath = AssetDatabase.GetAssetPath(_target.TextAsset);
            File.WriteAllText(filePath, _allText);
        }

        if (GUILayout.Button("Назначить автоматически"))
        {
            for (int i = 0; i < _target.States.Count; i++)
            {
                _target.States[i].stateID = (AvatarStateID)i;
            }
        }

        EditorUtility.SetDirty(_target);
        serializedObject.ApplyModifiedProperties();
    }
}