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
            // �������� ��� ����������� ������� ���������, ���� �� ����� �� �� ������
            return true;
        }

        public void Update()
        {

        }
    }
}