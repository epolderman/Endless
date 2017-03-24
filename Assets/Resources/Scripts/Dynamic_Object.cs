using UnityEngine;

public class Dynamic_Object : MonoBehaviour
{
	public Vector2 initial_Velocity;
	public float initial_Angular_Velocity;
	void Start ()
	{
		Gravity.affected_Rigid_Bodies_List.Add(this.gameObject.GetComponent<Rigidbody2D>());
		this.gameObject.GetComponent<Rigidbody2D>().velocity = initial_Velocity;
		this.gameObject.GetComponent<Rigidbody2D>().angularVelocity = initial_Angular_Velocity;
	}
}
