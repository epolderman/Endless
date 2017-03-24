using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

public class KeyBoardHandler : MonoBehaviour
{

    //menu
    private GameObject thisMainMenu;
    private MenuManager scriptMainMenu;
    
    //buttons
    public Button initz;
    public Button hangar;
    public Button systemcontrol;
    public Button escape;

    //game menu manager
    private GameObject GameMenuManager;
    private GameManager script;

    //array of buttons
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

    ActiveButtons themBs;
    private List<ActiveButtons> theButton;
    private int count;
    private float minimal;
    private float maximal;
   // private string ps4 = "Wireless Controller";
   // private string xboxOne = "Controller (Xbox One For Windows)";
    //private string xbox360 = "Controller (Xbox 360 For Windows)";
    private bool mainmenu;

   
  

	// Use this for initialization
	void Start ()
    {   
       
        minimal = 0;
        maximal = 1;
        initButtons();
        mainmenu = false;


        //main menu
        thisMainMenu = GameObject.Find("Canvas");
        scriptMainMenu = thisMainMenu.GetComponent<MenuManager>();

        //game manager
        GameMenuManager = GameObject.Find("GameMenuManager");
        script = GameMenuManager.GetComponent<GameManager>();

        
       
           
       
       
	}
    public void initButtons()
    {
        count = 0;
        //adds the buttons to the list
        theButton = new System.Collections.Generic.List<ActiveButtons>();
        //init
        themBs.button = initz;
        themBs.isActive = false;
        theButton.Add(themBs);
        //hangar
        themBs.button = hangar;
        themBs.isActive = false;
        theButton.Add(themBs);
        //system
        themBs.button = systemcontrol;
        themBs.isActive = false;
        theButton.Add(themBs);
        //escape
        themBs.button = escape;
        themBs.isActive = false;
        theButton.Add(themBs);

    }
	
	// Update is called once per frame
	void Update ()
    {
       
        if(mainmenu == false)
        {
            CheckOpen();
        }

      
        if (scriptMainMenu != null)
        {
            string stringzz = scriptMainMenu.returnOpen();

            if (stringzz != "MainMenu")
            {
                mainmenu = false;
            }

        }
           

        if(mainmenu == true)
        {
            CheckActive();
        }

      
	}
    public void CheckOpen()
    {


        if (scriptMainMenu != null)
        {
            string stringzz = scriptMainMenu.returnOpen();

            if (stringzz == "MainMenu")
            {
                StartCoroutine(CheckStatus());
            }

        }

    }
    IEnumerator CheckStatus()
    {
        yield return new WaitForSeconds(1.0f);

        mainmenu = true;

    }
    


    public void CheckActive()
    {
        
            MakeActiveButton();

            //your down traverse
            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.MtraverseDown.keyCode))
            {

                count += 1;

                if (count >= theButton.Count)
                {
                    count = 0;
                }

            }

            //your down traverse joystick
            if ((Input.GetAxis("AllVert") == 1))
            {

                minimal += 0.17f;

                if (minimal >= 1.00f)
                {
                    count += 1;
                    minimal = 0;
                }

                if (count >= theButton.Count)
                {
                    count = 0;
                }

            }



            //your up traverse
            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.MtraverseUp.keyCode))
            {

                count -= 1;

                if (count < 0)
                {
                    count = theButton.Count - 1;
                }

            }



            //up joystick traverse
            if (Input.GetAxis("AllVert") == -1)
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
                        Debug.Log(theButton[i].button + " is being selected");
                        theButton[i].button.onClick.Invoke();
                    }

                }

            }




            //select active controller     
            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMselectmenu.keyCode))
            {

                for (int i = 0; i < theButton.Count; i++)
                {


                    if (theButton[i].isActive == true)
                    {
                        Debug.Log(theButton[i].button + " is being selected");
                        theButton[i].button.onClick.Invoke();
                    }

                }

            }



            //escape
            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
                || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
            {
                if (escape != null)
                {
                    escape.onClick.Invoke();
                }

            }


        


    }

    public void MakeActiveButton()
    {
        if (theButton.Count == 4)
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
    }

  
 










}
