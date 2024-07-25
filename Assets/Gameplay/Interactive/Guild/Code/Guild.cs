using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Guild : Singleton<Guild>
    {
        public static event Action<int> Reputation—hange;

        [SerializeField]
        private Transform _parkingPosition;
        [SerializeField]
        private int _reputation;
        [SerializeField]
        private List<ActorData> _currentJobActors = new List<ActorData>();

        public Transform ParkingPosition => _parkingPosition;
        public int GetReputation => _reputation;

        private void Start()
        {
            for (int i = 0; i < _currentJobActors.Count; i++)
            {
                _currentJobActors[i] = Instantiate(_currentJobActors[i]);
            }
            HUDUserInterface.Instance.RepaintCalls(_currentJobActors);
        }

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