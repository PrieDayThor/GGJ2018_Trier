using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Paint : MonoBehaviour {

    public Transform baseDot;
    public Transform redDot;
    public KeyCode mouseLeft;
    public static string toolTyp;


    private float distance;
    private Vector2 previousMousePosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        distance = calculateDistance(previousMousePosition, mousePosition);

        if (Input.GetKey(mouseLeft))
        {
            if (distance > 1.0f)
            {
                drawLine(previousMousePosition, mousePosition);
            }
            
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

    void drawLine(Vector2 start, Vector2 end)
    {
        start = Camera.main.ScreenToWorldPoint(start);
        end = Camera.main.ScreenToWorldPoint(end);

        float endDistance = calculateDistance(start, end);

        Vector2 dirVector = end - start;

        Vector2 currentPoint = start + dirVector / endDistance * 0.05f;

        float distanceStep = calculateDistance(start, currentPoint);
        float currentDistance = distanceStep;

        while(currentDistance < endDistance)
        {
            var square = Instantiate(redDot, currentPoint, redDot.rotation);
            square.transform.parent = gameObject.transform;
            currentPoint += (dirVector / endDistance)*0.05f;
            currentDistance += distanceStep;
        }
    }
}
