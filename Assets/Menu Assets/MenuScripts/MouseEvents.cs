using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //grabs the active button the mouse is over and returns it to control handler script
    //for finding the correct button to highlight(communication through mouse and key traverse for menu's)
    public static string ActiveMouseHover;
    private GameObject controller;
    private ControllerHandler CH;

    // Use this for initialization
    void Start ()
    {
        ActiveMouseHover = string.Empty;
        controller = GameObject.Find("ControllerHandler");
        CH = controller.GetComponent<ControllerHandler>();
        CH.ActiveButtonName = ActiveMouseHover;
    }
	
    public void OnPointerExit(PointerEventData eventData)
    {
        CH.ActiveButtonName = string.Empty;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {    
        ActiveMouseHover = gameObject.name;
        CH.ActiveButtonName = ActiveMouseHover;
       
    }
}
