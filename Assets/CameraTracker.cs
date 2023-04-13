using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// tracks the camera to be above the tracked object, however it moves
[RequireComponent(typeof(Camera))]
public class CameraTracker : MonoBehaviour {
    private Camera myCamera;
    private Vector3 velocity;

    public GameObject trackedObject;
    // acceleration is -spring1 * delta x + - spring2 * deltax^3
    [Tooltip("spring1 represents a normal spring between camera and target")]
    public float spring1 = 0.0f;
    [Tooltip("spring2 represents a spring which is loose for small changes, but much stronger when pulled tight")]
    public float spring2 = 1.0f;

    void Start() {
        if (trackedObject == null)
            Debug.LogError("camera tracker needs an object to track");
        myCamera = GetComponent<Camera>();
        if (myCamera == null)
            myCamera = Camera.main;
        if (myCamera == null)
            Debug.LogError("no camera could be found for this camera tracker");
        velocity = new Vector3();
    }

    void Update() {
        Vector3 deltax = trackedObject.transform.position - transform.position;
        deltax.y = 0.0f;
        Vector3 acceleration = spring1 * deltax + spring2 * deltax * deltax.magnitude * deltax.magnitude;
        Vector3 finalVelocity = velocity + acceleration * Time.deltaTime;
        Vector3 deltaPosition = 0.5f * (velocity + finalVelocity) * Time.deltaTime; 
        myCamera.transform.position += deltaPosition;
        //Debug.Log("deltax=" + deltax + " acceleration=" + acceleration + " finalVelocity=" + finalVelocity + " deltaPosition=" + deltaPosition);
        velocity = finalVelocity;
    }
}
