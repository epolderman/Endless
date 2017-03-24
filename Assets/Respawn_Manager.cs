using UnityEngine;

public class Respawn_Manager : MonoBehaviour
{
	private static System.Collections.Generic.List<Respawn_Manager> respawn_Locations_List = new System.Collections.Generic.List<Respawn_Manager>();
	private static Collider2D[] colliders_In_Respawn_Location = new Collider2D[100];

	private int number_Of_Objects_In_Trigger = 0;

	public float respawn_Radius;

	void OnEnable()
	{
		this.number_Of_Objects_In_Trigger = 0;
	}

	void Start ()
	{
		Respawn_Manager.respawn_Locations_List.Add(this);
	}

	void OnDestroy()
	{
		Respawn_Manager.respawn_Locations_List.Remove(this);
	}

	public static Vector3 find_Best_Respawn_Location()
	{
		System.Collections.Generic.List<Respawn_Manager> best_Respawn_Locations = new System.Collections.Generic.List<Respawn_Manager>();
		int lowest_Object_Count = int.MaxValue;
		for(int i = 0; i < Respawn_Manager.respawn_Locations_List.Count; i++)
		{
			Respawn_Manager.respawn_Locations_List[i].number_Of_Objects_In_Trigger = Physics2D.OverlapCircleNonAlloc((Vector2)Respawn_Manager.respawn_Locations_List[i].transform.position, Respawn_Manager.respawn_Locations_List[i].respawn_Radius, Respawn_Manager.colliders_In_Respawn_Location);
//			Debug.Log("Location " + i + " has " + Respawn_Manager.respawn_Locations_List[i].number_Of_Objects_In_Trigger + " objects inside.");
			if(Respawn_Manager.respawn_Locations_List[i].number_Of_Objects_In_Trigger <= lowest_Object_Count)
			{
				if(Respawn_Manager.respawn_Locations_List[i].number_Of_Objects_In_Trigger < lowest_Object_Count)
				{
					best_Respawn_Locations.Clear();
					lowest_Object_Count = Respawn_Manager.respawn_Locations_List[i].number_Of_Objects_In_Trigger;
				}
				best_Respawn_Locations.Add(Respawn_Manager.respawn_Locations_List[i]);
			}
		}
//		Debug.Log("There are " + best_Respawn_Locations.Count + " good respawn locations.");
		int best_Respawn_Location_Index = Random.Range(0, best_Respawn_Locations.Count);
		Vector3 best_Respawn_Location = best_Respawn_Locations[best_Respawn_Location_Index].transform.position + (Vector3)Random.insideUnitCircle * best_Respawn_Locations[best_Respawn_Location_Index].respawn_Radius;
		return best_Respawn_Location;
	}
}
