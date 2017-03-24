using UnityEngine;

[System.Serializable]
public struct Projectile_Settings
{
	[Tooltip("The name of the GameObject that will be instantiated on the corresponding fire action.")]
	public string projectile;
	[Tooltip("The number of projectiles that will be produced on each fire action.")]
	public int projectiles_Per_Burst;
	[Tooltip("The amount of time that the ship will be unable to execute the corresponding fire action in seconds.")]
	public float projectile_Burst_Delay;
	[Tooltip("The initial forward velocity of the projectiles instantiated from the fire action.")]
	public float projectile_Initial_Velocity;
	[Tooltip("The maximum amount of time in seconds the corresponding projectile should last after firing.")]
	public float projectile_Duration;
	[Tooltip("Whether or not to attach the created projectile to the ship's transform hierarchy and remove the RigidBody2D from the projectile after creation.")]
	public bool projectile_Use_Ship_Transform;
	[Tooltip("The areas where projectiles will be instantiated at along with any Particle Systems and Audio Sources that should be activated for each location's successful fire action.")]
	public GameObject[] projectile_Locations_And_Effects;

	[HideInInspector]
	public int projectile_Sequence_Number;
	[HideInInspector]
	public float time_Of_Last_Burst_Fired;
}

//[System.Serializable]
//public struct Thruster_Settings
//{
//	[Tooltip("The rate at which the corresponding movement action should accelerate the ship in the corresponding direction, measured in Meters per Second Squared for directional actions and is not applied for rotational actions ([Newton Meters]* for rotational actions).")]	//Verify that the Torque forces are actually Newton Meters, maybe convert to degrees per second squared if not equivalent?
//	public float rate_Of_Acceleration;
//	[Tooltip("The velocity at which acceleration will no longer be applied- a ship may exceed this velocity by outside forces and from the acceleration applied just prior to exceeding the velocity. To limit the directional velocity from any forces, change the field \"Maximum_Ship_Velocity\" found as the first item under the Thruster Settings, and for rotational velocity, change the field \"Maximum_Ship_Rotational_Velocity\" found as the second item under the Thruster Settings; even these velocities may still be exceeded slightly for a Physics frame while a force acts on the ship. The units of measurement for this velocity are Meters per Second for directional actions and Degrees per Second for rotational actions.")]
//	public float maximum_Velocity;
//	[Tooltip("The areas where the corresponding movement action should activate Particle Systems and Audio Sources. Forces are applied independent of these locations and are instead based on the ship's direction.")]
//	public GameObject[] thruster_Locations_And_Effects;

//	[HideInInspector]
//	public float maximum_Velocity_Squared;
//}

//[System.Serializable]
//public struct Rotational_Thruster_Settings
//{
//	public float rotational_Velocity;
//	public GameObject[] thruster_Locations_And_Effects;
//}

public class Ship_Controls : MonoBehaviour
{
//	public static readonly Vector3 zero_Vector3 = new Vector3(0.0f, 0.0f, 0.0f);
//	public static readonly Quaternion zero_Quaternion = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
//	public static readonly Vector3 respawn_Location = new Vector3(1300.0f, 0.0f, 0.0f);
	public static System.Collections.Generic.List<Ship_Controls> active_Ships_List;

	[Header("Projectile Settings")]
	public Projectile_Settings primary_Projectile_Settings;
	public Projectile_Settings secondary_Projectile_Settings;
    //	public Projectile_Settings special_Projectile_Settings;
    public bool fireDouble;

	[Header("Thruster Settings")]
	public float maximum_Ship_Velocity;
	public float rate_Of_Acceleration;
	public float maximum_Rotational_Velocity;
    //	public float maximum_Ship_Rotational_Velocity;
    public GameObject[] forward_Thrusters;
//	public Thruster_Settings backward_Thrusters;
//	public Thruster_Settings leftward_Thrusters;
//	public Thruster_Settings rightward_Thrusters;
	public GameObject[] left_Rotational_Thrusters;
	public GameObject[] right_Rotational_Thrusters;

	[Header("Other")]
//	[HideInInspector]
//	public Camera player_Camera;
	public Transform center_Of_Mass;
	public bool player_Controlled;

    [HideInInspector]
	public Health_Statistics health_Statistics;

	private Rigidbody2D rigid_Body_2D;

	private ParticleSystem[] primary_Projectile_Particle_Systems;
	private ParticleSystem.EmissionModule[] primary_Projectile_Emissions;
	private AudioSource[] primary_Projectile_Audio_Sources;
//	private float[] primary_Projectile_Audio_Sources_Original_Volume;

	private ParticleSystem[] secondary_Projectile_Particle_Systems;
	private ParticleSystem.EmissionModule[] secondary_Projectile_Emissions;
	private AudioSource[] secondary_Projectile_Audio_Sources;
//	private float[] secondary_Projectile_Audio_Sources_Original_Volume;

//	private ParticleSystem[] special_Projectile_Particle_Systems;
//	private ParticleSystem.EmissionModule[] special_Projectile_Emissions;
//	private AudioSource[] special_Projectile_Audio_Sources;

	private ParticleSystem[] forward_Thruster_Particle_Systems;
	private ParticleSystem.EmissionModule[] forward_Thruster_Emissions;
	private AudioSource[] forward_Thruster_Audio_Sources;
	private float[] forward_Thruster_Audio_Sources_Original_Volume;

//	private ParticleSystem[] backward_Thruster_Particle_Systems;
//	private ParticleSystem.EmissionModule[] backward_Thruster_Emissions;
//	private AudioSource[] backward_Thruster_Audio_Sources;

//	private ParticleSystem[] leftward_Thruster_Particle_Systems;
//	private ParticleSystem.EmissionModule[] leftward_Thruster_Emissions;
//	private AudioSource[] leftward_Thruster_Audio_Sources;

//	private ParticleSystem[] rightward_Thruster_Particle_Systems;
//	private ParticleSystem.EmissionModule[] rightward_Thruster_Emissions;
//	private AudioSource[] rightward_Thruster_Audio_Sources;

	private ParticleSystem[] left_Rotational_Thruster_Particle_Systems;
	private ParticleSystem.EmissionModule[] left_Rotational_Thruster_Emissions;
	private AudioSource[] left_Rotational_Thruster_Audio_Sources;
	private float[] left_Rotational_Thruster_Audio_Sources_Original_Volume;

	private ParticleSystem[] right_Rotational_Thruster_Particle_Systems;
	private ParticleSystem.EmissionModule[] right_Rotational_Thruster_Emissions;
	private AudioSource[] right_Rotational_Thruster_Audio_Sources;
	private float[] right_Rotational_Thruster_Audio_Sources_Original_Volume;

	private GameManager game_Manager;
    public MainHUD main_hud;
    
    

    //May need to change to public and just hide in editor so changes in maximum velocity can be updated or add a setter tied to Velocity and Velocity_Squared
    [HideInInspector]
    public float maximum_Ship_Velocity_Squared;

	private bool is_Boosting = false;
	private bool is_Thrusting = false;

    public float next_Fire_Available;
    public bool canFireBothWeapons;
    public float next_Primary_Available, next_Secondary_Available;

	static Ship_Controls()
	{
		Ship_Controls.active_Ships_List = new System.Collections.Generic.List<Ship_Controls>();
	}

	private void Start()
	{
		Ship_Controls.active_Ships_List.Add(this);
		this.health_Statistics = this.GetComponent<Health_Statistics>();
		this.rigid_Body_2D = this.GetComponent<Rigidbody2D>();

		if(this.center_Of_Mass != null)
		{
			this.rigid_Body_2D.centerOfMass = this.center_Of_Mass.localPosition;
		}
		
		if(this.gameObject.activeInHierarchy == true)		//Avoids adding prefabricated ships into the Gravity list due to OnValidate's behavior
		{
			Gravity.affected_Rigid_Bodies_List.RemoveAll(element => element == this.rigid_Body_2D);	//While editing in Unity, a Start() can be called multiple times due to OnValidate's behavior- this ensures Gravity will only be applied once for each object
			Gravity.affected_Rigid_Bodies_List.Add(this.rigid_Body_2D);
		}

		this.primary_Projectile_Settings.time_Of_Last_Burst_Fired = -this.primary_Projectile_Settings.projectile_Burst_Delay;
		this.secondary_Projectile_Settings.time_Of_Last_Burst_Fired = -this.secondary_Projectile_Settings.projectile_Burst_Delay;
        next_Fire_Available = 0;
        next_Primary_Available = 0;
        next_Secondary_Available = 0;

		//Particle Systems and Audio Sources may change to being retrieved from GetComponentsInChildren at a later date- more research and testing is required.
		this.primary_Projectile_Particle_Systems = get_Valid_Particle_Systems_Array(this.primary_Projectile_Settings.projectile_Locations_And_Effects);
		this.primary_Projectile_Emissions = new ParticleSystem.EmissionModule[this.primary_Projectile_Particle_Systems.Length];
		for(int i = 0; i < this.primary_Projectile_Emissions.Length; i++)
		{
			this.primary_Projectile_Emissions[i] = this.primary_Projectile_Particle_Systems[i].emission;
		}
		this.primary_Projectile_Audio_Sources = get_Valid_Audio_Sources_Array(this.primary_Projectile_Settings.projectile_Locations_And_Effects);

		this.secondary_Projectile_Particle_Systems = get_Valid_Particle_Systems_Array(this.secondary_Projectile_Settings.projectile_Locations_And_Effects);
		this.secondary_Projectile_Emissions = new ParticleSystem.EmissionModule[this.secondary_Projectile_Particle_Systems.Length];
		for(int i = 0; i < this.secondary_Projectile_Emissions.Length; i++)
		{
			this.secondary_Projectile_Emissions[i] = this.secondary_Projectile_Particle_Systems[i].emission;
		}
		this.secondary_Projectile_Audio_Sources = get_Valid_Audio_Sources_Array(this.secondary_Projectile_Settings.projectile_Locations_And_Effects);

//		this.special_Projectile_Particle_Systems = get_Valid_Particle_Systems_Array(this.special_Projectile_Settings.projectile_Locations_And_Effects);
//		this.special_Projectile_Emissions = new ParticleSystem.EmissionModule[this.special_Projectile_Particle_Systems.Length];
//		for(int i = 0; i < this.special_Projectile_Emissions.Length; i++)
//		{
//			this.special_Projectile_Emissions[i] = this.special_Projectile_Particle_Systems[i].emission;
//		}
//		this.special_Projectile_Audio_Sources = get_Valid_Audio_Sources_Array(this.special_Projectile_Settings.projectile_Locations_And_Effects);

		this.forward_Thruster_Particle_Systems = get_Valid_Particle_Systems_Array(this.forward_Thrusters);
		this.forward_Thruster_Emissions = new ParticleSystem.EmissionModule[this.forward_Thruster_Particle_Systems.Length];
		for(int i = 0; i < this.forward_Thruster_Emissions.Length; i++)
		{
			this.forward_Thruster_Emissions[i] = this.forward_Thruster_Particle_Systems[i].emission;
		}
		this.forward_Thruster_Audio_Sources = get_Valid_Audio_Sources_Array(this.forward_Thrusters);
		this.forward_Thruster_Audio_Sources_Original_Volume = new float[this.forward_Thruster_Audio_Sources.Length];
		for(int i = 0; i < this.forward_Thruster_Audio_Sources_Original_Volume.Length; i++)
		{
			this.forward_Thruster_Audio_Sources_Original_Volume[i] = this.forward_Thruster_Audio_Sources[i].volume;
		}

//		this.backward_Thruster_Particle_Systems = get_Valid_Particle_Systems_Array(this.backward_Thrusters.thruster_Locations_And_Effects);
//		this.backward_Thruster_Emissions = new ParticleSystem.EmissionModule[this.backward_Thruster_Particle_Systems.Length];
//		for(int i = 0; i < this.backward_Thruster_Emissions.Length; i++)
//		{
//			this.backward_Thruster_Emissions[i] = this.backward_Thruster_Particle_Systems[i].emission;
//		}
//		this.backward_Thruster_Audio_Sources = get_Valid_Audio_Sources_Array(this.backward_Thrusters.thruster_Locations_And_Effects);

//		this.leftward_Thruster_Particle_Systems = get_Valid_Particle_Systems_Array(this.leftward_Thrusters.thruster_Locations_And_Effects);
//		this.leftward_Thruster_Emissions = new ParticleSystem.EmissionModule[this.leftward_Thruster_Particle_Systems.Length];
//		for(int i = 0; i < this.leftward_Thruster_Emissions.Length; i++)
//		{
//			this.leftward_Thruster_Emissions[i] = this.leftward_Thruster_Particle_Systems[i].emission;
//		}
//		this.leftward_Thruster_Audio_Sources = get_Valid_Audio_Sources_Array(this.leftward_Thrusters.thruster_Locations_And_Effects);

//		this.rightward_Thruster_Particle_Systems = get_Valid_Particle_Systems_Array(this.rightward_Thrusters.thruster_Locations_And_Effects);
//		this.rightward_Thruster_Emissions = new ParticleSystem.EmissionModule[this.rightward_Thruster_Particle_Systems.Length];
//		for(int i = 0; i < this.rightward_Thruster_Emissions.Length; i++)
//		{
//			this.rightward_Thruster_Emissions[i] = this.rightward_Thruster_Particle_Systems[i].emission;
//		}
//		this.rightward_Thruster_Audio_Sources = get_Valid_Audio_Sources_Array(this.rightward_Thrusters.thruster_Locations_And_Effects);

		this.left_Rotational_Thruster_Particle_Systems = get_Valid_Particle_Systems_Array(this.left_Rotational_Thrusters);
		this.left_Rotational_Thruster_Emissions = new ParticleSystem.EmissionModule[this.left_Rotational_Thruster_Particle_Systems.Length];
		for(int i = 0; i < this.left_Rotational_Thruster_Emissions.Length; i++)
		{
			this.left_Rotational_Thruster_Emissions[i] = this.left_Rotational_Thruster_Particle_Systems[i].emission;
		}
		this.left_Rotational_Thruster_Audio_Sources = get_Valid_Audio_Sources_Array(this.left_Rotational_Thrusters);
		this.left_Rotational_Thruster_Audio_Sources_Original_Volume = new float[this.left_Rotational_Thruster_Audio_Sources.Length];
		for(int i = 0; i < this.left_Rotational_Thruster_Audio_Sources_Original_Volume.Length; i++)
		{
			this.left_Rotational_Thruster_Audio_Sources_Original_Volume[i] = this.left_Rotational_Thruster_Audio_Sources[i].volume;
		}

		this.right_Rotational_Thruster_Particle_Systems = get_Valid_Particle_Systems_Array(this.right_Rotational_Thrusters);
		this.right_Rotational_Thruster_Emissions = new ParticleSystem.EmissionModule[this.right_Rotational_Thruster_Particle_Systems.Length];
		for(int i = 0; i < this.right_Rotational_Thruster_Emissions.Length; i++)
		{
			this.right_Rotational_Thruster_Emissions[i] = this.right_Rotational_Thruster_Particle_Systems[i].emission;
		}
		this.right_Rotational_Thruster_Audio_Sources = get_Valid_Audio_Sources_Array(this.right_Rotational_Thrusters);
		this.right_Rotational_Thruster_Audio_Sources_Original_Volume = new float[this.right_Rotational_Thruster_Audio_Sources.Length];
		for(int i = 0; i < this.right_Rotational_Thruster_Audio_Sources_Original_Volume.Length; i++)
		{
			this.right_Rotational_Thruster_Audio_Sources_Original_Volume[i] = this.right_Rotational_Thruster_Audio_Sources[i].volume;
		}

//		this.forward_Thrusters.maximum_Velocity_Squared = this.forward_Thrusters.maximum_Velocity * this.forward_Thrusters.maximum_Velocity;
//		this.backward_Thrusters.maximum_Velocity_Squared = this.backward_Thrusters.maximum_Velocity * this.backward_Thrusters.maximum_Velocity;
//		this.leftward_Thrusters.maximum_Velocity_Squared = this.leftward_Thrusters.maximum_Velocity * this.leftward_Thrusters.maximum_Velocity;
//		this.rightward_Thrusters.maximum_Velocity_Squared = this.rightward_Thrusters.maximum_Velocity * this.rightward_Thrusters.maximum_Velocity;
		//this.left_Rotational_Thrusters.maximum_Velocity_Squared = this.left_Rotational_Thrusters.maximum_Velocity * this.left_Rotational_Thrusters.maximum_Velocity;
		//this.right_Rotational_Thrusters.maximum_Velocity_Squared = this.right_Rotational_Thrusters.maximum_Velocity * this.right_Rotational_Thrusters.maximum_Velocity;

		this.game_Manager = GameObject.Find("GameMenuManager").GetComponent<GameManager>();
        this.main_hud = GameObject.Find("MainHUDObject").GetComponent<MainHUD>();
       
		this.maximum_Ship_Velocity_Squared = this.maximum_Ship_Velocity * this.maximum_Ship_Velocity;

		this.health_Statistics.team = (Health_Statistics.Team)this.GetComponent<PhotonView>().owner.customProperties["Team"];
		this.health_Statistics.shieldStats.team = this.health_Statistics.team;

		if(PhotonNetwork.player == this.gameObject.GetComponent<PhotonView>().owner)
		{
			this.player_Controlled = true;
			GameObject.Find("Ship Camera").GetComponent<Camera>().GetComponent<Ship_Camera>().attached_To = this.gameObject;
		}
		else
        {   //determining radar objects and ally radar objects
            if(this.game_Manager.team == Health_Statistics.Team.None)
            {
                this.gameObject.AddComponent<GenerateRadarObject>();
            }
            else if(this.game_Manager.team == Health_Statistics.Team.Blue)
            {
                if (this.health_Statistics.team == Health_Statistics.Team.Blue)
                {
                    this.gameObject.AddComponent<GenerateAllyObject>();
                }
                else
                {
                    this.gameObject.AddComponent<GenerateRadarObject>();
                }
            }
            else if(this.game_Manager.team == Health_Statistics.Team.Red)
            {
                if (this.health_Statistics.team == Health_Statistics.Team.Red)
                {
                    this.gameObject.AddComponent<GenerateAllyObject>();
                }
                else
                {
                    this.gameObject.AddComponent<GenerateRadarObject>();
                }
            }
          
            //this.gameObject.AddComponent<GenerateRadarObject>();
			this.player_Controlled = false;
			this.health_Statistics.death_Explosion_Name = "";
		}

		SpriteRenderer sprite_Renderer = this.gameObject.GetComponentInChildren<SpriteRenderer>();
        if (this.health_Statistics.team == Health_Statistics.Team.Blue)
		{
			switch(this.health_Statistics.derived_From_Prefab)
			{
				case "Arbiter":
					sprite_Renderer.sprite = Resources.Load<Sprite>("Sprites/Ship/Barbiter");
                    sprite_Renderer.transform.localScale = new Vector3(0.2925f, 0.2925f, 1.0f);
                    break;

				case "Clank R7":
					sprite_Renderer.sprite = Resources.Load<Sprite>("Sprites/Ship/Bclank");
                    sprite_Renderer.transform.localScale = new Vector3(0.2f, 0.2f, 1.0f);
                    break;

				case "Jupiter":
					sprite_Renderer.sprite = Resources.Load<Sprite>("Sprites/Ship/Bjupiter");
                    sprite_Renderer.transform.localScale = new Vector3(0.465f, 0.465f, 1.0f);
                    sprite_Renderer.transform.localRotation = new Quaternion(180, 0, 0, 0);
                    break;

				case "Lock On":
					sprite_Renderer.sprite = Resources.Load<Sprite>("Sprites/Ship/Blockon");
					break;

				case "Mecha":
					sprite_Renderer.sprite = Resources.Load<Sprite>("Sprites/Ship/Bmecha");
					sprite_Renderer.transform.localScale = new Vector3(1.06f, 1.06f, 1.0f);
					break;

				case "SoulEater":
					sprite_Renderer.sprite = Resources.Load<Sprite>("Sprites/Ship/Bsoul");
					break;
			}
		}
		else if(this.health_Statistics.team == Health_Statistics.Team.Red)
		{
			switch(this.health_Statistics.derived_From_Prefab)
			{
				case "Arbiter":
					sprite_Renderer.sprite = Resources.Load<Sprite>("Sprites/Ship/Rarbiter");
                    sprite_Renderer.transform.localScale = new Vector3(0.2925f, 0.2925f, 1.0f);
                    break;

				case "Clank R7":
					//FFA version is colored properly already
					break;

				case "Jupiter":
					sprite_Renderer.sprite = Resources.Load<Sprite>("Sprites/Ship/Rjupiter");
                    sprite_Renderer.transform.localScale = new Vector3(0.465f, 0.465f, 1.0f);
                    sprite_Renderer.transform.localRotation = new Quaternion(180, 0, 0, 0);
                    break;

				case "Lock On":
					sprite_Renderer.sprite = Resources.Load<Sprite>("Sprites/Ship/Rlockon");
					break;

				case "Mecha":
					//FFA version is colored properly already
					break;

				case "SoulEater":
					//FFA version is colored properly already
					break;
			}
		}
	}

	//Used to allow ships to continue working after a script has been edited and to keep squared velocity in sync with velocity if changed in the editor
//	private void OnValidate()
//	{
//		if(this.gameObject.activeInHierarchy == true)
//		{
//			this.Start();
//		}
//	}

    private void FixedUpdate()
    {
		if (main_hud.InCenter == true || main_hud.InController == true || main_hud.InStats == true)
        {
            //Debug.Log("All In Game Controls Turned Off");
        }
        else if(this.player_Controlled == true)
        {
			//Disables the particle systems for the projectiles and thrusters- they are activated depending on controls used
			this.is_Thrusting = false;
			for (int i = 0; i < this.primary_Projectile_Emissions.Length; i++)
			{
				this.primary_Projectile_Emissions[i].enabled = false;
			}
			for (int i = 0; i < this.secondary_Projectile_Emissions.Length; i++)
			{
				this.secondary_Projectile_Emissions[i].enabled = false;
			}
			for (int i = 0; i < this.forward_Thruster_Emissions.Length; i++)
			{
				this.forward_Thruster_Emissions[i].enabled = false;
			}
			//			for(int i = 0; i < this.backward_Thruster_Emissions.Length; i++)
			//			{
			//				this.backward_Thruster_Emissions[i].enabled = false;
			//			}
			//			for(int i = 0; i < this.leftward_Thruster_Emissions.Length; i++)
			//			{
			//				this.leftward_Thruster_Emissions[i].enabled = false;
			//			}
			//			for(int i = 0; i < this.rightward_Thruster_Emissions.Length; i++)
			//			{
			//				this.rightward_Thruster_Emissions[i].enabled = false;
			//			}
			for (int i = 0; i < this.left_Rotational_Thruster_Emissions.Length; i++)
			{
				this.left_Rotational_Thruster_Emissions[i].enabled = false;
			}
			for (int i = 0; i < this.right_Rotational_Thruster_Emissions.Length; i++)
			{
				this.right_Rotational_Thruster_Emissions[i].enabled = false;
			}

			//Mutes the volume of the Thrusters- Thrusters are unmuted if they are active. For Projectiles, the audio clip is played on fire
			for (int i = 0; i < this.forward_Thruster_Audio_Sources.Length; i++)
			{
				this.forward_Thruster_Audio_Sources[i].volume = 0.0f;
			}
			//			for(int i = 0; i < this.backward_Thruster_Audio_Sources.Length; i++)
			//			{
			//				this.backward_Thruster_Audio_Sources[i].volume = 0.0f;
			//			}
			//			for(int i = 0; i < this.leftward_Thruster_Audio_Sources.Length; i++)
			//			{
			//				this.leftward_Thruster_Audio_Sources[i].volume = 0.0f;
			//			}
			//			for(int i = 0; i < this.rightward_Thruster_Audio_Sources.Length; i++)
			//			{
			//				this.rightward_Thruster_Audio_Sources[i].volume = 0.0f;
			//			}
			for (int i = 0; i < this.left_Rotational_Thruster_Audio_Sources.Length; i++)
			{
				this.left_Rotational_Thruster_Audio_Sources[i].volume = 0.0f;
			}
			for (int i = 0; i < this.right_Rotational_Thruster_Audio_Sources.Length; i++)
			{
				this.right_Rotational_Thruster_Audio_Sources[i].volume = 0.0f;
			}

            //Primary weapon fire- creates the object placed in the primary_Projectile_Settings.projectile field at a number of positions in the primary_Projectile_Settings.projectile_Locations_And_Effects array based on a combination of the primary_Projectile_Settings.projectile_Sequence_Number, projectiles_Per_Burst, and  with an initial velocity and direction equal to the ship's rigid body and transforms with the addition of a forward velocity of the value in the primary_Projectile_Settings.projectile_Initial_Velocity public field
            if (Input.GetAxis("Fire1") > 0 || Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.weapon1.keyCode) == true || Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.ACweapon1.keyCode) == true)
            {
                if (Time.fixedTime > next_Fire_Available && this.is_Boosting == false && !canFireBothWeapons ||
                    (canFireBothWeapons && Time.fixedTime > next_Primary_Available && !this.is_Boosting))
                {
                    next_Fire_Available = Time.fixedTime + this.primary_Projectile_Settings.projectile_Burst_Delay;
                    next_Primary_Available = Time.fixedTime + this.primary_Projectile_Settings.projectile_Burst_Delay;
                    for (int i = 0; i < this.primary_Projectile_Settings.projectiles_Per_Burst; i++)
                    {
                        GameObject primary_Projectile = PhotonNetwork.Instantiate(this.primary_Projectile_Settings.projectile, this.primary_Projectile_Settings.projectile_Locations_And_Effects[this.primary_Projectile_Settings.projectile_Sequence_Number].transform.position, this.primary_Projectile_Settings.projectile_Locations_And_Effects[this.primary_Projectile_Settings.projectile_Sequence_Number].transform.rotation, 0);
                        primary_Projectile.GetComponent<Rigidbody2D>().velocity = this.rigid_Body_2D.velocity + this.primary_Projectile_Settings.projectile_Initial_Velocity * new Vector2(primary_Projectile.gameObject.transform.up.x, primary_Projectile.gameObject.transform.up.y).normalized;//May be already normalized
                        //primary_Projectile.GetComponent<Projectile>().damage = this.primary_Projectile_Settings.projectile_Damage;
						primary_Projectile.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.player);
                        primary_Projectile.GetComponent<Projectile>().duration = this.primary_Projectile_Settings.projectile_Duration;
                        primary_Projectile.GetComponent<Projectile>().local_Projectile = true;
                        if (this.primary_Projectile_Settings.projectile_Use_Ship_Transform == true)
                        {
                            primary_Projectile.transform.parent = this.gameObject.transform;
                            Object.Destroy(primary_Projectile.GetComponent<Rigidbody2D>());
                        }
                        if (this.primary_Projectile_Settings.projectile_Sequence_Number < this.primary_Projectile_Audio_Sources.Length)
                        {
                            this.primary_Projectile_Audio_Sources[this.primary_Projectile_Settings.projectile_Sequence_Number].Play();
                        }
                        else if (this.primary_Projectile_Audio_Sources.Length >= 1)
                        {
                            this.primary_Projectile_Audio_Sources[0].Play();
                        }
                        if (this.primary_Projectile_Settings.projectile_Sequence_Number < this.primary_Projectile_Emissions.Length)
                        {
                            this.primary_Projectile_Emissions[this.primary_Projectile_Settings.projectile_Sequence_Number].enabled = true;
                            this.primary_Projectile_Particle_Systems[this.primary_Projectile_Settings.projectile_Sequence_Number].Play();
                        }
                        else if (this.primary_Projectile_Emissions.Length >= 1)
                        {
                            this.primary_Projectile_Emissions[0].enabled = true;
                            this.primary_Projectile_Particle_Systems[0].Play();
                        }
                        this.primary_Projectile_Settings.projectile_Sequence_Number = this.primary_Projectile_Settings.projectile_Sequence_Number + 1;
                        if (this.primary_Projectile_Settings.projectile_Sequence_Number >= this.primary_Projectile_Settings.projectile_Locations_And_Effects.Length)
                        {
                            this.primary_Projectile_Settings.projectile_Sequence_Number = 0;
                        }

                        if(i == this.primary_Projectile_Settings.projectiles_Per_Burst - 1 && fireDouble)
                        {
                            i = -1;
                            fireDouble = false;
                        }
                    }
                }
            }
            //Secondary weapon fire- creates the object placed in the secondary_Projectile public field at the primary_Projectile_Weapon_Location with an initial velocity and direction equal to the ship's rigid body and transforms with the addition of a forward velocity of the value in the primary_Projectile_Settings.projectile_Initial_Velocity public field
            if (Input.GetAxis("Fire2") > 0 || Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.weapon2.keyCode) == true || Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.ACweapon2.keyCode) == true)
            {
                if (next_Fire_Available < Time.fixedTime && this.is_Boosting == false && !canFireBothWeapons ||
                    (canFireBothWeapons && next_Secondary_Available < Time.fixedTime && !this.is_Boosting))
                {
                    next_Fire_Available = Time.fixedTime + this.secondary_Projectile_Settings.projectile_Burst_Delay;
                    next_Secondary_Available = Time.fixedTime + this.secondary_Projectile_Settings.projectile_Burst_Delay;
                    for (int i = 0; i < this.secondary_Projectile_Settings.projectiles_Per_Burst; i++)
                    {
                        GameObject secondary_Projectile = PhotonNetwork.Instantiate(this.secondary_Projectile_Settings.projectile, this.secondary_Projectile_Settings.projectile_Locations_And_Effects[this.secondary_Projectile_Settings.projectile_Sequence_Number].transform.position, this.secondary_Projectile_Settings.projectile_Locations_And_Effects[this.secondary_Projectile_Settings.projectile_Sequence_Number].transform.rotation, 0);
                        secondary_Projectile.GetComponent<Rigidbody2D>().velocity = this.rigid_Body_2D.velocity + this.secondary_Projectile_Settings.projectile_Initial_Velocity * new Vector2(secondary_Projectile.gameObject.transform.up.x, secondary_Projectile.gameObject.transform.up.y).normalized;//May be already normalized
                        //secondary_Projectile.GetComponent<Projectile>().damage = this.secondary_Projectile_Settings.projectile_Damage;
                        secondary_Projectile.GetComponent<Projectile>().duration = this.secondary_Projectile_Settings.projectile_Duration;
                        secondary_Projectile.GetComponent<Projectile>().local_Projectile = true;
                        if (this.secondary_Projectile_Settings.projectile_Use_Ship_Transform == true)
                        {
                            secondary_Projectile.transform.parent = this.gameObject.transform;
                            Object.Destroy(secondary_Projectile.GetComponent<Rigidbody2D>());
                        };
                        if (this.secondary_Projectile_Settings.projectile_Sequence_Number < this.secondary_Projectile_Audio_Sources.Length)
                        {
                            this.secondary_Projectile_Audio_Sources[this.secondary_Projectile_Settings.projectile_Sequence_Number].Play();
                        }
                        else if (this.secondary_Projectile_Audio_Sources.Length >= 1)
                        {
                            this.secondary_Projectile_Audio_Sources[0].Play();
                        }
                        if (this.secondary_Projectile_Settings.projectile_Sequence_Number < this.secondary_Projectile_Emissions.Length)
                        {
                            this.secondary_Projectile_Emissions[this.secondary_Projectile_Settings.projectile_Sequence_Number].enabled = true;
                            this.secondary_Projectile_Particle_Systems[this.secondary_Projectile_Settings.projectile_Sequence_Number].Play();
                        }
                        else if (this.secondary_Projectile_Emissions.Length >= 1)
                        {
                            this.secondary_Projectile_Emissions[0].enabled = true;
                            this.secondary_Projectile_Particle_Systems[0].Play();
                        }
                        this.secondary_Projectile_Settings.projectile_Sequence_Number = this.secondary_Projectile_Settings.projectile_Sequence_Number + 1;
                        if (this.secondary_Projectile_Settings.projectile_Sequence_Number >= this.secondary_Projectile_Settings.projectile_Locations_And_Effects.Length)
                        {
                            this.secondary_Projectile_Settings.projectile_Sequence_Number = 0;
                        }

                        if (i == this.secondary_Projectile_Settings.projectiles_Per_Burst - 1 && fireDouble)
                        {
                            i = -1;
                            fireDouble = false;
                        }
                    }
                }
            }

			//Boost
			if(Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.boost.keyCode) == true || Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.ACboost.keyCode) == true)
			{
				if(this.is_Boosting == false)
				{
					this.is_Boosting = true;
					this.maximum_Ship_Velocity = this.maximum_Ship_Velocity * 2.0f;
					this.maximum_Ship_Velocity_Squared = this.maximum_Ship_Velocity * this.maximum_Ship_Velocity;
					this.rate_Of_Acceleration = this.rate_Of_Acceleration * 2.0f;
				}
			}
			else
			{
				if(this.is_Boosting == true)
				{
					this.is_Boosting = false;
					this.maximum_Ship_Velocity = this.maximum_Ship_Velocity / 2.0f;
					this.maximum_Ship_Velocity_Squared = this.maximum_Ship_Velocity * this.maximum_Ship_Velocity;
					this.rate_Of_Acceleration = this.rate_Of_Acceleration / 2.0f;
				}
			}

            //Movement Code
            //Assigns a vector corresponding to the inputs from the Input Manager (Axes) or GameManager (Menu Mappings)
            Vector2 leftJoystick = new Vector2(
                    Input.GetAxis("Horizontal") != 0.0f ? Input.GetAxis("Horizontal") : Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.moveLeft.keyCode)
                || Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.ACmoveLeft.keyCode) ? -1.0f : Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.moveRight.keyCode)
                || Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.ACmoveRight.keyCode) ? 1.0f : 0.0f,
                    Input.GetAxis("Vertical") != 0.0f ? Input.GetAxis("Vertical") : Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.moveBack.keyCode)
                || Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.ACmoveBack.keyCode) ? -1.0f : Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.moveForward.keyCode)
                || Input.GetKey((UnityEngine.KeyCode)this.game_Manager.userControlsKeyboard.moveForward.keyCode) ? 1.0f : 0.0f
                );

            this.rigid_Body_2D.AddForce(leftJoystick * this.rate_Of_Acceleration * this.rigid_Body_2D.mass);

            if (this.rigid_Body_2D.velocity.sqrMagnitude > this.maximum_Ship_Velocity_Squared)
            {
                this.rigid_Body_2D.AddForce(((this.rigid_Body_2D.velocity.normalized * this.maximum_Ship_Velocity) - this.rigid_Body_2D.velocity) * this.rigid_Body_2D.mass / Time.fixedDeltaTime);
            }

            if (Mathf.Abs(leftJoystick.x) > 0.01 || Mathf.Abs(leftJoystick.y) > 0.01)
            {
				this.is_Thrusting = true;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(-(leftJoystick).x, (leftJoystick).y) * Mathf.Rad2Deg), this.maximum_Rotational_Velocity * Time.deltaTime);

                for (int i = 0; i < this.forward_Thruster_Emissions.Length; i++)
                {
                    this.forward_Thruster_Emissions[i].enabled = true;
                }
                for (int i = 0; i < this.forward_Thruster_Audio_Sources.Length; i++)
                {
                    this.forward_Thruster_Audio_Sources[i].volume = this.forward_Thruster_Audio_Sources_Original_Volume[i];
                }
            }
        }
    }

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting == true)
		{
			stream.SendNext(this.is_Thrusting);
			stream.SendNext(this.health_Statistics.hull_Integrity);
			stream.SendNext(this.health_Statistics.shieldStats.shield_Strength);
		}
		else
		{
			if(this.rigid_Body_2D != null)
			{
				if((this.transform.position - (Vector3)this.rigid_Body_2D.velocity * PhotonNetwork.GetPing() * 0.0008f).sqrMagnitude > 100.0f)	//If the ship's current position is 10 or more meters away from the predicted networked position, update the position. Predictions use 80% of the current velocity to not overestimate quick changes in acceleration.
				{
					this.transform.position = this.transform.position + (Vector3)this.rigid_Body_2D.velocity * PhotonNetwork.GetPing() * 0.0008f;
				}
				this.is_Thrusting = (bool)stream.ReceiveNext();
				if(this.is_Thrusting == true)
				{
					//Enable networked Ship Thruster Particle Effects
					for (int i = 0; i < this.forward_Thruster_Emissions.Length; i++)
					{
						this.forward_Thruster_Emissions[i].enabled = true;
					}
					for (int i = 0; i < this.left_Rotational_Thruster_Emissions.Length; i++)
					{
						this.left_Rotational_Thruster_Emissions[i].enabled = true;
					}
					for (int i = 0; i < this.right_Rotational_Thruster_Emissions.Length; i++)
					{
						this.right_Rotational_Thruster_Emissions[i].enabled = true;
					}

					//Enable networked Ship Thruster Sounds
					for (int i = 0; i < this.forward_Thruster_Audio_Sources.Length; i++)
					{
						this.forward_Thruster_Audio_Sources[i].volume = this.forward_Thruster_Audio_Sources_Original_Volume[i];
					}
					for (int i = 0; i < this.left_Rotational_Thruster_Audio_Sources.Length; i++)
					{
						this.left_Rotational_Thruster_Audio_Sources[i].volume = this.left_Rotational_Thruster_Audio_Sources_Original_Volume[i];
					}
					for (int i = 0; i < this.right_Rotational_Thruster_Audio_Sources.Length; i++)
					{
						this.right_Rotational_Thruster_Audio_Sources[i].volume = this.right_Rotational_Thruster_Audio_Sources_Original_Volume[i];
					}
				}
				else
				{
					//Disable networked Ship Thruster Particle Effects
					for (int i = 0; i < this.forward_Thruster_Emissions.Length; i++)
					{
						this.forward_Thruster_Emissions[i].enabled = false;
					}
					for (int i = 0; i < this.left_Rotational_Thruster_Emissions.Length; i++)
					{
						this.left_Rotational_Thruster_Emissions[i].enabled = false;
					}
					for (int i = 0; i < this.right_Rotational_Thruster_Emissions.Length; i++)
					{
						this.right_Rotational_Thruster_Emissions[i].enabled = false;
					}

					//Disable networked Ship Thruster Sounds
					for (int i = 0; i < this.forward_Thruster_Audio_Sources.Length; i++)
					{
						this.forward_Thruster_Audio_Sources[i].volume = 0.0f;
					}
					for (int i = 0; i < this.left_Rotational_Thruster_Audio_Sources.Length; i++)
					{
						this.left_Rotational_Thruster_Audio_Sources[i].volume = 0.0f;
					}
					for (int i = 0; i < this.right_Rotational_Thruster_Audio_Sources.Length; i++)
					{
						this.right_Rotational_Thruster_Audio_Sources[i].volume = 0.0f;
					}
				}
				this.health_Statistics.hull_Integrity = (float)stream.ReceiveNext();
				this.health_Statistics.shieldStats.shield_Strength = (float)stream.ReceiveNext();
			}
		}
	}

	void OnDestroy()
	{
		Ship_Controls.active_Ships_List.Remove(this);
	}

	public static ParticleSystem[] get_Valid_Particle_Systems_Array(GameObject[] game_Object_Array)
	{
		int number_Of_Valid_Particle_Systems = 0;
		for(int i = 0; i < game_Object_Array.Length; i++)
		{
			if(game_Object_Array[i].GetComponentInChildren<ParticleSystem>() != null)
			{
				number_Of_Valid_Particle_Systems = number_Of_Valid_Particle_Systems + 1;
			}
		}
		ParticleSystem[] emissions = new ParticleSystem[number_Of_Valid_Particle_Systems];
		number_Of_Valid_Particle_Systems = 0;
		for(int i = 0; i < game_Object_Array.Length; i++)
		{
			if(game_Object_Array[i].GetComponentInChildren<ParticleSystem>() != null)
			{
				emissions[number_Of_Valid_Particle_Systems] = game_Object_Array[i].GetComponentInChildren<ParticleSystem>();
				number_Of_Valid_Particle_Systems = number_Of_Valid_Particle_Systems + 1;
			}
		}
		return emissions;
	}

	public static AudioSource[] get_Valid_Audio_Sources_Array(GameObject[] game_Object_Array)
	{
		int number_Of_Valid_Audio_Sources = 0;
		for(int i = 0; i < game_Object_Array.Length; i++)
		{
			if(game_Object_Array[i].GetComponentInChildren<AudioSource>() != null)
			{
				number_Of_Valid_Audio_Sources = number_Of_Valid_Audio_Sources + 1;
			}
		}
		AudioSource[] audio_Sources = new AudioSource[number_Of_Valid_Audio_Sources];
		number_Of_Valid_Audio_Sources = 0;
		for(int i = 0; i < game_Object_Array.Length; i++)
		{
			if(game_Object_Array[i].GetComponentInChildren<AudioSource>() != null)
			{
				audio_Sources[number_Of_Valid_Audio_Sources] = game_Object_Array[i].GetComponentInChildren<AudioSource>();
				number_Of_Valid_Audio_Sources = number_Of_Valid_Audio_Sources + 1;
			}
		}
		return audio_Sources;
	}
}
