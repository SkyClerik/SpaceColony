using UnityEngine;

namespace Gameplay
{
    [System.Serializable]
    public class Coast
    {
        [SerializeField]
        private int _price;

        public int Price => _price;
        //TODO: Чудесным образом описать цену объекта за любые плюшки  
    }
}