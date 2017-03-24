using UnityEngine;
using System.Collections;

public class Blackhole : MonoBehaviour {

    void OnTriggerEnter2D ( Collider2D other )
    {
        other.gameObject.GetComponentInParent<Rigidbody2D>().drag = 0.5f;
    }

    void OnTriggerStay2D (Collider2D other)
    {
        //disable the players ship control while in black hole
        if (other.gameObject.GetComponentInParent<Ship_Controls>() != null)
        {
            other.gameObject.GetComponentInParent<Ship_Controls>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //enable the players ship control when exiting the black hole
        if (other.gameObject.GetComponentInParent<Ship_Controls>() != null)
        {
            other.gameObject.GetComponentInParent<Ship_Controls>().enabled = true;
        }
    }
}
