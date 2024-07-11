using System.Collections.Generic;
using UnityEngine;

namespace AvatarLogic
{
    [CreateAssetMenu(fileName = "AvatarStateEnumBuilder", menuName = "Enum/AvatarStateEnumBuilder")]
    public class AvatarStateEnumBuilder : ScriptableObject
    {
        [SerializeField]
        private TextAsset _textAsset;

        [SerializeField]
        private List<StateBase> _states = new List<StateBase>();

        public TextAsset TextAsset => _textAsset;
        public List<StateBase> States => _states;
    }
}