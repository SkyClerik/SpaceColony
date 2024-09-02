using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "DrawingDefinition", menuName = "Gameplay/Data/DrawingDefinition")]
    public class DrawingDefinition : ObjectBase
    {
        [SerializeField]
        private ResourceDefinition _inResource;
        [SerializeField]
        private ItemDefinition _inItem;
        [SerializeField]
        private int _inCount;

        [SerializeField]
        private int _jobPrice;
        [SerializeField]
        private byte _learningDegree;

        [SerializeField]
        private ResourceDefinition _outResource;
        [SerializeField]
        private ItemDefinition _outItem;
        [SerializeField]
        private int _outCount;

    }

    [System.Serializable]
    public class TrophyDrawing
    {
        public DrawingDefinition Drawing;
        [Range(0f, 100f)]
        public byte Chance;
    }
}