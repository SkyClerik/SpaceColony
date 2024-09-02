using System.Collections.Generic;
using UnityEngine;
using Gameplay.Data;

namespace Gameplay
{
    public class ProjectActorsContainer : Singleton<ProjectActorsContainer>
    {
        [SerializeField]
        private List<ActorDefinition> _gameActors = new List<ActorDefinition>();
        public List<ActorDefinition> GameActors => _gameActors;
    }
}