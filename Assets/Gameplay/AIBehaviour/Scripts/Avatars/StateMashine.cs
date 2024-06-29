using System.Collections.Generic;
using UnityEngine;

namespace AvatarLogic
{
    [System.Serializable]
    public class StateMashine : ILitleMono
    {
        [SerializeField]
        private List<StateBase> _states = new List<StateBase>();

        private StateBase _curentState;
        private StateBase _defaultState;
        private CarBehaviour _carBehaviour;

        private bool _isReady;

        public void SetState(AvatarStateID stateID)
        {
            foreach (var state in _states)
            {
                if (state.stateID == stateID)
                {
                    SetCurentState(state);
                    return;
                }
            }
            SetCurentState(_defaultState);
        }

        public void Start(GameObject owner)
        {
            if (_isReady)
                return;

            for (int i = 0; i < _states.Count; i++)
            {
                _states[i] = GameObject.Instantiate(_states[i]);
            }

            if (owner.TryGetComponent(out CarBehaviour avatarBehaviour))
            {
                _carBehaviour = avatarBehaviour;

                if (_states.Count != 0)
                {
                    _defaultState = _states[0];
                    SetCurentState(_defaultState);
                }
            }
            _isReady = true;
        }

        public void Update()
        {
            _curentState?.Update();
        }

        private void SetCurentState(StateBase state)
        {
            _curentState = state;
            _curentState?.Init(_carBehaviour);
            _curentState?.Start();
        }
    }
}