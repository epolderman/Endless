using UnityEngine;
using System.Collections;

public class FlowFlux : MonoBehaviour {

	public ParticleSystem particleSys;
	public int upperLimit;
	public int lowerLimit;
	public bool randomBetween;
	int midPoint;

	// Use this for initialization
	void Start () {

		midPoint = (upperLimit + lowerLimit) / 2;

		//print(midPoint + (Mathf.Sin (Time.time) * (midPoint / 2)));

		//particleSys.emission.rate (midPoint + (Mathf.Sin (Time.time) * (midPoint / 2)));

	
	}

	// Update is called once per frame
	void Update () {
		//print(midPoint + (Mathf.Sin (Time.time) * (midPoint / 2.5f)));
		float emRate = 0;
		if(randomBetween == false)
			emRate = midPoint + (Mathf.Sin (Time.time) * (midPoint / 2));
		else
			emRate = Random.Range(upperLimit, lowerLimit);
		print (emRate);

		var emission = particleSys.emission;
		emission.enabled = true;
		emission.rate = new ParticleSystem.MinMaxCurve(emRate);
	}
}
