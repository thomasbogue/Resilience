using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public EntityType entityType;
    public float wounds = 0.0f;
    public Vector3 velocity = new Vector3();
    public Vector3 acceleration = new Vector3();
    public List<Strength> strengths = new List<Strength>();

    public UnityEvent onWound;
    public UnityEvent onDeath;

    public float maxAcceleration { get {return entityType.speed / entityType.responseTime;} }
    public float maxSpeed { get {
        float speed = entityType.speed;
        foreach(Strength strength in strengths) {
            speed = strength.Speed(speed);
        }
        return speed;
    } }
    public float size { get {return entityType.size;} }
    public float damage { get {return entityType.damage;} }
    public float maxHealth { get {return entityType.maxHealth;} }
    public float responseTime { get {return entityType.responseTime;} }

    void Startup() {
        onWound.Invoke();
    }

    void FixedUpdate() {
        Vector3 totalAcceleration = acceleration;
        if (entityType.friction) {
            // friction is proportional to velocity
            totalAcceleration = totalAcceleration - velocity / maxSpeed * maxAcceleration;
        }
    	Vector3 deltaV = acceleration * Time.deltaTime;
    	// the velocity at the end of this time interval
    	Vector3 finalVelocity = velocity + deltaV;
    	Vector3 deltaX = 0.5f * (velocity + deltaV) * Time.deltaTime;
        Vector3 newPosition = transform.position + deltaX;
        transform.position = newPosition;
        //Debug.Log("newPosition = " + newPosition + ", globalMousePosition=" + globalMousePosition + ", localPosition=" + localPosition + ", stupid=" + stupid + ", deltax=" + deltaX);
     	velocity = finalVelocity;
    }

    // target is the global vector to point toward.  Rotations should keep the object in the canvas
	public void TurnToward(Vector3 target) {
		transform.localEulerAngles = new Vector3();
		Vector3 localTarget = transform.parent.InverseTransformVector(target);
		float angle = Mathf.Atan2(localTarget.y, localTarget.x);
		Vector3 eulerAngles = new Vector3(0.0f, 0.0f, angle * 180.0f / Mathf.PI + 270.0f);
		transform.localEulerAngles = eulerAngles;
	}

    public void AccelerateToward(Vector3 position, bool faceForward) {
        Vector3 delta = position - transform.position;
        acceleration = delta.normalized * maxAcceleration;
        if (faceForward) {
            TurnToward(position);
        }
    }

    public void AccelerateToward(Vector3 position) {
        AccelerateToward(position, true);
    }

    public bool IsTouching(Entity target) {
        return (target.transform.position - transform.position).magnitude < size + target.size;
    }

    public virtual void Wound(float amount) {
        wounds += amount;
        onWound.Invoke();
        if (wounds >= maxHealth) {
            onDeath.Invoke();
            Destroy(gameObject);
        }
    }
}
