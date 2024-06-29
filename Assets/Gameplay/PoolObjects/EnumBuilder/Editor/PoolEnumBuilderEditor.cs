using System.IO;
using UnityEditor;
using UnityEngine;

namespace PoolObjectSystem
{
    [CustomEditor(typeof(PoolEnumBuilder))]
    public class PoolEnumBuilderEditor : Editor
    {
        private PoolEnumBuilder _target;

        private string _allText;
        private string _objectsNames;


        private void OnEnable()
        {
            _target = target as PoolEnumBuilder;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Обновить перечисление"))
            {
                _objectsNames = "";

                for (int i = 0; i < _target.GameObjects.Count; i++)
                {
                    _objectsNames += $"{_target.GameObjects[i].name} = {i},\n";
                }
                _allText = $"public enum PoolObjectID : byte\r\n{{\n{_objectsNames}}}";

                string filePath = AssetDatabase.GetAssetPath(_target.TextAsset);
                File.WriteAllText(filePath, _allText);
            }

            EditorUtility.SetDirty(_target);
        }
    }
}