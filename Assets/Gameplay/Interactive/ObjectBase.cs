using UnityEngine;

namespace Gameplay
{
    public class ObjectBase : ScriptableObject
    {
        [SerializeField]
        private string _friendlyName;
        [SerializeField]
        private string _description;
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private int _minPCS;
        [SerializeField]
        private int _maxPCS;

        public string Description => _description;
        public string FriendlyName => _friendlyName;
        public Sprite Icon => _icon;
        public int MinPCS => _minPCS;
        public int MaxPCS => _maxPCS;
    }
}