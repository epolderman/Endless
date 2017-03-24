using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShipRotation : MonoBehaviour {

    /// ship rotation and sprite selection class that is attached to the controlships empty gameobject

    //public Sprite[] spriteRotation;
    public Image shipimage;
    private List<Sprite> spriteRotation;
    private int count;
    public Text shipN;
    public Text hullV;
    public Text shieldV;
    public Text thrust;
    public Text primWeapon;
    public Text secondWeapon;
    public Text roomshipN;
    public Image roomShipImage;
    //for ffa
    public Text FFAroomshipN;
    public Image FFAroomShipImage;
    
    
    private struct shipvalues
    {
        public string shipname;
        public string hullvalue;
        public string shieldvalue;
        public string thrusters;
        public string primweapon;
        public string secondweapon;
        public int rotationNumber;
        public string path;
        //public List<Sprite> spriteRotation;

    };

    private shipvalues lockonz;
    private shipvalues mechaz;
    private shipvalues jupiter;
    private shipvalues souleater;
    private shipvalues arbiter;
    private shipvalues clankR7;
    private List<shipvalues> shipInfo;

    private string CurrentShip;
    private string CurrentPath;
    public GameObject GameMenuManager;

    private GameObject Controller;
    private ControllerHandler CHS;
    

    //declares and sets all the ship information 
    public void GrabShipInformation()
    {
        

        //will be subscript 0 in sprite array
        lockonz.shipname = "Lock On";
        lockonz.hullvalue = "15%";
        lockonz.shieldvalue = "15%";
        lockonz.thrusters = "Medium";
        lockonz.primweapon = "Shotgun Blast";
        lockonz.secondweapon = "Laser Spike";
        lockonz.rotationNumber = 0;
        lockonz.path = "Sprites/Ship/LockONSprite";
       
        
        //will be 1 subscript in sprite array
        mechaz.shipname = "Mecha";
        mechaz.hullvalue = "15%";
        mechaz.shieldvalue = "15%";
        mechaz.thrusters = "Strong";
        mechaz.primweapon = "Machine Gun";
        mechaz.secondweapon = "Twin Laser Burst";
        mechaz.rotationNumber = 1;
        mechaz.path = "Sprites/Ship/PotentialShipSprite";


        ///will be 2 in subscript array
        jupiter.shipname = "Jupiter";
        jupiter.hullvalue = "25%";
        jupiter.shieldvalue = "20%";
        jupiter.thrusters = "Weak";
        jupiter.primweapon = "Dual Laser Cannons";
        jupiter.secondweapon = "Photon Torpedos";
        jupiter.rotationNumber = 2;
        jupiter.path = "Sprites/Ship/Jupiter";

        //will be 3
        souleater.shipname = "SoulEater";
        souleater.hullvalue = "20%";
        souleater.shieldvalue = "15%";
        souleater.thrusters = "Weak";
        souleater.primweapon = "Magnetic Field";
        souleater.secondweapon = "Homing Rocket";
        souleater.rotationNumber = 3;
        souleater.path = "Sprites/Ship/souleater";

        //4
        arbiter.shipname = "Arbiter";
        arbiter.hullvalue = "25%";
        arbiter.shieldvalue = "25%";
        arbiter.thrusters = "Weak";
        arbiter.primweapon = "Mini Gatling Turret";
        arbiter.secondweapon = "Mini Tesla Turret";
        arbiter.rotationNumber = 4;
        arbiter.path = "Sprites/Ship/Arbiter";

        //5
        clankR7.shipname = "Clank R7";
        clankR7.hullvalue = "15%";
        clankR7.shieldvalue = "20%";
        clankR7.thrusters = "Medium";
        clankR7.primweapon = "Shrapnel Mine";
        clankR7.secondweapon = "Magnetic Mines";
        clankR7.rotationNumber = 5;
        clankR7.path = "Sprites/Ship/ClankR7";


        shipInfo = new System.Collections.Generic.List<shipvalues>();

        //0
        shipInfo.Add(lockonz);
        //1
        shipInfo.Add(mechaz);
        //2
        shipInfo.Add(jupiter);
        //3
        shipInfo.Add(souleater);
        //4
        shipInfo.Add(arbiter);
        //5
        shipInfo.Add(clankR7);

    }

	// Use this for initialization
	void Start () 
    {
        //controller handler
        Controller = GameObject.Find("ControllerHandler");
        CHS = Controller.GetComponent<ControllerHandler>();


        GrabShipInformation();

        count = 0;
        spriteRotation = new System.Collections.Generic.List<Sprite>();

        Sprite lockOn = Resources.Load<Sprite>(shipInfo[0].path);
        Sprite Mecha = Resources.Load<Sprite>(shipInfo[1].path);
        Sprite jupiter = Resources.Load<Sprite>(shipInfo[2].path);
        Sprite souleater = Resources.Load<Sprite>(shipInfo[3].path);
        Sprite arbiter = Resources.Load<Sprite>(shipInfo[4].path);
        Sprite clank = Resources.Load<Sprite>(shipInfo[5].path);

        //0
        spriteRotation.Add(lockOn);
        //1
        spriteRotation.Add(Mecha);
        //2
        spriteRotation.Add(jupiter);
        //3
        spriteRotation.Add(souleater);
        //4
        spriteRotation.Add(arbiter);
        //5
        spriteRotation.Add(clank);

      

        //load in first sprite
        if (spriteRotation[count])
        {
            shipimage.GetComponentInChildren<Image>().sprite = spriteRotation[count];
            roomShipImage.GetComponentInChildren<Image>().sprite = spriteRotation[count];
            FFAroomShipImage.GetComponentInChildren<Image>().sprite = spriteRotation[count];
            if (shipInfo[count].rotationNumber == count)
            {
                shipN.GetComponentInChildren<Text>().text = "Ship Name: " + shipInfo[count].shipname;
                roomshipN.GetComponentInChildren<Text>().text = "Ship Name: " + shipInfo[count].shipname;
                FFAroomshipN.GetComponentInChildren<Text>().text = "Ship Name: " + shipInfo[count].shipname;
                CurrentShip = shipInfo[count].shipname;
                CurrentPath = shipInfo[count].path;
                hullV.GetComponentInChildren<Text>().text = "Hull value: " + shipInfo[count].hullvalue;
                shieldV.GetComponentInChildren<Text>().text = "Shield Value: " + shipInfo[count].shieldvalue;
                thrust.GetComponentInChildren<Text>().text = "Thrusters: " + shipInfo[count].thrusters;
                primWeapon.GetComponentInChildren<Text>().text = "Primary Weapon: " + shipInfo[count].primweapon;
                secondWeapon.GetComponentInChildren<Text>().text = "Secondary Weapon: " + shipInfo[count].secondweapon;
            }
            else
            {
                Debug.LogError("Syncronization Ship Stat Error");
            }
        }
        else
        {
            Debug.LogError("Sprite not found", this);
             
        }
        
	

        
	
	}
    public void MakeItSpin()
    {

        InvokeRepeating("spinShipImage", 2, 0.05f);

    }
    public void CancelSpin()
    {
        CancelInvoke("spinShipImage");
    }
   
	
    //rotates the ship sprite
    public void spinShipImage()
    {
        //Vector3 temp = new Vector3(6.103516e-05f, 0.0f, 1.0f + 1.0f);

        shipimage.transform.Rotate(new Vector3(0.0f, 1.5f, 0.0f));
    
    }
    //switches the ship on user command
    public void SwitchShip()
    {
        count++;

        if(count >= spriteRotation.Count)
        {
            count = 0;

        }

        if (spriteRotation[count])
        {
            shipimage.GetComponentInChildren<Image>().sprite = spriteRotation[count];
            roomShipImage.GetComponentInChildren<Image>().sprite = spriteRotation[count];
            FFAroomShipImage.GetComponentInChildren<Image>().sprite = spriteRotation[count];
            if (shipInfo[count].rotationNumber == count)
            {
                shipN.GetComponentInChildren<Text>().text = "Ship Name: " + shipInfo[count].shipname;
                roomshipN.GetComponentInChildren<Text>().text = "Ship Name: " + shipInfo[count].shipname;
                FFAroomshipN.GetComponentInChildren<Text>().text = "Ship Name: " + shipInfo[count].shipname;
                hullV.GetComponentInChildren<Text>().text = "Hull value: " + shipInfo[count].hullvalue;
                CurrentShip = shipInfo[count].shipname;
                CurrentPath = shipInfo[count].path;
                shieldV.GetComponentInChildren<Text>().text = "Shield Value: " + shipInfo[count].shieldvalue;
                thrust.GetComponentInChildren<Text>().text = "Thrusters: " + shipInfo[count].thrusters;
                primWeapon.GetComponentInChildren<Text>().text = "Primary Weapon: " + shipInfo[count].primweapon;
                secondWeapon.GetComponentInChildren<Text>().text = "Secondary Weapon: " + shipInfo[count].secondweapon;
            }
            else
            {
                Debug.LogError("Syncronization Ship Stat Error");
            }
        }
        else
        {
            Debug.LogError("Sprite not found", this);

        }



    
    }

    //for accessing through the game manager script
    public string returnCurrentShipName()
    {

        return CurrentShip;
        
    }
    public string returnCurrentShipPath()
    {

        return CurrentPath;

    }
    

    
   

    

 

}
