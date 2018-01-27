﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TrailSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] componentsToDisable;

	// Use this for initialization
	void Start () {
        if(!isLocalPlayer){
            for (int i = 0; i < componentsToDisable.Length; i++){
                componentsToDisable[i].enabled = false;
            }
        }
	}
	
}