using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "GuildData", menuName = "Gameplay/Data/GuildData")]
    public class GuildData : ScriptableObject
    {
        public List<ActorData> ActorData;
    }
}