using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class DropDownRoom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    ///this creates a dynamic drop down list, grabs the rooms and generates them as buttons for user
    ///to click on to join room


    //children and parent instance variables
    public RectTransform container;
    public bool isOpen;
    public Text mainText;
    public Image image { get { return GetComponent<Image>(); } }
    public List<DropdownChild> children;
    public float childHeight = 50;
    public int childFontSize = 11;
    public Color normal = Color.cyan;
    public Color highlighted = Color.cyan;
    public Color pressed = Color.cyan;
    public string font = "Arial";
    public DropdownChild room;
    private RoomInfo[] roomsList;
    //GM
    private GameObject BO;
    private MatchMakingClicks BOscript;
    private GameObject controller;
    private ControllerHandler CH;
    

	// Use this for initialization
	void Start () 
    {
        
        container = transform.FindChild("Container").GetComponent<RectTransform>();
        isOpen = true;
        this.children = new System.Collections.Generic.List<DropdownChild>();

        BO = GameObject.Find("ButtonActionObject");
        BOscript = BO.GetComponent<MatchMakingClicks>();

        InvokeRepeating("CheckThemRooms", 2, 2.0f);
            
	}
	
	// Update is called once per frame
	public void Update () 
    {
            Vector3 scale = container.localScale;
            scale.y = Mathf.Lerp(scale.y, isOpen ? 1 : 0, Time.deltaTime * 12);
            container.localScale = scale;
	}
    //scroll over even grab the rooms
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    ///scroll over event
    public void OnPointerExit(PointerEventData eventData)
    {
    
    }
    ///adds a childdropdown button as a room for user
    public void AddRoom(string roomName)
    {
        room = new DropdownChild(this, "RoomButton", roomName);

        if (room.UpdateChild(this) == true)
        {
            children.Add(room);
        }
        else
            return;

    }

    //gets list of rooms and turns them into buttons for drop down list
    public void getRooms()
    {
       
        roomsList = PhotonNetwork.GetRoomList();

        //Debug.Log("roomslistcount is " + roomsList.Length);

            if (roomsList.Length > 0)
            {

                for (int i = 0; i < roomsList.Length; i++)
                {
                    if (roomsList[i].playerCount > 0)
                    {
                        if (roomsList[i].customProperties["GameType"].ToString() == BOscript.returnGTYPE().ToString())
                        {
                            if (roomsList[i].open == true)
                            {
                                AddRoom(roomsList[i].name);
                            }
                            
                        }
                    }
                  
                 
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
             
                if(this.children[z].UpdateChild(this) == false)
                {
                   
                    this.children.RemoveAt(z);
                    

                }

                    
            }
        }



    }
    //clears the drop down button room list
    public void Clean()
    {

        if (this.children != null)
        {
            for (int z = 0; z < this.children.Count; z++)
            {
                DestroyImmediate(this.children[z].childobj);

            }


        }


    }
    ///wait for user to join lobby then run the function
    void CheckThemRooms()
    {
        
        Clean();

        UpdateIt();

        getRooms();

        
    }


}

//[System.Serializable]
public class DropdownChild
{
    //playerbutton
    private GameObject shuttleButton;

    //personal inforamtion about the child
    public GameObject childobj;
    public Text childText;
    public Button.ButtonClickedEvent childEvents;
    private string roomName;
    private string NameClicked;

    ///for the setting up the properties of the object
    private LayoutElement element { get { return childobj.GetComponent<LayoutElement>(); } }
    private Button button { get { return childobj.GetComponent<Button>();  } }
    private Image image { get { return childobj.GetComponent<Image>(); } }

    
    /// this is for setting up the join room on click event
    public GameObject TextRoom;
    private MatchMakingClicks grabscript;
    private RoomInfo[] roomsList;

    //personal information variables that will be sent to the action object
    private string name;
    private string currentPlayers;
    private string maxPlayers;

    ///constructor
    public DropdownChild(DropDownRoom parent, string nameinUnity, string rName)
    {
        //initialize and set up properties
        this.name = "";
        this.currentPlayers = "";
        this.maxPlayers = "";
        this.NameClicked = "";
        this.roomName = rName;

        shuttleButton = GameObject.Find("DropDownRoomChoice");

        childobj = Utility.NewButton(nameinUnity, roomName, parent.container).gameObject;

        childobj.AddComponent<LayoutElement>();

        childText = childobj.GetComponentInChildren<Text>();

        childEvents = button.onClick;

        childEvents.AddListener(delegate { Selected(); });

        
        
        
    }

    //sets the properties from the parent
    public bool UpdateChild(DropDownRoom parent)
    {
        if (childobj == null)
            return false;

        float efloat = shuttleButton.GetComponent<RectTransform>().rect.height;
        ///set layout element minhieght
        element.minHeight = efloat;
        
        ///set image sprite and type
        image.sprite = parent.image.sprite;
        image.type = parent.image.type;

        ///set button noraml, highlighted pressed colors
        ///
        ColorBlock b = button.colors;

        childText.color = Color.red;
        button.colors = b;
        b.highlightedColor = Color.green;

        //set button onclick
        button.onClick = childEvents;

        ///set childtext font, fotn color, and font size
        childText.font = parent.mainText.font;
        //childText.color = Color.black;
        childText.fontSize = 11;

        return true;
    }


    ///getter
   public string returnName()
   {

       return roomName;

   }
   ///on click method that sends the information the the matchmaking object to display information to the user, and sets the room name
   ///properties
    public void Selected()
    {
        NameClicked = returnName();
      
        CheckRooms();

        TextRoom = GameObject.Find("ButtonActionObject");
        grabscript = TextRoom.GetComponent<MatchMakingClicks>();

        grabscript.SetRoom(name);
        grabscript.Selected = true;
        grabscript.RoomNameSelected.GetComponentInChildren<Text>().text = "Shuttle Name: " + name;
        grabscript.RoomPlayersSelected.GetComponentInChildren<Text>().text = "Players Currently in Shuttle: " + currentPlayers;
        grabscript.RoomMaxPlayersSelected.GetComponentInChildren<Text>().text = "Max Amount of Players: " + maxPlayers;

    }
    //fimds the room name associated to the onlick button event
    public void CheckRooms()
    {

                roomsList = PhotonNetwork.GetRoomList();

                if(roomsList.Length > 0)
                {

                for (int i = 0; i < roomsList.Length; i++)

                 {
                        if(roomsList[i].name == NameClicked)
                        {

                            

                            name = roomsList[i].name;
                            currentPlayers = roomsList[i].playerCount.ToString();
                            maxPlayers = roomsList[i].maxPlayers.ToString();
                      

                         }

                    }
               }
               


    }




}
