using Behavior;
using Gameplay;
using SkyClericExt;
using UnityEngine;
using UnityEngine.AI;

namespace Car.State
{
    public class CarMoveToPoint : ICarState
    {
        private NavMeshAgent _navMeshAgent;
        private CarBehavior _carBehavior;
        private CarStateMachine _machine;

        private Vector3 _nextPosition;
        private int _currentCornerIndex;

        private const int _valueZero = 0;
        private const int _valueOne = 1;

        private bool _isMoving = false;

        public void Init(CarBehavior carBehavior, CarStateMachine machine)
        {
            _carBehavior = carBehavior;
            _machine = machine;
            _navMeshAgent = _carBehavior.NavMeshAgent;
            _isMoving = true;
        }

        public void Update()
        {
            if (_navMeshAgent.enabled == false)
                return;

            if (!_isMoving)
                return;

            MoveToTargetOrStop();
            //FindClosesEdge();
        }

        //private void FindClosesEdge()
        //{
        //    if (NavMesh.FindClosestEdge(_navMeshAgent.destination, out NavMeshHit hit, NavMesh.AllAreas) == false)
        //    {
        //        MoveEnd();
        //    }
        //}

        private void MoveToTargetOrStop()
        {
            if (DistanceLessStoppingDistance() == true)
            {
                MoveEnd();
            }
            else
            {
                _currentCornerIndex = _navMeshAgent.path.corners.Length > _valueOne ? _valueOne : _valueZero;
                _nextPosition = _navMeshAgent.path.corners[_currentCornerIndex];
                _navMeshAgent.gameObject.SetRotation(_nextPosition, _navMeshAgent.angularSpeed);
            }
        }

        private void MoveEnd()
        {
            Debug.Log($"MoveEnd", _carBehavior);
            _isMoving = false;
            _machine.SetState(_machine.StateIdle);
            DungeonEvents.Instance.OnCarTaskComplete(_carBehavior, _carBehavior.GetDestination);
        }

        private bool DistanceLessStoppingDistance()
        {
            float distance = (_navMeshAgent.destination - _navMeshAgent.transform.position).magnitude;
            if (distance <= _navMeshAgent.stoppingDistance)
                return true;

            return false;
        }
    }
}