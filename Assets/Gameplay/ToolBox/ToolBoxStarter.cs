using System.Collections.Generic;
using UnityEngine;

namespace ToolBoxSystem
{
    public class ToolBoxStarter : MonoBehaviour
    {
        [Header("[Managers]")]
        public List<ManagerBase> managers = new List<ManagerBase>();

        void Awake()
        {
            ToolBox.ClearToolBox();

            foreach (var managerBase in managers)
            {
                ToolBox.Add(managerBase);
            }

            UnityEngine.Time.timeScale = 1;
        }
    }
}