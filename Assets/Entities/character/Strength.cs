using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : ScriptableObject {
    virtual public float Speed(float speed) {return speed;}
    virtual public float ResponseTime(float responseTime) {return responseTime;}
    virtual public float Damage(float damage) {return damage;}
    virtual public float MaxHealth(float maxHealth) {return maxHealth;}

    // if the Strength needs to add a component or something, plug it in through this function
    // should this be a UnityEvent?
    virtual public void ConfigEntity(Entity entity) {}
    // if this strength has requirements, put them here
    virtual public bool MeetsRequirements(Entity entity) {return true;}
}
