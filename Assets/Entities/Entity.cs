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

    [Tooltip("the maximum acceleration forward or backward")]
    public float maxAcceleration { get {return maxSpeed / responseTime;} }
    [Tooltip("the maximum acceleration to the side (i.e. to turn)")]
    public float responseTime { get {return entityType.responseTime;} }
    public float maxTurningAcceleration { get {return Mathf.PI * maxSpeed / entityType.turnTime;}}
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

    public void TurnInDirection(Vector3 dir) {
          Vector3 localDir = transform.parent.InverseTransformVector(dir);
		transform.localEulerAngles = new Vector3();
		float angle = Mathf.Atan2(localDir.y, localDir.x);
		Vector3 eulerAngles = new Vector3(0.0f, 0.0f, angle * 180.0f / Mathf.PI + 270.0f);
		transform.localEulerAngles = eulerAngles;
    }

    // target is the global vector to point toward.  Rotations should keep the object in the canvas
	public void TurnToward(Vector3 target) {
		Vector3 localTarget = transform.parent.TransformVector(transform.parent.InverseTransformVector(target));
          TurnInDirection(localTarget);
	}

    public void AccelerateToward(Vector3 position, bool faceForward) {
        Vector3 delta = position - transform.position;
        acceleration = 10f * delta.normalized * maxAcceleration;
        NormalizeAcceleration();
        if (faceForward) {
            TurnInDirection(velocity);
        }
    }

    // limits the acceleration to maxAcceleration
    public void NormalizeAcceleration() {
        Vector3 accelerationForward = Vector3.Project(acceleration, velocity);
        Vector3 accelerationSideways = acceleration - accelerationForward;
        float accelerationForwardMag = accelerationForward.magnitude;
        if (accelerationForwardMag > maxAcceleration) {
            accelerationForward = accelerationForward * maxAcceleration / accelerationForwardMag;
        }
        float accelerationSidewaysMag = accelerationSideways.magnitude;
        if (accelerationSidewaysMag > maxTurningAcceleration) {
            accelerationSideways = accelerationSideways * maxTurningAcceleration / accelerationSidewaysMag;
        }
        acceleration = accelerationForward + accelerationSideways;
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

    // utility function to locate the character
    public Entity FindCharacter() {
        MainCharacterController controller = transform.parent.GetComponentInChildren(typeof(MainCharacterController), false) as MainCharacterController;
        if (controller != null) {
            GameObject character = controller.gameObject;
            Entity characterEntity = character.GetComponent<Entity>();
            if (characterEntity == null) {
                Debug.LogError("characters should have entity components, but this one doesn't???");
            }
            return characterEntity;
        }
        return null;
    }

}
