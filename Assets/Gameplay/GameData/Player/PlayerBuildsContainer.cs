using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerBuildsContainer : Singleton<PlayerBuildsContainer>
    {
        [SerializeField]
        private BuildInfo _commandCenter;
        private Dictionary<string, BuildingBehavior> _buildBehavior = new Dictionary<string, BuildingBehavior>();

        public BuildInfo GetCommandCenterInfo => _commandCenter;
        public Dictionary<string, BuildingBehavior> GetBuildBehavior => _buildBehavior;

        public void TryAddBuild(BuildingBehavior buildBehavior)
        {
            _buildBehavior.Add(buildBehavior.name, buildBehavior);
        }

        public BuildingBehavior GetBuildingBehavior(string name)
        {
            return _buildBehavior[name];
        }

        public BuildingBehavior GetBuildingBehavior(BuildInfo buildInfo)
        {
            foreach (var item in _buildBehavior)
            {
                if (item.Value.GetBuildInfo == buildInfo)
                {
                    return item.Value;
                }
            }
            return null;
        }
    }
}