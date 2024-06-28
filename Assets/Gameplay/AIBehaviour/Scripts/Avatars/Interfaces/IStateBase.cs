namespace AvatarLogic
{
    public interface IStateBase
    {
        bool Previously();
        void Update();
    }
}