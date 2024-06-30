using UnityEngine;

namespace PoolObjectSystem
{
    public class PoolObject : MonoBehaviour, IPoolObject
    {
        public void SetActiveObject(bool active)
        {
            //Debug.Log($"Создан объект");
        }
    }
}