using SkyClerikExt;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarLogic
{
    [CreateAssetMenu(fileName = "Patroling", menuName = "AI/State/Patroling")]
    internal class Patroling : StateBase
    {
        [SerializeField]
        private float _patrolingRadius;

        private CarBehaviour _avatarBehaviour;
        private Vector3 _currentPoint;
        private Vector3 _startPosition;
        private Vector3 _nextPosition;
        private int _currentCornerIndex;
        private float _curentDistance;

        private Vector3 _randomDirection;
        private NavMeshHit _navMeshHit;
        private Vector3 _finalPosition;
        private int _areaMask = 1;

        private const int _valueZero = 0;
        private const int _valueOne = 1;

        public override void Init(CarBehaviour avatarBehaviour)
        {
            _avatarBehaviour = avatarBehaviour;
            _startPosition = avatarBehaviour.transform.position;
        }

        public override void Start()
        {
            SetNewRandomPosition();
        }

        public override void Update() => MoveTo();

        private void MoveTo()
        {
            if (DistanceLessStoppingDistance() == true)
            {
                SetNewRandomPosition();
                _avatarBehaviour.NavMeshAgent.SetDestination(_currentPoint);
                return;
            }

            _currentCornerIndex = _avatarBehaviour.NavMeshAgent.path.corners.Length > _valueOne ? _valueOne : _valueZero;
            _nextPosition = _avatarBehaviour.NavMeshAgent.path.corners[_currentCornerIndex];
            _avatarBehaviour.SetRotation(_nextPosition, _avatarBehaviour.NavMeshAgent.angularSpeed);
        }

        private bool DistanceLessStoppingDistance()
        {
            _curentDistance = (_avatarBehaviour.NavMeshAgent.destination - _avatarBehaviour.transform.position).magnitude;
            if (_curentDistance <= _avatarBehaviour.NavMeshAgent.stoppingDistance)
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
            _avatarBehaviour.NavMeshAgent.enabled = true;
            _avatarBehaviour.NavMeshAgent.isStopped = false;
        }

        private void SetTarget()
        {
            _currentPoint = RandomNavmeshLocation(_patrolingRadius);
            _avatarBehaviour.NavMeshAgent.SetDestination(_currentPoint);
        }

        public Vector3 RandomNavmeshLocation(float radius)
        {
            _randomDirection = Random.insideUnitSphere * radius;
            _randomDirection += _avatarBehaviour.transform.position;
            _finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(_randomDirection, out _navMeshHit, radius, _areaMask))
                _finalPosition = _navMeshHit.position;

            var fce = FindClosesEdge();
            if (fce)
                _finalPosition = RandomNavmeshLocation(radius);

            return _finalPosition;
        }

        private bool FindClosesEdge()
        {
            if (NavMesh.FindClosestEdge(_avatarBehaviour.NavMeshAgent.destination, out _, NavMesh.AllAreas) == false)
                return true;

            return false;
        }
    }
}