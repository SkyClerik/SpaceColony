using SkyClerikExt;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarLogic
{
    internal class Patroling : IStateBase
    {
        private AvatarBehaviour _avatarData;
        private Vector3 _currentPoint;
        private Vector3 _startPosition;
        private float _waypointSearchRadius;
        private Vector3 _nextPosition;
        private int _currentCornerIndex;
        private GameObject _nextPoint;
        private GameObject _endPoint;

        private Vector3 _randomDirection;
        private NavMeshHit _navMeshHit;
        private Vector3 _finalPosition;
        private int _areaMask = 1;

        public Patroling(AvatarBehaviour avatarData, float waypointSearchRadius, GameObject nextPoint, GameObject endPoint)
        {
            _avatarData = avatarData;
            _waypointSearchRadius = waypointSearchRadius;
            _startPosition = avatarData.transform.position;
            _nextPoint = nextPoint;
            _endPoint = endPoint;
        }

        public bool Previously()
        {
            SetNewRandomPosition();
            return true;
        }

        public void Update()
        {
            MoveTo();
        }

        private void MoveTo()
        {
            float distance = (_avatarData.transform.position - _currentPoint).magnitude;
            if (distance <= _avatarData.NavMeshAgent.stoppingDistance)
            {
                SetNewRandomPosition();
                _avatarData.NavMeshAgent.SetDestination(_currentPoint);
                return;
            }

            _currentCornerIndex = _avatarData.NavMeshAgent.path.corners.Length > 1 ? 1 : 0;
            _nextPosition = _avatarData.NavMeshAgent.path.corners[_currentCornerIndex];
            _nextPoint.transform.position = _nextPosition;
            _avatarData.SetRotation(_nextPosition, _avatarData.NavMeshAgent.angularSpeed);
        }

        private void SetNewRandomPosition()
        {
            NavMeshActive();
            SetTarget();
        }

        private void SetTarget()
        {
            _currentPoint = RandomNavmeshLocation(_waypointSearchRadius);
            _avatarData.NavMeshAgent.SetDestination(_currentPoint);
            _endPoint.transform.position = _currentPoint;
        }

        private void NavMeshActive()
        {
            _avatarData.NavMeshAgent.enabled = true;
            _avatarData.NavMeshAgent.isStopped = false;
        }
        public Vector3 RandomNavmeshLocation(float radius)
        {
            _randomDirection = Random.insideUnitSphere * radius;
            _randomDirection += _avatarData.transform.position;
            _finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(_randomDirection, out _navMeshHit, radius, _areaMask))
                _finalPosition = _navMeshHit.position;


            return _finalPosition;
        }
    }
}