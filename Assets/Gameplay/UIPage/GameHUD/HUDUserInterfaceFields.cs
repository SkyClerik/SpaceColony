using Gameplay.UI;
using UnityEngine.UIElements;

public class HUDUserInterfaceFields : UIPage<HUDUserInterface>
{
    protected Label reputationValueElement;
    protected const string reputationValueElementName = "ReputationValueLabel";
    protected VisualElement globalResourcesRoot;
    protected const string globalResourcesRootName = "global_resources_root";
    protected const string line01 = "line_01";
    protected const string line02Name = "line_02";
    protected const string line02LeftName = "line_02_left";
    protected const string line02RightName = "line_02_right";
    protected const string line03Name = "line_03";
    protected const string line03LeftName = "line_03_left";
    protected const string line03RightName = "line_03_right";
    protected Button buttonCommandCenter;
    protected const string buttonCommandCenterName = "button_command_center";
    protected Button buttonMining;
    protected const string buttonMiningName = "button_mining";
    protected Button buttonActorSelected;
    protected const string buttonActorSelectedName = "button_actor_selected";
    protected Button buttonItems;
    protected const string buttonItemsName = "button_items";
    protected Button buttonDungeon;
    protected const string buttonDungeonName = "button_dungeon";
    protected Button buttonBuildType;
    protected const string buttonBuildTypeName = "button_build_type";
    protected string buttonBuildTypeText;

    protected void Init()
    {
        var line02 = rootElement.Q(line02Name);
        var line02Right = line02.Q(line02RightName);
        reputationValueElement = line02Right.Q<Label>(reputationValueElementName);
        var line03 = rootElement.Q(line03Name);
        var line03Left = line03.Q(line03LeftName);
        var line03Right = line03.Q(line03RightName);
        buttonCommandCenter = line03Right.Q<Button>(buttonCommandCenterName);
        buttonMining = line03Right.Q<Button>(buttonMiningName);
        buttonActorSelected = line03Right.Q<Button>(buttonActorSelectedName);
        buttonItems = line03Right.Q<Button>(buttonItemsName);
        buttonDungeon = line03Right.Q<Button>(buttonDungeonName);
        buttonBuildType = line03Right.Q<Button>(buttonBuildTypeName);
        buttonBuildTypeText = buttonBuildType.text;
        globalResourcesRoot = line03Left.Q(globalResourcesRootName);
        globalResourcesRoot.Clear();

    }
}