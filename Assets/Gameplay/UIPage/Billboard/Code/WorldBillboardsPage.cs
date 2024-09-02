using Behavior;
using Gameplay.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class WorldBillboardsPage : UIPage<WorldBillboardsPage>
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
                billboardCash = CreateBillboard();
                billboardCash.style.display = DisplayStyle.None;
            }
        }

        private void FixedUpdate()
        {
            Camera camera = Camera.main;

            foreach (Billboard billboard in _billboards)
            {
                if (billboard.GetSceneObject == null)
                    continue;

                Relocation(billboard);

                if (billboard.style.display == DisplayStyle.Flex)
                    billboard.Tick();
            }
        }

        public void CarBillboardsShow(DungeonBehavior dungeonBehavior, TimeSpan timerTime)
        {
            CarBehavior carBehavior = dungeonBehavior.GetCarInMission;
            Billboard freeBillboard = GetFreeBillboardFrom();
            freeBillboard.Reset(carBehavior.gameObject, timerTime, null);
            Relocation(freeBillboard);

            if (dungeonBehavior.GetActorParty.GetFirstActor(out ActorDefinition actorDefinition))
                freeBillboard.SetImage(actorDefinition.Icon);
        }

        public void DungeonBillboardShow(DungeonBehavior dungeonBehavior, TimeSpan timerTime, Action billboardTimeUp)
        {
            Billboard freeBillboard = GetFreeBillboardFrom();
            freeBillboard.Reset(dungeonBehavior.gameObject, timerTime, billboardTimeUp);
            Relocation(freeBillboard);
            freeBillboard.SetImage(dungeonBehavior.GetDungeonDefinition.Icon);
        }

        public void Relocation(Billboard billboard)
        {
            billboard.CanvasPosition = Camera.main.WorldToScreenPoint(billboard.GetSceneObject.transform.position);
            billboard.CanvasPosition.y = (Screen.height - billboard.CanvasPosition.y);
            var panelLocalPosition = RuntimePanelUtils.ScreenToPanel(rootElement.panel, billboard.CanvasPosition);
            billboard.style.top = panelLocalPosition.y;
            billboard.style.left = panelLocalPosition.x;
        }

        public Billboard GetFreeBillboardFrom()
        {
            for (int i = 0; i < _billboards.Count; i++)
            {
                if (_billboards[i].style.display == DisplayStyle.None)
                {
                    return _billboards[i];
                }
            }

            var newBillboard = CreateBillboard();
            newBillboard.style.display = DisplayStyle.Flex;
            return newBillboard;
        }

        private Billboard CreateBillboard()
        {
            var newBillboard = new Billboard();
            newBillboard.Init(_billboardTemplate, rootElement);
            _billboards.Add(newBillboard);
            return newBillboard;
        }
    }
}