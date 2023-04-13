using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Entity))]
public class MainCharacterController : MonoBehaviour
{
    private RectTransform rectTransform;
    private float maxSpeed { get {return entity.maxSpeed;}}
    private float accelerationTime { get {return entity.responseTime;} }
    private float maxAcceleration { get {return maxSpeed / accelerationTime;} }
    private Vector3 acceleration { get {return entity.acceleration;} set {entity.acceleration = value;}}
    private Vector3 velocity { get {return entity.velocity;} }
    private Entity entity;

    public Camera localCamera;

    void Awake() {
        if (localCamera == null) {
            //Debug.LogError("this object must be assigned a camera");
            localCamera = Camera.main;
        }
        if ((rectTransform = GetComponent<RectTransform>()) == null) {
            Debug.LogError("oops -- this component must have a RectTransform");
        }
        if ((entity = GetComponent<Entity>()) == null) {
            Debug.LogError("oops -- this component must have an Entity");
        }
    }

    void FixedUpdate() {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 globalMousePosition = localCamera.ScreenToWorldPoint(mousePosition);
        // the canvas is at y=0, so set z to 0
        globalMousePosition.y = 0.0f;

        Vector3 localPosition = rectTransform.position;
        // rectTransform positions are listed in the inspector in a local reference frame,
        // but the transform.position is a global position
        Vector3 targetDisplacement = globalMousePosition - transform.position;
	    if (targetDisplacement.magnitude > 0.01f)
	    	entity.TurnToward(targetDisplacement);
    	// the algorithm is as follows:  accelerate toward target unless you
    	// need more than the maximum amount of acceleration to reach target
       	// this seems like it would result in moving over the mark, but 
    	// air friction will stop this ( I hope)
	
    	// this formula is only approximate if the velocity is off to the side of the target displacement -- maybe I should do more math on this one... 
    	// I tried more math, but the math was hard
    	float speed = velocity.magnitude;
    	float requiredAcceleration = speed * speed / targetDisplacement.magnitude;
    	if (requiredAcceleration > maxAcceleration) { // we need to brake
    		acceleration = - maxAcceleration * velocity.normalized;
    	} else {
    		acceleration = maxAcceleration * targetDisplacement.normalized;
    	}
    }
}
