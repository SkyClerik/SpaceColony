using UnityEngine;

namespace AvatarLogic
{
    [RequireComponent(typeof(AvatarBehaviour))]
    public class StartSettingsFromAI : MonoBehaviour
    {
        [SerializeField]
        private float _patrolingRadius = 8;
        [SerializeField]
        private float _agressiveDistance = 10;

        [SerializeField]
        private GameObject _nextPoint;
        [SerializeField]
        private GameObject _endPoint;

        private StateMashine _stateMashine;

        private void Start()
        {
            if (TryGetComponent(out _stateMashine) == false)
            {
                _stateMashine = gameObject.AddComponent<StateMashine>();
            }

            AvatarBehaviour avatarData = GetComponent<AvatarBehaviour>();
            avatarData.StateMashine = _stateMashine;

            _stateMashine.States.Add(typeof(Patroling), new Patroling(avatarData, _patrolingRadius, _nextPoint, _endPoint));
            //_stateMashine.States.Add(typeof(MoveWithinAttackRange), new MoveWithinAttackRange(avatarData, _agressiveDistance, null));
            //_stateMashine.States.Add(typeof(AIAttack), new AIAttack(avatarData));
            //TODO для ожидания указал магическое число
            //_stateMashine.States.Add(typeof(WaitSkillJob), new WaitSkillJob(avatarData, 3));
            //_stateMashine.States.Add(typeof(FindingEnemy), new FindingEnemy(avatarData, _agressiveDistance));
            //_stateMashine.States.Add(typeof(MoveToPoint), new MoveToPoint(avatarData));

            _stateMashine.States.TryGetValue(typeof(Patroling), out IStateBase state);
            _stateMashine.SetDefault(state);
            _stateMashine.DefaultState();

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _agressiveDistance);
        }
    }
}