using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchMakingClicks : MonoBehaviour 
{


    //Bugs List->//
    //
    //*POSSIBLEY FIXED//
    //possible bug in the input field when you join a room, leave the room, then try to the same one without
    //typing the actual name, then an error will occur.
    //TIP its how the input field works!
    //
    

    ///Handles all MatchMaking Button Clicks -> Listing rooms, Creating Rooms, etc.
    public Button thebutton;
    public InputField RoomInputField;
    private GameObject GameMenuManager;
    private GameManager scriptFinder;
    private string Room;
   // private RoomInfo[] roomsList;
    

    //for room selection drop down
    public Text RoomNameSelected;
    public Text RoomPlayersSelected;
    public Text RoomMaxPlayersSelected;
    public Button roomSelectionButton;
    public bool Selected;
    public int once;
    private int DetermineGameType;

    private GameObject Canvas;
    private MenuManager MMscript;

    public GameObject FFAmenu;
    public GameObject TDmenu;





	// Use this for initialization
	void Start ()
    {
        once = 0;
        Selected = false;
        Room = "";
        //gamemanager
       GameMenuManager = GameObject.Find("GameMenuManager");
       scriptFinder = GameMenuManager.GetComponent<GameManager>();

       InvokeRepeating("BlinkingRooms", 2, 0.75f);

        //canvas grab
       Canvas = GameObject.Find("Canvas");
       MMscript = Canvas.GetComponent<MenuManager>();

	}	
	// Update is called once per frame
	void Update () 
    {

	}
    public void getGameType()
    {
        if(scriptFinder.returnGameType() == "FFA")
        {
            DetermineGameType = 1;
            Debug.Log("GameType is FFA " + DetermineGameType);
        }
        else if (scriptFinder.returnGameType() == "Team Deathmatch")
        {
            DetermineGameType = 2;
            Debug.Log("GameType is TD " + DetermineGameType);
        }
        else
        {
            Debug.Log(" -- Major Error Within Setting the Game Type -- ");
        }


    }
    public int returnGTYPE()
    {
        return DetermineGameType;
    }

    ///joins a room with the name, or there is no room is creates then joins it
    public void CreateButtonClick()
    {
        if (Room != null && Room != "")
        {
            RoomOptions newRoomOptions = new RoomOptions();
            newRoomOptions.isVisible = true;
            newRoomOptions.maxPlayers = 8;
            newRoomOptions.isOpen = true;
            newRoomOptions.customRoomProperties = new ExitGames.Client.Photon.Hashtable();
            newRoomOptions.customRoomProperties.Add("GameType", DetermineGameType);
            newRoomOptions.customRoomPropertiesForLobby = new string [] {"GameType"};

            //RoomOptions roomO = new RoomOptions() { isVisible = true, maxPlayers = 6 };
            if( PhotonNetwork.JoinOrCreateRoom(Room, newRoomOptions, TypedLobby.Default) )
            {
               
                Debug.Log("Room Created with Room " + Room);
                //Debug.Log("Room has a game type of " + PhotonNetwork.room.customProperties["GameType"].ToString());
               
            }
        }
            else
            { ///if you click create it creates a room with player name

                Room = "RandomRoomChoice";
                RoomOptions newRoomOptions = new RoomOptions();
                newRoomOptions.isVisible = true;
                newRoomOptions.maxPlayers = 8;
                newRoomOptions.isOpen = true;
                newRoomOptions.customRoomProperties = new ExitGames.Client.Photon.Hashtable();
                newRoomOptions.customRoomProperties.Add("GameType", DetermineGameType);
                newRoomOptions.customRoomPropertiesForLobby = new string[] { "GameType" };
                
                RoomOptions roomO = new RoomOptions() { isVisible = true, maxPlayers = 6 };
                if (PhotonNetwork.JoinOrCreateRoom(Room, newRoomOptions, TypedLobby.Default))
                {

                    Debug.Log("Room Created with Room " + Room);
                    //Debug.Log("Room has a game type of " + PhotonNetwork.room.customProperties["GameType"].ToString());

                }

            
            
        
        
        }
      
    }
    
    public void Clear()
    {

        Room = "";
        RoomInputField.GetComponentInChildren<Text>().text = "";

    }

    ///set room name for creation
    public void CreateRoomEdit(string roomname)
    {
        Room = roomname;
    }

    //getter for roomname
    public string ReturnRoom()
    {
        return Room;
    }
    //setter for roomname
    public void SetRoom(string z)
    {
        Room = z;

    }


    //for debugging
    void OnJoinedRoom()
    {
        Debug.Log("Connected to Room ");
        Debug.Log("Room i am in " + PhotonNetwork.room.name);
        Debug.Log("Name is " + PhotonNetwork.player.name);
    }

    /// for the join room button in the drop down selection
    public void OnJoin()
    {
        
        PhotonNetwork.JoinRoom(Room);
      


    }
    public void BlinkingRooms()
    {
        

        if (once > 3)
        {
            once = 0;   
        }

        if (Selected == false)
        {
            if (once == 0)
            {
                RoomNameSelected.GetComponentInChildren<Text>().text = "Shuttle Name: .";
                RoomPlayersSelected.GetComponentInChildren<Text>().text = "Players Currently in Shuttle: .";
                RoomMaxPlayersSelected.GetComponentInChildren<Text>().text = "Max Amount of Players: .";
                
                
            }
            else if (once == 1)
            {
                RoomNameSelected.GetComponentInChildren<Text>().text = "Shuttle Name: ..";
                RoomPlayersSelected.GetComponentInChildren<Text>().text = "Players Currently in Shuttle: ..";
                RoomMaxPlayersSelected.GetComponentInChildren<Text>().text = "Max Amount of Players: ..";

            }
            else if(once == 2)
            {
                RoomNameSelected.GetComponentInChildren<Text>().text = "Shuttle Name: ...";
                RoomPlayersSelected.GetComponentInChildren<Text>().text = "Players Currently in Shuttle: ...";
                RoomMaxPlayersSelected.GetComponentInChildren<Text>().text = "Max Amount of Players: ...";
                

            }
            else if (once == 3)
            {
                RoomNameSelected.GetComponentInChildren<Text>().text = "Shuttle Name: ";
                RoomPlayersSelected.GetComponentInChildren<Text>().text = "Players Currently in Shuttle: ";
                RoomMaxPlayersSelected.GetComponentInChildren<Text>().text = "Max Amount of Players: ";


            }

            once++;

          


        }

    }

    public void DictateRoom()
    {
        if (DetermineGameType == 2)
        {
            MMscript.ShowMenu(TDmenu.GetComponent<Menu>());
        }
        else if(DetermineGameType == 1)
        {
            MMscript.ShowMenu(FFAmenu.GetComponent<Menu>());
        }

    }
 





















    //OnReceivedRoomListUpdate();
    //OnJoinedRoom();

    //private PhotonPlayer[] playerlist;
    // roomsList = PhotonNetwork.GetRoomList();
    // Debug.Log("Size is " + roomsList.Length);
    // if(roomsList.Length > 0)
    // {
    // for (int i = 0; i < roomsList.Length; i++)
    // {
    //  Debug.Log("Room Information for Room# " + (i+1) );
    //  Debug.Log("Roomlist " + roomsList[i].name);
    // Debug.Log("Max Players " + roomsList[i].maxPlayers);
    // }

    //  }
        
    
        
   



}
