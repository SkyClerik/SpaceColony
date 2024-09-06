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
            return $"ГИР (GS): {GS} \nТекущее здоровье: {CurrentHP} \nМаксимальное здоровье: {MaxHP}\nУрон: {Damage} \nСкорость атаки: {AttackSpeed} \nСкорость передвижения: {MoveSpeed}\nВыносливость: {Stamina}\n";
        }
    }
}