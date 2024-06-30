using System.Collections.Generic;
using UnityEngine;

namespace PoolObjectSystem
{
    [CreateAssetMenu(fileName = "PoolEnumBuilder", menuName = "Enum/PoolEnumBuilder")]
    public class PoolEnumBuilder : ScriptableObject
    {
        [SerializeField]
        private TextAsset _textAsset;
        [SerializeField]
        private List<GameObject> _gameObjects = new List<GameObject>();

        public TextAsset TextAsset => _textAsset;
        public List<GameObject> GameObjects => _gameObjects;
    }
}