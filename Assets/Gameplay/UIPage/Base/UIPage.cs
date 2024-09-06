using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.UI
{
    public class UIPage<T> : Singleton<T> where T : MonoBehaviour
    {
        protected UIDocument document;
        protected VisualElement rootElement;

        [SerializeField]
        private bool _hideInStart = true;

        protected virtual void Awake()
        {
            document = GetComponent<UIDocument>();
            rootElement = document.rootVisualElement;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        protected virtual void Start()
        {
            if (_hideInStart)
                Hide();
        }

        public virtual void Show()
        {
            UserInterfaceShare.Instance.OnPageOpen(document);
            rootElement.style.display = DisplayStyle.Flex;
        }

        public virtual void Hide()
        {
            rootElement.style.display = DisplayStyle.None;
        }
    }
}