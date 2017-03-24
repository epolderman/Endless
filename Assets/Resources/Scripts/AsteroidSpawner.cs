using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {

    public GameObject Asteroid;
	public int maxSpawnCount;
    public float maxSpawnRateinSeconds = 10f;

	// Use this for initialization
	void Start () {
        Invoke("SpawnAsteroid", maxSpawnRateinSeconds);

        //increase spawn rate every 30 seconds
        //Invoke("IncreaseSpawnRate", 0f);
        SpawnAsteroid();
	}

	public Vector2 location = new Vector2 (1500, 0);
	public Vector2 min = new Vector2(-1000, -1000);
	public Vector2 max = new Vector2(1000, 1000);

    void SpawnAsteroid()
    { 
		//instantiate asteroid
		if(PhotonNetwork.isMasterClient == true)
		{
			GameObject anAsteroid = PhotonNetwork.Instantiate("prefab/" + Asteroid.name, new Vector2(location.x + Random.Range(min.x, max.x), location.y + Random.Range(min.y, max.y)), Quaternion.identity, 0);
			anAsteroid.GetComponent<Dynamic_Object> ().initial_Angular_Velocity = Random.Range (anAsteroid.GetComponent<AsteroidController> ().minimum_Angle, anAsteroid.GetComponent<AsteroidController> ().maximum_Angle);
			anAsteroid.GetComponent<Dynamic_Object> ().initial_Velocity.x = Random.Range (anAsteroid.GetComponent<AsteroidController> ().minspeed, anAsteroid.GetComponent<AsteroidController> ().maxspeed);

			//when to spaen next asteroid
			spawnNextAsteroid ();
		}

    }

    void spawnNextAsteroid()
    {
        float spawnInSeconds;

        if (maxSpawnRateinSeconds > 5f)
        {
            spawnInSeconds = Random.Range(1f, maxSpawnRateinSeconds);
        }
        else
            spawnInSeconds = 1f;

        Invoke("SpawnAsteroid", spawnInSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateinSeconds > 5f)
            maxSpawnRateinSeconds--;

        if (maxSpawnRateinSeconds == 5f)
            CancelInvoke("IncreaseSpawnRate");
    }
}
