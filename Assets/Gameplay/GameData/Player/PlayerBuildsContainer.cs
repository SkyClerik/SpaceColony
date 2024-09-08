using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerBuildsContainer : Singleton<PlayerBuildsContainer>
    {
        private BuildingBehavior _mainBuild;
        public BuildingBehavior GetMainBuild
        {
            get
            {
                if (_mainBuild != null)
                    return _mainBuild;

                foreach (var build in _buildBoxOnHand)
                {
                    if (build.GetBuildDefinition.IsMainBuild)
                    {
                        _mainBuild = build;
                    }
                }

                Debug.LogError($"{_mainBuild} �� ��������. ��� �� ���������. ������� IsMainBuild � ���� ������ � � ����� ���� ������ ������", gameObject);
                return null;
            }
        }

        [Header("���� ����������� ������")]
        [SerializeField]
        private List<BuildingBehavior> _buildBoxOnHand = new List<BuildingBehavior>();
        public List<BuildingBehavior> BuildBoxOnHand => _buildBoxOnHand;

        [Header("���� ���������� ������")]
        [SerializeField]
        private List<BuildingBehavior> _buildBoxOnCreating = new List<BuildingBehavior>();

        private void OnEnable()
        {
            BuildingBehaviorEvents.Instance.OnBuildCreateStart += OnBuildCreateStart;
            BuildingBehaviorEvents.Instance.OnBuildCreateComplete += OnBuildCreateComplete;
        }

        private void OnDisable()
        {
            BuildingBehaviorEvents.Instance.OnBuildCreateStart -= OnBuildCreateStart;
            BuildingBehaviorEvents.Instance.OnBuildCreateComplete -= OnBuildCreateComplete;
        }

        private void OnBuildCreateStart(BuildingBehavior buildingBehavior)
        {
            AddOnCreating(buildingBehavior);
        }

        private void OnBuildCreateComplete(BuildingBehavior buildingBehavior)
        {
            AddOnHand(buildingBehavior);
            RemoveOnCreating(buildingBehavior);
        }

        public void AddOnHand(BuildingBehavior buildingBehavior)
        {
            if (buildingBehavior.GetBuildDefinition.IsMainBuild)
                _mainBuild = buildingBehavior;

            _buildBoxOnHand.Add(buildingBehavior);
        }

        public void RemoveOnHand(BuildingBehavior buildingBehavior)
        {
            _buildBoxOnHand.Remove(buildingBehavior);
        }

        public void AddOnCreating(BuildingBehavior buildingBehavior)
        {
            _buildBoxOnCreating.Add(buildingBehavior);
        }

        public void RemoveOnCreating(BuildingBehavior buildingBehavior)
        {
            _buildBoxOnCreating.Remove(buildingBehavior);
        }
    }
}