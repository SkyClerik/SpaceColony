using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
    public class NavMeshHelper : MonoBehaviour
    {
        [Header("�������� �������� � ��������� ������� ����� ��� ��������� ����.")]
        [SerializeField]
        private List<GameObject> gameObjects = new List<GameObject>();

        public void SetActiveObjects(bool active)
        {
            foreach (GameObject obj in gameObjects)
            {
                obj?.SetActive(active);
            }
        }
    }
}