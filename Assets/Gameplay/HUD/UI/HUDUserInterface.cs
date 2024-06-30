using UnityEngine.UIElements;
using SkyClerikExt;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(UIDocument))]
    public class HUDUserInterface : Singleton<HUDUserInterface>
    {
        private UIDocument _document;
        private VisualElement _rootElement;

        private Label _reputationValueElement;
        private const string _reputationValueElementName = "ReputationValueLabel";

        private void OnEnable()
        {
            Guild.ReputationÑhange += UpdateReputation;
        }

        private void OnDestroy()
        {
            Guild.ReputationÑhange -= UpdateReputation;
        }

        private void Awake() => Init();

        private void Init()
        {
            _document = GetComponent<UIDocument>();
            _rootElement = _document.rootVisualElement;

            var right = _rootElement.Q("Right");
            _reputationValueElement = right.Q<Label>(_reputationValueElementName);
            LoadReputation();
        }

        public void UpdateReputation(int value)
        {
            _reputationValueElement.text = value.ToString().ToPriceStyle();
            //_reputationValueElement.text = StringExt.ToPriceStyle(value);
            //_reputationValueElement.text = $"{StringExt.ToPriceStyle(value)}";
        }

        public void LoadReputation()
        {
            UpdateReputation(Guild.Instance.GetReputation);
        }
    }
}