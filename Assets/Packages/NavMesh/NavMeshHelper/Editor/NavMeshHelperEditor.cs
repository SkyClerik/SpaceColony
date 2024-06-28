using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NavMeshHelper))]
public class NavMeshHelperEditor : Editor
{
    private NavMeshHelper _navMeshHelper;

    private void OnEnable()
    {
        _navMeshHelper = (NavMeshHelper)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Active"))
        {
            _navMeshHelper.SetActiveObjects(true);
        }

        if (GUILayout.Button("DeActive"))
        {
            _navMeshHelper.SetActiveObjects(false);
        }
    }
}
