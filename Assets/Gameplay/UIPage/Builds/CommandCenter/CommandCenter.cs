using UnityEngine.UIElements;

namespace Gameplay.UI
{
    public class CommandCenter : UIPage<CommandCenter>
    {
        private Button _closeButton;
        private const string _closeButtonName = "close_button";

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            _closeButton = rootElement.Q<Button>(_closeButtonName);
            _closeButton.clicked += ClickedCloseButton;
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        private void ClickedCloseButton()
        {
            Hide();
        }
    }
}