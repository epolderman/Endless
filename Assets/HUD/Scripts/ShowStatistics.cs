using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowStatistics : MonoBehaviour 
{

    //playerlist
    private PhotonPlayer[] playerlist;

    //game obs
    public GameObject statsPanel;
    
    //users
    public Button u1;
    public Button u2;
    public Button u3;
    public Button u4;
    public Button u5;
    public Button u6;
    public Button u7;
    public Button u8;
    

    //kills
    public Button k1;
    public Button k2;
    public Button k3;
    public Button k4;
    public Button k5;
    public Button k6;
    public Button k7;
    public Button k8;

    //deaths
    public Button d1;
    public Button d2;
    public Button d3;
    public Button d4;
    public Button d5;
    public Button d6;
    public Button d7;
    public Button d8;
    
    //teams
    public Button redTeam;
    public Button blueTeam;
    public Button redScore;
    public Button blueScore;

    //winners
    public Text WinName;
    private bool WinnerFound;

    //main hud
    //center menu GUI object
    private GameObject mainHUD;
    private MainHUD MH;

    //gameMANGER
    private GameObject GameMenuManager;
    private GameManager scriptFinder;




	// Use this for initialization
	void Start ()
    {
        //find gamemanager
        mainHUD = GameObject.Find("MainHUDObject");
        MH = mainHUD.GetComponent<MainHUD>();

        //open up gamemanager            
        GameMenuManager = GameObject.Find("GameMenuManager");
        scriptFinder = GameMenuManager.GetComponent<GameManager>();

        WinnerFound = false;
        WinName.gameObject.SetActive(false);
        InvokeRepeating("FinalSet", 5, 2.0f);
	}
	
	// Update is called once per frame
	void Update () 
    {
       // Debug.Log("Red Score " + (int)PhotonNetwork.room.customProperties["Red"]);
       // Debug.Log("Blue Score " + (int)PhotonNetwork.room.customProperties["Blue"]);
	}

    public void GrabPlayerList()
    { 
        
        //get the length of playerList
        playerlist = PhotonNetwork.playerList;

        //Debug.Log("PlayerList size is " + playerlist.Length);
        
        //make certain buttons active
        switch (playerlist.Length)
        { 
            
            case 1:
                //user 1
                u1.gameObject.SetActive(true);
                k1.gameObject.SetActive(true);
                d1.gameObject.SetActive(true);
                //user 2
                u2.gameObject.SetActive(false);
                k2.gameObject.SetActive(false);
                d2.gameObject.SetActive(false);
                //user3
                u3.gameObject.SetActive(false);
                k3.gameObject.SetActive(false);
                d3.gameObject.SetActive(false);
                //user4
                u4.gameObject.SetActive(false);
                k4.gameObject.SetActive(false);
                d4.gameObject.SetActive(false);
                //user5
                u5.gameObject.SetActive(false);
                k5.gameObject.SetActive(false);
                d5.gameObject.SetActive(false);
                //user6
                u6.gameObject.SetActive(false);
                k6.gameObject.SetActive(false);
                d6.gameObject.SetActive(false);
                //user7
                u7.gameObject.SetActive(false);
                k7.gameObject.SetActive(false);
                d7.gameObject.SetActive(false);
                //user8
                u8.gameObject.SetActive(false);
                k8.gameObject.SetActive(false);
                d8.gameObject.SetActive(false);
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(true);
                    redScore.gameObject.SetActive(true);
                    //blue team
                    blueScore.gameObject.SetActive(true);
                    blueTeam.gameObject.SetActive(true);

                }
                else
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(false);
                    redScore.gameObject.SetActive(false);
                    //blue team
                    blueScore.gameObject.SetActive(false);
                    blueTeam.gameObject.SetActive(false);
                }
                break;
            case 2:
                //user 1
                u1.gameObject.SetActive(true);
                k1.gameObject.SetActive(true);
                d1.gameObject.SetActive(true);
                //user 2
                u2.gameObject.SetActive(true);
                k2.gameObject.SetActive(true);
                d2.gameObject.SetActive(true);
                //user3
                u3.gameObject.SetActive(false);
                k3.gameObject.SetActive(false);
                d3.gameObject.SetActive(false);
                //user4
                u4.gameObject.SetActive(false);
                k4.gameObject.SetActive(false);
                d4.gameObject.SetActive(false);
                //user5
                u5.gameObject.SetActive(false);
                k5.gameObject.SetActive(false);
                d5.gameObject.SetActive(false);
                //user6
                u6.gameObject.SetActive(false);
                k6.gameObject.SetActive(false);
                d6.gameObject.SetActive(false);
                //user7
                u7.gameObject.SetActive(false);
                k7.gameObject.SetActive(false);
                d7.gameObject.SetActive(false);
                //user8
                u8.gameObject.SetActive(false);
                k8.gameObject.SetActive(false);
                d8.gameObject.SetActive(false);
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(true);
                    redScore.gameObject.SetActive(true);
                    //blue team
                    blueScore.gameObject.SetActive(true);
                    blueTeam.gameObject.SetActive(true);

                }
                else
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(false);
                    redScore.gameObject.SetActive(false);
                    //blue team
                    blueScore.gameObject.SetActive(false);
                    blueTeam.gameObject.SetActive(false);
                }
                break;
            case 3:
                //user 1
                u1.gameObject.SetActive(true);
                k1.gameObject.SetActive(true);
                d1.gameObject.SetActive(true);
                //user 2
                u2.gameObject.SetActive(true);
                k2.gameObject.SetActive(true);
                d2.gameObject.SetActive(true);
                //user3
                u3.gameObject.SetActive(true);
                k3.gameObject.SetActive(true);
                d3.gameObject.SetActive(true);
                //user4
                u4.gameObject.SetActive(false);
                k4.gameObject.SetActive(false);
                d4.gameObject.SetActive(false);
                //user5
                u5.gameObject.SetActive(false);
                k5.gameObject.SetActive(false);
                d5.gameObject.SetActive(false);
                //user6
                u6.gameObject.SetActive(false);
                k6.gameObject.SetActive(false);
                d6.gameObject.SetActive(false);
                //user7
                u7.gameObject.SetActive(false);
                k7.gameObject.SetActive(false);
                d7.gameObject.SetActive(false);
                //user8
                u8.gameObject.SetActive(false);
                k8.gameObject.SetActive(false);
                d8.gameObject.SetActive(false);
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(true);
                    redScore.gameObject.SetActive(true);
                    //blue team
                    blueScore.gameObject.SetActive(true);
                    blueTeam.gameObject.SetActive(true);

                }
                else
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(false);
                    redScore.gameObject.SetActive(false);
                    //blue team
                    blueScore.gameObject.SetActive(false);
                    blueTeam.gameObject.SetActive(false);
                }
                break;
            case 4:
                //user 1
                u1.gameObject.SetActive(true);
                k1.gameObject.SetActive(true);
                d1.gameObject.SetActive(true);
                //user 2
                u2.gameObject.SetActive(true);
                k2.gameObject.SetActive(true);
                d2.gameObject.SetActive(true);
                //user3
                u3.gameObject.SetActive(true);
                k3.gameObject.SetActive(true);
                d3.gameObject.SetActive(true);
                //user4
                u4.gameObject.SetActive(true);
                k4.gameObject.SetActive(true);
                d4.gameObject.SetActive(true);
                //user5
                u5.gameObject.SetActive(false);
                k5.gameObject.SetActive(false);
                d5.gameObject.SetActive(false);
                //user6
                u6.gameObject.SetActive(false);
                k6.gameObject.SetActive(false);
                d6.gameObject.SetActive(false);
                //user7
                u7.gameObject.SetActive(false);
                k7.gameObject.SetActive(false);
                d7.gameObject.SetActive(false);
                //user8
                u8.gameObject.SetActive(false);
                k8.gameObject.SetActive(false);
                d8.gameObject.SetActive(false);
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(true);
                    redScore.gameObject.SetActive(true);
                    //blue team
                    blueScore.gameObject.SetActive(true);
                    blueTeam.gameObject.SetActive(true);

                }
                else
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(false);
                    redScore.gameObject.SetActive(false);
                    //blue team
                    blueScore.gameObject.SetActive(false);
                    blueTeam.gameObject.SetActive(false);
                }
                break;
            case 5:
                //user 1
                u1.gameObject.SetActive(true);
                k1.gameObject.SetActive(true);
                d1.gameObject.SetActive(true);
                //user 2
                u2.gameObject.SetActive(true);
                k2.gameObject.SetActive(true);
                d2.gameObject.SetActive(true);
                //user3
                u3.gameObject.SetActive(true);
                k3.gameObject.SetActive(true);
                d3.gameObject.SetActive(true);
                //user4
                u4.gameObject.SetActive(true);
                k4.gameObject.SetActive(true);
                d4.gameObject.SetActive(true);
                //user5
                u5.gameObject.SetActive(true);
                k5.gameObject.SetActive(true);
                d5.gameObject.SetActive(true);
                //user6
                u6.gameObject.SetActive(false);
                k6.gameObject.SetActive(false);
                d6.gameObject.SetActive(false);
                //user7
                u7.gameObject.SetActive(false);
                k7.gameObject.SetActive(false);
                d7.gameObject.SetActive(false);
                //user8
                u8.gameObject.SetActive(false);
                k8.gameObject.SetActive(false);
                d8.gameObject.SetActive(false);
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(true);
                    redScore.gameObject.SetActive(true);
                    //blue team
                    blueScore.gameObject.SetActive(true);
                    blueTeam.gameObject.SetActive(true);

                }
                else
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(false);
                    redScore.gameObject.SetActive(false);
                    //blue team
                    blueScore.gameObject.SetActive(false);
                    blueTeam.gameObject.SetActive(false);
                }
                break;
            case 6:
                //user 1
                u1.gameObject.SetActive(true);
                k1.gameObject.SetActive(true);
                d1.gameObject.SetActive(true);
                //user 2
                u2.gameObject.SetActive(true);
                k2.gameObject.SetActive(true);
                d2.gameObject.SetActive(true);
                //user3
                u3.gameObject.SetActive(true);
                k3.gameObject.SetActive(true);
                d3.gameObject.SetActive(true);
                //user4
                u4.gameObject.SetActive(true);
                k4.gameObject.SetActive(true);
                d4.gameObject.SetActive(true);
                //user5
                u5.gameObject.SetActive(true);
                k5.gameObject.SetActive(true);
                d5.gameObject.SetActive(true);
                //user6
                u6.gameObject.SetActive(true);
                k6.gameObject.SetActive(true);
                d6.gameObject.SetActive(true);
                //user7
                u7.gameObject.SetActive(false);
                k7.gameObject.SetActive(false);
                d7.gameObject.SetActive(false);
                //user8
                u8.gameObject.SetActive(false);
                k8.gameObject.SetActive(false);
                d8.gameObject.SetActive(false);
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(true);
                    redScore.gameObject.SetActive(true);
                    //blue team
                    blueScore.gameObject.SetActive(true);
                    blueTeam.gameObject.SetActive(true);

                }
                else
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(false);
                    redScore.gameObject.SetActive(false);
                    //blue team
                    blueScore.gameObject.SetActive(false);
                    blueTeam.gameObject.SetActive(false);
                }
                break;
            case 7:
                //user 1
                u1.gameObject.SetActive(true);
                k1.gameObject.SetActive(true);
                d1.gameObject.SetActive(true);
                //user 2
                u2.gameObject.SetActive(true);
                k2.gameObject.SetActive(true);
                d2.gameObject.SetActive(true);
                //user3
                u3.gameObject.SetActive(true);
                k3.gameObject.SetActive(true);
                d3.gameObject.SetActive(true);
                //user4
                u4.gameObject.SetActive(true);
                k4.gameObject.SetActive(true);
                d4.gameObject.SetActive(true);
                //user5
                u5.gameObject.SetActive(true);
                k5.gameObject.SetActive(true);
                d5.gameObject.SetActive(true);
                //user6
                u6.gameObject.SetActive(true);
                k6.gameObject.SetActive(true);
                d6.gameObject.SetActive(true);
                //user7
                u7.gameObject.SetActive(true);
                k7.gameObject.SetActive(true);
                d7.gameObject.SetActive(true);
                //user8
                u6.gameObject.SetActive(false);
                k6.gameObject.SetActive(false);
                d6.gameObject.SetActive(false);
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(true);
                    redScore.gameObject.SetActive(true);
                    //blue team
                    blueScore.gameObject.SetActive(true);
                    blueTeam.gameObject.SetActive(true);

                }
                else
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(false);
                    redScore.gameObject.SetActive(false);
                    //blue team
                    blueScore.gameObject.SetActive(false);
                    blueTeam.gameObject.SetActive(false);
                }
                break;
            case 8:
                //user 1
                u1.gameObject.SetActive(true);
                k1.gameObject.SetActive(true);
                d1.gameObject.SetActive(true);
                //user 2
                u2.gameObject.SetActive(true);
                k2.gameObject.SetActive(true);
                d2.gameObject.SetActive(true);
                //user3
                u3.gameObject.SetActive(true);
                k3.gameObject.SetActive(true);
                d3.gameObject.SetActive(true);
                //user4
                u4.gameObject.SetActive(true);
                k4.gameObject.SetActive(true);
                d4.gameObject.SetActive(true);
                //user5
                u5.gameObject.SetActive(true);
                k5.gameObject.SetActive(true);
                d5.gameObject.SetActive(true);
                //user6
                u6.gameObject.SetActive(true);
                k6.gameObject.SetActive(true);
                d6.gameObject.SetActive(true);
                //user7
                u7.gameObject.SetActive(true);
                k7.gameObject.SetActive(true);
                d7.gameObject.SetActive(true);
                //user8
                u8.gameObject.SetActive(true);
                k8.gameObject.SetActive(true);
                d8.gameObject.SetActive(true);
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(true);
                    redScore.gameObject.SetActive(true);
                    //blue team
                    blueScore.gameObject.SetActive(true);
                    blueTeam.gameObject.SetActive(true);

                }
                else
                {
                    //teamRed 
                    redTeam.gameObject.SetActive(false);
                    redScore.gameObject.SetActive(false);
                    //blue team
                    blueScore.gameObject.SetActive(false);
                    blueTeam.gameObject.SetActive(false);
                }
                break;
        
        
        
        }


    }


    public void SetStats()
    {


        playerlist = PhotonNetwork.playerList;

        //make certain buttons active
        switch (playerlist.Length)
        {

            case 1:
                u1.GetComponentInChildren<Text>().text = playerlist[0].name;
                k1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Kills"].ToString();
                d1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Deaths"].ToString();
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {
                    
                    redScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Red"].ToString();
                    blueScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Blue"].ToString();
                   

                }
                break;
            case 2:
                u1.GetComponentInChildren<Text>().text = playerlist[0].name;
                k1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Kills"].ToString();
                d1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Deaths"].ToString();
                u2.GetComponentInChildren<Text>().text = playerlist[1].name;
                k2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Kills"].ToString();
                d2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Deaths"].ToString();
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {

                    redScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Red"].ToString();
                    blueScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Blue"].ToString();


                }
                break;
            case 3:
                u1.GetComponentInChildren<Text>().text = playerlist[0].name;
                k1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Kills"].ToString();
                d1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Deaths"].ToString();
                u2.GetComponentInChildren<Text>().text = playerlist[1].name;
                k2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Kills"].ToString();
                d2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Deaths"].ToString();
                u3.GetComponentInChildren<Text>().text = playerlist[2].name;
                k3.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[2].customProperties["Kills"].ToString();
                d3.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[2].customProperties["Deaths"].ToString();
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {

                    redScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Red"].ToString();
                    blueScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Blue"].ToString();


                }
                break;
            case 4:
                u1.GetComponentInChildren<Text>().text = playerlist[0].name;
                k1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Kills"].ToString();
                d1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Deaths"].ToString();
                u2.GetComponentInChildren<Text>().text = playerlist[1].name;
                k2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Kills"].ToString();
                d2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Deaths"].ToString();
                u3.GetComponentInChildren<Text>().text = playerlist[2].name;
                k3.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[2].customProperties["Kills"].ToString();
                d3.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[2].customProperties["Deaths"].ToString();
                u4.GetComponentInChildren<Text>().text = playerlist[3].name;
                k4.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[3].customProperties["Kills"].ToString();
                d4.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[3].customProperties["Deaths"].ToString();
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {

                    redScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Red"].ToString();
                    blueScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Blue"].ToString();


                }
                break;
            case 5:
                //1
                u1.GetComponentInChildren<Text>().text = playerlist[0].name;
                k1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Kills"].ToString();
                d1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Deaths"].ToString();
                //2
                u2.GetComponentInChildren<Text>().text = playerlist[1].name;
                k2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Kills"].ToString();
                d2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Deaths"].ToString();
                //3
                u3.GetComponentInChildren<Text>().text = playerlist[2].name;
                k3.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[2].customProperties["Kills"].ToString();
                d3.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[2].customProperties["Deaths"].ToString();
                //4
                u4.GetComponentInChildren<Text>().text = playerlist[3].name;
                k4.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[3].customProperties["Kills"].ToString();
                d4.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[3].customProperties["Deaths"].ToString();
                //5
                u5.GetComponentInChildren<Text>().text = playerlist[4].name;
                k5.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[4].customProperties["Kills"].ToString();
                d5.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[4].customProperties["Deaths"].ToString();
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {

                    redScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Red"].ToString();
                    blueScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Blue"].ToString();


                }
                break;
            case 6:
                //1
                u1.GetComponentInChildren<Text>().text = playerlist[0].name;
                k1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Kills"].ToString();
                d1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Deaths"].ToString();
                //2
                u2.GetComponentInChildren<Text>().text = playerlist[1].name;
                k2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Kills"].ToString();
                d2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Deaths"].ToString();
                //3
                u3.GetComponentInChildren<Text>().text = playerlist[2].name;
                k3.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[2].customProperties["Kills"].ToString();
                d3.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[2].customProperties["Deaths"].ToString();
                //4
                u4.GetComponentInChildren<Text>().text = playerlist[3].name;
                k4.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[3].customProperties["Kills"].ToString();
                d4.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[3].customProperties["Deaths"].ToString();
                //5
                u5.GetComponentInChildren<Text>().text = playerlist[4].name;
                k5.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[4].customProperties["Kills"].ToString();
                d5.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[4].customProperties["Deaths"].ToString();
                //6
                u6.GetComponentInChildren<Text>().text = playerlist[5].name;
                k6.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[5].customProperties["Kills"].ToString();
                d6.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[5].customProperties["Deaths"].ToString();
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {

                    redScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Red"].ToString();
                    blueScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Blue"].ToString();


                }
                break;
            case 7:
                //1
                u1.GetComponentInChildren<Text>().text = playerlist[0].name;
                k1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Kills"].ToString();
                d1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Deaths"].ToString();
                //2
                u2.GetComponentInChildren<Text>().text = playerlist[1].name;
                k2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Kills"].ToString();
                d2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Deaths"].ToString();
                //3
                u3.GetComponentInChildren<Text>().text = playerlist[2].name;
                k3.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[2].customProperties["Kills"].ToString();
                d3.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[2].customProperties["Deaths"].ToString();
                //4
                u4.GetComponentInChildren<Text>().text = playerlist[3].name;
                k4.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[3].customProperties["Kills"].ToString();
                d4.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[3].customProperties["Deaths"].ToString();
                //5
                u5.GetComponentInChildren<Text>().text = playerlist[4].name;
                k5.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[4].customProperties["Kills"].ToString();
                d5.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[4].customProperties["Deaths"].ToString();
                //6
                u6.GetComponentInChildren<Text>().text = playerlist[5].name;
                k6.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[5].customProperties["Kills"].ToString();
                d6.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[5].customProperties["Deaths"].ToString();
                //7
                u6.GetComponentInChildren<Text>().text = playerlist[6].name;
                k6.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[6].customProperties["Kills"].ToString();
                d6.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[6].customProperties["Deaths"].ToString();
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {

                    redScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Red"].ToString();
                    blueScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Blue"].ToString();


                }
                break;
            case 8:
                //1
                u1.GetComponentInChildren<Text>().text = playerlist[0].name;
                k1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Kills"].ToString();
                d1.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[0].customProperties["Deaths"].ToString();
                //2
                u2.GetComponentInChildren<Text>().text = playerlist[1].name;
                k2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Kills"].ToString();
                d2.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[1].customProperties["Deaths"].ToString();
                //3
                u3.GetComponentInChildren<Text>().text = playerlist[2].name;
                k3.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[2].customProperties["Kills"].ToString();
                d3.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[2].customProperties["Deaths"].ToString();
                //4
                u4.GetComponentInChildren<Text>().text = playerlist[3].name;
                k4.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[3].customProperties["Kills"].ToString();
                d4.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[3].customProperties["Deaths"].ToString();
                //5
                u5.GetComponentInChildren<Text>().text = playerlist[4].name;
                k5.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[4].customProperties["Kills"].ToString();
                d5.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[4].customProperties["Deaths"].ToString();
                //6
                u6.GetComponentInChildren<Text>().text = playerlist[5].name;
                k6.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[5].customProperties["Kills"].ToString();
                d6.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[5].customProperties["Deaths"].ToString();
                //7
                u6.GetComponentInChildren<Text>().text = playerlist[6].name;
                k6.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[6].customProperties["Kills"].ToString();
                d6.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[6].customProperties["Deaths"].ToString();
                //8
                u6.GetComponentInChildren<Text>().text = playerlist[7].name;
                k6.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[7].customProperties["Kills"].ToString();
                d6.GetComponentInChildren<Text>().text = PhotonNetwork.playerList[7].customProperties["Deaths"].ToString();
                //TEAMS
                if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
                {

                    redScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Red"].ToString();
                    blueScore.GetComponentInChildren<Text>().text = PhotonNetwork.room.customProperties["Blue"].ToString();


                }
                break;

        }
        //end switch

                if(u1.GetComponentInChildren<Text>().text == scriptFinder.returnUserName())
                {
                    u1.GetComponentInChildren<Image>().color = Color.yellow;
                    k1.GetComponentInChildren<Image>().color = Color.yellow;
                    d1.GetComponentInChildren<Image>().color = Color.yellow;
                    d1.GetComponentInChildren<Text>().color = Color.black;
                }
                else if (u2.GetComponentInChildren<Text>().text == scriptFinder.returnUserName())
                {
                    u2.GetComponentInChildren<Image>().color = Color.yellow;
                    k2.GetComponentInChildren<Image>().color = Color.yellow;
                    d2.GetComponentInChildren<Image>().color = Color.yellow;
                    d2.GetComponentInChildren<Text>().color = Color.black;
                }
                else if (u3.GetComponentInChildren<Text>().text == scriptFinder.returnUserName())
                {
                    u3.GetComponentInChildren<Image>().color = Color.yellow;
                    k3.GetComponentInChildren<Image>().color = Color.yellow;
                    d3.GetComponentInChildren<Image>().color = Color.yellow;
                    d3.GetComponentInChildren<Text>().color = Color.black;
                }
                else if (u4.GetComponentInChildren<Text>().text == scriptFinder.returnUserName())
                {
                    u4.GetComponentInChildren<Image>().color = Color.yellow;
                    k4.GetComponentInChildren<Image>().color = Color.yellow;
                    d4.GetComponentInChildren<Image>().color = Color.yellow;
                    d4.GetComponentInChildren<Text>().color = Color.black;
                }
                else if (u5.GetComponentInChildren<Text>().text == scriptFinder.returnUserName())
                {
                    u5.GetComponentInChildren<Image>().color = Color.yellow;
                    k5.GetComponentInChildren<Image>().color = Color.yellow;
                    d5.GetComponentInChildren<Image>().color = Color.yellow;
                    d5.GetComponentInChildren<Text>().color = Color.black;
                }
                else if (u6.GetComponentInChildren<Text>().text == scriptFinder.returnUserName())
                {
                    u6.GetComponentInChildren<Image>().color = Color.yellow;
                    k6.GetComponentInChildren<Image>().color = Color.yellow;
                    d6.GetComponentInChildren<Image>().color = Color.yellow;
                    d6.GetComponentInChildren<Text>().color = Color.black;
                }
                else if (u7.GetComponentInChildren<Text>().text == scriptFinder.returnUserName())
                {
                    u7.GetComponentInChildren<Image>().color = Color.yellow;
                    k7.GetComponentInChildren<Image>().color = Color.yellow;
                    d7.GetComponentInChildren<Image>().color = Color.yellow;
                    d7.GetComponentInChildren<Text>().color = Color.black;
                }
                else if (u8.GetComponentInChildren<Text>().text == scriptFinder.returnUserName())
                {
                    u8.GetComponentInChildren<Image>().color = Color.yellow;
                    k8.GetComponentInChildren<Image>().color = Color.yellow;
                    d8.GetComponentInChildren<Image>().color = Color.yellow;
                    d8.GetComponentInChildren<Text>().color = Color.black;
                }
             


    }
    public void CheckWins()
    {

        if (WinnerFound != true)
        {

            if ((int)PhotonNetwork.room.customProperties["GameType"] == 2)
            {
                //checking TD winner

                //red team wins
                if ((int)PhotonNetwork.room.customProperties["Red"] >= 20)
                {
                    Debug.Log("Red Team Wins");
                    WinName.gameObject.SetActive(true);
                    WinName.text = "Red Teams Wins!";
                    WinnerFound = true;
                    MH.SetStatsActive();
                    
                }

                //blue teams wins
                if ((int)PhotonNetwork.room.customProperties["Blue"] >= 20)
                {
                    Debug.Log("Blue Team Wins");
                    WinName.gameObject.SetActive(true);
                    WinName.text = "Blue Teams Wins!";
                    WinnerFound = true;
                    MH.SetStatsActive();
                }

            }
            else if ((int)PhotonNetwork.room.customProperties["GameType"] == 1)
            {
                //checking FFA winner 

                for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
                {
                    if ((int)PhotonNetwork.playerList[i].customProperties["Kills"] >= 20)
                    {
                        Debug.Log("Player " + PhotonNetwork.playerList[i].name + " Wins!");
                        WinName.gameObject.SetActive(true);
                        WinName.text = "Player " + PhotonNetwork.playerList[i].name + " Wins!";
                        WinnerFound = true;
                        MH.SetStatsActive();

                    }


                }



            }
        }


    }
   
    public void FinalSet()
    {
        
        GrabPlayerList();
        SetStats();
        CheckWins();

    
    
    }

    
    
    
    
    


}
