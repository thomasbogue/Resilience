using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class BomberController : MonoBehaviour
{
    private Entity entity;
    private GameObject character;
    private Entity characterEntity;
    // Start is called before the first frame update
    void Start() {
        if ((entity = GetComponent<Entity>()) == null) {
            Debug.Log("This requires an Entity component");
        }
        characterEntity = entity.FindCharacter();
    }

    // Update is called once per frame
    void Update() {
        if (character == null) {
            characterEntity = entity.FindCharacter();
            character = characterEntity.gameObject;
            return;
        }
        entity.AccelerateToward(character.transform.position);
    }
    
}
