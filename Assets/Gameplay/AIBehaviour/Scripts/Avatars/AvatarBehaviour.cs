using UnityEngine;
using UnityEngine.AI;

namespace AvatarLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AvatarBehaviour : MonoBehaviour
    {
        [SerializeField]
        private int _curentMoveSpeed;
        [SerializeField]
        private int _maximumMoveSpeed;

        private StateMashine _stateMashine;
        private NavMeshAgent _navMeshAgent;

        public StateMashine StateMashine
        {
            get => _stateMashine;
            set => _stateMashine = value;
        }

        public NavMeshAgent NavMeshAgent
        {
            get => _navMeshAgent;
            set => _navMeshAgent = value;
        }

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = _curentMoveSpeed;
            _navMeshAgent.updateRotation = false;
        }
    }
}