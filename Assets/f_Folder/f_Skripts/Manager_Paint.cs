using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Paint : MonoBehaviour {

    public Transform baseDot;
    public KeyCode mouseLeft;
    public static string toolTyp;
   
    private Vector2 previousMousePosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if(Input.GetKey(mouseLeft))
        {   
            var square = Instantiate(baseDot, objPosition, baseDot.rotation);
            square.transform.parent = gameObject.transform;
        }

        previousMousePosition = mousePosition;
    }

    float calculateDistance(Vector2 p1, Vector2 p2)
    {
        float distnace;

        distnace = Mathf.Sqrt(Mathf.Pow((p2.y - p1.y), 2) + Mathf.Pow((p2.x - p1.x), 2));

        return distnace;
    }
}
