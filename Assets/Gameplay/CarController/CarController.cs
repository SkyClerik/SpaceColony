using AvatarLogic;
using PoolObjectSystem;
using System.Collections.Generic;
using UnityEngine;

public class CarController : Singleton<CarController>
{
    [SerializeField]
    private List<CarBehaviour> _cars = new List<CarBehaviour>();

    public List<CarBehaviour> Cars
    {
        get { return _cars; }
        set { _cars = value; }
    }

    public bool TryFindFreeCar(out CarBehaviour carBehaviour)
    {
        var car = BasePooler.Instance.Get(PoolObjectID.Car);
        if (car.TryGetComponent(out carBehaviour))
        {
            return true;
        }

        return false;
    }
}