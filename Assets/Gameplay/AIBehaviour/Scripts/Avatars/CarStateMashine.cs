using Behavior;

namespace Car.State
{
    [System.Serializable]
    public class CarStateMachine
    {
        private CarIdle _stateIdle = new CarIdle();
        private CarMoveToPoint _stateCarMoveToPoint = new CarMoveToPoint();
        private CarParking _stateParking = new CarParking();
        private CarPatrolling _statePatrolling = new CarPatrolling();

        public CarIdle StateIdle => _stateIdle;
        public CarMoveToPoint StateCarMoveToPoint => _stateCarMoveToPoint;
        public CarParking StateParking => _stateParking;
        public CarPatrolling StatePatrolling => _statePatrolling;

        private ICarState _currentState;
        private ICarState _defaultState;
        private CarBehavior _carBehavior;

        public CarStateMachine(CarBehavior carBehavior)
        {
            _carBehavior = carBehavior;

            _defaultState = _stateIdle;
            SetCurrentState(_defaultState);
        }

        public void SetState(ICarState carState)
        {
            SetCurrentState(carState);
        }

        public void Update()
        {
            _currentState?.Update();
        }

        private void SetCurrentState(ICarState state)
        {
            _currentState = state;
            _currentState?.Init(_carBehavior, mashine: this);
        }
    }
}