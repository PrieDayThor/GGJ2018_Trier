using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TrailDrawer : NetworkBehaviour {

    private Plane objPlane;
    private GameObject currentTrail;
    private Vector3 startPos;
    public GameObject trail;



    public void Start()
    {
        objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position + Vector3.forward * 1.0f);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!isLocalPlayer) return;

        if (Input.GetMouseButtonDown(0)) {
            // Do the raycast and calculation on the client
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance)) {
                startPos = mRay.GetPoint(rayDistance);

                // pass the calling Players gameObject and the
                // position as parameter to the server
                CmdSpawn(this.gameObject, startPos);
            }
        } else if (Input.GetMouseButton(0)) {
            // Do the raycast and calculation on the client
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance)) {
                // only update your local object on the client
                // since they have NetworkTransform attached
                // it will be updated on the server (and other clients) anyway
                if(currentTrail != null){
                    currentTrail.transform.position = mRay.GetPoint(rayDistance);//null sometimes on Client
                }
            }
        }
    }

    [Command]
    private void CmdSpawn(GameObject callingClient, Vector3 spawnPosition) {
        // Note that this only sets currentTrail on the server
        currentTrail = (GameObject)Instantiate(trail, spawnPosition, Quaternion.identity);
        NetworkServer.Spawn(currentTrail);

        // set currentTrail in the calling Client
        RpcSetCurrentTrail(callingClient, currentTrail);
    }

    // ClientRpc is somehow the opposite of Command
    // It is invoked from the server but only executed on ALL clients
    // so we have to make sure that it is only executed on the client
    // who originally called the CmdSpawn method
    [ClientRpc]
    private void RpcSetCurrentTrail(GameObject client, GameObject trails) {
        // do nothing if this client is not the one who called the spawn method
        //if (this.gameObject != client) return;

        // also do nothing if the calling client himself is the server
        // -> he is the host
        //if (isServer) return;

        // set currentTrail on the client
        currentTrail = trails;
    }
}
