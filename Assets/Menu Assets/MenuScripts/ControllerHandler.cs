using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

[HideInInspector]
public class ControllerHandler : MonoBehaviour
{

    //active button
    public string ActiveButtonName;

	public AudioClip menuButton;
	//private AudioSource audio;

    //menu objects
    private GameObject thisMainMenu;
    private MenuManager scriptMainMenu;

    //game menu manager objects for controller mapping
    private GameObject GameMenuManager;
    private GameManager script;

    //list of buttons
    public struct ActiveButtons
    {
        public Button button;
        public bool isActive;
        public string name;
        public int index;

        public ActiveButtons(Button b, bool k, string n, int i)
        {
            button = b;
            isActive = k;
            name = n;
            index = i;

        }

    }

    

    //struct
    private ActiveButtons themBs;

    //main menu -> done
    public Button Mstart;
    public Button Mselect;
    public Button Msettings;
    public Button Mexit;
    private List<ActiveButtons> MainButtons;
    private bool mainmenu;
   

    //settings menu -> done
    public Button Scontrol;
    public Button Ssound;
    public Button Saccount;
    public Button Sexit;
    private List<ActiveButtons> SettingsButtons;
    private bool settingsmenu;
   

    //hangar -> done
    public Button Hswitch;
    public Button Hexit;
    private List<ActiveButtons> HangarButtons;
    public bool hangarmenu;
  

    //mode menu ->done
    public Button ModeFFA;
    public Button ModeTD;
    public Button ModeCancel;
    public Button ModeSelect;
    private List<ActiveButtons> ModeButtons;
    public bool modemenu;
  
    //matchmaking menu -> done
    public Button MMcreate;
    public Button MMback;
    private bool matchmenu;
   
    
    ///room menu -> FFA
    public Button RMready;
    public Button RMlaunch;
    public Button RMback;
    public Button RMswitch;
    private List<ActiveButtons> RoomButtons;
    public bool roommenu;

    //room menu -> TD
    public Button RMTready;
    public Button RMTlaunch;
    public Button RMTback;
    public Button RMTred;
    public Button RMTblue;
    public Button RMTswitch;
    private List<ActiveButtons> RoomButtonsTD;
    public bool roommenuTD;

    //controls menu
    public Button Cleft;
    public Button Cright;
    public Button Cdown;
    public Button Cforward;
    public Button Cweapon1;
    public Button Cweapon2;
    public Button Cmenuselect;
    public Button Cmenuback;
    public Button CmenuTUP;
    public Button CmenuTDOWN;
    public Button Cback;
    public Button Csettings;
    public Button Cboost;
    private bool controlmenu;
    private List<ActiveButtons> ControlButtons;

    //sound menu
    public Button Soundback;
    private bool soundmenu;

    //account menu
    public Button AccountBack;
    private bool accountmenu;

    //for axis movements smoothness
    private float minimal;
    private float maximal;
    private int count;

  


    // Use this for initialization
    void Start()
    {
        //audio = GetComponent<AudioSource>();
        // audio.clip = menuButton;
        ActiveButtonName = string.Empty;

        //main menu
        thisMainMenu = GameObject.Find("Canvas");
        scriptMainMenu = thisMainMenu.GetComponent<MenuManager>();

        //game manager
        GameMenuManager = GameObject.Find("GameMenuManager");
        script = GameMenuManager.GetComponent<GameManager>();

        //inits
        count = 0;
        minimal = 0;
        maximal = 1;
        mainmenu = false;
        settingsmenu = false;
        hangarmenu = false;
        modemenu = false;
        matchmenu = false;
        roommenu = false;
        controlmenu = false;
        accountmenu = false;
        soundmenu = false;
        roommenuTD = false;

       
        //load the lists the buttons on each menu
        //wont be activated until the response from the main menu does so
        loadLists();
        


    }
    public void loadLists()
    {
        //main menu
        MainButtons = new System.Collections.Generic.List<ActiveButtons>();
        //start
        themBs.button = Mstart;
        themBs.isActive = false;
        themBs.name = "Initiate Launch";
        themBs.index = 0;
        MainButtons.Add(themBs);
        //hangar
        themBs.button = Mselect;
        themBs.isActive = false;
        themBs.name = "Select Spaceship Button";
        themBs.index = 1;
        MainButtons.Add(themBs);
        //system
        themBs.button = Msettings;
        themBs.isActive = false;
        themBs.name = "Settings Button";
        themBs.index = 2;
        MainButtons.Add(themBs);
        //escape
        themBs.button = Mexit;
        themBs.isActive = false;
        themBs.name = "Exit Button";
        themBs.index = 3;
        MainButtons.Add(themBs);

        //settings menu
        //adds the buttons to the list
        SettingsButtons = new System.Collections.Generic.List<ActiveButtons>();
        //controls
        themBs.button = Scontrol;
        themBs.isActive = false;
        themBs.name = "Controls";
        themBs.index = 0;
        SettingsButtons.Add(themBs);
        //sound
        themBs.button = Ssound;
        themBs.isActive = false;
        themBs.name = "Sound";
        themBs.index = 1;
        SettingsButtons.Add(themBs);
        //account
        themBs.button = Saccount;
        themBs.isActive = false;
        themBs.name = "Manage Account";
        themBs.index = 2;
        SettingsButtons.Add(themBs);
        //escape
        themBs.button = Sexit;
        themBs.isActive = false;
        themBs.name = "Return to Endless System";
        themBs.index = 3;
        SettingsButtons.Add(themBs);

        //room menu FFA
        RoomButtons = new System.Collections.Generic.List<ActiveButtons>();
        //switch
        themBs.button = RMswitch;
        themBs.isActive = false;
        themBs.name = "SwitchShipButtonFFA";
        themBs.index = 0;
        RoomButtons.Add(themBs);
        //back
        themBs.button = RMback;
        themBs.isActive = false;
        themBs.name = "CancelButtonFFA";
        themBs.index = 1;
        RoomButtons.Add(themBs);
        //master launch
        themBs.button = RMlaunch;
        themBs.isActive = false;
        themBs.name = "MasterLaunchButtonFFA";
        themBs.index = 2;
        RoomButtons.Add(themBs);
        //readu
        themBs.button = RMready;
        themBs.isActive = false;
        themBs.name = "ImReadyForLaunchButtonFFA";
        themBs.index = 3;
        RoomButtons.Add(themBs);
       
        ///room menu TD
        RoomButtonsTD = new System.Collections.Generic.List<ActiveButtons>();
        //red
        themBs.button = RMTred;
        themBs.isActive = false;
        themBs.name = "RedButton";
        themBs.index = 0;
        RoomButtonsTD.Add(themBs);
        //blue
        themBs.button = RMTblue;
        themBs.isActive = false;
        themBs.name = "BlueButton";
        themBs.index = 1;
        RoomButtonsTD.Add(themBs);
        //back
        themBs.button = RMTback;
        themBs.isActive = false;
        themBs.name = "CancelButton";
        themBs.index = 2;
        RoomButtonsTD.Add(themBs);
        //master launch
        themBs.button = RMTlaunch;
        themBs.isActive = false;
        themBs.name = "MasterLaunchButton";
        themBs.index = 3;
        RoomButtonsTD.Add(themBs);
        //ready
        themBs.button = RMTready;
        themBs.isActive = false;
        themBs.name = "ImReadyForLaunchButton";
        themBs.index = 4;
        RoomButtonsTD.Add(themBs);
        //switch
        themBs.button = RMTswitch;
        themBs.isActive = false;
        themBs.name = "SwitchShipButton";
        themBs.index = 5;
        RoomButtonsTD.Add(themBs);

        //hangar menu
        HangarButtons = new System.Collections.Generic.List<ActiveButtons>();
        //switch
        themBs.button = Hswitch;
        themBs.isActive = false;
        themBs.name = "Switch";
        themBs.index = 0;
        HangarButtons.Add(themBs);     
        //exit
        themBs.button = Hexit;
        themBs.isActive = false;
        themBs.name = "Back";
        themBs.index = 1;
        HangarButtons.Add(themBs);
        

        //mode menu
        ModeButtons  = new System.Collections.Generic.List<ActiveButtons>();
        //ffa
        themBs.button = ModeFFA;
        themBs.isActive = false;
        themBs.name = "FFA";
        themBs.index = 0;
        ModeButtons.Add(themBs);
        //td
        themBs.button = ModeTD;
        themBs.isActive = false;
        themBs.name = "TD";
        themBs.index = 1;
        ModeButtons.Add(themBs);
        //select
        themBs.button = ModeSelect;
        themBs.isActive = false;
        themBs.name = "SelectButton";
        themBs.index = 2;
        ModeButtons.Add(themBs);
        //cancel
        themBs.button = ModeCancel;
        themBs.isActive = false;
        themBs.name = "LeaveSystem";
        themBs.index = 3;
        ModeButtons.Add(themBs);

        //controls menu
        ControlButtons = new System.Collections.Generic.List<ActiveButtons>();
        //left
        themBs.button = Cleft;
        themBs.isActive = false;
        themBs.name = "EditMoveLeftB";
        themBs.index = 0;
        ControlButtons.Add(themBs);
        //right
        themBs.button = Cright;
        themBs.isActive = false;
        themBs.name = "EditMoveRightB";
        themBs.index = 1;
        ControlButtons.Add(themBs);
        //forward
        themBs.button = Cdown;
        themBs.isActive = false;
        themBs.name = "EditMoveDownB";
        themBs.index = 2;
        ControlButtons.Add(themBs);
        //back
        themBs.button = Cforward;
        themBs.isActive = false;
        themBs.name = "EditMoveForwardB";
        themBs.index = 3;
        ControlButtons.Add(themBs);
        //weapon1
        themBs.button = Cweapon1;
        themBs.isActive = false;
        themBs.name = "EditWeapon1B";
        themBs.index = 4;
        ControlButtons.Add(themBs);
        //weapon2
        themBs.button = Cweapon2;
        themBs.isActive = false;
        themBs.name = "EditWeapon2B";
        themBs.index = 5;
        ControlButtons.Add(themBs);
        //boost
        themBs.button = Cboost;
        themBs.isActive = false;
        themBs.name = "EditBoost";
        themBs.index = 6;
        ControlButtons.Add(themBs);
        //settings
        themBs.button = Csettings;
        themBs.isActive = false;
        themBs.name = "EditSettingsIG";
        themBs.index = 7;
        ControlButtons.Add(themBs);
        //menuback
        themBs.button = Cmenuback;
        themBs.isActive = false;
        themBs.name = "EditMenuBackB";
        themBs.index = 8;
        ControlButtons.Add(themBs);
        //menuselect
        themBs.button = Cmenuselect;
        themBs.isActive = false;
        themBs.name = "EditMenuSelectB";
        themBs.index = 9;
        ControlButtons.Add(themBs);
        //menutraverseup
        themBs.button = CmenuTUP;
        themBs.isActive = false;
        themBs.name = "EditMenuTraverseBUp";
        themBs.index = 10;
        ControlButtons.Add(themBs);
        //menutraversdown
        themBs.button = CmenuTDOWN;
        themBs.isActive = false;
        themBs.name = "EditMenuTraverseBDown";
        themBs.index = 11;
        ControlButtons.Add(themBs);
        //back
        themBs.button = Cback;
        themBs.isActive = false;
        themBs.name = "BackButton";
        themBs.index = 12;
        ControlButtons.Add(themBs);


      


    }
   
  
    void Update()
    {
        //wait 1 second before a switch in one menu to another boolean changes
        //the menu moves too fast at times so the wait makes the menu more responsive and secure in the 
        //right buttons are only active
        CheckCoreRoutine();

        //adds a more responsive UI
        Activate();

    }
 
    public void CheckCoreRoutine()
    {


        if (scriptMainMenu != null)
        {
            string stringzz = scriptMainMenu.returnOpen();

            switch(stringzz)
            {
                case "MainMenu":
                    StartCoroutine(CheckStatusMainMenu());
                    break;
                case "SettingsMenu":
                    StartCoroutine(CheckStatusSettings());
                    break;
                case "HangarClankMenu":
                    StartCoroutine(CheckStatusHangarMenu());
                    break;
                case "GM":
                    StartCoroutine(CheckStatusModeMenu());
                    break;
                case "MatchMakingMenu":
                    StartCoroutine(CheckStatusMatchMenu());
                    break;
                case "RoomMenuFFA":
                    StartCoroutine(CheckStatusRoomMenu());
                    break;
                case "RoomMenuTD":
                    StartCoroutine(CheckStatusROOMmenuTD());
                    break;
                case "ControlsMenu":
                    StartCoroutine(CheckStatusControlRoomMenu());
                    break;
                case "AccountManagerMenu":
                    StartCoroutine(CheckStatusAccountMenu());
                    break;
                case "SoundControlMenu":
                    StartCoroutine(CheckStatusSoundMenu());
                    break;
             
            }

      

        }



    }
    IEnumerator CheckStatusSoundMenu()
    {
        yield return new WaitForSeconds(1.0f);

        soundmenu = true;
        accountmenu = false;
        settingsmenu = false;
        mainmenu = false;
        hangarmenu = false;
        modemenu = false;
        matchmenu = false;
        roommenu = false;
        controlmenu = false;
        roommenuTD = false;
       

    }
    IEnumerator CheckStatusAccountMenu()
    {
        yield return new WaitForSeconds(1.0f);

        accountmenu = true;
        settingsmenu = false;
        mainmenu = false;
        hangarmenu = false;
        modemenu = false;
        matchmenu = false;
        roommenu = false;
        controlmenu = false;
        soundmenu = false;
        roommenuTD = false;
        

    }
    IEnumerator CheckStatusSettings()
    {
        yield return new WaitForSeconds(1.0f);

        settingsmenu = true;
        mainmenu = false;
        hangarmenu = false;
        modemenu = false;
        matchmenu = false;
        roommenu = false;
        controlmenu = false;
        accountmenu = false;
        soundmenu = false;
        roommenuTD = false;
       
        

    }
    IEnumerator CheckStatusMainMenu()
    {
        yield return new WaitForSeconds(1.0f);

        mainmenu = true;
        settingsmenu = false;
        hangarmenu = false;
        modemenu = false;
        matchmenu = false;
        roommenu = false;
        controlmenu = false;
        accountmenu = false;
        soundmenu = false;
        roommenuTD = false;
       

    }
    IEnumerator CheckStatusHangarMenu()
    {
        yield return new WaitForSeconds(1.0f);

        hangarmenu = true;
        mainmenu = false;
        settingsmenu = false;
        modemenu = false;
        matchmenu = false;
        roommenu = false;
        controlmenu = false;
        accountmenu = false;
        soundmenu = false;
        roommenuTD = false;
 

    }
    IEnumerator CheckStatusModeMenu()
    {
        yield return new WaitForSeconds(1.0f);

        modemenu = true;
        mainmenu = false;
        settingsmenu = false;
        hangarmenu = false;
        matchmenu = false;
        roommenu = false;
        controlmenu = false;
        accountmenu = false;
        soundmenu = false;
        roommenuTD = false;
        
    }
    IEnumerator CheckStatusMatchMenu()
    {
        yield return new WaitForSeconds(1.0f);

        matchmenu = true;
        mainmenu = false;
        settingsmenu = false;
        hangarmenu = false;
        modemenu = false;
        roommenu = false;
        controlmenu = false;
        accountmenu = false;
        soundmenu = false;
        roommenuTD = false;
        
    }
    IEnumerator CheckStatusRoomMenu()
    {
        //FFA
        yield return new WaitForSeconds(1.0f);

        roommenu = true;
        mainmenu = false;
        settingsmenu = false;
        hangarmenu = false;
        modemenu = false;
        matchmenu = false;
        controlmenu = false;
        accountmenu = false;
        soundmenu = false;
        roommenuTD = false;
       
    }
    IEnumerator CheckStatusControlRoomMenu()
    {
        yield return new WaitForSeconds(1.0f);

        controlmenu = true;
        mainmenu = false;
        settingsmenu = false;
        hangarmenu = false;
        modemenu = false;
        matchmenu = false;
        roommenu = false;
        accountmenu = false;
        soundmenu = false;
        roommenuTD = false;
        
    }
    IEnumerator CheckStatusROOMmenuTD()
    {
        yield return new WaitForSeconds(1.0f);

        roommenuTD = true;
        mainmenu = false;
        settingsmenu = false;
        hangarmenu = false;
        modemenu = false;
        matchmenu = false;
        roommenu = false;
        controlmenu = false;
        accountmenu = false;
        soundmenu = false;
       
    }

    public void Activate()
    {

        //check which menu should be active
        if (mainmenu == true)
        {
            makeResponsive(MainButtons); 
        }
        else if (settingsmenu == true)
        {
            makeResponsive(SettingsButtons);  
        }
        else if (hangarmenu == true)
        {
            makeResponsive(HangarButtons);
        }
        else if(modemenu == true)
        {
            makeResponsive(ModeButtons); 
        }
        else if(matchmenu == true)
        {
            checkMatchMaking();   
        }
        else if(roommenu == true)
        {
            //checkRoomMenuMaking();
            makeResponsive(RoomButtons); 
        }
        else if(roommenuTD == true)
        {
            makeResponsive(RoomButtonsTD);
        }
        else if(controlmenu == true)
        {
            makeResponsive(ControlButtons);          
        }
        else if(soundmenu == true)
        {
            checkSoundMenu();
        }
        else if(accountmenu == true)
        {
            checkAccountMenu();
        }


    }

    //make buttons active and responsive
    public void makeActive(List<ActiveButtons> theButton)
    {
        //make sure we dont have an index out of array(control menu has 12 buttons -> to settings has 4 (basically clearing the iterator)
        if(count > theButton.Count)
        {
            count = 0;
        }


        for (int i = 0; i < theButton.Count; i++)
            {
                if (theButton[count].button != null)
                {
                    if (count != i)
                    {
                        theButton[i] = new ActiveButtons(theButton[i].button, false, theButton[i].name, i);
                    }
                    else
                    {


                    //checks if mouse is on a button if no then allow key traverse active button to be selected(highlighted)
                    if (ActiveButtonName == string.Empty)
                    {
                        theButton[count] = new ActiveButtons(theButton[count].button, true, theButton[i].name, i);
                        theButton[count].button.Select();
                    }

                    
                    //if mouse is over button find the active button and iterate to it and make selected(highlighted)
                    if (ActiveButtonName != theButton[count].name)
                       {


                        
                                for(int z = 0; z < theButton.Count; z++)
                                {
                            
                                        if(theButton[z].name == ActiveButtonName)
                                        {
                                             
                                            //success     
                                            count = theButton[z].index;
                                            theButton[z] = new ActiveButtons(theButton[z].button, true, theButton[z].name, theButton[z].index);
                                            theButton[z].button.Select();

                                         } 
                                        else
                                        {
                                           //not success
                                           theButton[z] = new ActiveButtons(theButton[z].button, false, theButton[z].name, theButton[z].index);

                                         }       

                                    }   

                        }

                                

                       
                    }


                }



            }

            

    }

    //make responsive depending on the open scene
    public void makeResponsive(List<ActiveButtons> theButton)
    {
        
        makeActive(theButton);

        //your down traverse
        if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.MtraverseDown.keyCode))
        {

            count += 1;
            AudioSource.PlayClipAtPoint(menuButton, this.transform.position);

            if (count > theButton.Count - 1)
            {
                count = 0;
            }

        }

        //your down traverse joystick
        if ((Input.GetAxis("HUDvertical") == 1))
        {

            minimal += 0.10f;

            if (minimal >= 1.00f)
            {
                count += 1;
                AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
                minimal = 0;
            }

            if (count > theButton.Count - 1)
            {
                count = 0;
            }

        }



        //your up traverse
        if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.MtraverseUp.keyCode))
        {

            count -= 1;
            AudioSource.PlayClipAtPoint(menuButton, this.transform.position);


            if (count < 0)
            {
                count = theButton.Count - 1;
            }

        }



        //up joystick traverse
        if (Input.GetAxis("HUDvertical") == -1)
        {
            maximal -= 0.10f;

            if (maximal <= 0.0f)
            {
                maximal = 1;
                count -= 1;
                AudioSource.PlayClipAtPoint(menuButton, this.transform.position);

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
                    Debug.Log("Count is " + count);
                    theButton[i].button.onClick.Invoke();
                    count = 0;
                    AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
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
                    AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
                }

            }

        }
    

        EscapeButton();


    }

    public void EscapeButton()
    {
        if (mainmenu == true)
        {
            

                if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
                || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
                {

                    Mexit.onClick.Invoke();
                    count = 0;
                    AudioSource.PlayClipAtPoint(menuButton, this.transform.position);

                }
            }
            else if (settingsmenu == true)
            {
                if (Sexit != null)
                {
                    if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
                   || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
                    {

                        Sexit.onClick.Invoke();
                        count = 0;
                        AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
                    }
                }
            }
            else if (hangarmenu == true)
            {
                if (Hexit != null)
                {
                    if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
                   || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
                    {

                        Hexit.onClick.Invoke();
                        count = 0;
                        AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
                    }


                    }

             }
        else if(modemenu == true)
        {
            if (ModeCancel != null)
            {
                if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
               || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
                {

                    ModeCancel.onClick.Invoke();
                    count = 0;
                    AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
                }


            }

        }
        else if(controlmenu == true)
        {
            if(Cback != null)
            {

                if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
               || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
                {

                    Cback.onClick.Invoke();
                    count = 0;
                    AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
                }



            }




        }
        else if (roommenuTD == true)
        {

            if (RMTback != null)
            {

                if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
               || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
                {

                    RMTback.onClick.Invoke();
                    count = 0;
                    AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
                }


            }




        }
  


            


     }
    public void checkMatchMaking()
    {

        if (matchmenu == true)
        {

            if (MMback != null)
            {
                if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
               || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
                {

                    MMback.onClick.Invoke();
                    count = 0;
                    AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
                }


            }

            if(MMcreate != null)
            {

                 if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMselectmenu.keyCode))
                 {

                     MMcreate.onClick.Invoke();
                     count = 0;
                     AudioSource.PlayClipAtPoint(menuButton, this.transform.position);

                 }



            }





        }
    }
    //ffa
    public void checkRoomMenuMaking()
    {


        if(roommenu == true)
        {

            if(RMback != null)
            {

                if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
               || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
                {

                    RMback.onClick.Invoke();
                    count = 0;
                    AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
                }


            }

            //master player launch
            if(RMlaunch != null && RMlaunch.IsActive() == true)
            {
                 if (PhotonNetwork.isMasterClient)
                 {
                     if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMselectmenu.keyCode))
                     {

                         RMlaunch.onClick.Invoke();
                         count = 0;
                         AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
                     }



                 }


            }



            //plagyer ready
            if (RMready != null && RMready.IsActive() == true)
            {
                if (!PhotonNetwork.isMasterClient)
                {
                    if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMselectmenu.keyCode))
                    {

                        RMready.onClick.Invoke();
                        count = 0;
                        AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
                    }



                }




            }









        }




    }
    public void checkAccountMenu()
    {
        if (AccountBack != null)
        {

            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
           || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
            {

                AccountBack.onClick.Invoke();
                count = 0;
                AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
            }


          

                if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMselectmenu.keyCode))
                {

                    AccountBack.onClick.Invoke();
                    count = 0;
                    AudioSource.PlayClipAtPoint(menuButton, this.transform.position);

                }



            


        }



    }
    public void checkSoundMenu()
    {

        if (Soundback != null)
        {

            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
           || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
            {

                Soundback.onClick.Invoke();
                count = 0;
                AudioSource.PlayClipAtPoint(menuButton, this.transform.position);
            }


        }


        if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMselectmenu.keyCode))
        {

            Soundback.onClick.Invoke();
            count = 0;
            AudioSource.PlayClipAtPoint(menuButton, this.transform.position);

        }



    }
  
  

    
}
