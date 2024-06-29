using AvatarLogic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[CustomEditor(typeof(CarController))]
public class CarControllerEditor : Editor
{
    private CarController _target;

    private void OnEnable()
    {
        _target = target as CarController;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Детей в лист"))
        {
            _target.Cars.Clear();
            _target.Cars = _target.gameObject.transform.GetComponentsInChildren<CarBehaviour>().ToList();
        }

        if (GUILayout.Button("Упорядочить детям приоритет NavMesh"))
        {
            for (int i = 0; i < _target.Cars.Count; i++)
            {
                if (_target.Cars[i].TryGetComponent(out NavMeshAgent navMeshAgent))
                {
                    navMeshAgent.avoidancePriority = i + 20;
                }
            }
        }
    }
}