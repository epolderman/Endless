using UnityEngine;
using System.Collections;

public class Health_Statistics : MonoBehaviour
{
	public enum Team
	{
		None,
		Red,
		Blue
	}

    [Header("Damage Modifiers")]
    public float hull_Damage_Resistance;

	[Header("Health Settings")]
	public float maximum_Hull_Integrity;
	public float hull_Integrity;

	[Header("On-hit particle and sound effects")]
	public GameObject[] on_Hull_Hit_Effects;
	public GameObject[] on_Hull_Minor_Damage_Effects;
	public GameObject[] on_Hull_Major_Damage_Effects;

	private ParticleSystem[] on_Hull_Hit_Particle_Systems;
	private ParticleSystem.EmissionModule[] on_Hull_Hit_Emissions;
	private AudioSource[] on_Hull_Hit_Audio_Sources;

	private ParticleSystem[] on_Hull_Minor_Damage_Particle_Systems;
	private ParticleSystem.EmissionModule[] on_Hull_Minor_Damage_Emissions;
	private AudioSource[] on_Hull_Minor_Damage_Audio_Sources;
	private float[] on_Hull_Minor_Damage_Audio_Sources_Original_Volume;

	private ParticleSystem[] on_Hull_Major_Damage_Particle_Systems;
	private ParticleSystem.EmissionModule[] on_Hull_Major_Damage_Emissions;
	private AudioSource[] on_Hull_Major_Damage_Audio_Sources;
	private float[] on_Hull_Major_Damage_Audio_Sources_Original_Volume;


	[Header("Other")]
	public string derived_From_Prefab;	//Temporarily being used for ship respawning- ensure the string stored here refers to a valid prefabricated object. A menu for selecting your next ship will be used instead.
	public bool respawn_On_Death;
	public float respawn_Delay;
	public string death_Explosion_Name;
	[HideInInspector]
	public Health_Statistics.Team team = Health_Statistics.Team.None;
	[HideInInspector]
	public PhotonPlayer last_Damaged_By = null;
//	public string owner;
//	[HideInInspector]
//	public PhotonPlayer owner;

	[HideInInspector]
	public bool is_Alive = true;
    //	[HideInInspector]
    //	public float time_Of_Last_Death;

    public Shield_Statistics shieldStats;

	private void Start()
	{
		if(this.gameObject.tag == "Ship" && this.GetComponent<Ship_Controls>().player_Controlled == true)
		{
			this.GetComponent<Ship_Controls>().main_hud.healthBar.value = this.hull_Integrity;
		}
		this.on_Hull_Hit_Particle_Systems = Ship_Controls.get_Valid_Particle_Systems_Array(this.on_Hull_Hit_Effects);
		this.on_Hull_Hit_Emissions = new ParticleSystem.EmissionModule[this.on_Hull_Hit_Particle_Systems.Length];
		for(int i = 0; i < this.on_Hull_Hit_Emissions.Length; i++)
		{
			this.on_Hull_Hit_Emissions[i] = this.on_Hull_Hit_Particle_Systems[i].emission;
		}
		this.on_Hull_Hit_Audio_Sources = Ship_Controls.get_Valid_Audio_Sources_Array(this.on_Hull_Hit_Effects);


		this.on_Hull_Minor_Damage_Particle_Systems = Ship_Controls.get_Valid_Particle_Systems_Array(this.on_Hull_Minor_Damage_Effects);
		this.on_Hull_Minor_Damage_Emissions = new ParticleSystem.EmissionModule[this.on_Hull_Minor_Damage_Particle_Systems.Length];
		for(int i = 0; i < this.on_Hull_Minor_Damage_Emissions.Length; i++)
		{
			this.on_Hull_Minor_Damage_Emissions[i] = this.on_Hull_Minor_Damage_Particle_Systems[i].emission;
		}
		this.on_Hull_Minor_Damage_Audio_Sources = Ship_Controls.get_Valid_Audio_Sources_Array(this.on_Hull_Minor_Damage_Effects);
		this.on_Hull_Minor_Damage_Audio_Sources_Original_Volume = new float[this.on_Hull_Minor_Damage_Audio_Sources.Length];
		for(int i = 0; i < this.on_Hull_Minor_Damage_Audio_Sources_Original_Volume.Length; i++)
		{
			this.on_Hull_Minor_Damage_Audio_Sources_Original_Volume[i] = this.on_Hull_Minor_Damage_Audio_Sources[i].volume;
			this.on_Hull_Minor_Damage_Audio_Sources[i].volume = 0.0f;
		}

		this.on_Hull_Major_Damage_Particle_Systems = Ship_Controls.get_Valid_Particle_Systems_Array(this.on_Hull_Major_Damage_Effects);
		this.on_Hull_Major_Damage_Emissions = new ParticleSystem.EmissionModule[this.on_Hull_Major_Damage_Particle_Systems.Length];
		for(int i = 0; i < this.on_Hull_Major_Damage_Emissions.Length; i++)
		{
			this.on_Hull_Major_Damage_Emissions[i] = this.on_Hull_Major_Damage_Particle_Systems[i].emission;
		}
		this.on_Hull_Major_Damage_Audio_Sources = Ship_Controls.get_Valid_Audio_Sources_Array(this.on_Hull_Major_Damage_Effects);
		this.on_Hull_Major_Damage_Audio_Sources_Original_Volume = new float[this.on_Hull_Major_Damage_Audio_Sources.Length];
		for(int i = 0; i < this.on_Hull_Major_Damage_Audio_Sources_Original_Volume.Length; i++)
		{
			this.on_Hull_Major_Damage_Audio_Sources_Original_Volume[i] = this.on_Hull_Major_Damage_Audio_Sources[i].volume;
			this.on_Hull_Major_Damage_Audio_Sources[i].volume = 0.0f;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(shieldStats != null)
		{
	        shieldStats.regenActive = false;
//			print("Shield Strength = " + shieldStats.shield_Strength);
		}
//        print(this.derived_From_Prefab.ToString() + " Hull Strength = " + this.hull_Integrity);

        if (shieldStats == null || shieldStats.shield_Strength == 0 || collision.gameObject.tag == "Bullet")
        {
			for(int i = 0; i < this.on_Hull_Hit_Emissions.Length; i++)
			{
				this.on_Hull_Hit_Particle_Systems[i].transform.position = collision.contacts[0].point;
				this.on_Hull_Hit_Emissions[i].enabled = true;
				this.Invoke("disable_Burst_Emissions", 0.1f);
            }
			for(int i = 0; i < this.on_Hull_Hit_Audio_Sources.Length; i++)
			{
				this.on_Hull_Hit_Audio_Sources[i].Play();
			}
            if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Plasma" && collision.gameObject.tag != "Explosive")
            {
				if(collision.gameObject.tag == "Ship")
				{
					PhotonPlayer ship_Owner = collision.gameObject.GetComponent<PhotonView>().owner;
					if(ship_Owner != null && (this.team == Health_Statistics.Team.None || (Health_Statistics.Team)ship_Owner.customProperties["Team"] != this.team))
					{
						this.last_Damaged_By = ship_Owner;
					}
				}
                float relative_Velocity_Magnitude = collision.relativeVelocity.magnitude;
                float inverse_Relative_Mass = 1.0f;     //Used to reduce damage on heavy objects, but is capped at 1 to avoid dealing extra damage to the lighter object. If no rigid body is present in either object, a value of 1 is used.

                if (collision.gameObject.GetComponentInParent<Rigidbody2D>() != null && this.gameObject.GetComponentInParent<Rigidbody2D>() != null)
                {
                    inverse_Relative_Mass = Mathf.Min(collision.gameObject.GetComponentInParent<Rigidbody2D>().mass / this.gameObject.GetComponentInParent<Rigidbody2D>().mass, 1.0f);
                }

                this.hull_Integrity = this.hull_Integrity - relative_Velocity_Magnitude * inverse_Relative_Mass * 0.25f;

				if(this.gameObject.tag == "Ship" && this.GetComponent<Ship_Controls>().player_Controlled == true)
				{
					this.GetComponent<Ship_Controls>().main_hud.healthBar.value = this.hull_Integrity;
				}

                if (this.hull_Integrity <= 0)
                {
                    this.handle_Death();
                }
				else if(this.hull_Integrity / this.maximum_Hull_Integrity <= 0.33333f)
				{
					for(int i = 0; i < this.on_Hull_Major_Damage_Emissions.Length; i++)
					{
						this.on_Hull_Major_Damage_Emissions[i].enabled = true;
                    }
					for(int i = 0; i < this.on_Hull_Major_Damage_Audio_Sources.Length; i++)
					{
						this.on_Hull_Major_Damage_Audio_Sources[i].volume = this.on_Hull_Major_Damage_Audio_Sources_Original_Volume[i];
					}
					for(int i = 0; i < this.on_Hull_Minor_Damage_Emissions.Length; i++)
					{
						this.on_Hull_Minor_Damage_Emissions[i].enabled = false;
                    }
					for(int i = 0; i < this.on_Hull_Minor_Damage_Audio_Sources.Length; i++)
					{
						this.on_Hull_Minor_Damage_Audio_Sources[i].volume = 0.0f;
					}
				}
				else if(this.hull_Integrity / this.maximum_Hull_Integrity <= 0.66667f)
				{
					for(int i = 0; i < this.on_Hull_Major_Damage_Emissions.Length; i++)
					{
						this.on_Hull_Major_Damage_Emissions[i].enabled = false;
                    }
					for(int i = 0; i < this.on_Hull_Major_Damage_Audio_Sources.Length; i++)
					{
						this.on_Hull_Major_Damage_Audio_Sources[i].volume = 0.0f;
					}
					for(int i = 0; i < this.on_Hull_Minor_Damage_Emissions.Length; i++)
					{
						this.on_Hull_Minor_Damage_Emissions[i].enabled = true;
                    }
					for(int i = 0; i < this.on_Hull_Minor_Damage_Audio_Sources.Length; i++)
					{
						this.on_Hull_Minor_Damage_Audio_Sources[i].volume = this.on_Hull_Minor_Damage_Audio_Sources_Original_Volume[i];
					}
				}
				else
				{
					for(int i = 0; i < this.on_Hull_Major_Damage_Emissions.Length; i++)
					{
						this.on_Hull_Major_Damage_Emissions[i].enabled = false;
                    }
					for(int i = 0; i < this.on_Hull_Major_Damage_Audio_Sources.Length; i++)
					{
						this.on_Hull_Major_Damage_Audio_Sources[i].volume = 0;
					}
					for(int i = 0; i < this.on_Hull_Minor_Damage_Emissions.Length; i++)
					{
						this.on_Hull_Minor_Damage_Emissions[i].enabled = false;
                    }
					for(int i = 0; i < this.on_Hull_Minor_Damage_Audio_Sources.Length; i++)
					{
						this.on_Hull_Minor_Damage_Audio_Sources[i].volume = 0.0f;
					}
				}
            }
            else
            {
                Projectile projectile = collision.gameObject.GetComponent<Projectile>();
				PhotonPlayer projectile_Owner = projectile.GetComponent<PhotonView>().owner;
				if(this.team == Health_Statistics.Team.None || (Health_Statistics.Team)projectile_Owner.customProperties["Team"] != this.team)
				{
					if(projectile_Owner != null && projectile_Owner != PhotonNetwork.player)
					{
						this.last_Damaged_By = projectile_Owner;
					}

					float hull_Damage_Suffered = projectile.damage;
                    if (projectile.tag == "Bullet" && shieldStats != null && shieldStats.shield_Strength > 0)
                        hull_Damage_Suffered *= .25f;

					this.hull_Integrity = this.hull_Integrity - (hull_Damage_Suffered - hull_Damage_Suffered * hull_Damage_Resistance);

					if(this.gameObject.tag == "Ship" && this.GetComponent<Ship_Controls>().player_Controlled == true)
					{
						this.GetComponent<Ship_Controls>().main_hud.healthBar.value = this.hull_Integrity;
					}

					if(this.hull_Integrity <= 0)
					{
						this.handle_Death();
					}
				}
            }
        }
	}

	public void handle_Death()
	{
		if (this.is_Alive == true)
        {
			this.is_Alive = false;
			if(this.death_Explosion_Name != "" && (PhotonNetwork.player == this.gameObject.GetComponent<PhotonView>().owner))
			{
				PhotonNetwork.Instantiate(this.death_Explosion_Name, this.transform.position, this.transform.rotation, 0);
			}
			if (this.respawn_On_Death == true && (PhotonNetwork.player == this.gameObject.GetComponent<PhotonView>().owner))
			{
				Ship_Generator ship_Generator = ((GameObject)PhotonNetwork.Instantiate("Ship Generator", Vector3.zero, Quaternion.identity, 0)).GetComponent<Ship_Generator>();
				ship_Generator.ship_To_Instantiate = this.derived_From_Prefab;
				ship_Generator.time_To_Generate_Ship = Time.fixedTime + this.respawn_Delay;
				ship_Generator.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player);
				Scene_Manager.deaths["Deaths"] = (int)PhotonNetwork.player.customProperties["Deaths"] + 1;
//				Scene_Manager.scores["Kills"] = (int)PhotonNetwork.player.customProperties["Kills"];
				PhotonNetwork.player.SetCustomProperties(Scene_Manager.deaths);
				if(this.last_Damaged_By != null && this.last_Damaged_By != PhotonNetwork.player)
				{
//					ExitGames.Client.Photon.Hashtable enemy_Score = new ExitGames.Client.Photon.Hashtable();
//					enemy_Score["Deaths"] = (int)this.last_Damaged_By.customProperties["Deaths"];
					Scene_Manager.kills["Kills"] = (int)this.last_Damaged_By.customProperties["Kills"] + 1;
					this.last_Damaged_By.SetCustomProperties(Scene_Manager.kills);
					if((Health_Statistics.Team)this.last_Damaged_By.customProperties["Team"] == Health_Statistics.Team.Red)
					{
//						ExitGames.Client.Photon.Hashtable enemy_Team_Score = new ExitGames.Client.Photon.Hashtable();
						Scene_Manager.red_Team_Score["Red"] = (int)PhotonNetwork.room.customProperties["Red"] + 1;
						PhotonNetwork.room.SetCustomProperties(Scene_Manager.red_Team_Score);
					}
					else if((Health_Statistics.Team)this.last_Damaged_By.customProperties["Team"] == Health_Statistics.Team.Blue)
					{
//						ExitGames.Client.Photon.Hashtable enemy_Team_Score = new ExitGames.Client.Photon.Hashtable();
						Scene_Manager.blue_Team_Score["Blue"] = (int)PhotonNetwork.room.customProperties["Blue"] + 1;
						PhotonNetwork.room.SetCustomProperties(Scene_Manager.blue_Team_Score);
					}

				}
			}
			if(this.GetComponent<PhotonView>().owner == PhotonNetwork.player)
			{
				if(this.gameObject.tag == "Ship")
				{
					this.hull_Integrity = 0.0f;
					this.shieldStats.shield_Strength = 0.0f;
					this.GetComponent<Ship_Controls>().main_hud.healthBar.value = this.hull_Integrity;
					this.GetComponent<Ship_Controls>().main_hud.sheildBar.value = this.shieldStats.shield_Strength;
				}
				PhotonNetwork.Destroy(this.gameObject);
			}
		}
	}

	private void disable_Burst_Emissions()
	{
		for(int i = 0; i < this.on_Hull_Hit_Emissions.Length; i++)
		{
			this.on_Hull_Hit_Emissions[i].enabled = false;
        }
	}
}
