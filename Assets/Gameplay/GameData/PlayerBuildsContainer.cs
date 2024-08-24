using System.Collections.Generic;

namespace Gameplay
{
    public class PlayerBuildsContainer : Singleton<PlayerBuildsContainer>
    {
        private Dictionary<string, BuildInfo> _buildInfos = new Dictionary<string, BuildInfo>();
        public Dictionary<string, BuildInfo> GetBuildInfos => _buildInfos;

        public void TryAddBuild(BuildInfo buildInfo)
        {
            _buildInfos.Add(buildInfo.name, buildInfo);
        }
    }
}