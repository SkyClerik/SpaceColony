namespace Gameplay.Data
{
    [System.Serializable]
    public class CharacterStats
    {
        public int GS;
        public int CurrentHP;
        public int MaxHP;
        public int Damage;
        public int AttackSpeed;
        public int MoveSpeed;
        public int Stamina;

        public override string ToString()
        {
            return $"��� (GS): {GS} \n������� ��������: {CurrentHP} \n������������ ��������: {MaxHP}\n����: {Damage} \n�������� �����: {AttackSpeed} \n�������� ������������: {MoveSpeed}\n������������: {Stamina}\n";
        }
    }
}