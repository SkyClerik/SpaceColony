using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Gameplay/Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField]
        private string _playerName;
    }
}