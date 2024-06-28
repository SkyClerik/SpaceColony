namespace AvatarLogic
{
    public class Idle : IStateBase
    {
        private AvatarBehaviour _avatarData;
        public Idle(AvatarBehaviour avatarData)
        {
            _avatarData = avatarData;
        }

        public bool Previously()
        {
            // Проверка для корректного запуска состояния, если не пошли то не меняем
            return true;
        }

        public void Update()
        {

        }
    }
}