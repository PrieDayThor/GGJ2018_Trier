using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	


    public void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Test");
            rb.AddForce(Vector3.up * 100.0f);
        }
    }
}
