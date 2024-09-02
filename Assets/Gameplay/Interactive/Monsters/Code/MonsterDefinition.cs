using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "New MonsterDefinition", menuName = "Gameplay/Data/MonsterDefinition")]
    public class MonsterDefinition : CharacterBase
    {

    }

    [System.Serializable]
    public class MonsterDungeonInfo
    {
        [SerializeField]
        private MonsterDefinition _monster;
        public MonsterDefinition GetMonster => _monster;
        public MinMaxValues PCS;
    }
}