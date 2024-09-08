using Gameplay.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerGlobalResourcesContainer : Singleton<PlayerGlobalResourcesContainer>
    {
        [SerializeField]
        private List<ResourceDefinition> _playerDataGlobalResources;
        public List<ResourceDefinition> GetGlobalResources => _playerDataGlobalResources;

        public Action<ResourceDefinition> OnResourcesChange;
        public void ResourcesChange(ResourceDefinition resourceDefinition)
        {
            Debug.Log($"[Event] Изменение кол-ва ресурса {resourceDefinition}");
            OnResourcesChange?.Invoke(resourceDefinition);
        }

        private void Awake()
        {
            Cloning();
        }

        private void Cloning()
        {
            for (int i = 0; i < _playerDataGlobalResources.Count; i++)
                _playerDataGlobalResources[i] = Instantiate(_playerDataGlobalResources[i]);
        }

        public void PlusOrAdd(TrophyResource trophyResource)
        {
            if (TryFindItemByTrophy(trophyResource, out ResourceDefinition resourceDefinition))
            {
                resourceDefinition.PlusPCS(trophyResource);
                ResourcesChange(resourceDefinition);
                return;
            }
            else
            {
                var newItem = Instantiate(trophyResource.Resource);
                newItem.PlusPCS(trophyResource);
                _playerDataGlobalResources.Add(newItem);
                ResourcesChange(newItem);
            }
        }

        public void Remove(ResourceDefinition resourceDefinition)
        {
            _playerDataGlobalResources.Remove(resourceDefinition);
        }

        public ItemDefinition Get(int index)
        {
            return _playerDataGlobalResources[index];
        }

        public bool TryFindItemByTrophy(TrophyResource trophyResource, out ResourceDefinition resourceDefinition)
        {
            foreach (var item in _playerDataGlobalResources)
            {
                if (item.ID.Equals(trophyResource.Resource.ID))
                {
                    resourceDefinition = item;
                    return true;
                }
            }
            resourceDefinition = null;
            return false;
        }
    }
}
