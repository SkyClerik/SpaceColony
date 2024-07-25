using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay
{
    [RequireComponent(typeof(UIDocument))]
    public class WorldBillboards : MonoBehaviour
    {
        private UIDocument _document;
        private VisualElement _rootElement;

        [SerializeField]
        private VisualTreeAsset _billboardTemplate;
        private const string _rootTemplateElementName = "root_template_element";
        [SerializeField]
        private float _templateVerticalOffset = 0f;
        [SerializeField]
        private float _templateHorizontalOffset = 0f;
        [SerializeField]
        private List<ObjectAsElement> _questBuilds = new List<ObjectAsElement>();

        private void Awake() => Init();

        private void Init()
        {
            _document = GetComponent<UIDocument>();
            _rootElement = _document.rootVisualElement;


            GC.Collect();
            GC.WaitForPendingFinalizers();
            foreach (ObjectAsElement item in _questBuilds)
            {
                item.Image = _billboardTemplate.Instantiate();
                item.Image.pickingMode = PickingMode.Ignore;
                _rootElement.Add(item.Image);
            }
        }

        private void FixedUpdate()
        {
            Camera camera = Camera.main;

            foreach (ObjectAsElement item in _questBuilds)
            {
                item.CanvasPosition = Camera.main.WorldToScreenPoint(item.SceneObject.transform.position);
                item.CanvasPosition.y = (Screen.height - item.CanvasPosition.y) - _templateVerticalOffset;
                item.CanvasPosition.x = item.CanvasPosition.x - _templateHorizontalOffset;
                var panelLocalPosition = RuntimePanelUtils.ScreenToPanel(_document.rootVisualElement.panel, item.CanvasPosition);
                item.Image.style.top = panelLocalPosition.y;
                item.Image.style.left = panelLocalPosition.x;
            }
        }

        [System.Serializable]
        private class ObjectAsElement
        {
            public GameObject SceneObject;
            public Vector2 CanvasPosition;
            public VisualElement Image;
        }
    }
}