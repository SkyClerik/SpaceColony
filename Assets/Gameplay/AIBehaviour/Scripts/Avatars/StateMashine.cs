using System;
using System.Collections.Generic;
using UnityEngine;

namespace AvatarLogic
{
    public class StateMashine : MonoBehaviour
    {
        [SerializeField]
        private IStateBase _curentState;
        private IStateBase _defaultState { get; set; }
        [SerializeField]
        private Dictionary<Type, IStateBase> _states = new Dictionary<Type, IStateBase>();
        public Dictionary<Type, IStateBase> States { get => _states; set => _states = value; }

        public void SetState(Type type)
        {
            _states.TryGetValue(type, out IStateBase state);

            if (state.Previously() == true)
            {
                _curentState = state;
            }
        }

        private void Update()
        {
            _curentState?.Update();
        }

        internal void SetDefault(IStateBase iStateBase)
        {
            _defaultState = iStateBase;

        }
        internal void DefaultState()
        {
            SetState(_defaultState.GetType());
        }
    }
}