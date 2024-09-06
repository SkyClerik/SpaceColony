using Gameplay.Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.UI
{
    public class ActorPage : UIPage<ActorPage>
    {
        [SerializeField]
        private ActorDefinition _actorDefinition;

        private Label _labelNameTitle;
        private const string _labelNameTitleName = "label_name_title";
        private Label _labelActorStats;
        private const string _labelActorStatsName = "label_actor_stats";
        private Button _buttonClose;
        private const string buttonCloseName = "button_close";

        protected override void Awake()
        {
            base.Awake();

            _labelNameTitle = rootElement.Q<Label>(_labelNameTitleName);
            _labelActorStats = rootElement.Q<Label>(_labelActorStatsName);

            _buttonClose = rootElement.Q<Button>(buttonCloseName);
            _buttonClose.clicked += ClickedButtonClose;
        }

        private void ClickedButtonClose()
        {
            Hide();
        }

        public override void Show()
        {
            if (_actorDefinition == null)
            {
                ActorSelected.Instance.Show(0, callbackActorData: ActorSelectedCallback, onCloseCallback: null);
                return;
            }
            else
            {
                Repaint();
                base.Show();
            }
        }

        private void ActorSelectedCallback(byte index, ActorDefinition actorDefinition)
        {
            _actorDefinition = actorDefinition;
            Show();
        }

        private void Repaint()
        {
            _labelNameTitle.text = $"Èìÿ: {_actorDefinition.FriendlyName}";
            _labelActorStats.text = _actorDefinition.GetInfo();
        }
    }
}