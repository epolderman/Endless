using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

    public float maxspeed;  //for asteroid speed
    public float minspeed;
	public float minimum_Angle;
	public float maximum_Angle;	

	private Rigidbody2D rigid_Body_2D;

	// Use this for initialization
	void Start () {
		this.rigid_Body_2D = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        //get the asteroid position
//        Vector2 position = transform.position;
			
        //compute the asteroid position
//        position = new Vector2(position.x - Random.Range(minspeed, maxspeed) * Time.deltaTime, position.y);

        //update asteroid position
//        transform.position = position;

        //this is the bottom-left point of the screen
		Vector2 min = new Vector2(-1300, -1200);
		Vector2 max = new Vector2(2260, 1200);

        //if the asteroid goes outside the map then destroy it
        
		if(transform.position.x < min.x)
        {
//			print ("Left Bound Delete");
			if(PhotonNetwork.player == this.GetComponent<PhotonView>().owner)
			{
				PhotonNetwork.Destroy(gameObject);
			}
        }
		if(transform.position.x > max.x)
		{
//			print ("Right Bound Delete");
			if(PhotonNetwork.player == this.GetComponent<PhotonView>().owner)
			{
				PhotonNetwork.Destroy(gameObject);
			}
		}
		if(transform.position.y < min.y)
		{
//			print ("Lower Bound Delete");
			if(PhotonNetwork.player == this.GetComponent<PhotonView>().owner)
			{
				PhotonNetwork.Destroy(gameObject);
			}
		}
		if(transform.position.y > max.y)
		{
//			print ("Upper Bound Delete");
			if(PhotonNetwork.player == this.GetComponent<PhotonView>().owner)
			{
				PhotonNetwork.Destroy(gameObject);
			}
		}


	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isReading == true)
		{
			if(this.rigid_Body_2D != null)
			{
				if((this.transform.position - (Vector3)this.rigid_Body_2D.velocity * PhotonNetwork.GetPing() * 0.001f).sqrMagnitude > 100.0f)	//If the asteroids's current position is 10 or more meters away from the predicted networked position, update the position.
				{
					this.transform.position = this.transform.position + (Vector3)this.rigid_Body_2D.velocity * PhotonNetwork.GetPing() * 0.001f;
				}
			}
		}
	}

}
