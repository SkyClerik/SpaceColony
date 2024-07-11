using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AvatarLogic
{
    [CustomEditor(typeof(AvatarStateEnumBuilder))]
    public class AvatarStateEnumBuilderEditor : Editor
    {
        private AvatarStateEnumBuilder _target;
        private string _resultText;
        private string _objectsNames;

        private void OnEnable()
        {
            _target = target as AvatarStateEnumBuilder;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_target == null)
                return;

            if (_target.TextAsset == null)
                return;

            if (GUILayout.Button("Обновить перечисление"))
            {
                _objectsNames = "";

                for (int i = 0; i < _target.States.Count; i++)
                {
                    var name = _target.States[i].name.Replace(" ", "");
                    _objectsNames += $"{name} = {i},\n";
                }
                _resultText = $"public enum AvatarStateID : byte\r\n{{\n{_objectsNames}}}";

                string filePath = AssetDatabase.GetAssetPath(_target.TextAsset);
                File.WriteAllText(filePath, _resultText);
            }

            if (GUILayout.Button("Назначить автоматически"))
            {
                for (int i = 0; i < _target.States.Count; i++)
                {
                    foreach (var id in Enum.GetValues(typeof(AvatarStateID)))
                    {
                        var name = _target.States[i].name.Replace(" ", "");
                        if (id.ToString() == name)
                        {
                            _target.States[i].stateID = (AvatarStateID)id;
                            EditorUtility.SetDirty(_target.States[i]);
                        }
                    }
                }
            }

            EditorUtility.SetDirty(_target);
        }
    }
}