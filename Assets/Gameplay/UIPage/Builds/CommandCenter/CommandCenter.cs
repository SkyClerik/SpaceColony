using UnityEngine.UIElements;

namespace Gameplay.UI
{
    public class CommandCenter : UIPage<CommandCenter>
    {
        private Button _buttonClose;
        private const string _buttonCloseName = "button_close";

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            _buttonClose = rootElement.Q<Button>(_buttonCloseName);
            _buttonClose.clicked += ClickedButtonClose;
        }

        private void OnEnable()
        {
            UserInterfaceShare.Instance.OpenNewPage += IsOpenNewPage;
        }

        private void OnDisable()
        {
            UserInterfaceShare.Instance.OpenNewPage -= IsOpenNewPage;
        }

        private void IsOpenNewPage(UIDocument uIDocument)
        {
            if (uIDocument != document)
                Hide();
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        private void ClickedButtonClose()
        {
            Hide();
        }
    }
}