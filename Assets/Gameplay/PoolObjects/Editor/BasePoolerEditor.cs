using System;
using UnityEditor;
using UnityEngine;

namespace PoolObjectSystem
{
    [CustomEditor(typeof(Pool))]
    public class BasePoolerEditor : Editor
    {
        private Pool _target;

        private void OnEnable()
        {
            _target = target as Pool;
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
                    foreach (var id in Enum.GetValues(typeof(PoolObjectID)))
                    {
                        var name = _target.GetPrefabs[i].GetPrefab.name.Replace(" ", "");
                        if (id.ToString() == name)
                            _target.GetPrefabs[i].SetPoolID((PoolObjectID)id);
                    }
                }
            }

            EditorUtility.SetDirty(_target);
        }
    }
}