using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TrailDrawer : NetworkBehaviour {

    private Plane objPlane;
    private GameObject currentTrail;
    private Vector3 startPos;
    public GameObject trail;

    private void Start()
    {
        objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            

            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance)) {
                startPos = mRay.GetPoint(rayDistance);
                currentTrail = (GameObject)Instantiate(trail, startPos, Quaternion.identity);
                currentTrail.transform.parent = this.transform;
                this.gameObject.SetActive(false);
                NetworkTransformChild t = gameObject.AddComponent<NetworkTransformChild>();
                t.target = currentTrail.transform;
                this.gameObject.SetActive(true);

            }
        } else if(Input.GetMouseButton(0)) {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance)) {
                currentTrail.transform.position = mRay.GetPoint(rayDistance);
            }
        } else if(Input.GetMouseButtonUp(0)){
            if(Vector3.Distance(this.trail.transform.position, startPos) < 0.1f){
                Destroy(currentTrail);
            }

        }
    }



}
