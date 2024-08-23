using System.Collections.Generic;

namespace Gameplay
{
    public class GameDataContainer : Singleton<GameDataContainer>
    {
        public List<ActorData> ActorDatas = new List<ActorData>();

        private void Awake()
        {
            for (int i = 0; i < ActorDatas.Count; i++)
            {
                ActorDatas[i] = Instantiate(ActorDatas[i]);
            }
        }
    }
}