using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {


	public float speed = 100f;
	void Update () {
		transform.Translate (new Vector3 (
			Input.GetAxis ("Horizontal") * speed,
			Input.GetAxis ("Vertical") * speed,
			0) * Time.deltaTime);

		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = 1000f;
		} else
			speed = 100f;


	}
}
