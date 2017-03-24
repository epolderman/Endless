using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//[RequireComponent(typeof(AudioSource))]

public class HangarClipPlay : MonoBehaviour 
{


   
  
    //public MovieTexture movie;
    public MovieTexture MechaMovie;
    public MovieTexture JupiterMovie;
    public MovieTexture SoulMovie;
    public MovieTexture LockOnMovie;
    public MovieTexture ArbiterMovie;
    public MovieTexture ClankMovie;
    //private AudioSource audio;

    //clips
    private GameObject ControlShips;
    private ShipRotation SR;

	// Use this for initialization
	void Start () 
    {
        //ship rotation
        ControlShips = GameObject.Find("ControlShips");
        SR = ControlShips.GetComponent<ShipRotation>();
        //StartCoroutine(WaitAndLoadShip());

	}

    public void PlayMovie(MovieTexture ActiveMovie)
    {

        ActiveMovie.Play();
        ActiveMovie.loop = true;
    
    }
    public void LockAndLoadShip()
    {
        
       

        if (SR.returnCurrentShipName().Equals("Lock On"))
        {
            Debug.Log("Lock on is active");
            GetComponent<RawImage>().texture = LockOnMovie as MovieTexture;
            PlayMovie(LockOnMovie);
            SoulMovie.Stop();
            ArbiterMovie.Stop();
            JupiterMovie.Stop();
            MechaMovie.Stop();
            ClankMovie.Stop();
           
        }
        else if (SR.returnCurrentShipName().Equals("Arbiter"))
        {
           Debug.Log("Arbiter is active");
           GetComponent<RawImage>().texture = ArbiterMovie as MovieTexture;
           PlayMovie(ArbiterMovie);
           SoulMovie.Stop();
           LockOnMovie.Stop();
           JupiterMovie.Stop();
           MechaMovie.Stop();
           ClankMovie.Stop();
           
        }
        else if (SR.returnCurrentShipName().Equals("Mecha"))
        {
            Debug.Log("Mecha is active");
            GetComponent<RawImage>().texture = MechaMovie as MovieTexture;
            PlayMovie(MechaMovie);
            SoulMovie.Stop();
            LockOnMovie.Stop();
            JupiterMovie.Stop();
            ArbiterMovie.Stop();
            ClankMovie.Stop();
            
        }
        else if (SR.returnCurrentShipName().Equals("Jupiter"))
        {
            Debug.Log("Jupiter is active");
            GetComponent<RawImage>().texture = JupiterMovie as MovieTexture;
            PlayMovie(JupiterMovie);
            SoulMovie.Stop();
            LockOnMovie.Stop();
            MechaMovie.Stop();
            ArbiterMovie.Stop();
            ClankMovie.Stop();
            

        }
        else if (SR.returnCurrentShipName().Equals("SoulEater"))
        {
            Debug.Log("SoulEater is active");
            GetComponent<RawImage>().texture = SoulMovie as MovieTexture;
            PlayMovie(SoulMovie);
            JupiterMovie.Stop();
            LockOnMovie.Stop();
            MechaMovie.Stop();
            ArbiterMovie.Stop();
            ClankMovie.Stop();
            

        }
        else if (SR.returnCurrentShipName().Equals("Clank R7"))
        {
            Debug.Log("Clank is Active");
            GetComponent<RawImage>().texture = ClankMovie as MovieTexture;
            PlayMovie(ClankMovie);
            JupiterMovie.Stop();
            LockOnMovie.Stop();
            MechaMovie.Stop();
            ArbiterMovie.Stop();
            SoulMovie.Stop();
            


        }
    
    
    
    }

     IEnumerator WaitAndLoadShip()
    {
        yield return new WaitForSeconds(2.0f);

        if (SR.returnCurrentShipName().Equals("Lock On"))
        {
            Debug.Log("Lock on is active");
            GetComponent<RawImage>().texture = LockOnMovie as MovieTexture;
            PlayMovie(LockOnMovie);
            //LockOnMovie.Play();

        }
        else if (SR.returnCurrentShipName().Equals("Arbiter"))
        {
            Debug.Log("Arbiter is active");
            GetComponent<RawImage>().texture = ArbiterMovie as MovieTexture;
            PlayMovie(ArbiterMovie);
            //ArbiterMovie.Play();
        }
        else if (SR.returnCurrentShipName().Equals("Mecha"))
        {
            Debug.Log("Mecha is active");
            GetComponent<RawImage>().texture = MechaMovie as MovieTexture;
            PlayMovie(MechaMovie);
            //MechaMovie.Play();
        }
        else if (SR.returnCurrentShipName().Equals("Jupiter"))
        {
            Debug.Log("Jupiter is active");
            GetComponent<RawImage>().texture = JupiterMovie as MovieTexture;
            PlayMovie(JupiterMovie);
            //JupiterMovie.Play();

        }
        else if (SR.returnCurrentShipName().Equals("SoulEater"))
        {
            Debug.Log("SoulEater is active");
            GetComponent<RawImage>().texture = SoulMovie as MovieTexture;
            PlayMovie(SoulMovie);
            //SoulMovie.Play();

        }
        else if (SR.returnCurrentShipName().Equals("Clank R7"))
        {
            Debug.Log("Clank is Active");
            GetComponent<RawImage>().texture = ClankMovie as MovieTexture;
            PlayMovie(ClankMovie);
            //SoulMovie.Play();

        }



    }

    



}
