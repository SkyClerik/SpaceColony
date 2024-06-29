using AvatarLogic;
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
        foreach (var car in _cars)
        {
            if (car.Busy)
                continue;

            carBehaviour = car;
            return true;
        }

        carBehaviour = null;
        return false;
    }
}