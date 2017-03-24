using UnityEngine;
using System.Collections;

public class ParticlesOnOff : MonoBehaviour {

	protected bool letPlay = true;

	public ParticleSystem recharger;

	public ParticleSystem shieldLvl1, shieldLvl2, shieldLvl3, shieldLvl4;

	public ParticleSystem DamageBurst1, DamageBurst2, DamageBurst3;
	//Damage Burst has varied intensities depending on how long it plays.
	//At 0.0 500 particles 0.15 1k particles 0.30 2k particles
	//Each teir is used together for different severities of hit.

    

	public Shield_Statistics shieldStats;

    int level;


    float shieldPercentageLast;
	float shieldPercentage;

	public void Start(){
        Color[] colors = new Color[1];
        colors[0] = new Color(255 / 255, 255 / 255, 255 / 255, 173 / 255);
        InvokeRepeating ("damageCheck", .2f, 1f);
        shieldLvl1.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
        shieldLvl2.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
        shieldLvl3.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
        shieldLvl4.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];

    }

    public void Update()
	{
		//shieldStats = (Shield_Statistics)GetComponent("Shield_Statistics");

		shieldPercentage = (float)shieldStats.shield_Strength / (float)shieldStats.maximum_Shield_Strength * 100.0f;

		shields (shieldPercentage);
	}

	public void damageCheck()
	{
		//print ("Damage Check");
		if ((shieldPercentage + 5) < shieldPercentageLast) {
//			print ("Major Damage 1!");
			if (DamageBurst1.isStopped)
				DamageBurst1.Play ();

        }
		if ((shieldPercentage + 10) < shieldPercentageLast) {
//			print ("Major Damage2!");
			if (DamageBurst2.isStopped)
				DamageBurst2.Play ();

		}
		if ((shieldPercentage + 15) < shieldPercentageLast) {
//			print ("Major Damage3!");
			if (DamageBurst3.isStopped)
				DamageBurst3.Play ();

		}


		shieldPercentageLast = shieldPercentage;
	}

	public void shields(float precentage)
	{
	    level = 0;

//		print (precentage);

		float delay = 0;
		if (precentage < 33) {
			delay = 4;
		}
		if (precentage < 66 && precentage > 33) {
			delay = 3;
		}
		if (precentage < 100 && precentage > 66) {
			delay = 2;
		}
		if (precentage == 100) {
			delay = 1;
		}

		rechargerSpeed (delay, precentage);

		//Sheild States
		if (precentage < 10 && precentage > 0) {
			level = 0;
            Color[] colors = new Color[2];
            colors[0] = new Color(255 / 255, 28 / 255, 28 / 255, 173 / 255);
            colors[1] = new Color(255 / 255, 60 / 255, 15 / 255, 213 / 255);
            shieldLvl1.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            //shieldLvl2.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            //shieldLvl3.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            //shieldLvl4.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];

        }else
		if (precentage < 25 && precentage >= 10) {
			level = 1;
            Color[] colors = new Color[2];
            colors[0] = new Color(255 / 255, 28 / 255, 28 / 255, 173 / 255);
            colors[1] = new Color(255 / 255, 60 / 255, 15 / 255, 213 / 255);
            shieldLvl1.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            shieldLvl2.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            shieldLvl3.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            shieldLvl4.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
        }else
		if (precentage < 50 && precentage >= 25) {
			level = 2;
            Color[] colors = new Color[2];
            colors[0] = new Color(224 / 255, 255 / 255, 28 / 255, 173 / 255);
            colors[1] = new Color(255 / 255, 229 / 255, 15 / 255, 213 / 255);
            shieldLvl1.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            shieldLvl2.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            shieldLvl3.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            shieldLvl4.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
        }else
		if (precentage < 75 && precentage >= 50) {
			level = 3;
            Color[] colors = new Color[2];
            colors[0] = new Color(28 / 255, 255 / 255, 48 / 255, 173 / 255);
            colors[1] = new Color(15 / 255, 225 / 255, 76 / 255, 213 / 255);
            shieldLvl1.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            shieldLvl2.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            shieldLvl3.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            shieldLvl4.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
        }else
		if (precentage <= 100 && precentage >= 75) {
			level = 4;
            Color[] colors = new Color[2];
            colors[0] = new Color(28 / 255, 142 / 255, 255 / 255, 173 / 255);
            colors[1] = new Color(15 / 255, 225 / 255, 255 / 255, 213 / 255);
            shieldLvl1.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            shieldLvl2.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            shieldLvl3.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
            shieldLvl4.GetComponent<ParticleSystem>().startColor = colors[Random.Range(0, colors.Length)];
        }
        else
        {
            shieldLvl1.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0, 0);
            shieldLvl2.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0, 0);
            shieldLvl3.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0, 0);
            shieldLvl4.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0, 0);
        }

		displayShields (level);

	}

	public void shieldBurst()
	{

	}

	public void rechargerSpeed(float delay, float precentage)
	{
		if ((precentage % delay) > 0 && (precentage % delay) <= (delay/2)) {
			if (recharger.isStopped)
				recharger.Play ();
		} else {
			if (recharger.isPlaying)
				recharger.Stop ();	
		}
	}

	public void displayShields (int shieldLvl)
	{
		switch (shieldLvl) {

		case 0:
			if(shieldLvl1.isPlaying)
                { 
				shieldLvl1.Stop ();
                    shieldLvl1.Clear();
                }
            if (shieldLvl2.isPlaying)
                {
                    shieldLvl2.Stop();
                    shieldLvl2.Clear();
                }
			if(shieldLvl3.isPlaying)
                {
                    shieldLvl3.Stop();
                    shieldLvl3.Clear();
                }
            if (shieldLvl4.isPlaying)
                {
                    shieldLvl4.Stop();
                    shieldLvl4.Clear();
                }
                break;

		case 1:
			if (shieldLvl1.isStopped)
				shieldLvl1.Play ();

            if (shieldLvl2.isPlaying)
            {
                shieldLvl2.Stop();
                shieldLvl2.Clear();
            }
            if (shieldLvl3.isPlaying)
            {
                shieldLvl3.Stop();
                shieldLvl3.Clear();
            }
            if (shieldLvl4.isPlaying)
            {
                shieldLvl4.Stop();
                shieldLvl4.Clear();
            }
                break;
		case 2:
			if (shieldLvl1.isStopped)
				shieldLvl1.Play ();
			if (shieldLvl2.isStopped)
				shieldLvl2.Play ();

            if (shieldLvl3.isPlaying)
            {
                shieldLvl3.Stop();
                shieldLvl3.Clear();
            }
            if (shieldLvl4.isPlaying)
            {
                shieldLvl4.Stop();
                shieldLvl4.Clear();
            }
                break;
		case 3:
			if (shieldLvl1.isStopped)
				shieldLvl1.Play ();
			if (shieldLvl2.isStopped)
				shieldLvl2.Play ();
			if (shieldLvl3.isStopped)
				shieldLvl3.Play ();

            if (shieldLvl4.isPlaying)
            {
                shieldLvl4.Stop();
                shieldLvl4.Clear();
            }
                break;
		case 4:
			if (shieldLvl1.isStopped)
				shieldLvl1.Play ();
			if (shieldLvl2.isStopped)
				shieldLvl2.Play ();
			if (shieldLvl3.isStopped)
				shieldLvl3.Play ();
			if (shieldLvl4.isStopped)
				shieldLvl4.Play ();
			break;
		}
	}
}
