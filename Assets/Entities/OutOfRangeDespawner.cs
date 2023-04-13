using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfRangeDespawner : MonoBehaviour {
    public float despawnDistance = 10.0f;
    private GameObject character { get { 
        MainCharacterController controller = transform.parent.GetComponentInChildren<MainCharacterController>();
        if (controller == null)
            return null;
        return controller.gameObject;
    } }

    void Update() {
        if ((character.transform.position - transform.position).magnitude > despawnDistance) {
            Destroy(gameObject);
        }
    }
}
