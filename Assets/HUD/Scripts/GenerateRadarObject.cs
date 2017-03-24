using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GenerateRadarObject : MonoBehaviour {

    //public Image image;
    public Image EnemyImage;
    private GameObject MiniMap;
    private RadarMap radar_map;
    private GameObject GameMenuManager;
    private GameManager scriptFinder;

    void Start()
    {
            //gamemanager
            GameMenuManager = GameObject.Find("GameMenuManager");
            scriptFinder = GameMenuManager.GetComponent<GameManager>();

            MiniMap = GameObject.Find("MiniMap");
            this.radar_map = MiniMap.GetComponent<RadarMap>();
            this.radar_map.RegisterRadarObject(this.gameObject, this.radar_map.EnemyImage);
        
   

         
    }
	
    void OnDestroy()
    {
        RadarMap.RemoveRadarObject(this.gameObject);   
    }
  
}
