using System.Collections.Generic;

namespace Gameplay
{
    public class PlayerActorsContainer : Singleton<PlayerActorsContainer>
    {
        private List<ActorData> _actorsData = new List<ActorData>();
        public List<ActorData> GetActorsData => _actorsData;

        private void Awake()
        {
            TakeAllActors();
        }

        private void TakeAllActors()
        {
            ProjectActorsContainer projectActorsContainer = ProjectActorsContainer.Instance;
            for (int i = 0; i < projectActorsContainer.ActorDatas.Count; i++)
            {
                _actorsData.Add(Instantiate(projectActorsContainer.ActorDatas[i]));
            }
        }
    }
}
