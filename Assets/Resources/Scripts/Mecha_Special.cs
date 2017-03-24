using UnityEngine;
using System.Collections;

public class Mecha_Special : MonoBehaviour {

    private float speedModifier;
    private float baseVelocity;

    public Ship_Controls shipControls;
    public Health_Statistics healthStats;

	// Use this for initialization
	void Start () {
        shipControls = (Ship_Controls)GetComponent("Ship_Controls");
        healthStats = (Health_Statistics)GetComponent("Health_Statistics");
        baseVelocity = shipControls.maximum_Ship_Velocity;
        speedModifier = healthStats.hull_Integrity / healthStats.maximum_Hull_Integrity;
	}
	
	// Update is called once per frame
	void Update () {
	    if(healthStats.hull_Integrity / healthStats.maximum_Hull_Integrity < speedModifier)
        {
            speedModifier = healthStats.hull_Integrity / healthStats.maximum_Hull_Integrity;
            shipControls.maximum_Ship_Velocity = baseVelocity + (baseVelocity * (1.0f - speedModifier));
            shipControls.maximum_Ship_Velocity_Squared = shipControls.maximum_Ship_Velocity * shipControls.maximum_Ship_Velocity;
            //print("New max speed = " + shipControls.maximum_Ship_Velocity);
        }
	}
}
