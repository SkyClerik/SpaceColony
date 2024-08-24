using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class ProjectActorsContainer : Singleton<ProjectActorsContainer>
    {
        [SerializeField]
        private List<ActorData> _gameActors = new List<ActorData>();
        public List<ActorData> ActorDatas => _gameActors;
    }
}