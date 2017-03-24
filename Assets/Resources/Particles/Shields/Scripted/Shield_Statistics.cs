using UnityEngine;

public class Shield_Statistics : MonoBehaviour {

    [Header("Damage Modifiers")]
    public float shield_Damage_Resistance;
    private float projectile_Damage_Modifier;

    [Header("Shield Settings")]
    public float maximum_Shield_Strength;
    public float shield_Strength;
    public float shield_Regeneration_Rate;

    //[Header("On-hit particle and sound effects")]
    //public GameObject[] shield_Active_Effects;
    //public GameObject[] on_Shield_Regenerating_Effects;
    //public GameObject[] on_Shield_Reached_Max_Effects;
    //public GameObject[] on_Shield_Hit_Effects;
    //public GameObject[] on_Shield_Depleted_Effects;
    //public GameObject[] on_Shield_Beginning_Regeneration_Effects;

    [Header("Time handlers")]
    public float shield_Regeneration_Delay;
    private float shield_Regen_Start_Time;

    public bool regenActive = true;
	[HideInInspector]
	public Health_Statistics.Team team = Health_Statistics.Team.None;
	private Health_Statistics health_Statistics;

	private void Start()
	{
		this.health_Statistics = this.GetComponent<Health_Statistics>();
		if(this.gameObject.tag == "Ship" && this.GetComponent<Ship_Controls>().player_Controlled == true)
		{
			this.GetComponent<Ship_Controls>().main_hud.sheildBar.value = this.shield_Strength;
		}
	}

    private void FixedUpdate()
    {
        if (regenActive)
        {
            this.shield_Strength = Mathf.Min(this.shield_Strength + this.shield_Regeneration_Rate * Time.fixedDeltaTime, this.maximum_Shield_Strength);
			if(this.gameObject.tag == "Ship" && this.GetComponent<Ship_Controls>().player_Controlled == true)
			{
				this.GetComponent<Ship_Controls>().main_hud.sheildBar.value = this.shield_Strength;
			}
        }

        else if (!regenActive)
        {
            if (Time.fixedTime >= shield_Regen_Start_Time)
            {
                //print(shield_Regen_Start_Time);
                //print(Time.fixedTime);
                regenActive = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Plasma" && collision.gameObject.tag != "Explosive")
        {
			if(collision.gameObject.tag == "Ship")
			{
				PhotonPlayer ship_Owner = collision.gameObject.GetComponent<PhotonView>().owner;
				if(ship_Owner != null && (this.team == Health_Statistics.Team.None || (Health_Statistics.Team)ship_Owner.customProperties["Team"] != this.team))
				{
					if(this.health_Statistics != null)
					{
						this.health_Statistics.last_Damaged_By = ship_Owner;
					}
				}
			}
            float relative_Velocity_Magnitude = collision.relativeVelocity.magnitude;
            float inverse_Relative_Mass = 1.0f;     //Used to reduce damage on heavy objects, but is capped at 1 to avoid dealing extra damage to the lighter object. If no rigid body is present in either object, a value of 1 is used.

            if (collision.gameObject.GetComponentInParent<Rigidbody2D>() != null && this.gameObject.GetComponentInParent<Rigidbody2D>() != null)
            {
                inverse_Relative_Mass = Mathf.Min(collision.gameObject.GetComponentInParent<Rigidbody2D>().mass / this.gameObject.GetComponentInParent<Rigidbody2D>().mass, 1.0f);
            }

            this.shield_Strength = this.shield_Strength - relative_Velocity_Magnitude * inverse_Relative_Mass * 0.25f;

            if(this.shield_Strength < 0)
            {
                this.shield_Strength = 0;
            }
			if(this.gameObject.tag == "Ship" && this.GetComponent<Ship_Controls>().player_Controlled == true)
			{
				this.GetComponent<Ship_Controls>().main_hud.sheildBar.value = this.shield_Strength;
			}
			shield_Regen_Start_Time = Time.fixedTime + shield_Regeneration_Delay;

			regenActive = false;
        }
        else
        {
            Projectile projectile = collision.gameObject.GetComponent<Projectile>();
			PhotonPlayer projectile_Owner = projectile.GetComponent<PhotonView>().owner;
			if(this.team == Health_Statistics.Team.None || (Health_Statistics.Team)projectile_Owner.customProperties["Team"] != this.team)
			{
				if(projectile_Owner != null && projectile_Owner != PhotonNetwork.player)
				{
					if(this.health_Statistics != null)
					{
						this.health_Statistics.last_Damaged_By = projectile_Owner;
					}
				}
				if (projectile.tag == "Plasma")
				{
					this.projectile_Damage_Modifier = 1f;
				}
				else if (projectile.tag == "Explosive")
				{
					this.projectile_Damage_Modifier = -.75f;
				}

				if (projectile.tag != "Bullet")
				{
					this.shield_Strength = this.shield_Strength - (projectile.damage - projectile.damage * this.shield_Damage_Resistance + projectile.damage * this.projectile_Damage_Modifier);
					if(this.shield_Strength < 0)
					{
						this.shield_Strength = 0;
					}
					if(this.gameObject.tag == "Ship" && this.GetComponent<Ship_Controls>().player_Controlled == true)
					{
						this.GetComponent<Ship_Controls>().main_hud.sheildBar.value = this.shield_Strength;
					}
					this.projectile_Damage_Modifier = 0;
				}
				shield_Regen_Start_Time = Time.fixedTime + shield_Regeneration_Delay;

				regenActive = false;
			}
        }
    }
}
