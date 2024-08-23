using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingUI))]
public class BuildingControl : Singleton<BuildingControl>
{
    [SerializeField]
    private BuildingContainer _buildingContainer;
    [SerializeField]
    private LayerMask _floorLayerMask;
    [SerializeField]
    private List<BuildingBehaviour> _buildingBehaviours = new();

    public LayerMask FloorLayerMask => _floorLayerMask;
    public int GreedSize = 1;
    public List<BuildingBehaviour> BuildingBehaviours => _buildingBehaviours;

    private void Start()
    {
        if (_buildingContainer == null)
        {
            Debug.LogError("Не назначена база данных с объектами строительства", gameObject);
            return;
        }

        BuildingUI buildingUI = GetComponent<BuildingUI>();
        buildingUI.Init(this, _buildingContainer);
    }

    public void SelectShadowBilding(int i)
    {
        GameObject shadowBuild = Instantiate(_buildingContainer.Buildings[i].gameObject);

        Rigidbody rigidbody = shadowBuild.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;

        BuildDruger buildDruger = shadowBuild.AddComponent<BuildDruger>();
        buildDruger.Init(DrugerTypes.Create);
    }

    public void TryAddBuildingBehaviour(BuildingBehaviour buildingBehaviour)
    {
        foreach (BuildingBehaviour item in _buildingBehaviours)
        {
            if (item.Equals(buildingBehaviour))
                return;
        }

        for (int i = 0; i < _buildingBehaviours.Count; i++)
        {
            if (_buildingBehaviours[i] == null)
            {
                _buildingBehaviours[i] = buildingBehaviour;
                return;
            }
        }
        _buildingBehaviours.Add(buildingBehaviour);
    }
}