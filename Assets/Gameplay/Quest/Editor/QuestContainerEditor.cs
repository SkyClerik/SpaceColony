using Gameplay;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestContainer))]
public class QuestContainerEditor : Editor
{
    private QuestContainer _target;

    private void OnEnable()
    {
        _target = target as QuestContainer;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (_target == null)
            return;

        if (GUILayout.Button("Детей в лист"))
        {
            _target.SetQuests(_target.GetComponentsInChildren<Quest>().ToList());
        }

        EditorUtility.SetDirty(_target);
    }
}
