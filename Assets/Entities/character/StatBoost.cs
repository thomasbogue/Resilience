using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="StatBoost", menuName="ScriptableObjects/Strength/StatBoost", order=1)]
public class StatBoost : Strength {
    public float speedAddend = 0.0f;
    public float speedFactor = 0.0f;
    public float responseTimeAddend = 0.0f;
    public float responseTimeFactor = 0.0f;
    public float damageAddend = 0.0f;
    public float damageFactor = 0.0f;
    public float maxHealthAddend = 0.0f;
    public float maxHealthFactor = 0.0f;

    public override float Speed(float speed) {
        return (speedAddend + speed) * (1.0f + speedFactor);
    }

    public override float ResponseTime(float responseTime) {
        return (responseTimeAddend + responseTime) * (1.0f + responseTimeFactor);
    }

    public override float Damage(float damage) {
        return (damageAddend + damage) * (1.0f + damageFactor);
    }

    public override float MaxHealth(float maxHealth) {
        return (maxHealthAddend + maxHealth) * (1.0f + maxHealthFactor);
    }
}
