using SkyClerikExt;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarLogic
{
    [CreateAssetMenu(fileName = "MoveToPoint", menuName = "AI/State/MoveToPoint")]
    internal class MoveToPoint : StateBase
    {
        private CarBehaviour _carBehaviour;

        private Vector3 _nextPosition;
        private int _currentCornerIndex;

        private const int _valueZero = 0;
        private const int _valueOne = 1;

        public override void Init(CarBehaviour carBehaviour)
        {
            _carBehaviour = carBehaviour;
        }

        public override void Start()
        {
            _carBehaviour.NavMeshAgent.SetDestination(_carBehaviour.MoveDestination.position);
        }

        public override void Update()
        {
            if (_carBehaviour.NavMeshAgent.enabled == false)
                return;

            MoveToTargetOrStop();
            FindClosesEdge();
        }

        private void FindClosesEdge()
        {
            if (NavMesh.FindClosestEdge(_carBehaviour.NavMeshAgent.destination, out NavMeshHit hit, NavMesh.AllAreas) == false)
            {
                MoveEnd();
            }
        }

        private void MoveToTargetOrStop()
        {
            if (DistanceLessStoppingDistance() == true)
            {
                MoveEnd();
            }
            else
            {
                _currentCornerIndex = _carBehaviour.NavMeshAgent.path.corners.Length > _valueOne ? _valueOne : _valueZero;
                _nextPosition = _carBehaviour.NavMeshAgent.path.corners[_currentCornerIndex];
                _carBehaviour.SetRotation(_nextPosition, _carBehaviour.NavMeshAgent.angularSpeed);
            }
        }

        private void MoveEnd()
        {
            Debug.Log($"Машина достигла цели", _carBehaviour.gameObject);
            _carBehaviour.EndMoveToPoin();
        }

        private bool DistanceLessStoppingDistance()
        {
            float distance = (_carBehaviour.NavMeshAgent.destination - _carBehaviour.transform.position).magnitude;
            if (distance <= _carBehaviour.NavMeshAgent.stoppingDistance)
                return true;

            return false;
        }
    }
}