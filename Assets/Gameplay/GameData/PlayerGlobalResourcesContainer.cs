using Gameplay.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerGlobalResourcesContainer : Singleton<PlayerGlobalResourcesContainer>
    {
        [SerializeField]
        private List<GlobalResource> _playerDataGlobalResources;
        public List<GlobalResource> GetGlobalResources => _playerDataGlobalResources;
    }
}
