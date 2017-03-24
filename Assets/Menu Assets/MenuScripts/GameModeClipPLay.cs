using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameModeClipPLay : MonoBehaviour 

{



    //movie clips
    public MovieTexture FFAmovie;
    public MovieTexture TDmovie;

    private GameObject Controller;
    private ControllerHandler CHS;

    private GameObject ETS;
    private ETSController zeScript;



	// Use this for initialization
	void Start ()
    {

        //ship rotation and bools
        Controller = GameObject.Find("ControllerHandler");
        CHS = Controller.GetComponent<ControllerHandler>();
 
        //Gets the game type
        ETS = GameObject.Find("ETSController");
        zeScript = ETS.GetComponent<ETSController>();


	}
	
    public void PlayMovie()
    {
        if (zeScript.ModeType.text == "GameType: FFA")
        {
            GetComponent<RawImage>().texture = FFAmovie as MovieTexture;
            FFAmovie.Play();
            FFAmovie.loop = true;
        }
        else if(zeScript.ModeType.text == "GameType: Team Deathmatch")
        {
            GetComponent<RawImage>().texture = TDmovie as MovieTexture;
            TDmovie.Play();
            TDmovie.loop = true;

        }

    }


}
