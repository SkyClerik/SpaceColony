using System;
using UnityEngine;

namespace Gameplay
{
    public class Guild : Singleton<Guild>
    {
        [SerializeField]
        private Transform _parkingPosition;
        [SerializeField]
        private int _reputation;

        public Transform ParkingPosition => _parkingPosition;
        public int GetReputation => _reputation;

        public static event Action<int> Reputation—hange;

        public void AddReputation(int value)
        {
            _reputation += value;
            Reputation—hange?.Invoke(_reputation);
        }

        private void OnMouseDown()
        {
            GuildUserInterface.Instance.View();
        }
    }
}