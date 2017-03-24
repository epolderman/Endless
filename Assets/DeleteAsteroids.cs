using UnityEngine;
using System.Collections;

public class DeleteAsteroids : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		//if (other.gameObject.tag == "SmallAsteroid")
			//Destroy(gameObject);

		//if (other.gameObject.tag == "LargeAsteroid")
			Destroy(gameObject);
	}
}
