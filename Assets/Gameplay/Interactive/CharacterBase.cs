using UnityEngine;

namespace Gameplay
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
    }
}