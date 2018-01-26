using UnityEngine.Networking;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public class b_Player : NetworkBehaviour {

    [SyncVar]
    public bool m_IsDrawingPlayer = false;
    [SyncVar]
    public int m_ID = 0;
    [SyncVar]
    public string m_Player = "";
}
