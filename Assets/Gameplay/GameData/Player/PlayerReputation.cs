using System;
using UnityEngine;

namespace Gameplay
{
    public class PlayerReputation : MonoBehaviour
    {
        public static event Action<int> ReputationChange;

        [SerializeField]
        private static int _reputation;

        public static int GetReputation => _reputation;

        public static void AddReputation(int value)
        {
            _reputation += value;
            ReputationChange?.Invoke(_reputation);
        }
    }
}