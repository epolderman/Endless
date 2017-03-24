using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float damage;
//	public float initial_Velocity;
	public float duration;

//	[HideInInspector]
//	public Ship_Controls owner = null;
	[HideInInspector]
	public bool local_Projectile = false;

	private void Start()
	{
		Gravity.affected_Rigid_Bodies_List.Add(this.gameObject.GetComponent<Rigidbody2D>());
//		if(this.owner.GetComponent<PhotonView>().owner != PhotonNetwork.player)
//		{
//			this.transform.position = this.transform.position + new Vector3((this.GetComponent<Rigidbody2D>().velocity.x + this.owner.GetComponent<Rigidbody2D>().velocity.x) * PhotonNetwork.GetPing(), (this.GetComponent<Rigidbody2D>().velocity.y + this.owner.GetComponent<Rigidbody2D>().velocity.y) * PhotonNetwork.GetPing(), 0.0f);
//		}
		this.Invoke("clear_Photon_View", 0.2f);
		this.Invoke("destroy", this.duration);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(this.GetComponent<SpriteRenderer>().enabled == true)
		{
			GameObject audio_Container = new GameObject();
			AudioSource original_Audio_Source_Properties = this.GetComponent<AudioSource>();
			AudioSource temporary_Audio_Source_Properties = audio_Container.AddComponent<AudioSource>();
			temporary_Audio_Source_Properties.clip = original_Audio_Source_Properties.clip;
			temporary_Audio_Source_Properties.volume = original_Audio_Source_Properties.volume;
			temporary_Audio_Source_Properties.pitch = original_Audio_Source_Properties.pitch;
			temporary_Audio_Source_Properties.spatialBlend = original_Audio_Source_Properties.spatialBlend;
			temporary_Audio_Source_Properties.maxDistance = original_Audio_Source_Properties.maxDistance;
			temporary_Audio_Source_Properties.rolloffMode = original_Audio_Source_Properties.rolloffMode;
			temporary_Audio_Source_Properties.Play();
			Object.Destroy(audio_Container, temporary_Audio_Source_Properties.clip.length);
			this.Invoke("destroy", 0.2f);
			this.GetComponent<SpriteRenderer>().enabled = false;
			if(this.GetComponent<PolygonCollider2D>() != null)
			{
				this.GetComponent<PolygonCollider2D>().enabled = false;
			}
			else if(this.GetComponent<CircleCollider2D>() != null)
			{
				this.GetComponent<CircleCollider2D>().enabled = false;
			}
			for(int i = 0; i < this.transform.childCount; i++)	//Disables all children of the object to clear up particle effects
			{
				this.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	private void destroy()
	{
		if(this.local_Projectile == true)
		{
			PhotonNetwork.Destroy(this.gameObject);
		}
	}

	private void clear_Photon_View()
	{
		this.GetComponent<PhotonView>().ObservedComponents.Clear();
	}
}
