using Gameplay.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [System.Serializable]
    public class ActorParty
    {
        private List<ActorDefinition> _actors;
        private byte _partyLimit = 3;
        private byte _partyCount = 0;

        public bool IsFullParty
        {
            get
            {
                return _partyCount == _partyLimit ? true : false;
            }
        }

        public bool IsPartyEmpty
        {
            get
            {
                foreach (var actor in _actors)
                {
                    if (actor != null)
                        return false;
                }
                return true;
            }
        }

        public ActorParty()
        {
            _actors = new List<ActorDefinition>(_partyLimit);
            _partyCount = 0;
            for (int i = 0; i < _partyLimit; i++)
            {
                _actors.Add(null);
            }
        }

        public ActorParty(List<ActorDefinition> party)
        {
            _actors = party;

            foreach (var actor in _actors)
            {
                if (actor != null)
                    _partyCount++;
            }
        }

        public void AddActor(ref ActorDefinition actorData, byte index)
        {
            if (IsFullParty)
                return;

            if (actorData.Busy)
                return;

            _actors[index] = actorData;
            actorData.Busy = true;
            _partyCount++;
        }

        public void RemoveAllActors()
        {
            for (int i = 0; i < _actors.Count; i++)
            {
                if (_actors[i] == null)
                    continue;

                _actors[i].Busy = false;
                _actors[i] = null;
            }
            _partyCount = 0;
        }

        public void RemoveActor(ActorDefinition actorData)
        {
            for (int i = 0; i < _actors.Count; i++)
            {
                if (_actors[i] == actorData)
                {
                    actorData.Busy = false;
                    _actors[i] = null;
                    _partyCount--;
                    break;
                }
            }
        }

        public void AddPartyExperience(DungeonDefinition dungeonDefinition, float multiple = 1)
        {
            foreach (ActorDefinition actorData in _actors)
            {
                if (actorData == null)
                    continue;

                actorData.Experience += Mathf.FloorToInt(dungeonDefinition.ExpFromWin * multiple) / _actors.Count;
            }
        }

        public int GetPartyGS()
        {
            //TODO: Заглушка на подсчете GS всей партии
            int gs = 0;
            foreach (ActorDefinition actorData in _actors)
            {
                if (actorData == null)
                    continue;

                gs += Mathf.FloorToInt(actorData.Experience);
            }
            return gs;
        }

        public ActorDefinition GetActorByIndex(byte index)
        {
            return _actors[index];
        }

        public bool GetFirstActor(out ActorDefinition actorDefinition)
        {
            foreach (var actor in _actors)
            {
                if (actor != null)
                {
                    actorDefinition = actor;
                    return true;
                }
            }
            actorDefinition = null;
            return false;
        }

        public ActorParty Clone()
        {
            List<ActorDefinition> actors = new List<ActorDefinition>(_actors);
            return new ActorParty(actors);
        }
    }
}