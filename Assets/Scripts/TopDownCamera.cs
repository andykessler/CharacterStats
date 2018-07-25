using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour {

    public float horizontalSpeed = 10;
    public float verticalSpeed = 10;
    public float zoomSpeed = 5; // NOTE: I inverted this axis in inspector to not have a negative here.

    Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        cam.transform.localEulerAngles = Vector3.right * 90f; // look down at ground (top-down style)
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 velocity = new Vector3(
            Input.GetAxis("Horizontal") * horizontalSpeed, 
            Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, 
            Input.GetAxis("Vertical") * verticalSpeed);
        cam.transform.position += velocity * Time.deltaTime;

        // Determine whether we want ortho or perspective
        cam.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;

        // TODO Add rotation
    }
}
