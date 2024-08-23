using UnityEngine;

namespace Gameplay.UI
{
    public class MainCanvasManager : Singleton<MainCanvasManager>
    {
        [SerializeField]
        private Canvas _canvas;
    }
}