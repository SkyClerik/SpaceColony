using Behavior;

namespace Car.State
{
    public class CarIdle : ICarState
    {
        private CarBehavior _carBehavior;
        private CarStateMachine _machine;

        public void Init(CarBehavior carBehavior, CarStateMachine machine)
        {
            _carBehavior = carBehavior;
            _machine = machine;
        }

        public void Update()
        {

        }
    }
}