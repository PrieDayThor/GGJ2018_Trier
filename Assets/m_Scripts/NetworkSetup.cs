using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkManager))]
public class NetworkSetup : MonoBehaviour {

    private NetworkManager manager;

	// Use this for initialization
	void Start () {
        manager = GetComponent<NetworkManager>();
	}

    public void SetIP(string ip){
        manager.networkAddress = ip;
    }

    public void SetPort(int port){
        manager.networkPort = port;
    }

    public string GetIP(){
        return Network.player.ipAddress;
    }

    public void PrintIP() {
        Debug.Log(Network.player.ipAddress);
    }

    public void StartHost(){
        manager.StartHost();
    }

    public void Join() {
        manager.StartClient();
    }

}
