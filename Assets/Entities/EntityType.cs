using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="EntityType", menuName="ScriptableObjects/EntityType", order=1)]
public class EntityType : ScriptableObject {
    public float maxHealth=100.0f;
    public float speed=100.0f;
    public float responseTime=1.0f;
    public Sprite[] frames;
    public float frameRate=1.0f;
    public bool friction=true;
    public float size=1.0f;
    public float damage=10.0f;
}
