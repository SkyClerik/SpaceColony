using Behavior;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerGarage : Singleton<PlayerGarage>
    {
        [SerializeField]
        private List<CarBehavior> _carBehaviors = new List<CarBehavior>();

        public List<CarBehavior> CarBehaviours => _carBehaviors;
    }
}