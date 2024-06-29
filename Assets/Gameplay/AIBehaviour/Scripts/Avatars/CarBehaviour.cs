using Gameplay;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarLogic
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CarBehaviour : MonoBehaviour
    {
        [SerializeField]
        private StateMashine _stateMashine = new StateMashine();
        [SerializeField]
        private Transform _destination;

        private NavMeshAgent _navMeshAgent;
        private bool _busy;
        private Quest _questInProcess;

        public bool Busy => _busy;
        public NavMeshAgent NavMeshAgent { get => _navMeshAgent; set => _navMeshAgent = value; }
        public Transform MoveDestination { get => _destination; set => _destination = value; }

        private void Awake()
        {
            gameObject.transform.parent = null;

            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
        }

        private void Start()
        {
            _stateMashine.Start(gameObject);
        }

        private void Update()
        {
            _stateMashine.Update();
        }

        private void MoveToPoint(Transform targetPosition)
        {
            _busy = true;
            _destination = targetPosition;
            _stateMashine.Start(gameObject);
            _stateMashine.SetState(stateID: AvatarStateID.MoveToPoint);
        }

        public void MoveToQuest(Quest quest, Transform targetPosition)
        {
            ResetCar(Guild.Instance.ParkingPosition);
            _questInProcess = quest;
            MoveToPoint(targetPosition);
        }

        public void MoveToGuild()
        {
            ResetCar(_questInProcess.ParkingPosition);
            _questInProcess.MissionFinished();
            _questInProcess = null;
            MoveToPoint(Guild.Instance.ParkingPosition);
        }

        public void EndMoveToPoin()
        {
            if (_questInProcess)
                MoveToGuild();
            else
                Finished();
        }

        public void Finished()
        {
            _busy = false;
            Debug.Log($"������ ��������� �� ����", gameObject);
            gameObject.SetActive(false);
        }

        private void ResetCar(Transform startingPoint)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = startingPoint.position;
        }
    }
}