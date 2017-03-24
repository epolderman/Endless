using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public static GameObject game_Manager = null;
    
    /// <summary>
    /// Main Game Information Object. Collects User Inforamtion Data
    /// </summary>


    public GameObject Canvas;
    public bool inRoom;

    

    ///manage IMPORTANT user information across the whole menu system
    private string Username = "";
    private string GameMode = "";

    //maintains the ship information for the user
    private struct CurrentShip
    { 
        public string shipName;
        public string path;
 
    }
    private CurrentShip userShip;
	[HideInInspector]
	public Health_Statistics.Team team = Health_Statistics.Team.None;

    //script ship rotation grab
    private ShipRotation ClassShipRotation;
    private GameObject ControlShips;

    //script for controls settings
    private ControlMapping CMaps;
    private GameObject ControlsMappingObject;

    

    ///the controls keyboard.mouse
    public struct twoMap
    {
        public string name;
        public int keyCode;

    }
    
    //controls keyboard mouse //ps4 // xbox one
    public struct ControlSelected
    {
        //keyboard.mouse
        public twoMap moveLeft;
        public twoMap moveRight;
        public twoMap moveBack;
        public twoMap moveForward;
        public twoMap weapon1;
        public twoMap weapon2;
        //added
        public twoMap boost;
        public twoMap SettingsSelect;
        //end added
        public twoMap Mbackmenu;
        public twoMap Mselectmenu;
        public twoMap MtraverseUp;
        public twoMap MtraverseDown;
        //active joystick AC = Active Controller
        public twoMap ACmoveLeft;
        public twoMap ACmoveRight;
        public twoMap ACmoveBack;
        public twoMap ACmoveForward;
        public twoMap ACweapon1;
        public twoMap ACweapon2;
        //added
        public twoMap ACboost;
        public twoMap ACsettingsIG;
        //end added
        public twoMap ACMbackmenu;
        public twoMap ACMselectmenu;
        public twoMap ACMtraverseUp;
        public twoMap ACMtraverseDown;
      

    }

    public ControlSelected userControlsKeyboard;

    //saving button mappings
    public struct TheMappingsSaved
    {
        public string ps4moveleft;
        public string ps4moveright;
        public string ps4moveforward;
        public string ps4moveback;
        public string ps4weapon1;
        public string ps4weapon2;
        public string ps4boost;
        public string ps4selectIG;
        public string ps4selectM;
        public string ps4backM;
        public string ps4traverseUPM;
        public string ps4traverseDOWNM;

        public string Xmoveleft;
        public string Xmoveright;
        public string Xmoveforward;
        public string Xmoveback;
        public string Xweapon1;
        public string Xweapon2;
        public string Xboost;
        public string XselectIG;
        public string XselectM;
        public string XbackM;
        public string XtraverseUPM;
        public string XtraverseDOWNM;
      
    }

    //for savings the mapping from control window to control window
    public TheMappingsSaved iverson;

	void Awake()
	{
		if(GameManager.game_Manager == null)
		{
			Object.DontDestroyOnLoad(this.gameObject);
			GameManager.game_Manager= this.gameObject;
		}
		else if(GameManager.game_Manager != this.gameObject)
		{
			Object.DestroyImmediate(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () 
    {
        //defulat username
        Username = "EndlessPilotX1";

        ///for setting user ship selection
        ControlShips = GameObject.Find("ControlShips");
        ClassShipRotation = ControlShips.GetComponent<ShipRotation>();

        //for setting controls
        ControlsMappingObject = GameObject.Find("ControlsMappingObject");
        CMaps = ControlsMappingObject.GetComponent<ControlMapping>();
       
        //initz the button saves
        initz();

        //set user in room to false
        SetInRoomFalse();
   
        //start setting information
        StartCoroutine(GrabShipInfo());
        StartCoroutine(GrabControlInfo());

        SetMappinvsViaScriptOnStart();

        

	}
    public void SetMappinvsViaScriptOnStart()
    {


        //xbox
        iverson.Xmoveleft = "LeftJoyStick";
        iverson.Xmoveright = "LeftJoyStick";
        iverson.Xmoveforward = "LeftJoyStick";
        iverson.Xmoveback = "LeftJoyStick";
        iverson.Xweapon1 = "A";
        iverson.Xweapon2 = "B";
        iverson.Xboost = "Right Bumper";
        iverson.XselectIG = "A";
        iverson.XbackM = "B";
        iverson.XselectM = "Left Bumper";
        iverson.XtraverseUPM = "D-Pad";
        iverson.XtraverseDOWNM = "D-Pad";


        //ps4
        iverson.ps4moveleft = "LeftJoyStick";
        iverson.ps4moveright = "LeftJoyStick";
        iverson.ps4moveforward = "LeftJoyStick";
        iverson.ps4moveback = "LeftJoyStick";
        iverson.ps4weapon1 = "Square";
        iverson.ps4weapon2 = "X";
        iverson.ps4boost = "L1";
        iverson.ps4selectIG = "R1";
        iverson.ps4backM = "Circle";
        iverson.ps4selectM = "L1";
        iverson.ps4traverseUPM = "D-Pad";
        iverson.ps4traverseDOWNM = "D-Pad";

        //key
        userControlsKeyboard.moveLeft.name = "A";
        userControlsKeyboard.moveRight.name = "D";
        userControlsKeyboard.moveBack.name = "S";
        userControlsKeyboard.moveForward.name = "W";
        userControlsKeyboard.weapon1.name = "Left Click";
        userControlsKeyboard.weapon2.name = "Right Click";
        userControlsKeyboard.boost.name = "Z";
        userControlsKeyboard.SettingsSelect.name = "K";
        userControlsKeyboard.Mbackmenu.name = "Escape";
        userControlsKeyboard.Mselectmenu.name = "S";
        userControlsKeyboard.MtraverseUp.name = "Up Arrow";
        userControlsKeyboard.MtraverseDown.name = "Down Arrow";


    }



    // Update is called once per frame
	public void Update () 
    {
       

	}
    //initz
    public void initz()
    {
        iverson.ps4backM = "";
        iverson.ps4moveleft = "";
        iverson.ps4moveright = "";
        iverson.ps4moveback = "";
        iverson.ps4moveforward = "";
        iverson.ps4selectIG = "";
        iverson.ps4selectM = "";
        iverson.ps4traverseUPM = "";
        iverson.ps4traverseDOWNM = "";
        iverson.ps4weapon1 = "";
        iverson.ps4weapon2 = "";
        iverson.ps4boost = "";

        iverson.XbackM = "";
        iverson.Xmoveleft = "";
        iverson.Xmoveright = "";
        iverson.Xmoveback = "";
        iverson.Xmoveforward = "";
        iverson.XselectIG = "";
        iverson.XselectM = "";
        iverson.XtraverseUPM = "";
        iverson.XtraverseDOWNM = "";
        iverson.Xweapon1 = "";
        iverson.Xweapon2 = "";
        iverson.Xboost = "";

    }

    // Set Game Type
    public void setGameType(string gametype)
    {
        GameMode = gametype;

    }

    public string returnGameType()
    {
        return GameMode;
    }
    public string returnUserName()
    {
        return Username;
    }

    ///Updates Username
    public void setUsername(string updated)
    {

        Username = updated;
        Debug.Log("Username Updated to " + updated);

    }
    public void SetInRoomTrue()
    {
        inRoom = true;
    }
    public void SetInRoomFalse()
    {
        inRoom = false;
    }

    //to save the user from having an empty string as a username
    public void setDefaultUsername()
    {
        if (Username == "")
        {
            Username = "EndlessPilotX1";
        }

    }
    public void setNameOfShip()
    {
        userShip.shipName = ClassShipRotation.returnCurrentShipName();
 
    }
    public void setNameOfPath()
    {
        userShip.path = ClassShipRotation.returnCurrentShipPath();
    
    }
    public string returnNameofShip()
    {
        return userShip.shipName;

    }
    public string returnNameofPath()
    {
        return userShip.path;

    }

    //gets selected ship information initialzies
    IEnumerator GrabShipInfo()
    {
        yield return new WaitForSeconds(2.0f);

        userShip.shipName = ClassShipRotation.returnCurrentShipName();
        userShip.path = ClassShipRotation.returnCurrentShipPath();


    }
    //this initializes all controls 2 seconds after menu starts up
    IEnumerator GrabControlInfo()
    {
        yield return new WaitForSeconds(1.0f);

        //keyboard mappings

        userControlsKeyboard.moveBack.keyCode = CMaps.FinalIndieMap.moveBack.keyCode;
        userControlsKeyboard.moveBack.name = CMaps.FinalIndieMap.moveBack.name;

        userControlsKeyboard.moveLeft.keyCode = CMaps.FinalIndieMap.moveLeft.keyCode;
        userControlsKeyboard.moveLeft.name = CMaps.FinalIndieMap.moveLeft.name;

        userControlsKeyboard.moveRight.keyCode = CMaps.FinalIndieMap.moveRight.keyCode;
        userControlsKeyboard.moveRight.name = CMaps.FinalIndieMap.moveRight.name;

        userControlsKeyboard.moveForward.keyCode = CMaps.FinalIndieMap.moveForward.keyCode;
        userControlsKeyboard.moveForward.name = CMaps.FinalIndieMap.moveForward.name;

        userControlsKeyboard.weapon1.keyCode = CMaps.FinalIndieMap.weapon1.keyCode;
        userControlsKeyboard.weapon1.name = CMaps.FinalIndieMap.weapon1.name;

        userControlsKeyboard.weapon2.keyCode = CMaps.FinalIndieMap.weapon2.keyCode;
        userControlsKeyboard.weapon2.name = CMaps.FinalIndieMap.weapon2.name;

        //boost
        userControlsKeyboard.boost.keyCode = CMaps.FinalIndieMap.boost.keyCode;
        userControlsKeyboard.boost.name = CMaps.FinalIndieMap.boost.name;

        //settingsIG
        userControlsKeyboard.SettingsSelect.keyCode = CMaps.FinalIndieMap.SettingsSelect.keyCode;
        userControlsKeyboard.SettingsSelect.name = CMaps.FinalIndieMap.SettingsSelect.name;

        userControlsKeyboard.Mbackmenu.keyCode = CMaps.FinalIndieMap.Mbackmenu.keyCode;
        userControlsKeyboard.Mbackmenu.name = CMaps.FinalIndieMap.Mbackmenu.name;

        userControlsKeyboard.Mselectmenu.keyCode = CMaps.FinalIndieMap.Mselectmenu.keyCode;
        userControlsKeyboard.Mselectmenu.name = CMaps.FinalIndieMap.Mselectmenu.name;

        userControlsKeyboard.MtraverseUp.keyCode = CMaps.FinalIndieMap.MtraverseUp.keyCode;
        userControlsKeyboard.MtraverseUp.name = CMaps.FinalIndieMap.MtraverseUp.name;

        userControlsKeyboard.MtraverseDown.keyCode = CMaps.FinalIndieMap.MtraverseDown.keyCode;
        userControlsKeyboard.MtraverseDown.name = CMaps.FinalIndieMap.MtraverseDown.name;

        //controller mappings

        //select menu
        userControlsKeyboard.ACMselectmenu.keyCode = CMaps.FinalIndieMap.activeControllerMSelectM.keyCode;
        userControlsKeyboard.ACMselectmenu.name = CMaps.FinalIndieMap.activeControllerMSelectM.name;

        //back menu
        userControlsKeyboard.ACMbackmenu.keyCode = CMaps.FinalIndieMap.activeControllerMBackM.keyCode;
        userControlsKeyboard.ACMbackmenu.name = CMaps.FinalIndieMap.activeControllerMBackM.name;

        //traverse up menu
        userControlsKeyboard.ACMtraverseUp.keyCode = CMaps.FinalIndieMap.activeControllerMTU.keyCode;
        userControlsKeyboard.ACMtraverseUp.name = CMaps.FinalIndieMap.activeControllerMTU.name;

        //traverse down menu
        userControlsKeyboard.ACMtraverseDown.keyCode = CMaps.FinalIndieMap.activeControllerMTD.keyCode;
        userControlsKeyboard.ACMtraverseDown.name = CMaps.FinalIndieMap.activeControllerMTD.name;

        //in game move left
        userControlsKeyboard.ACmoveLeft.keyCode = CMaps.FinalIndieMap.activeControllerML.keyCode;
        userControlsKeyboard.ACmoveLeft.name = CMaps.FinalIndieMap.activeControllerML.name;

        //in game move back
        userControlsKeyboard.ACmoveBack.keyCode = CMaps.FinalIndieMap.activeControllerMB.keyCode;
        userControlsKeyboard.ACmoveBack.name = CMaps.FinalIndieMap.activeControllerMB.name;

        //in game move right
         userControlsKeyboard.ACmoveRight.keyCode = CMaps.FinalIndieMap.activeControllerMR.keyCode;
        userControlsKeyboard.ACmoveRight.name = CMaps.FinalIndieMap.activeControllerMR.name;

        //in game move forward
        userControlsKeyboard.ACmoveForward.keyCode = CMaps.FinalIndieMap.activeControllerMF.keyCode;
        userControlsKeyboard.ACmoveForward.name = CMaps.FinalIndieMap.activeControllerMF.name;

        //in game weapon 1
        userControlsKeyboard.ACweapon1.keyCode = CMaps.FinalIndieMap.activeControllerW1.keyCode;
        userControlsKeyboard.ACweapon1.name = CMaps.FinalIndieMap.activeControllerW1.name;

        //in game weapon 2
        userControlsKeyboard.ACweapon2.keyCode = CMaps.FinalIndieMap.activeControllerW2.keyCode;
        userControlsKeyboard.ACweapon2.name = CMaps.FinalIndieMap.activeControllerW2.name;

       //boost
        userControlsKeyboard.ACboost.keyCode = CMaps.FinalIndieMap.ACboost.keyCode;
        userControlsKeyboard.ACboost.name = CMaps.FinalIndieMap.ACboost.name;

        //settings INGAME
        userControlsKeyboard.ACsettingsIG.keyCode = CMaps.FinalIndieMap.ACsettingsIG.keyCode;
        userControlsKeyboard.ACsettingsIG.name = CMaps.FinalIndieMap.ACsettingsIG.name;
           

        
    }
    //this is for edits / if user edits in the control menu 
    public void setUserControls()
    {
        //keyboard mappings
      
        userControlsKeyboard.moveBack.keyCode = CMaps.FinalIndieMap.moveBack.keyCode;
        userControlsKeyboard.moveBack.name = CMaps.FinalIndieMap.moveBack.name;

        userControlsKeyboard.moveLeft.keyCode = CMaps.FinalIndieMap.moveLeft.keyCode;
        userControlsKeyboard.moveLeft.name = CMaps.FinalIndieMap.moveLeft.name;

        userControlsKeyboard.moveRight.keyCode = CMaps.FinalIndieMap.moveRight.keyCode;
        userControlsKeyboard.moveRight.name = CMaps.FinalIndieMap.moveRight.name;

        userControlsKeyboard.moveForward.keyCode = CMaps.FinalIndieMap.moveForward.keyCode;
        userControlsKeyboard.moveForward.name = CMaps.FinalIndieMap.moveForward.name;

        userControlsKeyboard.weapon1.keyCode = CMaps.FinalIndieMap.weapon1.keyCode;
        userControlsKeyboard.weapon1.name = CMaps.FinalIndieMap.weapon1.name;

        userControlsKeyboard.weapon2.keyCode = CMaps.FinalIndieMap.weapon2.keyCode;
        userControlsKeyboard.weapon2.name = CMaps.FinalIndieMap.weapon2.name;

        //boost
        userControlsKeyboard.boost.keyCode = CMaps.FinalIndieMap.boost.keyCode;
        userControlsKeyboard.boost.name = CMaps.FinalIndieMap.boost.name;

        //settingsIG
        userControlsKeyboard.SettingsSelect.keyCode = CMaps.FinalIndieMap.SettingsSelect.keyCode;
        userControlsKeyboard.SettingsSelect.name = CMaps.FinalIndieMap.SettingsSelect.name;


        userControlsKeyboard.Mbackmenu.keyCode = CMaps.FinalIndieMap.Mbackmenu.keyCode;
        userControlsKeyboard.Mbackmenu.name = CMaps.FinalIndieMap.Mbackmenu.name;

        userControlsKeyboard.Mselectmenu.keyCode = CMaps.FinalIndieMap.Mselectmenu.keyCode;
        userControlsKeyboard.Mselectmenu.name = CMaps.FinalIndieMap.Mselectmenu.name;

        userControlsKeyboard.MtraverseUp.keyCode = CMaps.FinalIndieMap.MtraverseUp.keyCode;
        userControlsKeyboard.MtraverseUp.name = CMaps.FinalIndieMap.MtraverseUp.name;

        userControlsKeyboard.MtraverseDown.keyCode = CMaps.FinalIndieMap.MtraverseDown.keyCode;
        userControlsKeyboard.MtraverseDown.name = CMaps.FinalIndieMap.MtraverseDown.name;

        //controller mappings

        //select menu
        userControlsKeyboard.ACMselectmenu.keyCode = CMaps.FinalIndieMap.activeControllerMSelectM.keyCode;
        userControlsKeyboard.ACMselectmenu.name = CMaps.FinalIndieMap.activeControllerMSelectM.name;

        //back menu
        userControlsKeyboard.ACMbackmenu.keyCode = CMaps.FinalIndieMap.activeControllerMBackM.keyCode;
        userControlsKeyboard.ACMbackmenu.name = CMaps.FinalIndieMap.activeControllerMBackM.name;

        //traverse up menu
        userControlsKeyboard.ACMtraverseUp.keyCode = CMaps.FinalIndieMap.activeControllerMTU.keyCode;
        userControlsKeyboard.ACMtraverseUp.name = CMaps.FinalIndieMap.activeControllerMTU.name;

        //traverse down menu
        userControlsKeyboard.ACMtraverseDown.keyCode = CMaps.FinalIndieMap.activeControllerMTD.keyCode;
        userControlsKeyboard.ACMtraverseDown.name = CMaps.FinalIndieMap.activeControllerMTD.name;

        //in game move left
        userControlsKeyboard.ACmoveLeft.keyCode = CMaps.FinalIndieMap.activeControllerML.keyCode;
        userControlsKeyboard.ACmoveLeft.name = CMaps.FinalIndieMap.activeControllerML.name;

        //in game move back
        userControlsKeyboard.ACmoveBack.keyCode = CMaps.FinalIndieMap.activeControllerMB.keyCode;
        userControlsKeyboard.ACmoveBack.name = CMaps.FinalIndieMap.activeControllerMB.name;

        //in game move right
        userControlsKeyboard.ACmoveRight.keyCode = CMaps.FinalIndieMap.activeControllerMR.keyCode;
        userControlsKeyboard.ACmoveRight.name = CMaps.FinalIndieMap.activeControllerMR.name;

        //in game move forward
        userControlsKeyboard.ACmoveForward.keyCode = CMaps.FinalIndieMap.activeControllerMF.keyCode;
        userControlsKeyboard.ACmoveForward.name = CMaps.FinalIndieMap.activeControllerMF.name;

        //in game weapon 1
        userControlsKeyboard.ACweapon1.keyCode = CMaps.FinalIndieMap.activeControllerW1.keyCode;
        userControlsKeyboard.ACweapon1.name = CMaps.FinalIndieMap.activeControllerW1.name;

        //in game weapon 2
        userControlsKeyboard.ACweapon2.keyCode = CMaps.FinalIndieMap.activeControllerW2.keyCode;
        userControlsKeyboard.ACweapon2.name = CMaps.FinalIndieMap.activeControllerW2.name;

        //boost     
        userControlsKeyboard.ACboost.keyCode = CMaps.FinalIndieMap.ACboost.keyCode;
        userControlsKeyboard.ACboost.name = CMaps.FinalIndieMap.ACboost.name;

        //settings INGAME
        userControlsKeyboard.ACsettingsIG.keyCode = CMaps.FinalIndieMap.ACsettingsIG.keyCode;
        userControlsKeyboard.ACsettingsIG.name = CMaps.FinalIndieMap.ACsettingsIG.name;
    



    }
   
  



    

}
