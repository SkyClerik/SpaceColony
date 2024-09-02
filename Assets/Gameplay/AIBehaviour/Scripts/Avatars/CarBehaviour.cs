using Car.State;
using Gameplay;
using Gameplay.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Behavior
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CarBehavior : MonoBehaviour
    {
        private CarStateMachine _stateMachine;
        private NavMeshAgent _navMeshAgent;
        private DungeonEvents _dungeonEvents;
        private Transform _destination;

        public NavMeshAgent NavMeshAgent { get => _navMeshAgent; set => _navMeshAgent = value; }
        public Transform GetDestination=> _destination;

        private void Awake()
        {
            gameObject.transform.parent = null;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _stateMachine = new CarStateMachine(carBehavior: this);
            _dungeonEvents = DungeonEvents.Instance;
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public void MoveToPoint(Vector3 startingPosition, Transform destination, DungeonBehavior dungeonBehavior)
        {
            gameObject.transform.position = startingPosition;
            _destination = destination;
            gameObject.SetActive(true);

            _navMeshAgent.SetDestination(_destination.position);
            _stateMachine.SetState(_stateMachine.StateCarMoveToPoint);

            WorldBillboardsPage.Instance.CarBillboardsShow(dungeonBehavior: dungeonBehavior, timerTime: GetPathTime());
        }

        private System.TimeSpan GetPathTime()
        {
            var gpl = GetPathLength();
            double x = (gpl / _navMeshAgent.speed);
            return System.TimeSpan.FromSeconds(x);
        }

        private float GetPathLength()
        {
            float pathLength = 0;
            var path = new NavMeshPath();
            if (_navMeshAgent.CalculatePath(_navMeshAgent.destination, path))
            {
                for (int i = 0; i < path.corners.Length - 1; i++)
                    pathLength += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            else
                Invoke(nameof(GetPathLength), 1);

            return pathLength;
        }
    }
}