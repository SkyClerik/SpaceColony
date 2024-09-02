using Behavior;

namespace Car.State
{
    public interface ICarState
    {
        void Init(CarBehavior carBehaviour, CarStateMachine mashine);
        void Update();
    }
}