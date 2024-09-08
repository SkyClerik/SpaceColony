using UnityEditor;
using UnityEngine;

namespace Helper
{
    [CustomEditor(typeof(MaterialReplacer))]
    public class MaterialReplacerEditor : Editor
    {
        private MaterialReplacer _sceneObjectsMaterialReplacer;

        private void OnEnable()
        {
            _sceneObjectsMaterialReplacer = (MaterialReplacer)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("������� ����� � ����"))
            {
                _sceneObjectsMaterialReplacer.AddChildrenToList();
            }

            if (_sceneObjectsMaterialReplacer.GetMaterial == null)
                return;

            if (GUILayout.Button("�������� ���������"))
            {
                _sceneObjectsMaterialReplacer.ReplaceMaterial();
            }
        }
    }
}