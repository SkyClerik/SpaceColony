using UnityEngine;

namespace AvatarLogic
{
    [CreateAssetMenu(fileName = "Idle", menuName = "AI/State/Idle")]
    public class Idle : StateBase
    {
        private CarBehaviour _avatarBehaviour;

        public override void Init(CarBehaviour avatarBehaviour)
        {
            _avatarBehaviour = avatarBehaviour;
        }

        public override void Start()
        {

        }

        public override void Update()
        {

        }
    }
}