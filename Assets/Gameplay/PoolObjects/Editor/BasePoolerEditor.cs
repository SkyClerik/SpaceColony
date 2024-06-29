using UnityEditor;
using UnityEngine;

namespace PoolObjectSystem
{
    [CustomEditor(typeof(BasePooler))]
    public class BasePoolerEditor : Editor
    {
        private BasePooler _target;

        private void OnEnable()
        {
            _target = target as BasePooler;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_target == null)
                return;

            if (GUILayout.Button("Назначить автоматически"))
            {
                for (int i = 0; i < _target.GetPrefabs.Count; i++)
                {
                    _target.GetPrefabs[i].SetPoolID((byte)i);
                }
            }

            EditorUtility.SetDirty(_target);
        }
    }
}