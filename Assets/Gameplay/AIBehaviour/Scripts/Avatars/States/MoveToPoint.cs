using SkyClerikExt;
using UnityEngine;
using UnityEngine.AI;

namespace AvatarLogic
{
    internal class MoveToPoint : IStateBase
    {
        private AvatarBehaviour _avatarData;
        private float _timer = -1f;
        private float _currectTime = 0f;
        private bool _isWait
        {
            get
            {
                if (_timer == 0)
                    return false;

                if (_currectTime >= _timer)
                {
                    _currectTime = 0f;
                    _timer = -1;
                    return false;
                }

                if (_timer > 0)
                    _currectTime += Time.deltaTime;

                return true;
            }
        }

        public MoveToPoint(AvatarBehaviour avatarData)
        {
            _avatarData = avatarData;
            _avatarData.NavMeshAgent.stoppingDistance = 0.2f;
        }

        public bool Previously()
        {
            return true;
        }

        public void Update()
        {
            if (_avatarData.NavMeshAgent.enabled == false)
                return;

            MoveToTargetOrStop();
            FindClosesEdgeEverySecond();
        }

        private void FindClosesEdgeEverySecond()
        {
            if (_isWait == true)
                return;

            if (NavMesh.FindClosestEdge(_avatarData.NavMeshAgent.destination, out NavMeshHit hit, NavMesh.AllAreas) == false)
            {
                MoveEnd();
            }
            _timer = 1f;
        }

        private void MoveToTargetOrStop()
        {
            if (DistanceLessStoppingDistance() == true)
            {
                MoveEnd();
            }
            else
            {
                _avatarData.SetRotation(_avatarData.NavMeshAgent.destination);
                _avatarData.NavMeshAgent.velocity = _avatarData.transform.forward * _avatarData.NavMeshAgent.speed;
            }
        }

        private void MoveEnd()
        {
            _avatarData.NavMeshAgent.ResetPath();
        }

        private bool DistanceLessStoppingDistance()
        {
            float distance = (_avatarData.NavMeshAgent.destination - _avatarData.transform.position).magnitude;
            if (distance <= _avatarData.NavMeshAgent.stoppingDistance)
                return true;

            return false;
        }
    }
}