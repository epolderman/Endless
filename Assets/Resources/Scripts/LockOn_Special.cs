using UnityEngine;
using System.Collections;

public class LockOn_Special : MonoBehaviour {

    public Ship_Controls shipControls;

    public float maxChargeTime;

    private float canFireDouble;
    private bool charging;

	// Use this for initialization
	void Start ()
    {
        charging = true;
        canFireDouble = maxChargeTime + Time.fixedTime;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(Time.fixedTime > canFireDouble && this.charging == true)
        {
            shipControls.fireDouble = true;
            charging = false;
        }

        if (shipControls.fireDouble == false && charging == false)
        {
            canFireDouble = maxChargeTime + Time.fixedTime;
            charging = true;
        }
	}
}
