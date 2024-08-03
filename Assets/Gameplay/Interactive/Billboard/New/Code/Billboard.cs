using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay
{
    public class Billboard : VisualElement
    {
        public GameObject SceneObject;
        public Vector2 CanvasPosition;
        public System.TimeSpan Timeout;

        public VisualElement Image;
        public Label Timer;

        private System.TimeSpan sec = new System.TimeSpan(0, 0, 1);

        public Billboard(VisualTreeAsset template, VisualElement root, GameObject sceneObject)
        {
            template.CloneTree(this);
            root.Add(this);
            style.position = Position.Absolute;

            Image = this.Q("image");
            Timer = this.Q<Label>("label");
            Timer.text = $"{Timeout}";

            SceneObject = sceneObject;
        }

        public void Tick(System.Action timeUp)
        {
            if (Timeout == System.TimeSpan.Zero)
                timeUp?.Invoke();

            Timer.text = $"{Timeout}";
            Timeout -= sec;
        }

        public void Hide()
        {
            style.display = DisplayStyle.None;
        }
    }
}