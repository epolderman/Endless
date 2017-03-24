using UnityEngine;

public class Ship_Generator : MonoBehaviour
{
	[HideInInspector]
	public string ship_To_Instantiate;
	[HideInInspector]
	public float time_To_Generate_Ship;
//	[HideInInspector]
//	public string owner;
//	[HideInInspector]
//	public PhotonPlayer owner;
//	[HideInInspector]
//	public Camera owner_Camera;
	private PhotonPlayer owner;

	private void Start ()
	{
		this.owner = this.gameObject.GetComponent<PhotonView>().owner;
	}

	private void FixedUpdate ()
	{
		if(this.owner == PhotonNetwork.player && Time.fixedTime > this.time_To_Generate_Ship)
		{
//			Debug.Log("Instantiating a ship for player " + this.owner);
			GameObject player_Ship = PhotonNetwork.Instantiate(ship_To_Instantiate, Respawn_Manager.find_Best_Respawn_Location(), Quaternion.identity, 0);
			player_Ship.GetComponent<PhotonView>().TransferOwnership(this.owner);
			PhotonNetwork.Destroy(this.gameObject);
		}
	}
}
