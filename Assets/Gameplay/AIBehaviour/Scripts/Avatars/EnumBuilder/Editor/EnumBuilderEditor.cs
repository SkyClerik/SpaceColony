using System.IO;
using UnityEditor;
using UnityEngine;

namespace AvatarLogic
{
    [CustomEditor(typeof(EnumBuilder))]
    public class EnumBuilderEditor : Editor
    {
        private EnumBuilder _target;

        private string _resultText;
        private string _objectsNames;

        private void OnEnable()
        {
            _target = target as EnumBuilder;
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
                    _objectsNames += $"{_target.States[i].name} = {i},\n";
                }
                _resultText = $"public enum AvatarStateID : byte\r\n{{\n{_objectsNames}}}";

                string filePath = AssetDatabase.GetAssetPath(_target.TextAsset);
                File.WriteAllText(filePath, _resultText);
            }

            if (GUILayout.Button("Назначить автоматически"))
            {
                for (int i = 0; i < _target.States.Count; i++)
                {
                    EditorUtility.SetDirty(_target.States[i]);
                    _target.States[i].stateID = (AvatarStateID)i;
                }
            }

            EditorUtility.SetDirty(_target);
        }
    }
}