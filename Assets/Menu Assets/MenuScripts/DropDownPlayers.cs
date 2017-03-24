using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class DropDownPlayers : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    ///creates a dynamic list of the current players in the room
  
     
    //children and parent instance variables
    public RectTransform container;
    public bool isOpen;
    public Text mainText;
    public Image image { get { return GetComponent<Image>(); } }

    public List<DropDownPlayerChild> children;
    public float childHeight = 30;
    public int childFontSize = 11;
    public Color normal = Color.green;
    public Color highlighted = Color.red;
    public Color pressed = Color.grey;
    public string font = "Arial";
    public DropDownPlayerChild player;
    private PhotonPlayer[] playerlist;

    public Button butt;
    private GameObject GameMenuManager;
    private GameManager scriptFinder;

    private bool red;
    private bool blue;
    private bool ffa;

    private GameObject controller;
    private ControllerHandler CH;
    

    // Use this for initialization
    void Start()
    {
        red = false;
        blue = false;
        ffa = false;

        if(gameObject.name == "DropDownBlue")
        {
            container = transform.FindChild("ContainerBlue").GetComponent<RectTransform>();
            blue = true;
        }
        else if(gameObject.name == "DropDownPlayersFFA")
        {
            container = transform.FindChild("Container").GetComponent<RectTransform>();
            ffa = true;
        }
        else if(gameObject.name == "DropDownRed")
        {
            container = transform.FindChild("ContainerRed").GetComponent<RectTransform>();
            red = true;
        }



        controller = GameObject.Find("ControllerHandler");
        CH = controller.GetComponent<ControllerHandler>();


        //container = transform.FindChild("Container").GetComponent<RectTransform>();
        isOpen = true;
        this.children = new System.Collections.Generic.List<DropDownPlayerChild>();

        //gamemanager
        GameMenuManager = GameObject.Find("GameMenuManager");
        scriptFinder = GameMenuManager.GetComponent<GameManager>();



        InvokeRepeating("CheckThemPlayers", 2, 1.0f);


    

    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 scale = container.localScale;
        scale.y = Mathf.Lerp(scale.y, isOpen ? 1 : 0, Time.deltaTime * 12);
        container.localScale = scale;
        
    }
    //scroll over 
    public void OnPointerEnter(PointerEventData eventData)
    {
     
    }
    ///scroll out
    public void OnPointerExit(PointerEventData eventData)
    {
  
    }
    //add children players to array list
    public void GeneratePlayerList()
    {

        playerlist = PhotonNetwork.playerList;

        //Debug.Log("Player List count is " + playerlist.Length);

        if (CH.roommenu == true || CH.roommenuTD == true)
        {

            if (ffa == true)
            {
                if (playerlist.Length > 0)
                {
                    for (int i = 0; i < playerlist.Length; i++)
                    {
                        if (playerlist[i].name != "")
                        {
                            AddPlayer(playerlist[i].name);
                            //Debug.Log("Children Created " + playerlist[i].name);

                        }

                    }

                }
            }
            else if (blue == true)
            {


                if (playerlist.Length > 0)
                {
                    for (int i = 0; i < playerlist.Length; i++)
                    {

                        if ((bool)playerlist[i].customProperties["Blue"] == true)
                        {
                            if (playerlist[i].name != "")
                            {
                                AddPlayer(playerlist[i].name);


                            }
                        }


                    }

                }






            }
            else if (red == true)
            {

                if (playerlist.Length > 0)
                {
                    for (int i = 0; i < playerlist.Length; i++)
                    {

                        if ((bool)playerlist[i].customProperties["Red"] == true)
                        {
                            if (playerlist[i].name != "")
                            {
                                AddPlayer(playerlist[i].name);


                            }
                        }


                    }

                }





            }


        }


    }
    ///adds a childdropdown button as a room for user
    public void AddPlayer(string playername)
    {
        player = new DropDownPlayerChild(this, "Player Button", playername);

        if (player.UpdateChild(this) == true)
        {
            children.Add(player);
        }
        else
            return;

    }
    //clears the drop down button room list
    public void Clean()
    {
        //Debug.Log("Children count is " + children.Count);

        if (this.children != null)
        {
            for (int z = 0; z < this.children.Count; z++)
            {
                DestroyImmediate(this.children[z].childobj);

            }


        }


    }
    /// just runs checks to make sure no button is there that is null
    public void UpdateIt()
    {

        if (this.children != null)
        {
            for (int z = 0; z < this.children.Count; z++)
            {
                //Debug.Log("In Update");


                if (this.children[z].UpdateChild(this) == false)
                {

                    this.children.RemoveAt(z);
                  

                }


            }
        }



    }

    public void CheckDrop()
    {

        if (scriptFinder != null)
        {
            if (scriptFinder.inRoom == true)
            {
                if (!PhotonNetwork.inRoom)
                {
                    if (butt != null)
                    {
                        if (butt.GetComponentInChildren<Text>().text == "Cancel")
                        {
                            butt.onClick.Invoke();

                        }
                    }
                }
            }
        }



    }
    public void CheckThemPlayers()
    {
        
        ///clean the list/ then update it with new players, check for players that have been kicked at iterations of 3 seconds
        playerlist = PhotonNetwork.playerList;

        if (playerlist.Length > 0)
        {
            Clean();

            UpdateIt();
          
            GeneratePlayerList();

            CheckDrop();

        }
 
    }


  

}




//[System.Serializable]
public class DropDownPlayerChild
{

    //playerbutton
    private GameObject playerButton;

    //personal inforamtion about the child
    public GameObject childobj;
    public Text childText;
    public Button.ButtonClickedEvent childEvents;
    private string player;
    

    ///for the setting up the properties of the object
    private LayoutElement element { get { return childobj.GetComponent<LayoutElement>(); } }
    private Button button { get { return childobj.GetComponent<Button>(); } }
    private Image image { get { return childobj.GetComponent<Image>(); } }


    /// this is for setting up the join room on click event
    //private GameObject LobbyBuddy;
    //private PreGameLobbyScript ScriptBuddy;
    private PhotonPlayer[] playerlist;
    private GameObject GameMenuManager;
    private GameManager scriptFinder;

   
    

    ///constructor
    public DropDownPlayerChild(DropDownPlayers parent, string nameinUnity, string playerName)
    {
        
        //gamemanager
        GameMenuManager = GameObject.Find("GameMenuManager");
        scriptFinder = GameMenuManager.GetComponent<GameManager>();

        this.player = playerName;

        childobj = Utility.NewButton(nameinUnity, playerName, parent.container).gameObject;

        childobj.AddComponent<LayoutElement>();
      
        childText = childobj.GetComponentInChildren<Text>();

        childEvents = button.onClick;

        if(childobj.transform.parent.name == "Container")
        {
            playerButton = GameObject.Find("DropDownPlayersFFA");

        }
        else if (childobj.transform.parent.name == "ContainerRed")
        {
            playerButton = GameObject.Find("DropDownRed");
        }
        else if (childobj.transform.parent.name == "ContainerBlue")
        {
            playerButton = GameObject.Find("DropDownBlue");

        }
              
        if(PhotonNetwork.isMasterClient)
        {

            //allows master to kick
            
            childEvents.AddListener(delegate { Selected(); });

        }

    }

    //sets the properties from the parent
    public bool UpdateChild(DropDownPlayers parent)
    {
        if (childobj == null)
            return false;

        if (playerButton != null)
        {
            float efloat = playerButton.GetComponent<RectTransform>().rect.height;

            ///set layout element minhieght
            element.minHeight = efloat;

            ///set image sprite and type
            image.sprite = parent.image.sprite;
            image.type = parent.image.type;

            //get vector of rotation and placement
            Vector3 yahs = playerButton.transform.rotation.eulerAngles;
            float z = playerButton.transform.position.z;
            float x = playerButton.transform.position.x;
            float y = playerButton.transform.position.y;

            //set them to the new childobj
            childobj.transform.position = new Vector3(x, y, z);
            childobj.transform.Rotate(yahs);

            //childobj.transform.Rotate(new Vector3(0.0f, 1.5f, 0.0f));
        }

        playerlist = PhotonNetwork.playerList;

        ColorBlock b = button.colors;

        //b.normalColor = Color.cyan;
        b.highlightedColor = Color.green;
        //b.pressedColor = Color.red;
        childText.color = Color.white;


        if (playerlist.Length > 0)
        {

            for (int i = 0; i < playerlist.Length; i++)
            {
                if (playerlist[i].name == this.player)
                {
                    if ((bool)playerlist[i].customProperties["Ready"] == true)
                    {
                       
                        childText.color = Color.green;

                    }
                }

            }

        }





        button.colors = b;

        //set button onclick
        button.onClick = childEvents;

        ///set childtext font, fotn color, and font size
        childText.font = parent.mainText.font;
        childText.fontSize = 14;

        return true;
    }
    public string returnPlayer()
    {

        return player;
    }

    public void Selected()
    {



        if(PhotonNetwork.isMasterClient)
        {

            if (playerlist.Length > 0)
            {
                for (int i = 0; i < playerlist.Length; i++)
                {

                    if(playerlist[i].name == this.returnPlayer())
                    {
                        if(PhotonNetwork.CloseConnection(playerlist[i]))
                        {
                            Debug.Log("Player Kicked " + playerlist[i].name);

                        }

                    }

              
                }
            }
            
        
        }
     



    }



}
