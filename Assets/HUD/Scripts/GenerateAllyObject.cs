using UnityEngine;
using System.Collections;

public class GenerateAllyObject : MonoBehaviour 
{

    private GameObject MiniMap;
    private RadarMap radar_map;
    private GameObject GameMenuManager;
    private GameManager scriptFinder;


	// Use this for initialization
	void Start () 
    {

        MiniMap = GameObject.Find("MiniMap");
        this.radar_map = MiniMap.GetComponent<RadarMap>();
        this.radar_map.RegisterRadarObject(this.gameObject, this.radar_map.AllyImage);

	}
	
    void OnDestroy()
    {
        RadarMap.RemoveRadarObject(this.gameObject);
    }


}
