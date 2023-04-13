using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public class Explosion : MonoBehaviour
{
     public GameObject prefab;
     public int minParticles = 2;
     public int maxParticles = 5;

    // Start is called before the first frame update
    void Start() {
        if (maxParticles <= 0)
            Debug.LogError("maxParticles must be positive");
        if (minParticles < 0)
            Debug.LogError("minParticles must be nonnegative");
        if (maxParticles < minParticles)
            Debug.LogError("maxParticles must be at least as large as minParticles");
        if (prefab == null)
            Debug.LogError("prefab must be set");
    }

    public void Explode() {
        int numParticles = Random.Range(minParticles, maxParticles);
        for (int particleNum = 0; particleNum < numParticles; ++particleNum) {
            Instantiate(prefab, transform.position, Random.rotation, transform.parent);
        }
    }

}
