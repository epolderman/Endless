using UnityEngine;
using System.Collections;

public class ParralaxPlanet : MonoBehaviour {

	public float rotationDegreesPerSecond;

	void Update () {
		Vector3 rot = transform.rotation.eulerAngles;
		rot.y = rot.y + rotationDegreesPerSecond * Time.deltaTime;
		if(rot.y > 360)
			rot.y -= 360;
		else if(rot.y < 360)
			rot.y += 360;

		transform.eulerAngles = rot;
	
	}
}
