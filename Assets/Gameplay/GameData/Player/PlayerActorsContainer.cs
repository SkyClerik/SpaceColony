using System.Collections.Generic;
using UnityEngine;
using Gameplay.Data;

namespace Gameplay
{
    public class PlayerActorsContainer : Singleton<PlayerActorsContainer>
    {
        [Header("Лист актеров заполняется из списка всех актеров проекта")]
        [SerializeField]
        private List<ActorDefinition> _actorsData = new List<ActorDefinition>();
        public List<ActorDefinition> GetActorsData => _actorsData;

        private void Awake()
        {
            TakeAllActors();
        }

        private void TakeAllActors()
        {
            ProjectActorsContainer projectActorsContainer = ProjectActorsContainer.Instance;
            for (int i = 0; i < projectActorsContainer.GameActors.Count; i++)
            {
                _actorsData.Add(Instantiate(projectActorsContainer.GameActors[i]));
            }
        }
    }
}
