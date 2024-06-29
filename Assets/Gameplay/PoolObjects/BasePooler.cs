using System.Collections.Generic;
using UnityEngine;

public class BasePooler : Singleton<BasePooler>
{
    [SerializeField]
    private List<ObjectInfo> _bulletObjects;
    private Dictionary<string, BaseObjectPool> _bulletPool = new();

    private void Start()
    {
        foreach (var obj in _bulletObjects)
            _bulletPool.Add(obj.GetPrefab.name, new BaseObjectPool(obj.GetAmount, obj.GetPrefab));
    }

    public GameObject Get(string id)
    {
        if (_bulletPool.TryGetValue(id, out BaseObjectPool carPool))
            return carPool.Get();

        return null;
    }

    public GameObject GetRandom(IPoolObject type)
    {
        string[] myShufledKeys;
        BaseObjectPool myShufledValue = null;

        //Debug.LogError($"type : {type.GetType()}");

        if (type.ToString() is "Bullet")
        {
            myShufledKeys = _bulletPool.Shuffle();
            myShufledValue = _bulletPool[myShufledKeys[0]];
        }

        return myShufledValue.Get();
    }
}

[System.Serializable]
public class BaseObjectPool
{
    private GameObject _default;
    public List<GameObject> Objects = new List<GameObject>();

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
public struct ObjectInfo
{
    [SerializeField]
    private GameObject _prefab;
    private string _id;
    [SerializeField]
    private int _amount;

    public GameObject GetPrefab => _prefab;
    public string GetId => _id;
    public int GetAmount => _amount;
}