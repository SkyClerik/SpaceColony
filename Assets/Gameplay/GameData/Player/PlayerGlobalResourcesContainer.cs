using Gameplay.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerGlobalResourcesContainer : Singleton<PlayerGlobalResourcesContainer>
    {
        [SerializeField]
        private List<ResourceDefinition> _playerDataGlobalResources;
        public List<ResourceDefinition> GetGlobalResources => _playerDataGlobalResources;

        private void Awake()
        {
            for (int i = 0; i < _playerDataGlobalResources.Count; i++)
            {
                _playerDataGlobalResources[i] = Instantiate(_playerDataGlobalResources[i]);
            }
        }
    }
}
