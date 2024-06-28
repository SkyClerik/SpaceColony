using System.Collections.Generic;
using UnityEngine;

public class NavMeshHelper : MonoBehaviour
{
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
