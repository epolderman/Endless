using UnityEngine;
using System.Collections;


public class LoginClick : MonoBehaviour {


    private GameObject GameMenuManager;
    private GameManager script;

	
    ///
    /// Handles Connection to Photon Server
    /// 
    public void OnClick()
    {
        GameMenuManager = GameObject.Find("GameMenuManager");
        script = GameMenuManager.GetComponent<GameManager>();
        ///connect to cloud service
        PhotonNetwork.ConnectUsingSettings("v4.2");
        //PhotonNetwork.autoJoinLobby = true;

        ///display log for debugging
        switch (PhotonNetwork.connectionState)
        {
            case ConnectionState.Disconnected:
                Debug.Log("Not Connected");
                break;
            default:
                Debug.Log("Connecting.." + PhotonNetwork.connectionStateDetailed);
                break;
        }

        PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
        {
            
            {"Ready", false},
            {"Red", false},
            {"Blue", false}
        
        });

        //setting team back to none
        script.team = Health_Statistics.Team.None;


    }

    /// <summary>
    /// Exit Application in Build 
    /// </summary>
    public void OnExitClick()
    {

        Application.Quit();
    
    }


}
