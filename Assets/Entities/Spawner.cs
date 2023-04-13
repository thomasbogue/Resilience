using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawns objects either on start as well as continuously over a Poisson distribution
public class Spawner : MonoBehaviour {
    [Tooltip("the object to spawn -- for multiple types, use multiple spawners")]
    public GameObject prefab;
    [Tooltip("the average time between spawns")]
    public float inverseRate = 5.0f;
    [Tooltip("the number to spawn at start")]
    public int initialSpawn = 0;
    [Tooltip("minimum distance from character to spawn")]
    public float minDistance = 1.0f;
    [Tooltip("maximum distance from character to spawn")]
    public float maxDistance = 3.0f;
    private GameObject character { get {
        MainCharacterController controller = transform.parent.GetComponentInChildren<MainCharacterController>();
        if (controller == null)
            return null;
        return controller.gameObject;
    } }

    void Start() {
        for (int i = 0; i < initialSpawn; ++i) {
            Spawn();
        }
    }

    void Update() {
        if (Random.value < Time.deltaTime / inverseRate) {
            Spawn();
        }
    }

    public void Spawn() {
        Vector2 spawnDelta;
        do {
            spawnDelta = Random.insideUnitCircle * maxDistance;
        }
        while (spawnDelta.magnitude >= minDistance);
        Vector3 localSpawnPosition = character.transform.localPosition + new Vector3(spawnDelta.x, spawnDelta.y, 0.0f);
        GameObject spawn = Instantiate(prefab, transform.parent, false);
        spawn.transform.localPosition = localSpawnPosition;
    }
}
