using UnityEngine;

namespace AvatarLogic
{
    public abstract class StateBase : ScriptableObject
    {
        public AvatarStateID stateID;
        public abstract void Init(CarBehaviour avatarBehaviour);
        public abstract void Start();
        public abstract void Update();
    }
}