using UnityEngine;

public class Rotate : MonoBehaviour
{
	public float degrees_Per_Second;
	//private Vector3 planet_Offset = new Vector3(1395.0f, -1311.0f, 0.0f);
	void FixedUpdate ()
	{
		//this.gameObject.transform.RotateAround(this.planet_Offset, Vector3.forward, Time.deltaTime * this.degrees_Per_Second);
		this.gameObject.transform.Rotate(0.0f, 0.0f, Time.fixedDeltaTime * this.degrees_Per_Second);
	}
}
