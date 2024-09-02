using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "òóö PetDefinition", menuName = "Gameplay/Data/PetDefinition")]
    public class PetDefinition : CharacterBase
    {
        [SerializeField]
        private int _experience = 0;

        public int Experience { get => _experience; set => _experience = value; }
    }

    [System.Serializable]
    public class TrophyPet
    {
        public PetDefinition Pet;
        [Range(0f, 100f)]
        public byte Chance;
    }
}