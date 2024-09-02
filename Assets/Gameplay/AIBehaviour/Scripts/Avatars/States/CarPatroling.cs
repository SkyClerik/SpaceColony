using Behavior;
using SkyClericExt;
using UnityEngine;
using UnityEngine.AI;

namespace Car.State
{
    public class CarPatrolling : ICarState
    {
        [SerializeField]
        private float _patrollingRadius;

        private CarBehavior _carBehavior;
        private CarStateMachine _machine;
        private Vector3 _currentPoint;
        private Vector3 _startPosition;
        private Vector3 _nextPosition;
        private int _currentCornerIndex;
        private float _currentDistance;

        private Vector3 _randomDirection;
        private NavMeshHit _navMeshHit;
        private Vector3 _finalPosition;
        private int _areaMask = 1;

        private const int _valueZero = 0;
        private const int _valueOne = 1;

        public void Init(CarBehavior carBehavior, CarStateMachine machine)
        {
            _carBehavior = carBehavior;
            _machine = machine;
            _startPosition = carBehavior.transform.position;
        }

        public void Start()
        {
            SetNewRandomPosition();
        }

        public void Update() => MoveTo();

        private void MoveTo()
        {
            if (DistanceLessStoppingDistance() == true)
            {
                SetNewRandomPosition();
                _carBehavior.NavMeshAgent.SetDestination(_currentPoint);
                return;
            }

            _currentCornerIndex = _carBehavior.NavMeshAgent.path.corners.Length > _valueOne ? _valueOne : _valueZero;
            _nextPosition = _carBehavior.NavMeshAgent.path.corners[_currentCornerIndex];
            _carBehavior.SetRotation(_nextPosition, _carBehavior.NavMeshAgent.angularSpeed);
        }

        private bool DistanceLessStoppingDistance()
        {
            _currentDistance = (_carBehavior.NavMeshAgent.destination - _carBehavior.transform.position).magnitude;
            if (_currentDistance <= _carBehavior.NavMeshAgent.stoppingDistance)
                return true;

            return false;
        }

        private void SetNewRandomPosition()
        {
            NavMeshActive();
            SetTarget();
        }

        private void NavMeshActive()
        {
            _carBehavior.NavMeshAgent.enabled = true;
            _carBehavior.NavMeshAgent.isStopped = false;
        }

        private void SetTarget()
        {
            _currentPoint = RandomNaveMeshLocation(_patrollingRadius);
            _carBehavior.NavMeshAgent.SetDestination(_currentPoint);
        }

        public Vector3 RandomNaveMeshLocation(float radius)
        {
            _randomDirection = Random.insideUnitSphere * radius;
            _randomDirection += _carBehavior.transform.position;
            _finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(_randomDirection, out _navMeshHit, radius, _areaMask))
                _finalPosition = _navMeshHit.position;

            var fce = FindClosesEdge();
            if (fce)
                _finalPosition = RandomNaveMeshLocation(radius);

            return _finalPosition;
        }

        private bool FindClosesEdge()
        {
            if (NavMesh.FindClosestEdge(_carBehavior.NavMeshAgent.destination, out _, NavMesh.AllAreas) == false)
                return true;

            return false;
        }
    }
}