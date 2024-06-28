using UnityEngine;

namespace ToolBoxSystem
{
    public abstract class ManagerBase : ScriptableObject
    {
        //  Существует для ограничения передачи в ToolBox объектов.
        //  ToolBox принимает менеджеры.
    }
}