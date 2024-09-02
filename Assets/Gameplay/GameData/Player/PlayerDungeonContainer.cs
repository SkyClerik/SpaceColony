using System.Collections.Generic;

namespace Gameplay
{
    public class PlayerDungeonContainer : Singleton<PlayerDungeonContainer>
    {
        public List<DungeonBehavior> Dungeons = new List<DungeonBehavior>();

        public void AddDungeon(DungeonBehavior quest)
        {
            Dungeons.Add(quest);
        }
    }
}