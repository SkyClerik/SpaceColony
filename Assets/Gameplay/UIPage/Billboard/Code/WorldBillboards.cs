using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class WorldBillboards : UIPage<WorldBillboards>
    {
        [SerializeField]
        private VisualTreeAsset _billboardTemplate;
        [SerializeField]
        private int _totalBillboards = 10;

        private List<Billboard> _billboards = new List<Billboard>();
        private const string _rootTemplateElementName = "root_template_element";

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        private void Init()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Billboard billboardCash;
            for (int i = 0; i < _totalBillboards; i++)
            {
                billboardCash = CreateBillboard(null);
                billboardCash.style.display = DisplayStyle.None;
            }
        }

        private void FixedUpdate()
        {
            Camera camera = Camera.main;

            foreach (Billboard billboard in _billboards)
            {
                if (billboard.SceneObject == null)
                    continue;

                Relocation(billboard);
            }
        }

        public void Relocation(Billboard billboard)
        {
            billboard.CanvasPosition = Camera.main.WorldToScreenPoint(billboard.SceneObject.transform.position);
            billboard.CanvasPosition.y = (Screen.height - billboard.CanvasPosition.y);
            var panelLocalPosition = RuntimePanelUtils.ScreenToPanel(rootElement.panel, billboard.CanvasPosition);
            billboard.style.top = panelLocalPosition.y;
            billboard.style.left = panelLocalPosition.x;
        }

        public Billboard GetFreeBillboardFrom(GameObject sceneObject)
        {
            for (int i = 0; i < _billboards.Count; i++)
            {
                if (_billboards[i].style.display == DisplayStyle.None)
                {
                    _billboards[i].SceneObject = sceneObject;
                    return _billboards[i];
                }
            }

            var newBillboard = CreateBillboard(sceneObject);
            newBillboard.style.display = DisplayStyle.Flex;
            return newBillboard;
        }

        private Billboard CreateBillboard(GameObject sceneObject)
        {
            var newBillboard = new Billboard(_billboardTemplate, rootElement, sceneObject);
            _billboards.Add(newBillboard);
            return newBillboard;
        }
    }
}