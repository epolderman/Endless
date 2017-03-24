using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ControllerMappingIG : MonoBehaviour {

    //middle game object pc
    public GameObject middlePC;

    //editing buttons keyboard.mouse
    public Button editLeft;
    public Button editRight;
    public Button editBack;
    public Button editForward;
    public Button editWeapon1;
    public Button editWeapon2;
    public Button editBoost;
    public Button editSettingsIG;

    //button maps keyboard.mouse
    public Button leftmap;
    public Button rightmap;
    public Button backmap;
    public Button forwardmap;
    public Button weapon1map;
    public Button weapon2map;
    public Button boostmap;
    public Button settingsIGmap;

    //button maps MENU keyboard.mouse
    public Button MenuBackb;
    public Button MenuSelectb;
    public Button MenuTraverseb;
    public Button MenutraversebDown;

    //editing button maps MENU keyboard.mouse
    public Button EditMB;
    public Button EditMS;
    public Button EditMT;
    public Button editMTDown;

    //drop down controller
    public Dropdown controllerType;

    //middle game object ps4
    public GameObject middlePS4;

    //buttons for ps4
    public Button leftmapPS4;
    public Button rightmapPS4;
    public Button backmapPS4;
    public Button forwardmapPS4;
    public Button weapon1mapPS4;
    public Button weapon2mapPS4;
    public Button boostPS4;
    public Button settingsIGPS4;
    //menu ps4 buttons
    public Button MenuBackbPS4;
    public Button MenuSelectbPS4;
    public Button MenuTraversebPS4;
    public Button MenutraversebDownPS4;

    //xbox buttons
    public GameObject middleX;
    //buttons
    public Button leftX;
    public Button rightX;
    public Button backX;
    public Button forwardX;
    public Button weapon1X;
    public Button weapon2X;
    public Button boostX;
    public Button settingsIGX;
    //menu buttons'
    public Button MbackX;
    public Button MSelX;
    public Button MTraverseUX;
    public Button MTraverseDX;


    //gameMANGER
    private GameObject GameMenuManager;
    private GameManager scriptFinder;

    //buttons for switching
    public Button PCactive;
    public Button PS4active;
    public Button XBOXactive;


    //for handling the key bindings
    /// structs are immutable in C# (not changable)
    public struct MappingKeys
    {

        public int num;
        public string map;
        public bool used;
        public int theKeyCode;

        public MappingKeys(int numz, string yahs, bool b, int keyCode)
        {
            num = numz;
            map = yahs;
            used = b;
            theKeyCode = keyCode;
        }

    }

    public struct twoMap
    {
        public string name;
        public int keyCode;

    }

    //this is the final mapping that can be passed to in game/out of game
    public struct FinalKeyMap
    {
        //keyboard / mouse
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

        //the Active Controller
        public twoMap activeControllerML;
        public twoMap activeControllerMR;
        public twoMap activeControllerMB;
        public twoMap activeControllerMF;
        public twoMap activeControllerW1;
        public twoMap activeControllerW2;
        //added
        public twoMap ACboost;
        public twoMap ACsettingsIG;
        //end added
        public twoMap activeControllerMBackM;
        public twoMap activeControllerMSelectM;
        public twoMap activeControllerMTU;
        public twoMap activeControllerMTD;

    }

    //this is the struct you are looking for!
    public FinalKeyMap FinalIndieMap;

    //for mapping keys functionality in keyboard.mouse.INgame
    public MappingKeys keyMap;
    public List<MappingKeys> keyInfo;

    //for maping keys functionality in menu keyboard.mouse menu
    public List<MappingKeys> MENUkeyInfo;

    //for in game keyboard.mouse
    //34 elements starting at 0
    private string[] key = {"Right Click", "Left Click", "A" ,"B" ,
                            "C", "D" ,"E", "F", "G", "H", "I", "J", 
                            "K", "L", "M", "N","O", "P","Q", "R", "S", 
                            "T", "U", "V", "W", "X", "Y", "Z", "Up Arrow", 
                            "Down Arrow", "Left Arrow", "Right Arrow", "Space Bar",
                           "Escape", "Enter" };

    //ps4 keys
    private string[] keyPS4 = {"Square","X", "Circle", "Triangle", "L1", 
                               "L2", "R1", "R2", "Share","Options", 
                               "L3", "R3", "PS", "PadPress"};

    //xbox keys
    private string[] keyXbox = {"A","B","X","Y","Left Bumper","Right Bumper", 
                               "Back Button", "Start Button", "Left Stick Click",
                               "Right Stick Click"};

    //for getting key codes /keyboard.mouse
    private List<int> blackKeys;

    //for getting key codes /ps4 
    private List<int> ps4keys;
    private List<MappingKeys> ps4keyInfo;
    private List<MappingKeys> ps4keyInfoMenu;

    //for getting key codes /xboxone 
    private List<int> xboxOnekeys;
    private List<MappingKeys> xboxkeyInfo;
    private List<MappingKeys> xboxkeyInfoMenu;

    //for detecting controllers
    private bool xbox;
    private bool ps4;
    private string xboxOne = "Controller (Xbox One For Windows)";
    private string xbox360 = "Controller (Xbox 360 For Windows)";
    private string ps4name = "Wireless Controller";
    private string ps4mac = "Unknown Wireless Controller";

    //for value mimicing what is controls are showing
    private int valuez;



    //initz the list of structs
    void Start()
    {
        valuez = 1;

        ///initz
        xbox = false;
        ps4 = false;

        //get key codes
        mapKeyCodes();
      
        //open up gamemanager            
        GameMenuManager = GameObject.Find("GameMenuManager");
        scriptFinder = GameMenuManager.GetComponent<GameManager>();

        Debug.Log("--Loading Mappings Across Scenes");
        LoadMappingsAcrossScenes();

        //create lists
        keyInfo = new System.Collections.Generic.List<MappingKeys>();
        //menu list traversal
        MENUkeyInfo = new System.Collections.Generic.List<MappingKeys>();
        //ps4 key menu
        ps4keyInfo = new System.Collections.Generic.List<MappingKeys>();
        ps4keyInfoMenu = new System.Collections.Generic.List<MappingKeys>();
        //xbox
        xboxkeyInfo = new System.Collections.Generic.List<MappingKeys>();
        xboxkeyInfoMenu = new System.Collections.Generic.List<MappingKeys>();


        //load keys for keyboard.mouse
        for (int iz = 0; iz < 35; iz++)
        {
            keyMap = new MappingKeys(iz, key[iz], false, blackKeys[iz]);
            keyInfo.Add(keyMap);
            MENUkeyInfo.Add(keyMap);
        }
        //load keys for ps4 keys
        for (int x = 0; x < 14; x++)
        {
            keyMap = new MappingKeys(x, keyPS4[x], false, ps4keys[x]);
            ps4keyInfo.Add(keyMap);
            ps4keyInfoMenu.Add(keyMap);

        }
        //load keys for xbox
        for (int xb = 0; xb < 10; xb++)
        {
            keyMap = new MappingKeys(xb, keyXbox[xb], false, xboxOnekeys[xb]);
            xboxkeyInfo.Add(keyMap);
            xboxkeyInfoMenu.Add(keyMap);
        }


        //check current mapped values
        checkUsed();

        //check the controller plugged in
        CheckControllerPluggedIn();

        //map the bindings
        mapBindings();



    }
    public void checkControllerType(Button b)
    {

        if (b.GetComponentInChildren<Text>().text == "PC")
        {   //pc is active
            valuez = 1;
        }
        else if (b.GetComponentInChildren<Text>().text == "PS4")
        {   //ps4 is active
            valuez = 2;
        }
        else if (b.GetComponentInChildren<Text>().text == "XBOX")
        {   //xbox is active
            valuez = 3;      
        }

    }
    public void CheckControllerPluggedIn()
    {


        //check what controller is plugged in
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            if (xboxOne == Input.GetJoystickNames()[i])
            {
                Debug.Log("Controller Name: " + Input.GetJoystickNames()[i]);
                Debug.Log(" -- XBOX One -- ");
                xbox = true;
            }
            else if (ps4name == Input.GetJoystickNames()[i] || ps4mac == Input.GetJoystickNames()[i])
            {
                Debug.Log("Controller Name: " + Input.GetJoystickNames()[i]);
                Debug.Log(" -- PS4 -- ");
                ps4 = true;
            }
            else if (xbox360 == Input.GetJoystickNames()[i])
            {
                Debug.Log("Controller Name: " + Input.GetJoystickNames()[i]);
                Debug.Log(" -- XBOX 360 -- ");
                xbox = true;

            }
            else
            {
                Debug.Log("No Controllers Present at this time...");
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (valuez == 1)
        {
            //set active pc   
            middlePC.gameObject.SetActive(true);
            //set false ps4
            middlePS4.gameObject.SetActive(false);
            //set false xbox
            middleX.gameObject.SetActive(false);

        }
        else if (valuez == 2)
        {
            //set active ps4
            middlePS4.gameObject.SetActive(true);
            //set false xbox
            middleX.gameObject.SetActive(false);
            //set false pc
            middlePC.gameObject.SetActive(false);
        }
        else
        {
            //set active xbox
            middleX.gameObject.SetActive(true);
            //set false ps4
            middlePS4.gameObject.SetActive(false);
            //set false pc
            middlePC.gameObject.SetActive(false);
        }
    }

    //for saving the bindings
    public void mapBindings()
    {

        //map bindings in game

        //back
        FinalIndieMap.moveBack.name = backmap.GetComponentInChildren<Text>().text;
      
        if (returnCode(FinalIndieMap.moveBack.name, keyInfo) != 0)
        {
            FinalIndieMap.moveBack.keyCode = returnCode(FinalIndieMap.moveBack.name, keyInfo);

        }
        else
        {
            Debug.LogError("Error Mapping MoveBack Code");
        }

        //left
        FinalIndieMap.moveLeft.name = leftmap.GetComponentInChildren<Text>().text;

        if (returnCode(FinalIndieMap.moveLeft.name, keyInfo) != 0)
        {
            FinalIndieMap.moveLeft.keyCode = returnCode(FinalIndieMap.moveLeft.name, keyInfo);
        }
        else
        {
            Debug.LogError("Error Mapping MoveLeft Code");
        }

        //right
        FinalIndieMap.moveRight.name = rightmap.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.moveRight.name, keyInfo) != 0)
        {
            FinalIndieMap.moveRight.keyCode = returnCode(FinalIndieMap.moveRight.name, keyInfo);
        }
        else
        {
            Debug.LogError("Error Mapping moveRight Code");
        }

        //forward
        FinalIndieMap.moveForward.name = forwardmap.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.moveForward.name, keyInfo) != 0)
        {
            FinalIndieMap.moveForward.keyCode = returnCode(FinalIndieMap.moveForward.name, keyInfo);
        }
        else
        {
            Debug.LogError("Error Mapping Foward Code");
        }

        //weapon1
        FinalIndieMap.weapon1.name = weapon1map.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.weapon1.name, keyInfo) != 0)
        {
            FinalIndieMap.weapon1.keyCode = returnCode(FinalIndieMap.weapon1.name, keyInfo);
        }
        else
        {
            Debug.LogError("Error Mapping weapon1 Code");
        }

        //weapon 2
        FinalIndieMap.weapon2.name = weapon2map.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.weapon2.name, keyInfo) != 0)
        {
            FinalIndieMap.weapon2.keyCode = returnCode(FinalIndieMap.weapon2.name, keyInfo);
        }
        else
        {
            Debug.LogError("Error Mapping MoveLeft Code");
        }
        //BOOST KEYBAORD

        FinalIndieMap.boost.name = boostmap.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.boost.name, keyInfo) != 0)
        {
            FinalIndieMap.boost.keyCode = returnCode(FinalIndieMap.boost.name, keyInfo);
        }
        else
        {
            Debug.LogError("Error Mapping Boost Code");
        }

        //settings in game
        FinalIndieMap.SettingsSelect.name = settingsIGmap.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.SettingsSelect.name, keyInfo) != 0)
        {
            FinalIndieMap.SettingsSelect.keyCode = returnCode(FinalIndieMap.SettingsSelect.name, keyInfo);
        }
        else
        {
            Debug.LogError("Error Mapping Settings IG Code");
        }





        //map bindings in menu

        //menu back
        FinalIndieMap.Mbackmenu.name = MenuBackb.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.Mbackmenu.name, MENUkeyInfo) != 0)
        {

            FinalIndieMap.Mbackmenu.keyCode = returnCode(FinalIndieMap.Mbackmenu.name, MENUkeyInfo);

        }
        else
        {
            Debug.LogError("Error Mapping Menuback Code");
        }

        //menu select
        FinalIndieMap.Mselectmenu.name = MenuSelectb.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.Mselectmenu.name, MENUkeyInfo) != 0)
        {
            FinalIndieMap.Mselectmenu.keyCode = returnCode(FinalIndieMap.Mselectmenu.name, MENUkeyInfo);
        }
        else
        {
            Debug.LogError("Error Mapping MenuSelect Code");
        }

        //menu travers up
        FinalIndieMap.MtraverseUp.name = MenuTraverseb.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.MtraverseUp.name, MENUkeyInfo) != 0)
        {
            FinalIndieMap.MtraverseUp.keyCode = returnCode(FinalIndieMap.MtraverseUp.name, MENUkeyInfo);
        }
        else
        {
            Debug.LogError("Error Mapping MenuUp Code");
        }

        //menu travers down
        FinalIndieMap.MtraverseDown.name = MenutraversebDown.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.MtraverseDown.name, MENUkeyInfo) != 0)
        {
            FinalIndieMap.MtraverseDown.keyCode = returnCode(FinalIndieMap.MtraverseDown.name, MENUkeyInfo);
        }
        else
        {
            Debug.LogError("Error Mapping MenuDown Code");
        }

        //set manually mother fucker

        scriptFinder.userControlsKeyboard.moveBack.keyCode = FinalIndieMap.moveBack.keyCode;
        scriptFinder.userControlsKeyboard.moveBack.name = FinalIndieMap.moveBack.name;

        scriptFinder.userControlsKeyboard.moveLeft.keyCode = FinalIndieMap.moveLeft.keyCode;
        scriptFinder.userControlsKeyboard.moveLeft.name = FinalIndieMap.moveLeft.name;

        scriptFinder.userControlsKeyboard.moveRight.keyCode = FinalIndieMap.moveRight.keyCode;
        scriptFinder.userControlsKeyboard.moveRight.name = FinalIndieMap.moveRight.name;

        scriptFinder.userControlsKeyboard.moveForward.keyCode = FinalIndieMap.moveForward.keyCode;
        scriptFinder.userControlsKeyboard.moveForward.name = FinalIndieMap.moveForward.name;

        scriptFinder.userControlsKeyboard.weapon1.keyCode = FinalIndieMap.weapon1.keyCode;
        scriptFinder.userControlsKeyboard.weapon1.name = FinalIndieMap.weapon1.name;

        scriptFinder.userControlsKeyboard.weapon2.keyCode = FinalIndieMap.weapon2.keyCode;
        scriptFinder.userControlsKeyboard.weapon2.name = FinalIndieMap.weapon2.name;

        //boost
        scriptFinder.userControlsKeyboard.boost.keyCode = FinalIndieMap.boost.keyCode;
        scriptFinder.userControlsKeyboard.boost.name = FinalIndieMap.boost.name;

        //settingsIG
        scriptFinder.userControlsKeyboard.SettingsSelect.keyCode = FinalIndieMap.SettingsSelect.keyCode;
        scriptFinder.userControlsKeyboard.SettingsSelect.name = FinalIndieMap.SettingsSelect.name;


        scriptFinder.userControlsKeyboard.Mbackmenu.keyCode = FinalIndieMap.Mbackmenu.keyCode;
        scriptFinder.userControlsKeyboard.Mbackmenu.name = FinalIndieMap.Mbackmenu.name;

        scriptFinder.userControlsKeyboard.Mselectmenu.keyCode = FinalIndieMap.Mselectmenu.keyCode;
        scriptFinder.userControlsKeyboard.Mselectmenu.name = FinalIndieMap.Mselectmenu.name;

        scriptFinder.userControlsKeyboard.MtraverseUp.keyCode = FinalIndieMap.MtraverseUp.keyCode;
        scriptFinder.userControlsKeyboard.MtraverseUp.name = FinalIndieMap.MtraverseUp.name;

        scriptFinder.userControlsKeyboard.MtraverseDown.keyCode = FinalIndieMap.MtraverseDown.keyCode;
        scriptFinder.userControlsKeyboard.MtraverseDown.name = FinalIndieMap.MtraverseDown.name;



        if (ps4 == true)
        {
            //if ps4 controller is active -> map to joystick 
        Debug.Log("Mapping PS4 as Active Controller");
        MapPS4Active();
          

        //select menu
        scriptFinder.userControlsKeyboard.ACMselectmenu.keyCode = FinalIndieMap.activeControllerMSelectM.keyCode;
        scriptFinder.userControlsKeyboard.ACMselectmenu.name = FinalIndieMap.activeControllerMSelectM.name;

        //back menu
        scriptFinder.userControlsKeyboard.ACMbackmenu.keyCode = FinalIndieMap.activeControllerMBackM.keyCode;
        scriptFinder.userControlsKeyboard.ACMbackmenu.name = FinalIndieMap.activeControllerMBackM.name;

        //traverse up menu
        scriptFinder.userControlsKeyboard.ACMtraverseUp.keyCode = FinalIndieMap.activeControllerMTU.keyCode;
        scriptFinder.userControlsKeyboard.ACMtraverseUp.name = FinalIndieMap.activeControllerMTU.name;

        //traverse down menu
        scriptFinder.userControlsKeyboard.ACMtraverseDown.keyCode = FinalIndieMap.activeControllerMTD.keyCode;
        scriptFinder.userControlsKeyboard.ACMtraverseDown.name = FinalIndieMap.activeControllerMTD.name;

        //in game move left
        scriptFinder.userControlsKeyboard.ACmoveLeft.keyCode = FinalIndieMap.activeControllerML.keyCode;
        scriptFinder.userControlsKeyboard.ACmoveLeft.name = FinalIndieMap.activeControllerML.name;

        //in game move back
        scriptFinder.userControlsKeyboard.ACmoveBack.keyCode = FinalIndieMap.activeControllerMB.keyCode;
        scriptFinder.userControlsKeyboard.ACmoveBack.name = FinalIndieMap.activeControllerMB.name;

        //in game move right
        scriptFinder.userControlsKeyboard.ACmoveRight.keyCode = FinalIndieMap.activeControllerMR.keyCode;
        scriptFinder.userControlsKeyboard.ACmoveRight.name = FinalIndieMap.activeControllerMR.name;

        //in game move forward
        scriptFinder.userControlsKeyboard.ACmoveForward.keyCode = FinalIndieMap.activeControllerMF.keyCode;
        scriptFinder.userControlsKeyboard.ACmoveForward.name = FinalIndieMap.activeControllerMF.name;

        //in game weapon 1
        scriptFinder.userControlsKeyboard.ACweapon1.keyCode = FinalIndieMap.activeControllerW1.keyCode;
        scriptFinder.userControlsKeyboard.ACweapon1.name = FinalIndieMap.activeControllerW1.name;

        //in game weapon 2
        scriptFinder.userControlsKeyboard.ACweapon2.keyCode = FinalIndieMap.activeControllerW2.keyCode;
        scriptFinder.userControlsKeyboard.ACweapon2.name = FinalIndieMap.activeControllerW2.name;
       
        //boost     
        scriptFinder.userControlsKeyboard.ACboost.keyCode = FinalIndieMap.ACboost.keyCode;
        scriptFinder.userControlsKeyboard.ACboost.name = FinalIndieMap.ACboost.name;

        //settings INGAME
        scriptFinder.userControlsKeyboard.ACsettingsIG.keyCode = FinalIndieMap.ACsettingsIG.keyCode;
        scriptFinder.userControlsKeyboard.ACsettingsIG.name = FinalIndieMap.ACsettingsIG.name;
    

        }
        else if (xbox == true)
        {
        Debug.Log("Mapping XBOX as Active Controller");
         MapXBOXActive();
            

        //select menu
        scriptFinder.userControlsKeyboard.ACMselectmenu.keyCode = FinalIndieMap.activeControllerMSelectM.keyCode;
        scriptFinder.userControlsKeyboard.ACMselectmenu.name = FinalIndieMap.activeControllerMSelectM.name;

        //back menu
        scriptFinder.userControlsKeyboard.ACMbackmenu.keyCode = FinalIndieMap.activeControllerMBackM.keyCode;
        scriptFinder.userControlsKeyboard.ACMbackmenu.name = FinalIndieMap.activeControllerMBackM.name;

        //traverse up menu
        scriptFinder.userControlsKeyboard.ACMtraverseUp.keyCode = FinalIndieMap.activeControllerMTU.keyCode;
        scriptFinder.userControlsKeyboard.ACMtraverseUp.name = FinalIndieMap.activeControllerMTU.name;

        //traverse down menu
        scriptFinder.userControlsKeyboard.ACMtraverseDown.keyCode = FinalIndieMap.activeControllerMTD.keyCode;
        scriptFinder.userControlsKeyboard.ACMtraverseDown.name = FinalIndieMap.activeControllerMTD.name;

        //in game move left
        scriptFinder.userControlsKeyboard.ACmoveLeft.keyCode = FinalIndieMap.activeControllerML.keyCode;
        scriptFinder.userControlsKeyboard.ACmoveLeft.name = FinalIndieMap.activeControllerML.name;

        //in game move back
        scriptFinder.userControlsKeyboard.ACmoveBack.keyCode = FinalIndieMap.activeControllerMB.keyCode;
        scriptFinder.userControlsKeyboard.ACmoveBack.name = FinalIndieMap.activeControllerMB.name;

        //in game move right
        scriptFinder.userControlsKeyboard.ACmoveRight.keyCode = FinalIndieMap.activeControllerMR.keyCode;
        scriptFinder.userControlsKeyboard.ACmoveRight.name = FinalIndieMap.activeControllerMR.name;

        //in game move forward
        scriptFinder.userControlsKeyboard.ACmoveForward.keyCode = FinalIndieMap.activeControllerMF.keyCode;
        scriptFinder.userControlsKeyboard.ACmoveForward.name = FinalIndieMap.activeControllerMF.name;

        //in game weapon 1
        scriptFinder.userControlsKeyboard.ACweapon1.keyCode = FinalIndieMap.activeControllerW1.keyCode;
        scriptFinder.userControlsKeyboard.ACweapon1.name = FinalIndieMap.activeControllerW1.name;

        //in game weapon 2
        scriptFinder.userControlsKeyboard.ACweapon2.keyCode = FinalIndieMap.activeControllerW2.keyCode;
        scriptFinder.userControlsKeyboard.ACweapon2.name = FinalIndieMap.activeControllerW2.name;
       
        //boost     
        scriptFinder.userControlsKeyboard.ACboost.keyCode = FinalIndieMap.ACboost.keyCode;
        scriptFinder.userControlsKeyboard.ACboost.name = FinalIndieMap.ACboost.name;

        //settings INGAME
        scriptFinder.userControlsKeyboard.ACsettingsIG.keyCode = FinalIndieMap.ACsettingsIG.keyCode;
        scriptFinder.userControlsKeyboard.ACsettingsIG.name = FinalIndieMap.ACsettingsIG.name;
    

        }
        else
        {
            Debug.Log("No Controller Plugged In So No Mapping Was Done");
        }

        

        



    }
    //checks what keys are mapped currently keyboard.mouse in game
    public void checkUsed()
    {
        //In game keyboard.mouse
        string value = "";
        string othervalue1 = "";
        string othervalue2 = "";
        string othervalue3 = "";
        string othervalue4 = "";
        string othervalue5 = "";
        string othervalue6 = "";
        string othervalue7 = "";
        value = leftmap.GetComponentInChildren<Text>().text;
        othervalue1 = rightmap.GetComponentInChildren<Text>().text;
        othervalue2 = backmap.GetComponentInChildren<Text>().text;
        othervalue3 = forwardmap.GetComponentInChildren<Text>().text;
        othervalue4 = weapon1map.GetComponentInChildren<Text>().text;
        othervalue5 = weapon2map.GetComponentInChildren<Text>().text;
        othervalue6 = boostmap.GetComponentInChildren<Text>().text;


        for (int i = 0; i < keyInfo.Count; i++)
        {

            if ((value == keyInfo[i].map) ||
                (othervalue1 == keyInfo[i].map) ||
                (othervalue2 == keyInfo[i].map) ||
                (othervalue3 == keyInfo[i].map) ||
                (othervalue4 == keyInfo[i].map) ||
                (othervalue5 == keyInfo[i].map) ||
                (othervalue6 == keyInfo[i].map))
            {

                keyInfo[i] = new MappingKeys(i, keyInfo[i].map, true, blackKeys[i]);
                //Debug.Log("Value Is Named " + keyInfo[i].map);
                //Debug.Log("Value Is Used " + keyInfo[i].used);
            }
            else
            {
                keyInfo[i] = new MappingKeys(i, keyInfo[i].map, false, blackKeys[i]);
            }




        }


        ///MENU keyboard.mouse
        string menuSelect = "";
        string menuBack = "";
        string menuTUP = "";
        string menuTDOWN = "";

        othervalue7 = settingsIGmap.GetComponentInChildren<Text>().text;
        menuSelect = MenuSelectb.GetComponentInChildren<Text>().text;
        menuBack = MenuBackb.GetComponentInChildren<Text>().text;
        menuTUP = MenuTraverseb.GetComponentInChildren<Text>().text;
        menuTDOWN = MenutraversebDown.GetComponentInChildren<Text>().text;

        for (int x = 0; x < MENUkeyInfo.Count; x++)
        {

            if ((menuSelect == MENUkeyInfo[x].map) ||
               (menuBack == MENUkeyInfo[x].map) ||
               (menuTUP == MENUkeyInfo[x].map) ||
               (menuTDOWN == MENUkeyInfo[x].map) ||
                (othervalue7 == MENUkeyInfo[x].map))
            {

                MENUkeyInfo[x] = new MappingKeys(x, MENUkeyInfo[x].map, true, blackKeys[x]);

            }
            else
            {
                MENUkeyInfo[x] = new MappingKeys(x, MENUkeyInfo[x].map, false, blackKeys[x]);
            }


        }


        ///ps4
        string weapon1p = "";
        string weapon2p = "";
        string menubackp = "";
        string menuselectp = "";
        string settingsIG = "";
        string thatBOOST = "";

        //weapon & boost & settings IG
        weapon1p = weapon1mapPS4.GetComponentInChildren<Text>().text;
        weapon2p = weapon2mapPS4.GetComponentInChildren<Text>().text;
        thatBOOST = boostPS4.GetComponentInChildren<Text>().text;


        for (int z = 0; z < ps4keyInfo.Count; z++)
        {

            if ((weapon1p == ps4keyInfo[z].map) ||
                 (weapon2p == ps4keyInfo[z].map) ||
                 (thatBOOST == ps4keyInfo[z].map))
            {
                ps4keyInfo[z] = new MappingKeys(z, ps4keyInfo[z].map, true, ps4keys[z]);
            }
            else
            {
                ps4keyInfo[z] = new MappingKeys(z, ps4keyInfo[z].map, false, ps4keys[z]);
            }

        }


        //menu
        menubackp = MenuBackbPS4.GetComponentInChildren<Text>().text;
        menuselectp = MenuSelectbPS4.GetComponentInChildren<Text>().text;
        settingsIG = settingsIGPS4.GetComponentInChildren<Text>().text;

        for (int k = 0; k < ps4keyInfoMenu.Count; k++)
        {

            if ((menubackp == ps4keyInfoMenu[k].map) || (menuselectp == ps4keyInfoMenu[k].map) || settingsIG == ps4keyInfoMenu[k].map)
            {
                ps4keyInfoMenu[k] = new MappingKeys(k, ps4keyInfoMenu[k].map, true, ps4keys[k]);
            }
            else
            {
                ps4keyInfoMenu[k] = new MappingKeys(k, ps4keyInfoMenu[k].map, false, ps4keys[k]);
            }

        }



        ///xboxone
        string weapon1xbox = "";
        string weapon2xbox = "";
        string Mselectxbox = "";
        string Mbackxbox = "";
        string boosttheX = "";
        string settingsIGxxx = "";

        //weapon & boost & settingsIG
        weapon1xbox = weapon1X.GetComponentInChildren<Text>().text;
        weapon2xbox = weapon2X.GetComponentInChildren<Text>().text;
        boosttheX = boostX.GetComponentInChildren<Text>().text;
        //settingsIGxxx = settingsIGX.GetComponentInChildren<Text>().text;

        for (int zz = 0; zz < xboxkeyInfo.Count; zz++)
        {

            if ((weapon1xbox == xboxkeyInfo[zz].map) ||
                (weapon2xbox == xboxkeyInfo[zz].map) ||
                (boosttheX == xboxkeyInfo[zz].map))
            {
                xboxkeyInfo[zz] = new MappingKeys(zz, xboxkeyInfo[zz].map, true, xboxOnekeys[zz]);
            }
            else
            {
                xboxkeyInfo[zz] = new MappingKeys(zz, xboxkeyInfo[zz].map, false, xboxOnekeys[zz]);
            }

        }


        //menu
        settingsIGxxx = settingsIGX.GetComponentInChildren<Text>().text;
        Mselectxbox = MbackX.GetComponentInChildren<Text>().text;
        Mbackxbox = MSelX.GetComponentInChildren<Text>().text;

        for (int kk = 0; kk < xboxkeyInfoMenu.Count; kk++)
        {

            if ((Mselectxbox == xboxkeyInfoMenu[kk].map) || (Mbackxbox == xboxkeyInfoMenu[kk].map) || (settingsIGxxx == xboxkeyInfoMenu[kk].map))
            {
                xboxkeyInfoMenu[kk] = new MappingKeys(kk, xboxkeyInfoMenu[kk].map, true, xboxOnekeys[kk]);
            }
            else
            {
                xboxkeyInfoMenu[kk] = new MappingKeys(kk, xboxkeyInfoMenu[kk].map, false, xboxOnekeys[kk]);
            }

        }




    }



    //filter button presses to run pass certain buttons through
    public void AllButtonPress(Button button)
    {


        string current = button.name;
        string value = "";

        if (valuez == 1)
        {
            switch (current)
            {
                //right
                case "EditMoveRightB":
                    //Debug.Log("EditRight Pressed");
                    value = rightmap.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(rightmap, value, keyInfo);
                    break;
                //left
                case "EditMoveLeftB":
                    //Debug.Log("editLeft pressed");
                    value = leftmap.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(leftmap, value, keyInfo);
                    break;
                //forward
                case "EditMoveForwardB":
                    //Debug.Log("editForward pressed");
                    value = forwardmap.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(forwardmap, value, keyInfo);
                    break;
                //down/back
                case "EditMoveDownB":
                    //Debug.Log("editDown pressed");
                    value = backmap.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(backmap, value, keyInfo);
                    break;
                //weapon1
                case "EditWeapon1B":
                    //Debug.Log("editWeapon1 pressed");
                    value = weapon1map.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(weapon1map, value, keyInfo);
                    break;
                //weapon2
                case "EditWeapon2B":
                    //Debug.Log("editWeapon2 pressed");
                    value = weapon2map.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(weapon2map, value, keyInfo);
                    break;
                //BOOST
                case "EditBoost":
                    value = boostmap.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(boostmap, value, keyInfo);
                    break;
                //SETTINGSIG
                case "EditSettingsIG":
                    value = settingsIGmap.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(settingsIGmap, value, MENUkeyInfo);
                    break;
                //menuSelect
                case "EditMenuSelectB":
                    value = MenuSelectb.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(MenuSelectb, value, MENUkeyInfo);
                    break;
                //menu back
                case "EditMenuBackB":
                    value = MenuBackb.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(MenuBackb, value, MENUkeyInfo);
                    break;
                //menu up traverse
                case "EditMenuTraverseBUp":
                    value = MenuTraverseb.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(MenuTraverseb, value, MENUkeyInfo);
                    break;
                //menu down traverse
                case "EditMenuTraverseBDown":
                    value = MenutraversebDown.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(MenutraversebDown, value, MENUkeyInfo);
                    break;
            }
        }
        else if (valuez == 2)
        {
            //ps4
            switch (current)
            {
                //weapon1
                case "EditWeapon1B":
                    value = weapon1mapPS4.GetComponentInChildren<Text>().text;
                    Debug.Log("Value is " + value);
                    ActionBronsonButton(weapon1mapPS4, value, ps4keyInfo);
                    break;
                //weapon2
                case "EditWeapon2B":
                    value = weapon2mapPS4.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(weapon2mapPS4, value, ps4keyInfo);
                    break;
                //boost ps4
                case "EditBoost":
                    value = boostPS4.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(boostPS4, value, ps4keyInfo);
                    break;
                //SETTINGSIG
                case "EditSettingsIG":
                    value = settingsIGPS4.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(settingsIGPS4, value, ps4keyInfoMenu);
                    break;
                //menuSelect
                case "EditMenuSelectB":
                    value = MenuSelectbPS4.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(MenuSelectbPS4, value, ps4keyInfoMenu);
                    break;
                //menu back
                case "EditMenuBackB":
                    value = MenuBackbPS4.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(MenuBackbPS4, value, ps4keyInfoMenu);
                    break;

            }


        }
        else if (valuez == 3)
        {
            //xbox
            switch (current)
            {
                //weapon1
                case "EditWeapon1B":
                    value = weapon1X.GetComponentInChildren<Text>().text;
                    Debug.Log("Value is " + value);
                    ActionBronsonButton(weapon1X, value, xboxkeyInfo);
                    break;
                //weapon2
                case "EditWeapon2B":
                    value = weapon2X.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(weapon2X, value, xboxkeyInfo);
                    break;
                //BOOST XBOX
                case "EditBoost":
                    value = boostX.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(boostX, value, xboxkeyInfo);
                    break;
                //SETTINGSIG
                case "EditSettingsIG":
                    value = settingsIGX.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(settingsIGX, value, xboxkeyInfoMenu);
                    break;
                //menuSelect
                case "EditMenuSelectB":
                    value = MSelX.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(MSelX, value, xboxkeyInfoMenu);
                    break;
                //menu back
                case "EditMenuBackB":
                    value = MbackX.GetComponentInChildren<Text>().text;
                    ActionBronsonButton(MbackX, value, xboxkeyInfoMenu);
                    break;

            }


        }







    }


    //runs main algo to check for keys already mapped vs keys that arent
    public void ActionBronsonButton(Button b, string value, List<MappingKeys> JackJ)
    {


        for (int i = 0; i < JackJ.Count; i++)
        {
            if (value == JackJ[i].map)
            {
                //if at the end of array -> start over and start looking for un mapped keys
                if (JackJ[i].num + 1 >= JackJ.Count)
                {
                    //Debug.Log("In");
                    b.GetComponentInChildren<Text>().text = returnFirstUnused(JackJ);
                }
                //if next array map is not used -> use it
                else if (JackJ[i + 1].used == false)
                {
                    // Debug.Log("In USED false");
                    b.GetComponentInChildren<Text>().text = JackJ[i + 1].map;

                }
                //if mapped value is taken already
                else if (JackJ[i + 1].used == true)
                {
                    //Debug.Log("in used true");
                    b.GetComponentInChildren<Text>().text = returnFirstUnusedEdge(i + 1, JackJ);
                }



            }

        }



        //check current mapped values
        checkUsed();


    }

    //returns the first unused mapping starting at the beginning -> solves use case of last string in array
    public string returnFirstUnused(List<MappingKeys> list)
    {
        string firstfind = "";

        for (int z = 0; z < list.Count; z++)
        {
            if (list[z].used == false)
            {
                firstfind = list[z].map;
                break;
            }


        }

        return firstfind;



    }
    //handles the edge case of being at the end of an array -> being at the beginning of array
    public string returnFirstUnusedEdge(int value, List<MappingKeys> thelist)
    {
        string firstfind = "";

        for (int i = value; i < thelist.Count; i++)
        {

            if (i == thelist.Count - 1)
            {
                firstfind = returnFirstUnused(thelist);
                break;
            }
            else if (thelist[i].used == false)
            {
                firstfind = thelist[i].map;
                break;
            }



        }

        return firstfind;

    }
    //for keyboard.mouse mapping
    public void mapKeyCodes()
    {

        blackKeys = new System.Collections.Generic.List<int>();
        ps4keys = new System.Collections.Generic.List<int>();
        xboxOnekeys = new System.Collections.Generic.List<int>();

        //pc
        int rightclick = (int)KeyCode.Mouse1;
        blackKeys.Add(rightclick);
        int leftclick = (int)KeyCode.Mouse0;
        blackKeys.Add(leftclick);
        int a = (int)KeyCode.A;
        blackKeys.Add(a);
        int b = (int)KeyCode.B;
        blackKeys.Add(b);
        int c = (int)KeyCode.C;
        blackKeys.Add(c);
        int d = (int)KeyCode.D;
        blackKeys.Add(d);
        int e = (int)KeyCode.E;
        blackKeys.Add(e);
        int f = (int)KeyCode.F;
        blackKeys.Add(f);
        int g = (int)KeyCode.G;
        blackKeys.Add(g);
        int h = (int)KeyCode.H;
        blackKeys.Add(h);
        int i = (int)KeyCode.I;
        blackKeys.Add(i);
        int j = (int)KeyCode.J;
        blackKeys.Add(j);
        int k = (int)KeyCode.K;
        blackKeys.Add(k);
        int l = (int)KeyCode.L;
        blackKeys.Add(l);
        int m = (int)KeyCode.M;
        blackKeys.Add(m);
        int n = (int)KeyCode.N;
        blackKeys.Add(n);
        int o = (int)KeyCode.O;
        blackKeys.Add(o);
        int p = (int)KeyCode.P;
        blackKeys.Add(p);
        int q = (int)KeyCode.Q;
        blackKeys.Add(q);
        int r = (int)KeyCode.R;
        blackKeys.Add(r);
        int s = (int)KeyCode.S;
        blackKeys.Add(s);
        int t = (int)KeyCode.T;
        blackKeys.Add(t);
        int u = (int)KeyCode.U;
        blackKeys.Add(u);
        int v = (int)KeyCode.V;
        blackKeys.Add(v);
        int w = (int)KeyCode.W;
        blackKeys.Add(w);
        int x = (int)KeyCode.X;
        blackKeys.Add(x);
        int y = (int)KeyCode.Y;
        blackKeys.Add(y);
        int z = (int)KeyCode.Z;
        blackKeys.Add(z);
        int up = (int)KeyCode.UpArrow;
        blackKeys.Add(up);
        int down = (int)KeyCode.DownArrow;
        blackKeys.Add(down);
        int left = (int)KeyCode.LeftArrow;
        blackKeys.Add(left);
        int right = (int)KeyCode.RightArrow;
        blackKeys.Add(right);
        int space = (int)KeyCode.Space;
        blackKeys.Add(space);
        int escape = (int)KeyCode.Escape;
        blackKeys.Add(escape);
        int enter = (int)KeyCode.Return;
        blackKeys.Add(enter);

        //ps4
        int square = (int)KeyCode.JoystickButton0;
        ps4keys.Add(square);
        int xx = (int)KeyCode.JoystickButton1;
        ps4keys.Add(xx);
        int circle = (int)KeyCode.JoystickButton2;
        ps4keys.Add(circle);
        int triangle = (int)KeyCode.JoystickButton3;
        ps4keys.Add(triangle);
        int l1 = (int)KeyCode.JoystickButton4;
        ps4keys.Add(l1);
        int l2 = (int)KeyCode.JoystickButton6;
        ps4keys.Add(l2);
        int r1 = (int)KeyCode.JoystickButton5;
        ps4keys.Add(r1);
        int r2 = (int)KeyCode.JoystickButton7;
        ps4keys.Add(r2);
        int share = (int)KeyCode.JoystickButton8;
        ps4keys.Add(share);
        int options = (int)KeyCode.JoystickButton9;
        ps4keys.Add(options);
        int l3 = (int)KeyCode.JoystickButton10;
        ps4keys.Add(l3);
        int r3 = (int)KeyCode.JoystickButton11;
        ps4keys.Add(r3);
        int ps = (int)KeyCode.JoystickButton12;
        ps4keys.Add(ps);
        int padpress = (int)KeyCode.JoystickButton13;
        ps4keys.Add(padpress);

        //xbox One / 360
        int aa = (int)KeyCode.JoystickButton0;
        xboxOnekeys.Add(aa);
        int bb = (int)KeyCode.JoystickButton1;
        xboxOnekeys.Add(bb);
        int xxx = (int)KeyCode.JoystickButton2;
        xboxOnekeys.Add(xxx);
        int yy = (int)KeyCode.JoystickButton3;
        xboxOnekeys.Add(yy);
        int lb = (int)KeyCode.JoystickButton4;
        xboxOnekeys.Add(lb);
        int rb = (int)KeyCode.JoystickButton5;
        xboxOnekeys.Add(rb);
        int backb = (int)KeyCode.JoystickButton6;
        xboxOnekeys.Add(backb);
        int startb = (int)KeyCode.JoystickButton7;
        xboxOnekeys.Add(startb);
        int lsclick = (int)KeyCode.JoystickButton8;
        xboxOnekeys.Add(lsclick);
        int rsclick = (int)KeyCode.JoystickButton9;
        xboxOnekeys.Add(rsclick);


    }

    public int returnCode(string name, List<MappingKeys> thatList)
    {
        int one = 0;

        for (int i = 0; i < thatList.Count; i++)
        {

            if (name == thatList[i].map)
            {
                one = thatList[i].theKeyCode;
                return one;
            }

        }

        return one;


    }
    //ps4 active controller map bindings
    public void MapPS4Active()
    {
        ///PS4 mapping
        //no key codes for axis movements

        //move right
        FinalIndieMap.activeControllerMR.name = rightmapPS4.GetComponentInChildren<Text>().text;
        FinalIndieMap.activeControllerMR.keyCode = returnCode(FinalIndieMap.activeControllerMR.name, ps4keyInfo);

        //move left
        FinalIndieMap.activeControllerML.name = leftmapPS4.GetComponentInChildren<Text>().text;
        FinalIndieMap.activeControllerML.keyCode = returnCode(FinalIndieMap.activeControllerML.name, ps4keyInfo);


        //move back
        FinalIndieMap.activeControllerMB.name = backmapPS4.GetComponentInChildren<Text>().text;
        FinalIndieMap.activeControllerMB.keyCode = returnCode(FinalIndieMap.activeControllerMB.name, ps4keyInfo);


        //move forward
        FinalIndieMap.activeControllerMF.name = forwardmapPS4.GetComponentInChildren<Text>().text;
        FinalIndieMap.activeControllerMF.keyCode = returnCode(FinalIndieMap.activeControllerMF.name, ps4keyInfo);


        ///weapon 1
        FinalIndieMap.activeControllerW1.name = weapon1mapPS4.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.activeControllerW1.name, ps4keyInfo) != 0)
        {
            FinalIndieMap.activeControllerW1.keyCode = returnCode(FinalIndieMap.activeControllerW1.name, ps4keyInfo);
        }
        else
        {
            Debug.LogError("Error Mappping PS4 weapon1");
        }

        //weapon2
        FinalIndieMap.activeControllerW2.name = weapon2mapPS4.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.activeControllerW1.name, ps4keyInfo) != 0)
        {
            FinalIndieMap.activeControllerW2.keyCode = returnCode(FinalIndieMap.activeControllerW2.name, ps4keyInfo);
        }
        else
        {
            Debug.LogError("Error Mappping PS4 weapon2");
        }

        //Boost ps4
        FinalIndieMap.ACboost.name = boostPS4.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.ACboost.name, ps4keyInfo) != 0)
        {
            FinalIndieMap.ACboost.keyCode = returnCode(FinalIndieMap.ACboost.name, ps4keyInfo);
        }
        else
        {
            Debug.LogError("Error Mappping PS4 boost");
        }
        //settings in game
        FinalIndieMap.ACsettingsIG.name = settingsIGPS4.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.ACsettingsIG.name, ps4keyInfo) != 0)
        {
            FinalIndieMap.ACsettingsIG.keyCode = returnCode(FinalIndieMap.ACsettingsIG.name, ps4keyInfo);
        }
        else
        {
            Debug.LogError("Error Mappping PS4 settings IN GAME");
        }

        //ps4 menu back
        FinalIndieMap.activeControllerMBackM.name = MenuBackbPS4.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.activeControllerMBackM.name, ps4keyInfoMenu) != 0)
        {
            FinalIndieMap.activeControllerMBackM.keyCode = returnCode(FinalIndieMap.activeControllerMBackM.name, ps4keyInfoMenu);
            //Debug.Log("PS4 menu back " + FinalIndieMap.activeControllerMBackM.name);
            // Debug.Log("PS4 menu back " + FinalIndieMap.activeControllerMBackM.keyCode);
        }
        else
        {
            Debug.LogError("Error Mappping PS4 menu back");
        }

        //ps4 menu select
        FinalIndieMap.activeControllerMSelectM.name = MenuSelectbPS4.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.activeControllerMSelectM.name, ps4keyInfoMenu) != 0)
        {
            FinalIndieMap.activeControllerMSelectM.keyCode = returnCode(FinalIndieMap.activeControllerMSelectM.name, ps4keyInfoMenu);
            // Debug.Log("PS4 menu select " + FinalIndieMap.activeControllerMSelectM.name);
            //Debug.Log("PS4 menu select " + FinalIndieMap.activeControllerMSelectM.keyCode);
        }
        else
        {
            Debug.LogError("Error Mappping PS4 menu select");
        }

        //menu travers up
        FinalIndieMap.activeControllerMTU.name = MenuTraversebPS4.GetComponentInChildren<Text>().text;
        FinalIndieMap.activeControllerMTU.keyCode = returnCode(FinalIndieMap.activeControllerMTU.name, ps4keyInfoMenu);


        //menu traverse down
        FinalIndieMap.activeControllerMTD.name = MenutraversebDownPS4.GetComponentInChildren<Text>().text;
        FinalIndieMap.activeControllerMTD.keyCode = returnCode(FinalIndieMap.activeControllerMTD.name, ps4keyInfoMenu);




    }

    //map xbox as the active controller bindings
    public void MapXBOXActive()
    {
        //no keycodes for joystick axis movements

        //move left
        FinalIndieMap.activeControllerML.name = leftX.GetComponentInChildren<Text>().text;
        FinalIndieMap.activeControllerML.keyCode = returnCode(FinalIndieMap.activeControllerML.name, xboxkeyInfo);



        //move right
        FinalIndieMap.activeControllerMR.name = rightX.GetComponentInChildren<Text>().text;
        FinalIndieMap.activeControllerMR.keyCode = returnCode(FinalIndieMap.activeControllerMR.name, xboxkeyInfo);


        //move forward
        FinalIndieMap.activeControllerMF.name = forwardX.GetComponentInChildren<Text>().text;
        FinalIndieMap.activeControllerMF.keyCode = returnCode(FinalIndieMap.activeControllerMF.name, xboxkeyInfo);


        //move back
        FinalIndieMap.activeControllerMB.name = backX.GetComponentInChildren<Text>().text;
        FinalIndieMap.activeControllerMB.keyCode = returnCode(FinalIndieMap.activeControllerMB.name, xboxkeyInfo);



        /// xbox weapon 1
        FinalIndieMap.activeControllerW1.name = weapon1X.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.activeControllerW1.name, xboxkeyInfo) != 0)
        {
            FinalIndieMap.activeControllerW1.keyCode = returnCode(FinalIndieMap.activeControllerW1.name, xboxkeyInfo);
        }
        else
        {
            Debug.LogError("Error Mappping XBOX weapon1");
        }

        // xbox weapon2
        FinalIndieMap.activeControllerW2.name = weapon2X.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.activeControllerW1.name, xboxkeyInfo) != 0)
        {
            FinalIndieMap.activeControllerW2.keyCode = returnCode(FinalIndieMap.activeControllerW2.name, xboxkeyInfo);
        }
        else
        {
            Debug.LogError("Error Mappping XBOX weapon2");
        }

        //BOOST
        FinalIndieMap.ACboost.name = boostX.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.ACboost.name, xboxkeyInfo) != 0)
        {
            FinalIndieMap.ACboost.keyCode = returnCode(FinalIndieMap.ACboost.name, xboxkeyInfo);
        }
        else
        {
            Debug.LogError("Error Mappping XBOX weapon2");
        }
        //settings in game
        FinalIndieMap.ACsettingsIG.name = settingsIGX.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.ACsettingsIG.name, xboxkeyInfo) != 0)
        {
            FinalIndieMap.ACsettingsIG.keyCode = returnCode(FinalIndieMap.ACsettingsIG.name, xboxkeyInfo);
        }
        else
        {
            Debug.LogError("Error Mappping XBOX weapon2");
        }

        //xbox menu back
        FinalIndieMap.activeControllerMBackM.name = MbackX.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.activeControllerMBackM.name, xboxkeyInfoMenu) != 0)
        {
            FinalIndieMap.activeControllerMBackM.keyCode = returnCode(FinalIndieMap.activeControllerMBackM.name, xboxkeyInfoMenu);
            //Debug.Log("xbox menu back " + FinalIndieMap.activeControllerMBackM.name);
            //Debug.Log("xbox menu back " + FinalIndieMap.activeControllerMBackM.keyCode);
        }
        else
        {
            Debug.LogError("Error Mappping XBOX menu back");
        }

        //xbox menu select
        FinalIndieMap.activeControllerMSelectM.name = MSelX.GetComponentInChildren<Text>().text;
        if (returnCode(FinalIndieMap.activeControllerMSelectM.name, xboxkeyInfoMenu) != 0)
        {
            FinalIndieMap.activeControllerMSelectM.keyCode = returnCode(FinalIndieMap.activeControllerMSelectM.name, xboxkeyInfoMenu);
            //Debug.Log("xbox menu select " + FinalIndieMap.activeControllerMSelectM.name);
            // Debug.Log("xbox menu select " + FinalIndieMap.activeControllerMSelectM.keyCode);
        }
        else
        {
            Debug.LogError("Error Mappping XBOX menu Select");
        }


        //xbox menu travese up
        FinalIndieMap.activeControllerMTU.name = MTraverseUX.GetComponentInChildren<Text>().text;
        FinalIndieMap.activeControllerMTU.keyCode = returnCode(FinalIndieMap.activeControllerMTU.name, xboxkeyInfoMenu);



        //xbox menu traverse down
        FinalIndieMap.activeControllerMTD.name = MTraverseDX.GetComponentInChildren<Text>().text;
        FinalIndieMap.activeControllerMTD.keyCode = returnCode(FinalIndieMap.activeControllerMTD.name, xboxkeyInfoMenu);


    }
    public void LoadMappingsAcrossScenes()
    {

            Debug.Log("--Mapping Keys Across Scenes--");
         
            
            //keyboard
            leftmap.GetComponentInChildren<Text>().text = scriptFinder.userControlsKeyboard.moveLeft.name;
            rightmap.GetComponentInChildren<Text>().text = scriptFinder.userControlsKeyboard.moveRight.name;
            backmap.GetComponentInChildren<Text>().text = scriptFinder.userControlsKeyboard.moveBack.name;
            forwardmap.GetComponentInChildren<Text>().text = scriptFinder.userControlsKeyboard.moveForward.name;
            weapon1map.GetComponentInChildren<Text>().text = scriptFinder.userControlsKeyboard.weapon1.name;
            weapon2map.GetComponentInChildren<Text>().text = scriptFinder.userControlsKeyboard.weapon2.name;
            boostmap.GetComponentInChildren<Text>().text = scriptFinder.userControlsKeyboard.boost.name;
            settingsIGmap.GetComponentInChildren<Text>().text = scriptFinder.userControlsKeyboard.SettingsSelect.name;
            MenuBackb.GetComponentInChildren<Text>().text = scriptFinder.userControlsKeyboard.Mbackmenu.name;
            MenuSelectb.GetComponentInChildren<Text>().text = scriptFinder.userControlsKeyboard.Mselectmenu.name;
            MenuTraverseb.GetComponentInChildren<Text>().text = scriptFinder.userControlsKeyboard.MtraverseUp.name;
            MenutraversebDown.GetComponentInChildren<Text>().text = scriptFinder.userControlsKeyboard.MtraverseDown.name;


            //xbox
            leftX.GetComponentInChildren<Text>().text = scriptFinder.iverson.Xmoveleft;
            rightX.GetComponentInChildren<Text>().text = scriptFinder.iverson.Xmoveright;
            forwardX.GetComponentInChildren<Text>().text = scriptFinder.iverson.Xmoveforward;
            backX.GetComponentInChildren<Text>().text = scriptFinder.iverson.Xmoveback;
            weapon1X.GetComponentInChildren<Text>().text = scriptFinder.iverson.Xweapon1;
            weapon2X.GetComponentInChildren<Text>().text = scriptFinder.iverson.Xweapon2;
            boostX.GetComponentInChildren<Text>().text = scriptFinder.iverson.Xboost;
            settingsIGX.GetComponentInChildren<Text>().text = scriptFinder.iverson.XselectIG;
            MbackX.GetComponentInChildren<Text>().text = scriptFinder.iverson.XbackM;
            MSelX.GetComponentInChildren<Text>().text = scriptFinder.iverson.XselectM;
            MTraverseUX.GetComponentInChildren<Text>().text = scriptFinder.iverson.XtraverseUPM;
            MTraverseDX.GetComponentInChildren<Text>().text = scriptFinder.iverson.XtraverseDOWNM;

            //ps4
            leftmapPS4.GetComponentInChildren<Text>().text = scriptFinder.iverson.ps4moveleft;
            rightmapPS4.GetComponentInChildren<Text>().text = scriptFinder.iverson.ps4moveright;
            forwardmapPS4.GetComponentInChildren<Text>().text = scriptFinder.iverson.ps4moveforward;
            backmapPS4.GetComponentInChildren<Text>().text = scriptFinder.iverson.ps4moveback;
            weapon1mapPS4.GetComponentInChildren<Text>().text = scriptFinder.iverson.ps4weapon1;
            Debug.Log("PS4 Weapon 1 LMA LM " + weapon1mapPS4.GetComponentInChildren<Text>().text);
            Debug.Log("PS4 Weapon 1 LMA LM Script " + scriptFinder.iverson.ps4weapon1);
            weapon2mapPS4.GetComponentInChildren<Text>().text = scriptFinder.iverson.ps4weapon2;          
            boostPS4.GetComponentInChildren<Text>().text = scriptFinder.iverson.ps4boost;
            settingsIGPS4.GetComponentInChildren<Text>().text = scriptFinder.iverson.ps4selectIG;
            MenuBackbPS4.GetComponentInChildren<Text>().text = scriptFinder.iverson.ps4backM;
            MenuSelectbPS4.GetComponentInChildren<Text>().text = scriptFinder.iverson.ps4selectM;
            MenuTraversebPS4.GetComponentInChildren<Text>().text = scriptFinder.iverson.ps4traverseUPM;
            MenutraversebDownPS4.GetComponentInChildren<Text>().text = scriptFinder.iverson.ps4traverseDOWNM;
        


    }
    public void MountUp()
    {
        if (scriptFinder != null)
        {

            //ps4
            scriptFinder.iverson.ps4moveleft = leftmapPS4.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.ps4moveright = rightmapPS4.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.ps4moveforward = forwardmapPS4.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.ps4moveback = backmapPS4.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.ps4weapon1 = weapon1mapPS4.GetComponentInChildren<Text>().text;
            Debug.Log("PS4 Weapon 1 IG MU " + weapon1mapPS4.GetComponentInChildren<Text>().text);
            Debug.Log("PS4 Weapon 1 IG MU Script " + scriptFinder.iverson.ps4weapon1);
            scriptFinder.iverson.ps4weapon2 = weapon2mapPS4.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.ps4boost = boostPS4.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.ps4selectIG = settingsIGPS4.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.ps4backM = MenuBackbPS4.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.ps4selectM = MenuSelectbPS4.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.ps4traverseUPM = MenuTraversebPS4.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.ps4traverseDOWNM = MenutraversebDownPS4.GetComponentInChildren<Text>().text;
            //xbox
            scriptFinder.iverson.Xmoveleft = leftX.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.Xmoveright = rightX.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.Xmoveforward = forwardX.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.Xmoveback = backX.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.Xweapon1 = weapon1X.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.Xweapon2 = weapon2X.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.Xboost = boostX.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.XselectIG = settingsIGX.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.XbackM = MbackX.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.XselectM = MSelX.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.XtraverseUPM = MTraverseUX.GetComponentInChildren<Text>().text;
            scriptFinder.iverson.XtraverseDOWNM = MTraverseDX.GetComponentInChildren<Text>().text;

        }

    }
  

}
