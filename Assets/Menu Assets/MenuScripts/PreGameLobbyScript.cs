using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PreGameLobbyScript : MonoBehaviour 
{

    //object instance variabtles -> basically grabs objects information and sets them and manipulates them
    //this is for setting up the lobby information
    private GameObject GameMenuManager;
    private GameObject MatchActionButton;
    private MatchMakingClicks GrabThatScript;
    private GameManager scriptFinder;
    public Text GameType;
    public Text RoomWeAreIn;
    public Text NumerOfPlayersInRoom;
    public Text PlayerName;
    private PhotonPlayer[] playerlist;

    ///buttons
    public Button MasterLaunch;
    public Button ImReady;
    private int finalLauncher;


    public Button butt;
    public Image ShipImage;
    public Text ShipName;

    public GameObject ContObject;
    public ControllerHandler script;

 

    

    //sets up the lobby for the player
    public void OnLoad()
    {
        
		PhotonNetwork.automaticallySyncScene = true;

        this.finalLauncher = 0;

        //set the button to false to determine if client is the master or not
        MasterLaunch.gameObject.SetActive(false);
        ImReady.gameObject.SetActive(false);

        //gamemanager
        GameMenuManager = GameObject.Find("GameMenuManager");
        scriptFinder = GameMenuManager.GetComponent<GameManager>();


        //matchmaking
        MatchActionButton = GameObject.Find("ButtonActionObject");
        GrabThatScript = MatchActionButton.GetComponent<MatchMakingClicks>();

        //controllerhandler ->checking if in room
        ContObject = GameObject.Find("ControllerHandler");
        script = ContObject.GetComponent<ControllerHandler>();

      

        //programmer debugging
        Debug.Log("Beginning of on load " + GrabThatScript.ReturnRoom());

        //just checks before starting room
        if(GrabThatScript.ReturnRoom() != "")
        {
           

            Debug.Log("GameType " + scriptFinder.returnGameType());

            //get the gametype we are in
            GameType.GetComponentInChildren<Text>().text = "Game Type: " + scriptFinder.returnGameType();
            //wait for the lobby to load
            StartCoroutine(Order());

          

        }
        else
        {
            GUI.color = new Color(1f, 1f, 1f, Mathf.Sin(Time.realtimeSinceStartup * 4f) * 0.4f + 0.6f);

            GameType.GetComponentInChildren<Text>().text = "Game Type: " + scriptFinder.returnGameType();
            NumerOfPlayersInRoom.GetComponentInChildren<Text>().text = "Number of Players: Error";
            RoomWeAreIn.GetComponentInChildren<Text>().text = "Shuttle Name: Failed to Join Shuttle/Room";
            PlayerName.GetComponentInChildren<Text>().text = "Logged in as: " + PhotonNetwork.player.name;

        }

             

        
    }
    //rotates the ship sprite
    public void spinShipImage()
    {
        //Vector3 temp = new Vector3(6.103516e-05f, 0.0f, 1.0f + 1.0f);

        ShipImage.transform.Rotate(new Vector3(0.0f, 1.5f, 0.0f));

    }
    public void StartSpin()
    {
        //spin ship
        if (ShipImage != null)
        {
            InvokeRepeating("spinShipImage", 2, 0.05f);

        }

    }
    public void StopSpin()
    {
        //spin ship
        if (ShipImage != null)
        {
            CancelInvoke("spinShipImage");

        }

    }

	
	// Update is called once per frame
	void Update () 
    {

        //update lobby count
        if (PhotonNetwork.room != null)
        {
            NumerOfPlayersInRoom.GetComponentInChildren<Text>().text = "Number of Players: " + PhotonNetwork.room.playerCount;
        }

        if (playerlist != null)
        {
            //Debug.Log("Playerlist is " + playerlist.Length);

            if (finalLauncher == playerlist.Length)
            {
                MasterLaunch.gameObject.SetActive(true);
            }
        }


     
  
	}
    public void CheckStatus()
    {
        //yield return new WaitForSeconds(3.0f);

        if (scriptFinder != null)
        {
            if (scriptFinder.inRoom == true)
            {
                if (!PhotonNetwork.inRoom)
                {
                    if (butt != null)
                    {
                        if (butt.GetComponentInChildren<Text>().text == "Return")
                        {
                            butt.onClick.Invoke();

                        }
                    }
                }
            }
        }


    }

    ///wait for user to join lobby then run the function
    IEnumerator Order()
    {

        yield return new WaitForSeconds(5.0f);

        if (PhotonNetwork.inRoom)
        {
            scriptFinder.SetInRoomTrue();
        }

        Debug.Log("Master Client is " + PhotonNetwork.isMasterClient);
        Debug.Log("Master Client name is " + PhotonNetwork.masterClient);
        

        //make buttons visible or not
        if (PhotonNetwork.isMasterClient)
        {
            MasterLaunch.gameObject.SetActive(true);
            ImReady.gameObject.SetActive(false);
           
            PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
            {      
            {"Ready", true},
			{"Loaded", false }
            });

        }
        else
        {
            MasterLaunch.gameObject.SetActive(false);
            ImReady.gameObject.SetActive(true);
        }

        //set player name in room and check if it conflicts with others -> if so, then change
        setName();

        ///check room we are in       
        if (PhotonNetwork.room != null)
        {
            RoomWeAreIn.GetComponentInChildren<Text>().text = "Shuttle Name: " + PhotonNetwork.room.name;
            NumerOfPlayersInRoom.GetComponentInChildren<Text>().text = "Number of Players: " + PhotonNetwork.room.playerCount;
            PlayerName.GetComponentInChildren<Text>().text = "Logged in as: " + PhotonNetwork.player.name;
            
        }
        else
        {   //give the network time to grab the room
            RoomWeAreIn.GetComponentInChildren<Text>().text = "Shuttle Name: Generating...";
        }


        //checking if player is kicked
        if (script != null)
        {
            if (script.roommenu == true || script.roommenuTD == true)
            {
                InvokeRepeating("CheckStatus", 2, 3.0f);
                
            }

        }


    }

    //sets the name of player in room and handles all conflicts
    public void setName()
    {   
        //algo breakdown->
        //set username when going into the room
        //integer hitlist determines if there is more than one hit against the same name
        //if so -> generate random number 
        //to string that bitch
        //and set the new username (original + random #) in menumanager object, and photo username as well :)

        PhotonNetwork.player.name = scriptFinder.returnUserName();
        playerlist = PhotonNetwork.playerList;
        int hitlist = 0;

        Debug.Log("Size is " + playerlist.Length);
        if(playerlist.Length > 0)
        {
        for (int i = 0; i < playerlist.Length; i++)
        {
        
            if(PhotonNetwork.player.name == playerlist[i].name)
            {
              
                hitlist++;
            }

       
        }

        }


        if(hitlist >= 2)
        {
            string name = scriptFinder.returnUserName();
            int rndvlue = Random.Range(10, 50);
            scriptFinder.setUsername(name + rndvlue.ToString());
            Debug.Log("New Name is " + (name + rndvlue.ToString()));
            PhotonNetwork.player.name = scriptFinder.returnUserName();
            Debug.Log("--Changed the Name due to Username Clashes--");
        }

  
    }

    
    /// leave room
    public void LeaveRoom()
    {
        if (PhotonNetwork.inRoom)
        {
            PhotonNetwork.LeaveRoom();
            scriptFinder.SetInRoomFalse();
        }


        scriptFinder.SetInRoomFalse();

    }

    //ready for shuttle launch each user hits the im ready button
    public void EndlessFighterReady()
    {
		if((int)PhotonNetwork.room.customProperties["GameType"] == 1) //Asteroid level
		{
			PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
			{
            
				{"Ready", true},
				{"Loaded", false }
        
			});
		}
		else if((int)PhotonNetwork.room.customProperties["GameType"] == 2 && ((bool)PhotonNetwork.player.customProperties["Blue"] == true || (bool)PhotonNetwork.player.customProperties["Red"] == true))
		{
			PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
			{
            
				{"Ready", true},
				{"Loaded", false }
        
			});
		}


        

    }

    /// LAUNCH LEVEL
    public void MasterClientLaunch()
    {

        playerlist = PhotonNetwork.playerList;
        //Debug.Log("FinalLauncher is set to " + finalLauncher);

        if (playerlist.Length > 0)
        {
            for (int i = 0; i < playerlist.Length; i++)
            {
                if ((bool)playerlist[i].customProperties["Ready"] == true)
                {
					if((int)PhotonNetwork.room.customProperties["GameType"] == 1)
					{
			            finalLauncher++;
					}
					else
					{
						if((bool)PhotonNetwork.playerList[i].customProperties["Red"] == true || (bool)PhotonNetwork.playerList[i].customProperties["Blue"] == true)
						{
							finalLauncher++;
						}
					}
                }
                


            }
        }


        if(finalLauncher == playerlist.Length)
        {
			Scene_Manager.red_Team_Score["Red"] = 0;
			Scene_Manager.blue_Team_Score["Blue"] = 0;
			PhotonNetwork.room.SetCustomProperties(Scene_Manager.red_Team_Score);
			PhotonNetwork.room.SetCustomProperties(Scene_Manager.blue_Team_Score);
			if((int)PhotonNetwork.room.customProperties["GameType"] == 1)
			{
				PhotonNetwork.LoadLevel("AsteroidField");
			}
			else if((int)PhotonNetwork.room.customProperties["GameType"] == 2)
			{
				PhotonNetwork.LoadLevel("TeamDeathMatch");
			}
			Object.FindObjectOfType<Scene_Manager>().wait_For_Players_To_Load();
            PhotonNetwork.room.open = false;
            Debug.Log("---Launching Level---");


        }
        else
        {
            finalLauncher = 0;
            Debug.Log("---Error Launching---");
        }





        
    }


    //ready for shuttle launch no ready
    public void EndlessFighterNotReady()
    {

        PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
        {
            
            {"Ready", false},
			{"Loaded", false }
        
        });

      

    }


    public void SetBlueTeam()
    {
        scriptFinder.team = Health_Statistics.Team.Blue;
        PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
        {
            
            {"Blue", true},
            {"Red", false}
        
        });
        Debug.Log("Blue Team Selected!");
    }
    public void SetRedTeam()
    {

        scriptFinder.team = Health_Statistics.Team.Red;
        PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
        {
            
            {"Red", true},
            {"Blue", false}
        
        });
        Debug.Log("Red Team Selected!");
    }
    public void ResetTeam()
    {
        scriptFinder.team = Health_Statistics.Team.None;
        PhotonNetwork.player.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
        {
            
            {"Red", false},
            {"Blue", false}
        
        });
        Debug.Log("Reseting Team Selected");
    }


}
