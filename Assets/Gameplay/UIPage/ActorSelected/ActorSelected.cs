using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Gameplay.UI
{
    public class ActorSelected : UIPage<ActorSelected>
    {
        private VisualElement _cells;
        private const string _cellsName = "cells";
        private List<HeroIconElement> _callsList = new List<HeroIconElement>();
        private Color _transparentColor = new Color(1, 1, 1, 0);
        private const string _iconName = "icon";

        public void RepaintCalls()
        {
            _cells = rootElement.Q<VisualElement>(_cellsName);
            _cells.Clear();

            List<ActorData> actorDatas = PlayerActorsContainer.Instance.GetActorsData;
            for (int i = 0; i < actorDatas.Count; i++)
            {
                HeroIconElement heroIconElement = new HeroIconElement(_cells, actorDatas[i], OnMouseDownCallback);
                _callsList.Add(heroIconElement);

                VisualElement icon = heroIconElement.Q(_iconName);
                icon.style.backgroundImage = new StyleBackground(actorDatas[i].Icon);

                SetShadow(actorDatas[i]);
            }
        }

        public void SetShadow(ActorData actorData)
        {
            foreach (var heroIconElement in _callsList)
            {
                if (heroIconElement.actorData == actorData)
                {
                    if (actorData.Busy == true)
                    {
                        heroIconElement.style.backgroundColor = Color.gray;
                        return;
                    }
                    else
                    {
                        heroIconElement.style.backgroundColor = _transparentColor;
                    }
                }
            }
        }

        private void OnMouseDownCallback(HeroIconElement heroIconElement)
        {
            if (heroIconElement.actorData.Busy)
                return;

            if (_callbackActorData != null)
            {
                heroIconElement.actorData.Busy = true;
                _callbackActorData.Invoke(_slotIndex, heroIconElement.actorData);
                Hide();
            }
        }

        byte _slotIndex;
        Action<byte, ActorData> _callbackActorData;
        public void Show(byte slotIndex, Action<byte, ActorData> callbackActorData)
        {
            base.Show();
            RepaintCalls();
            _slotIndex = slotIndex;
            _callbackActorData = callbackActorData;
        }

        public override void Show()
        {
            base.Show();
            RepaintCalls();
        }
    }
}