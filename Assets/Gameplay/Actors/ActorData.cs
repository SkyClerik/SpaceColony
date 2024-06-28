using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "ActorData", menuName = "Gameplay/Data/ActorData")]
    public class ActorData : ScriptableObject
    {
        [SerializeField]
        private string _frendlyName;
    }
}