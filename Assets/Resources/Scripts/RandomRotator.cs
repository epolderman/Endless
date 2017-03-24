using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

    public float rotate;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * rotate;
    }
}
