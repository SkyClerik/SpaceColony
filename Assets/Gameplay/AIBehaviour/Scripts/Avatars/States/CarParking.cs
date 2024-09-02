using Behavior;

namespace Car.State
{
    public class CarParking : ICarState
    {
        private CarStateMachine _mashine;
        public void Init(CarBehavior carBehaviour, CarStateMachine mashine)
        {
            _mashine = mashine;
        }

        public void Update()
        {

        }
    }
}