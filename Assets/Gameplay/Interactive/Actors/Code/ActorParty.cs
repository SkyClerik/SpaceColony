using Gameplay.Data;
using UnityEngine;

namespace Gameplay
{
    [System.Serializable]
    public class ActorParty
    {
        private ActorDefinition[] _actors;
        private int _partyLimit;

        public bool IsFullParty
        {
            get
            {
                return (_actors.Length + 1) == _partyLimit ? true : false;
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

        public ActorParty(int partyLimit)
        {
            _partyLimit = partyLimit;

            if (_actors == null)
                _actors = new ActorDefinition[_partyLimit];
        }

        public void AddActor(ref ActorDefinition actorData, byte index)
        {
            if (IsFullParty)
                return;

            if (actorData.Busy)
                return;

            _actors[index] = actorData;
            actorData.Busy = true;
        }

        public void RemoveAllActors()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i] == null)
                    continue;

                _actors[i].Busy = false;
                _actors[i] = null;
            }
        }

        public void RemoveActor(ActorDefinition actorData)
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i] == actorData)
                {
                    actorData.Busy = false;
                    _actors[i] = null;
                    break;
                }
            }
        }

        private void FreeTheParty()
        {
            foreach (ActorDefinition actorData in _actors)
            {
                if (actorData == null)
                    continue;

                actorData.Busy = false;
            }
        }

        public void AddPartyExperience(DungeonDefinition dungeonDefinition, float multiple = 1)
        {
            foreach (ActorDefinition actorData in _actors)
            {
                if (actorData == null)
                    continue;

                actorData.Experience += Mathf.FloorToInt(dungeonDefinition.ExpFromWin * multiple) / _actors.Length;
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
    }
}