using UnityEngine;

public class Ship_Seeking_Missile : MonoBehaviour
{
	Transform ship_Transform_Locked_On = null;
	PhotonView projectile_Photon_View;
	Rigidbody2D rigid_Body_2D;

	void Start()
	{
		this.projectile_Photon_View = this.GetComponentInParent<PhotonView>();
		this.rigid_Body_2D = this.GetComponentInParent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
 		if(this.ship_Transform_Locked_On != null)
		{
			this.transform.parent.rotation = Quaternion.Slerp(this.transform.parent.rotation, Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2((this.ship_Transform_Locked_On.transform.position - this.transform.parent.position).y, (this.ship_Transform_Locked_On.transform.position - this.transform.parent.position).x) * Mathf.Rad2Deg - 90.0f), 0.1f);
			this.rigid_Body_2D.velocity = this.transform.parent.up * Mathf.Max(this.rigid_Body_2D.velocity.magnitude, 150.0f);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(this.ship_Transform_Locked_On == null)
		{
			if(other.gameObject.tag == "Ship" && this.projectile_Photon_View.owner != null && this.projectile_Photon_View.owner != other.gameObject.GetComponentInParent<PhotonView>().owner && ((Health_Statistics.Team)this.projectile_Photon_View.owner.customProperties["Team"] == Health_Statistics.Team.None || (Health_Statistics.Team)other.gameObject.GetComponentInParent<PhotonView>().owner.customProperties["Team"] != (Health_Statistics.Team)this.projectile_Photon_View.owner.customProperties["Team"]))
			{
				this.ship_Transform_Locked_On = other.gameObject.transform;
			}
		}
	}
}
