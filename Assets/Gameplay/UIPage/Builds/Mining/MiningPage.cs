using UnityEngine.UIElements;

namespace Gameplay.UI
{
    public class MiningPage : UIPage<MiningPage>
    {
        private VisualElement _mainImage;
        private const string _mainImageName = "main_image";
        private Label _labelDescription;
        private const string _labelDescriptionName = "label_description";

        private Button _buttonClose;
        private const string _buttonCloseName = "button_close";

        private MiningBehavior _miningBehavior;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            _mainImage = rootElement.Q(_mainImageName);
            _labelDescription = rootElement.Q<Label>(_labelDescriptionName);
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

        public void Show(MiningBehavior miningBehavior)
        {
            _miningBehavior = miningBehavior;

            base.Show();
            Repaint();
        }

        public override void Hide()
        {
            base.Hide();
        }

        private void ClickedButtonClose()
        {
            Hide();
        }

        public void Repaint()
        {
            _mainImage.style.backgroundImage = new StyleBackground(_miningBehavior.GetTrophyResource.Resource.Icon);
            _labelDescription.text = _miningBehavior.GetBuildDefinition.Description;
        }
    }
}