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
        private ActorType _type;
        [SerializeField]
        private bool _busy = false;

        public Sprite Icon => _icon;
        public ActorType Type => _type;
        public bool Busy
        {
            get => _busy;
            set => _busy = value;
        }
    }
}