using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class b_Local_Player : NetworkBehaviour {

    [SyncVar]
    public string m_Name = "";

    public bool m_IsDrawing = false;
}
