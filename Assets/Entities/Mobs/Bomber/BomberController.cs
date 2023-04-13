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
        FindCharacter();
    }

    // Update is called once per frame
    void Update() {
        if (character == null) {
            FindCharacter();
            return;
        }
        entity.AccelerateToward(character.transform.position);
    }
    
    void FindCharacter() {
        MainCharacterController controller = transform.parent.GetComponentInChildren(typeof(MainCharacterController), false) as MainCharacterController;
        if (controller != null) {
            character = controller.gameObject;
            characterEntity = character.GetComponent<Entity>();
            if (characterEntity == null) {
                Debug.LogError("characters should have entity components, but this one doesn't???");
            }
        }
    }
}
