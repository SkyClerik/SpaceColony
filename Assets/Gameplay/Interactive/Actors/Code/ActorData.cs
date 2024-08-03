using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ActorData", menuName = "Gameplay/Data/ActorData")]
    public class ActorData : ScriptableObject
    {
        [SerializeField]
        private string _frendlyName;
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private ActorClass _class;
        [SerializeField]
        private bool _busy = false;
        [SerializeField]
        private int _experience = 0;
        [SerializeField]
        private int _level = 1;
        [SerializeField, Tooltip("Усталость")]
        private int _fatigue = 0;

        public Sprite Icon => _icon;
        public ActorClass Type => _class;
        public bool Busy
        {
            get => _busy;
            set => _busy = value;
        }
        public int Experience { get => _experience; set => _experience = value; }
        public int Level { get => _level; set => _level = value; }
        public int Fatigue { get => _fatigue; set => _fatigue = value; }
    }
}