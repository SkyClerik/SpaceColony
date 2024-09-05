using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UserInterfaceRaycaster : Singleton<UserInterfaceRaycaster>
{
    private PointerEventData _pointer;
    private List<RaycastResult> _resultRaycast;
    public bool IsPickingMode
    {
        get
        {
            _pointer = new PointerEventData(EventSystem.current);
            _pointer.position = Input.mousePosition;
            _resultRaycast = new List<RaycastResult>();
            // this can hit ui with "picking mode" set to "position"
            EventSystem.current.RaycastAll(_pointer, _resultRaycast);

            //if (ResultRaycast.Count > 0)
            //{
            //    foreach (RaycastResult result in ResultRaycast)
            //    {
            //        Debug.Log($"name: {result.gameObject.name}");
            //    }
            //}

            if (_resultRaycast.Count > 0)
                return true;

            return false;
        }
    }
}
