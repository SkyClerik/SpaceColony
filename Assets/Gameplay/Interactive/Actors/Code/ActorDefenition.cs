using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "new ActorDefinition", menuName = "Gameplay/Data/ActorDefinition")]
    public class ActorDefinition : CharacterBase
    {
        [SerializeField]
        private ActorClass _class;
        [SerializeField]
        private bool _busy = false;
        [SerializeField]
        private int _experience = 0;

        public ActorClass Type => _class;
        public bool Busy { get => _busy; set => _busy = value; }
        public int Experience { get => _experience; set => _experience = value; }

        public string GetInfo()
        {
            var stats = this.ToString();
            var characterBase = base.ToString();
            var characterStats = Stats.ToString();

            return $"{stats}\n{characterBase}\n{characterStats}";
        }

        public override string ToString()
        {
            return $"Класс: {_class}\nНа задании: {_busy}\nОпыт: {_experience}\n";
        }
    }

    [System.Serializable]
    public class TrophyActor
    {
        public ActorDefinition Actor;
        [Range(0f, 100f)]
        public byte Chance;
    }
}