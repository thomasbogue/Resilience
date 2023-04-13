using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class CollisionAttack : MonoBehaviour {
    private Entity entity;
    public bool dieOnAttack = true;

    void Start() {
        if ((entity = GetComponent<Entity>()) == null) {
            Debug.LogError("there should be an entity here");
        }
    }

    void Update() {
        foreach (Entity possibleTarget in transform.parent.GetComponentsInChildren<Entity>()) {
            if (possibleTarget == entity)
                continue;
            if (entity.IsTouching(possibleTarget)) {
                possibleTarget.Wound(entity.damage);
                if (dieOnAttack) {
                    entity.onDeath.Invoke();
                    Destroy(gameObject);
                }
            }
        }
    }
}
