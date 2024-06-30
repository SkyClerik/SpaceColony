using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PoolObjectSystem
{
    public class Pool : Singleton<Pool>
    {
        [SerializeField]
        private List<ObjectInfo> _prefabs;
        private Dictionary<PoolObjectID, BaseObjectPool> _objectPool = new();

        public List<ObjectInfo> GetPrefabs => _prefabs;

        private void Start()
        {
            foreach (var obj in _prefabs)
                _objectPool.Add(obj.GetPoolID, new BaseObjectPool(obj.GetAmount, obj.GetPrefab));
        }

        public GameObject Get(PoolObjectID poolObjectID)
        {
            if (_objectPool.TryGetValue(poolObjectID, out BaseObjectPool carPool))
                return carPool.Get();

            return null;
        }
    }

    [System.Serializable]
    public class BaseObjectPool
    {
        private GameObject _default;
        public List<GameObject> Objects = new List<GameObject>();
        public int unicValue = 0;

        public BaseObjectPool(int count, GameObject prefab)
        {
            _default = prefab;
            for (int i = 0; i < count; i++)
                Objects.Add(CreateNewObject());
        }

        public GameObject CreateNewObject()
        {
            var obj = GetInstantiate(_default);
            obj.GetComponent<IPoolObject>().SetActiveObject(false);
            Objects.Add(obj);
            return obj;
        }

        private GameObject GetInstantiate(GameObject prefab)
        {
            var obj = Object.Instantiate(prefab);
            obj.name = prefab.name;
            obj.SetActive(false);

            if (obj.TryGetComponent(out NavMeshAgent navMeshAgent))
                navMeshAgent.avoidancePriority = unicValue++;

            return obj;
        }

        public GameObject Get()
        {
            foreach (var go in Objects)
            {
                if (go.activeInHierarchy == false)
                {
                    go.transform.parent = null;
                    return go;
                }
            }
            var newGO = CreateNewObject();
            newGO.transform.parent = null;
            return newGO;
        }
    }

    [System.Serializable]
    public class ObjectInfo
    {
        [SerializeField]
        private GameObject _prefab;
        [SerializeField]
        private PoolObjectID _id;
        [SerializeField]
        private int _amount;

        public GameObject GetPrefab => _prefab;
        public PoolObjectID GetPoolID => _id;
        public int GetAmount => _amount;
        public void SetPoolID(PoolObjectID i) => _id = i;
    }
}