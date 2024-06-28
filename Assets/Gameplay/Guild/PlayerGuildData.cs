using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "PlayerGuildData", menuName = "Gameplay/Data/PlayerGuildData")]
    public class PlayerGuildData : ScriptableObject
    {
        [SerializeField]
        private List<ActorData> _actorData;
    }
}