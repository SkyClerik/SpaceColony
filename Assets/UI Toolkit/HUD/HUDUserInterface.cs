using UnityEngine.UIElements;

namespace Gameplay
{
    public class HUDUserInterface : Singleton<HUDUserInterface>
    {
        private UIDocument _document;
        private VisualElement _rootElement;

        private Label _reputationValueElement;
        private const string _reputationValueElementName = "ReputationValue";

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
            _reputationValueElement = _rootElement.Q<Label>(_reputationValueElementName);
            LoadReputation();
        }

        public void UpdateReputation(int value)
        {
            _reputationValueElement.text = value.ToString();
        }

        public void LoadReputation()
        {
            _reputationValueElement.text = Guild.Instance.GetReputation.ToString();
        }
    }
}