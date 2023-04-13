using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomWalk : MonoBehaviour
{
    public Vector3 velocity = new Vector3();
    [Tooltip("typical change in velocity over 1 second")]
    public float sigma = 1.0f;
    public float maxLifetime = 5.0f;

    void Start() {
        Destroy(gameObject, Random.Range(0.0f, maxLifetime));
    }

    // Update is called once per frame
    void Update() {
        Vector3 deltaV = new Vector3(MathSup.RandomNormal(), MathSup.RandomNormal(), 0.0f) * Time.deltaTime;
        transform.localPosition += (velocity + 0.5f * deltaV) * Time.deltaTime;
        velocity += deltaV;
    }
}
