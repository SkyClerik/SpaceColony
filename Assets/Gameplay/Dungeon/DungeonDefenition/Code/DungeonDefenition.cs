using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "New DungeonDefinition", menuName = "Gameplay/Data/DungeonDefinition")]
    public class DungeonDefinition : ScriptableObject
    {
        public string Title;
        public Sprite TitleSprite;
        public Sprite Icon;
        public string Description;

        public int AddReputation;
        public int RemoveReputation;
        public int ExpFromWin;
        public int HowMuchGSNeeded = 1;

        public Timer _waitingTime;
        public System.TimeSpan GetWaitingTime => new System.TimeSpan(0, _waitingTime.Min, _waitingTime.Sec);

        public List<MonsterDungeonInfo> Monsters = new List<MonsterDungeonInfo>();

        [SerializeField]
        private LootInfo _loot;

        public void GiveOutLoot()
        {
            foreach (var item in _loot.Items)
            {
                PlayerItemsContainer.Instance.Add(item);
            }
        }
    }

    [System.Serializable]
    public struct Timer
    {
        public byte Min;
        public byte Sec;
    }
}