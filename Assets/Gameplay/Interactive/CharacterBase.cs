using UnityEngine;

namespace Gameplay.Data
{
    public class CharacterBase : ObjectBase
    {
        [SerializeField]
        private int _level = 1;
        [SerializeField]
        private CharacterStats _stats;
        [SerializeField]
        private Coast _coast;

        public int Level { get => _level; set => _level = value; }
        public CharacterStats Stats => _stats;
        public Coast Coast => _coast;

        public override string ToString()
        {
            return $"�������: {Level}\n����: {Coast.Price}\n";
        }
    }
}