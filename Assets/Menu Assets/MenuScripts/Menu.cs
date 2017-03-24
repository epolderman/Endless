using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{

    ///menu controls the canvas group, and animator


    //variables for components in GUI
    private Animator _animator;
    private CanvasGroup _canvasGroup;


    
    public bool IsOpen
    {

        get { return _animator.GetBool("IsOpen");  }
        set { _animator.SetBool("IsOpen", value);  }

    }


    //sets position of menu to center of canvas when we start, but when editing, it sits below the canvas so we can edit freely
    public void Awake()
    {
        _animator = GetComponent<Animator>();
        _canvasGroup = GetComponent<CanvasGroup>();


        //allows us to take the windows in design mode, when we play the game they get positions in the center of the screen
        var rect = GetComponent<RectTransform>();
        rect.offsetMax = rect.offsetMin = new Vector2(0, 0);
    
    }


    //animation controller, are you in state called open?
    //if yes -> enable the canvas group to be interactable
    //if no -> disable canvas from being interactable
    public void Update()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;

        }
        else
        {

            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
        
        }
    }

   





	
}
