using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SettingsHandler : MonoBehaviour 
{

    //array of buttons
    public struct ActiveButtonsz
    {
        public Button button;
        public bool isActive;

        public ActiveButtonsz(Button b, bool k)
        {
            button = b;
            isActive = k;

        }

    }

    //buttons on settings menu
    public Button Controlsb;
    public Button Soundb;
    public Button Accountb;
    public Button Returnb;

    //menu
    private GameObject thisMainMenu;
    private MenuManager scriptMainMenu;

    //game menu manager
    private GameObject GameMenuManager;
    private GameManager script;


    ActiveButtonsz themBsz;
    private List<ActiveButtonsz> theButtons;
    private int count;
    private float minimal;
    private float maximal;
    private bool settingsmenu;

	// Use this for initialization
	void Start () 
    {


        minimal = 0;
        maximal = 1;
        initButtons();
        settingsmenu = false;


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
        theButtons = new System.Collections.Generic.List<ActiveButtonsz>();
        //init
        themBsz.button = Controlsb;
        themBsz.isActive = false;
        theButtons.Add(themBsz);
        //hangar
        themBsz.button = Soundb;
        themBsz.isActive = false;
        theButtons.Add(themBsz);
        //system
        themBsz.button = Accountb; ;
        themBsz.isActive = false;
        theButtons.Add(themBsz);
        //escape
        themBsz.button = Returnb;
        themBsz.isActive = false;
        theButtons.Add(themBsz);

    }
	
	// Update is called once per frame
	void Update () 
    {

        if(settingsmenu == false)
        {
            CheckOpen();
        }



        if (scriptMainMenu != null)
        {
            string stringz = scriptMainMenu.returnOpen();
            if (stringz != "SettingsMenu")
            {
                settingsmenu = false;
                
            }
 
        }

        if(settingsmenu == true)
        {

            CheckActive();
        }





	}

    public void CheckOpen()
    {


        if (scriptMainMenu != null)
        {
            string stringzz = scriptMainMenu.returnOpen();

            if (stringzz == "SettingsMenu")
            {
                StartCoroutine(CheckStatus());
            }

        }

    }
    IEnumerator CheckStatus()
    {
        yield return new WaitForSeconds(1.0f);

        settingsmenu = true;

    }


    public void CheckActive()
    {


            MakeActiveButton();

            //your down traverse
            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.MtraverseDown.keyCode))
            {

                count += 1;

                if (count >= theButtons.Count)
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

                if (count >= theButtons.Count)
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
                    count = 3;
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
                    count = 3;
                }

            }



            //select
            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mselectmenu.keyCode))
            {
                for (int i = 0; i < theButtons.Count; i++)
                {
                    if (theButtons[i].isActive == true)
                    {
                        theButtons[i].button.onClick.Invoke();
                    }

                }

            }

            //select active controller     
            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMselectmenu.keyCode))
            {
                
                for (int i = 0; i < theButtons.Count; i++)
                {
                    
                    if (theButtons[i].isActive == true)
                    {
                        Debug.Log(theButtons[i].button + " is being selected");
                        theButtons[i].button.onClick.Invoke();
                    }

                }

            }


            //escape
            if (Input.GetKeyDown((KeyCode)script.userControlsKeyboard.Mbackmenu.keyCode)
                || Input.GetKeyDown((KeyCode)script.userControlsKeyboard.ACMbackmenu.keyCode))
            {
                if (Returnb != null)
                {
                    Returnb.onClick.Invoke();
                }

            }


        


    }
    public void MakeActiveButton()
    {
        if (theButtons.Count == 4)
        {

            for (int i = 0; i < theButtons.Count; i++)
            {
                if (theButtons[count].button != null)
                {
                    if (count != i)
                    {
                        theButtons[i] = new ActiveButtonsz(theButtons[i].button, false);
                    }
                    else
                    {
                        theButtons[count] = new ActiveButtonsz(theButtons[count].button, true);
                        theButtons[count].button.Select();
                    }
                }



            }

        }
    }





}
