using UnityEngine;
using System.Collections;

public class Arbiter_Special : MonoBehaviour {

    private float shieldModifier;
    private float baseShield;

    public Shield_Statistics shieldStats;
    public Health_Statistics healthStats;

    // Use this for initialization
    void Start()
    {
        baseShield = shieldStats.maximum_Shield_Strength;
        shieldModifier = healthStats.hull_Integrity / healthStats.maximum_Hull_Integrity;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthStats.hull_Integrity / healthStats.maximum_Hull_Integrity < shieldModifier)
        {
            shieldModifier = healthStats.hull_Integrity / healthStats.maximum_Hull_Integrity;
            shieldStats.maximum_Shield_Strength = baseShield + (baseShield * (1.0f - shieldModifier));
        }
    }
}
