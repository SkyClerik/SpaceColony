using Photon.Pun;
using UnityEngine;

public class PunSingleton<T> : MonoBehaviourPunCallbacks where T : MonoBehaviourPunCallbacks
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject("[PUN_SINGLETON] " + typeof(T));
                        _instance = singleton.AddComponent<T>();
                        DontDestroyOnLoad(singleton);
                    }
                }
            }
            return _instance;
        }
    }
}
