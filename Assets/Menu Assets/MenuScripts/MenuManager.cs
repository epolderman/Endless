using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour 
{
	public static MenuManager menu_Manager = null;
     

    public Menu CurrentMenu;
    public string currentMenuname;

	void Awake()
	{
		if(MenuManager.menu_Manager == null)
		{
			Object.DontDestroyOnLoad(this.gameObject);
			MenuManager.menu_Manager = this;
		}
		else if(MenuManager.menu_Manager != this)
		{
			Object.DestroyImmediate(this.gameObject);
		}
	}
    
    /// shows the current menu uses ShowMenu
    public void Start()
    {

        ShowMenu(CurrentMenu);

    }


    ///allows us to map a button in the menumanager to different menu with the smallest bit of code available
    ///returns a unity object
    ///can invoke in the inspector
    public void ShowMenu(Menu menu)
    {

        if (CurrentMenu != null)
            CurrentMenu.IsOpen = false;

        CurrentMenu = menu;
        CurrentMenu.IsOpen = true;

        currentMenuname = CurrentMenu.gameObject.transform.name;

    }

    ///for sound controller
    public void soundController(float currentVolume)
    {
        GetComponent<AudioSource>().volume = currentVolume;

    }
    public string returnOpen()
    {
        return currentMenuname;
    }

    





}
