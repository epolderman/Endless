using UnityEngine;
using System.Collections;

public class Jupiter_Special : MonoBehaviour {

    public Ship_Controls shipControls;

	// Use this for initialization
	void Start () {
        shipControls.canFireBothWeapons = true;
	}
}
