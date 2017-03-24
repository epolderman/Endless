using UnityEngine;

public class Scene_Manager : MonoBehaviour
{
	public static ExitGames.Client.Photon.Hashtable kills = new ExitGames.Client.Photon.Hashtable();	//Not reliably local player kills
	public static ExitGames.Client.Photon.Hashtable deaths = new ExitGames.Client.Photon.Hashtable();	//Should always contain local player deaths after launch
	public static ExitGames.Client.Photon.Hashtable team = new ExitGames.Client.Photon.Hashtable();		//Should always contain the local player's team after launch
	public static ExitGames.Client.Photon.Hashtable red_Team_Score = new ExitGames.Client.Photon.Hashtable();	//Not reliably team kills
	public static ExitGames.Client.Photon.Hashtable blue_Team_Score = new ExitGames.Client.Photon.Hashtable();	//Not reliably team kills
	public static ExitGames.Client.Photon.Hashtable player_Loaded = new ExitGames.Client.Photon.Hashtable();
	private static bool exists = false;

	void OnLevelWasLoaded(int level_Loaded)
	{
		if(level_Loaded == 0)
		{
			MenuManager.menu_Manager.gameObject.SetActive(true);
			MenuManager.menu_Manager.ShowMenu(GameObject.Find("MainMenu").GetComponent<Menu>());
			Start_Menu_Camera.start_Menu_Camera.gameObject.SetActive(true);
			Scene_Manager.player_Loaded["Loaded"] = false;
			PhotonNetwork.player.SetCustomProperties(Scene_Manager.player_Loaded);
			Ship_Controls.active_Ships_List.Clear();
		}
		else
		{
			MenuManager.menu_Manager.gameObject.SetActive(false);
			Start_Menu_Camera.start_Menu_Camera.gameObject.SetActive(false);
			Scene_Manager.player_Loaded["Loaded"] = true;
			PhotonNetwork.player.SetCustomProperties(Scene_Manager.player_Loaded);
		}
	}

	[PunRPC]
	void buffered_Photon_Network_Instantiate(Vector3 initial_Position, Quaternion initial_Rotation, int group_Index, int owner_Index)
	{
		if(PhotonNetwork.playerList[owner_Index] == PhotonNetwork.player)
		{
			Scene_Manager.kills["Kills"] = 0;
			Scene_Manager.deaths["Deaths"] = 0;
			Scene_Manager.team["Team"] = GameObject.Find("GameMenuManager").GetComponent<GameManager>().team;
			PhotonNetwork.player.SetCustomProperties(Scene_Manager.kills);
			PhotonNetwork.player.SetCustomProperties(Scene_Manager.deaths);
			PhotonNetwork.player.SetCustomProperties(Scene_Manager.team);
			GameObject player_Ship = PhotonNetwork.Instantiate(GameObject.Find("GameMenuManager").GetComponent<GameManager>().returnNameofShip(), Respawn_Manager.find_Best_Respawn_Location(), Quaternion.identity, 0);
			player_Ship.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.playerList[owner_Index]);
		}
	}

	public void delay_Launch_Level_Public()
	{
		for(int i = 0; i < PhotonNetwork.playerList.Length; i++)
		{
			Object.FindObjectOfType<Scene_Manager>().gameObject.GetComponent<PhotonView>().RPC("buffered_Photon_Network_Instantiate", PhotonTargets.AllBuffered, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, 0, i);
		}
	}

	public void wait_For_Players_To_Load()
	{
		this.StartCoroutine(internal_Wait_For_Players_To_Load());
	}

	private System.Collections.IEnumerator internal_Wait_For_Players_To_Load()
	{
		    int number_Of_Players_Loaded_Into_Level = 0;
			while(number_Of_Players_Loaded_Into_Level < PhotonNetwork.playerList.Length)
			{
				yield return new WaitForSeconds(0.1f);
				number_Of_Players_Loaded_Into_Level = 0;
				for(int i = 0; i < PhotonNetwork.playerList.Length; i++)
				{
					if((bool)PhotonNetwork.playerList[i].customProperties["Loaded"] == true)
					{
						number_Of_Players_Loaded_Into_Level = number_Of_Players_Loaded_Into_Level + 1;
					}
				}
			}
			this.delay_Launch_Level_Public();
	}
}
