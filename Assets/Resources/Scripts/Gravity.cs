using UnityEngine;

public class Gravity : MonoBehaviour
{
	public float mass;

	private Rigidbody2D rigid_Body_2D;

	public static System.Collections.Generic.List<Rigidbody2D> affected_Rigid_Bodies_List;

	static Gravity()
	{
		affected_Rigid_Bodies_List = new System.Collections.Generic.List<Rigidbody2D>();
	}

	private void Start()
	{
		rigid_Body_2D = this.gameObject.GetComponentInParent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		for(int i = 0; i < affected_Rigid_Bodies_List.Count; i++)
		{
			if(affected_Rigid_Bodies_List[i] != null)
			{
				if(affected_Rigid_Bodies_List[i] != this.rigid_Body_2D)
				{
					affected_Rigid_Bodies_List[i].AddForce((0.00000000006674f * ((this.mass * affected_Rigid_Bodies_List[i].mass) / (this.gameObject.transform.position - affected_Rigid_Bodies_List[i].gameObject.transform.position).sqrMagnitude)) *  (this.gameObject.transform.position - affected_Rigid_Bodies_List[i].gameObject.transform.position).normalized);    //Could use doubles during intermediary calculations and convert to a float at the end for more precision
				}
			}
			else
			{
				affected_Rigid_Bodies_List[i] = affected_Rigid_Bodies_List[affected_Rigid_Bodies_List.Count - 1];	//Swaps the last element in the list with the current
				affected_Rigid_Bodies_List.RemoveAt(affected_Rigid_Bodies_List.Count - 1);							//Removes the null-valued last element which reduces the list Count by 1
				i--;																								//The swapped-in element now needs to be checked
			}
		}
	}
}
