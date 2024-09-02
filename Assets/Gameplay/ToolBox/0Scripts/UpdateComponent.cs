using UnityEngine;

namespace ToolBoxSystem
{
    public class UpdateComponent : MonoBehaviour
    {
        private ToolBoxSystem.Update mng;

        public void Setup(ToolBoxSystem.Update mng)
        {
            this.mng = mng;
        }

        private void Update()
        {
            mng.Tick();
        }

        private void FixedUpdate()
        {
            mng.TickFixed();
        }

        private void LateUpdate()
        {
            mng.TickLate();
        }
    }
}