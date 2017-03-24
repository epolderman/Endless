using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainHUD : MonoBehaviour 
{

    //in main hude settings menu
    public Button SettingsMainHUD;

    //in settings menu GUI
    public Button ExitINGAME;

    //center menu GUI object
    public GameObject CenterMenu;

    //controller menu GUI object
    public GameObject ControllerMenu;

    //main hud
    public GameObject thatMainHUD;

    //stats panel
    public GameObject StatsPanel;

    public GameObject AreYouPanel;
 
    //game manager object
    private GameObject GameMenuManager;
    private GameManager script;

    //controllermapping
    private GameObject ControlsMappingObjectIG;
    private ControlMapping cs;

    
    //list of buttons
    public struct ActiveButtons
    {
        public Button button;
        public bool isActive;

        public ActiveButtons(Button b, bool k)
        {
            button = b;
            isActive = k;

        }

    }

    //struct
    private ActiveButtons themBs;

    //iterator
    private int count;

    //main center menu
    private List<ActiveButtons> MainButtons;

    //controller menu
    private List<ActiveButtons> ControlButtons;

    //game stats menu
    private List<ActiveButtons> StatsButtons;

    //are you sure
    private List<ActiveButtons> AUSButtons;

    //for controller axis movements
    public bool InController;
    public bool InCenter;
    public bool InStats;
    public bool InAUS;

    //buttons
    public Button controller;
    public Button stats;
    public Button exitgame;
    public Button backtoingame;
    public Button Sbacktomenu;
    public Button Sbacktogame;
    public Button Cleft;
    public Button Cright;
    public Button Cdown;
    public Button Cforward;
    public Button Cweapon1;
    public Button Cweapon2;
    public Button Cboost;
    public Button Csettings;
    public Button Cmenuback;
    public Button Cmenuselect;
    public Button CmenuTUP;
    public Button CmenuTDOWN;
    public Button Cback;
    public Button PS4select;
    public Button PCselect;
    public Button XBOXselect;
    public Button AUSleave;
    public Button AUSreturn;

    private float minimal;
    private float maximal;




    //health bar
    public Slider healthBar;
    public Slider sheildBar;
    

    //playerlist
    private PhotonPlayer[] playerlist;


	// Use this for initialization
	void Start () 
    {
        //find gamemanager
        GameMenuManager = GameObject.Find("GameMenuManager");
        script = GameMenuManager.GetComponent<GameManager>();

        //make not active GUI's invisible
        CenterMenu.SetActive(false);
        ControllerMenu.SetActive(false);
        StatsPanel.SetActive(false);
        AreYouPanel.SetActive(false);
        InController = false;
        InCenter = false;
        InStats = false;
        InAUS = false;

        //iterator
        minimal = 0;
        maximal = 1;
        count = 0;

        //load the lists with buttons
        loadLists();

       
        //get health
//        InvokeRepeating("findMainPlayer", 1, 0.1f);
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        //get health for player
        //findMainPlayer();
        //Debug.Log("Red Health Stats " + Health_Statistics.Team.Red);
        //Debug.Log("Red Health Stats " + Health_Statistics.Team.Blue);

        LightUp();

      


        if (ControllerMenu.activeSelf == false && StatsPanel.activeSelf == false && ControllerMenu.activeSelf == false && AreYouPanel.activeSelf == false)
        {
            InController = false;
            InCenter = false;
            InStats = false;
            InAUS = false;
        }

        if (ControllerMenu.activeSelf == true)
        {

            InController = true;
            InCenter = false;
            InAUS = false;
            InStats = false;
           
        }


        if (CenterMenu.activeSelf == true)
        {
            InController = false;
            InCenter = true;
            InAUS = false;
            InStats = false;
            

        }

        if(StatsPanel.activeSelf == true)
        {
            InStats = true;
            InController = false;
            InCenter = false;
            InAUS = false;
        }

        if(AreYouPanel.activeSelf == true)
        {
            InController = false;
            InCenter = false;
            InAUS = true;
            InStats = false;
            

        }

        if (InController == true || InCenter == true || InStats == true || InAUS == true)
        {
            checkwhichResponse();
        }

 
	
	}
    //check which response
    public void checkwhichResponse()
    {

        //check which menu should be active
        if (InCenter == true)
        {
            //Debug.Log("Count ins Main is " + count);
            if(count > MainButtons.Count - 1)
            {
                count = 0;
            }
            MakeResponsive(MainButtons);
            

        }
        else if (InController == true)
        {
           // Debug.Log("Count ins Settings is " + count);
            if (count > ControlButtons.Count - 1)
            {
                count = 0;
            }

            MakeResponsive(ControlButtons);
            //Debug.Log("Controller is true");
            //thatMainHUD.SetActive(false);
        }
        else if(InStats == true)
        {
            //Debug.Log("Count ins Stats is " + count);
            if (count > StatsButtons.Count - 1)
            {
                count = 0;
            }

            MakeResponsive(StatsButtons);
           
        }
        else if(InAUS == true)
        {
            if (count > AUSButtons.Count - 1)
            {
                count = 0;
            }

            MakeResponsive(AUSButtons);

        }
       
    }
    /// show settings menu
    public void SettingsActive()
    {
        if (CenterMenu.activeSelf == true)
        {
            CenterMenu.SetActive(false);
            ControllerMenu.SetActive(false);
            StatsPanel.SetActive(false);
           

        }
        else
        {
            ControllerMenu.SetActive(false);
            CenterMenu.SetActive(true);
            StatsPanel.SetActive(false);

        }
       
    }
    /// hide settings menu
    public void SettingsNotActive()
    {
       CenterMenu.SetActive(false);
    }
    //show controller menu
    public void ControllerMenuActive()
    {
        CenterMenu.SetActive(false);
        ControllerMenu.SetActive(true);
        StatsPanel.SetActive(false);
    
    }

    //make controller menu false
    public void ControllerMenuNotActive()
    {

        ControllerMenu.SetActive(false);
        StatsPanel.SetActive(false);
        CenterMenu.SetActive(true);
    
    }
    public void BackToGame()
    {
        CenterMenu.SetActive(false);
        ControllerMenu.SetActive(false);
        InController = false;
        InCenter = false;
        InStats = false;
    }
    public void SetStatsActive()
    {
        StatsPanel.SetActive(true);
        CenterMenu.SetActive(false);
        ControllerMenu.SetActive(false);
        InController = false;
        InCenter = false;
        InStats = true;

    }
    public void SetStatsNotActive()
    {
        StatsPanel.SetActive(false);
        CenterMenu.SetActive(false);
        ControllerMenu.SetActive(false);
        InController = false;
        InCenter = false;
        InStats = false;
  
    }
    public void SetStatsNotActiveToMenu()
    {

        StatsPanel.SetActive(false);
        CenterMenu.SetActive(true);
        ControllerMenu.SetActive(false);
        InController = false;
        InCenter = true;
        InStats = false;

    }
    public void TriggerAreYouSure()
    {
        StatsPanel.SetActive(false);
        CenterMenu.SetActive(false);
        ControllerMenu.SetActive(false);
        AreYouPanel.SetActive(true);
        InAUS = true;
        InController = false;
        InCenter = false;
        InStats = false;

    }
    public void UnTriggerAreYouSure()
    {
        StatsPanel.SetActive(false);
        CenterMenu.SetActive(true);
        ControllerMenu.SetActive(false);
        AreYouPanel.SetActive(false);
        InAUS = false;
        InController = false;
        InCenter = true;
        InStats = false;
    }
    //check if in controller menu may or may not use
    public void CheckControllerMenuStatus()
    {
        if (ControllerMenu.activeSelf == true)
        {

            InController = true;
            //Debug.Log("Is true mofo");

        }
        else
        {
            InController = false;
            //Debug.Log("Is not true mofo");

        }
    
    }
    //make buttons active and responsive
    public void makeActive(List<ActiveButtons> theButton)
    {

        for (int i = 0; i < theButton.Count; i++)
        {
            if (theButton[count].button != null)
            {
                if (count != i)
                {
                    theButton[i] = new ActiveButtons(theButton[i].button, false);
                }
                else
                {
                    theButton[count] = new ActiveButtons(theButton[count].button, true);
                    theButton[count].button.Select();
                }
            }



        }



    }

    //load the active lists with buttons
    public void loadLists()
    {
         //main center menu
        MainButtons = new System.Collections.Generic.List<ActiveButtons>();
        //controller settings
        themBs.button = controller;
        themBs.isActive = false;
        MainButtons.Add(themBs);
        //back to in game
        themBs.button = backtoingame;
        themBs.isActive = false;
        MainButtons.Add(themBs);
        //stats
        themBs.button = stats;
        themBs.isActive = false;
        MainButtons.Add(themBs);
        //exit
        themBs.button = exitgame;
        themBs.isActive = false;
        MainButtons.Add(themBs);
        
        //areyousure
        AUSButtons = new System.Collections.Generic.List<ActiveButtons>();
        //controller settings
        themBs.button = AUSreturn;
        themBs.isActive = false;
        AUSButtons.Add(themBs);
        //back to in game
        themBs.button = AUSleave;
        themBs.isActive = false;
        AUSButtons.Add(themBs);
      

        //stats menu
        StatsButtons = new System.Collections.Generic.List<ActiveButtons>();
        //stats back to menu
        themBs.button = Sbacktomenu;
        themBs.isActive = false;
        StatsButtons.Add(themBs);
        //stats back to game
        themBs.button = Sbacktogame;
        themBs.isActive = false;
        StatsButtons.Add(themBs);
       

        //controller menu
        ControlButtons = new System.Collections.Generic.List<ActiveButtons>();
        //left
        themBs.button = Cleft;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //right
        themBs.button = Cright;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //forward
        themBs.button = Cdown;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //back
        themBs.button = Cforward;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //weapon1
        themBs.button = Cweapon1;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //weapon2
        themBs.button = Cweapon2;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //boost
        themBs.button = Cboost;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //settings
        themBs.button = Csettings;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //menuback
        themBs.button = Cmenuback;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //menuselect
        themBs.button = Cmenuselect;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //menutraverseup
        themBs.button = CmenuTUP;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //menutraversdown
        themBs.button = CmenuTDOWN;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //back
        themBs.button = Cback;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //pc select
        themBs.button = PCselect;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //ps4 select
        themBs.button = PS4select;
        themBs.isActive = false;
        ControlButtons.Add(themBs);
        //xbox slect
        themBs.button = XBOXselect;
        themBs.isActive = false;
        ControlButtons.Add(themBs);

    }

    public void LightUp()
    {




        //select in game menu
        if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.SettingsSelect.keyCode) ||
            Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACsettingsIG.keyCode))
        {
            SettingsMainHUD.onClick.Invoke();

        }

       

        
    
    
    
    
    }
    //make panel buttons responsive
    public void MakeResponsive(List<ActiveButtons> theButton)
    {

        makeActive(theButton);


        //your down traverse joystick
        if ((Input.GetAxis("HUDvertical") == 1))
        {

            minimal += 0.17f;

            if (minimal >= 1.00f)
            {
                count += 1;
                minimal = 0;
            }

            if (count > theButton.Count - 1)
            {
                count = 0;
            }

        }


        //up joystick traverse
        if (Input.GetAxis("HUDvertical") == -1)
        {
            maximal -= 0.17f;

            if (maximal <= 0.0f)
            {
                maximal = 1;
                count -= 1;

            }

            if (count < 0)
            {
                count = theButton.Count - 1;
            }

        }

        //select
        if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mselectmenu.keyCode))
        {
            for (int i = 0; i < theButton.Count; i++)
            {
                if (theButton[i].isActive == true)
                {
                    //Debug.Log(theButton[i].button + " is being selected");
                    theButton[i].button.onClick.Invoke();
                    count = 0;
                }

            }

        }

        if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMselectmenu.keyCode))
        {
            //Debug.Log("count is " + count);
            for (int i = 0; i < theButton.Count; i++)
            {


                if (theButton[i].isActive == true)
                {
                    //Debug.Log(theButton[i].button + " is being selected");
                    theButton[i].button.onClick.Invoke();
                    count = 0;
                }

            }

        }



        EscapeButton();


    
    }
    public void EscapeButton()
    {
        if (InController == true)
        {


            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
            || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
            {

                Cback.onClick.Invoke();
                count = 0;

            }
        }
        else if (InCenter == true)
        {
           
                if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
               || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
                {

                    backtoingame.onClick.Invoke();
                    count = 0;
                }
            
        }
        else if(InStats == true)
        {

            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
               || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
            {

                Sbacktomenu.onClick.Invoke();
                count = 0;
            }



        }
        else if(InAUS == true)
        {

            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
              || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
            {

                AUSreturn.onClick.Invoke();
                count = 0;
            }


        }
    }
    //leave the game
    public void leaveGame()
    {

      
        InCenter = false;
        InController = false;
        InStats = false;
        InAUS = false;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
		UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

  
/*    public void findMainPlayer()
    {


//        Debug.Log("Finding Health for Player");
        if (Ship_Controls.active_Ships_List.Count != 0)
        {
            for (int i = 0; i < Ship_Controls.active_Ships_List.Count; i++)
            {
                if (Ship_Controls.active_Ships_List[i].player_Controlled == true)
                {
                    

                    //sheild statistics
                    healthBar.value = Ship_Controls.active_Ships_List[i].health_Statistics.hull_Integrity;

                    //sheild statistics
                    sheildBar.value = Ship_Controls.active_Ships_List[i].health_Statistics.shieldStats.shield_Strength;
                 
                }


            }
        }



    }*/

    






}
