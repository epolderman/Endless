using UnityEngine;

public class Ship_Camera : MonoBehaviour
{
	public GameObject attached_To = null;

	void Update ()
	{
		if(this.attached_To != null)
		{
			this.transform.position = new Vector3(this.attached_To.transform.position.x, this.attached_To.transform.position.y, this.transform.position.z);
		}
	}
}
