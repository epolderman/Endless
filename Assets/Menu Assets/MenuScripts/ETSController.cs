using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ETSController : MonoBehaviour 
{
    private GameObject Controller;
    private ControllerHandler CHS;
    public Text WelcomeText;
    public Text ModeType;
    public Text GameD;
    public Button FFA;
    public Button TD;
    private int once;
    private bool displayed;
    private string FFAd = "Every player for themselves in free for all combat. The battles take place in the Bohemian Asteroid Field. First player to 20 kills takes home the skulls.";
    private string TDd = "Solar System battles on team deathmatch teams. First team to 20 kills takes all the glory and gets to bring home all the skulls.";
    private GameObject GameMenuManager;
    private GameManager script;

    private GameObject GCP;
    private GameModeClipPLay clips;


	// Use this for initialization
	void Start () 
    {
        Controller = GameObject.Find("ControllerHandler");
        CHS = Controller.GetComponent<ControllerHandler>();
        once = 0;
        displayed = false;      
        InvokeRepeating("ControlWelcome", 1, 1.5f);
        GameMenuManager = GameObject.Find("GameMenuManager");
        script = GameMenuManager.GetComponent<GameManager>();

        GCP = GameObject.Find("ClipImage");
        clips = GCP.GetComponent<GameModeClipPLay>();

        //clips.PlayMovie();
        
        //GameD.text = FFAd;
	}
    public void PlayClip()
    {
        //clips.FFAmovie.Stop();
        //clips.TDmovie.Stop();
        clips.PlayMovie();
    }
    public void StopClips()
    {
        clips.FFAmovie.Stop();
        clips.TDmovie.Stop();
    }
	
	// Update is called once per frame
	void Update () 
    {
        DetermineText();
	}
    //select game mode
    public void Select()
    {
        if(ModeType.text == "GameType: FFA")
        {
            Debug.Log("FFA Selected");
            script.setGameType("FFA");
            Debug.Log("The Game Type is " + script.returnGameType());
            StopClips();
        }
        
        if(ModeType.text == "GameType: Team Deathmatch")
        {
            Debug.Log("TD Selected");
            script.setGameType("Team Deathmatch");
            Debug.Log("The Game Type is " + script.returnGameType());
            StopClips();

        }

    }
    public void FFASelected()
    {
        FFA.Select();
        ModeType.text = "GameType: FFA";
        clips.FFAmovie.Stop();
        clips.TDmovie.Stop();
        PlayClip();

    }
    public void TDSelected()
    {
        TD.Select();
        ModeType.text = "GameType: Team Deathmatch";
        clips.FFAmovie.Stop();
        clips.TDmovie.Stop();
        PlayClip();
    }
    public void DetermineText()
    {
        if(ModeType.text == "GameType: FFA")
        {
            GameD.text = FFAd;
        }


        if(ModeType.text == "GameType: Team Deathmatch")
        {
            GameD.text = TDd;
        }
       

    }
    public void ControlWelcome()
    {

        if (CHS.modemenu == true)
        {
            if (once > 6 && displayed == true)
            {
                once = 3;
            }


            if (once == 0)
            {
                WelcomeText.text = "Endless";

            }
            else if (once == 1)
            {
                WelcomeText.text = "Endless Transportation";

            }
            else if (once == 2)
            {
                WelcomeText.text = "Endless Transportation System";

            }
            else if (once == 3)
            {
                WelcomeText.text = "Endless Transportation System";
                displayed = true;
            }
            else if (once == 4)
            {
                WelcomeText.text = "Endless Transportation System.";
            }
            else if (once == 5)
            {
                WelcomeText.text = "Endless Transportation System..";
            }
            else if (once == 6)
            {
                WelcomeText.text = "Endless Transportation System...";
            }



            once++;

        }


        
    }
    public void LogOut()
    {
        PhotonNetwork.Disconnect();

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


    }
















}
