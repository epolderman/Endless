using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RadarObject
{

    public Image icon { get; set; }
    public GameObject owner { get; set; }


}



public class RadarMap : MonoBehaviour 
{

    //center player
    public Transform playerPos;
    public Image EnemyImage;
    public Image AllyImage;
    

    //increase decrease the distance between objects in radar
    //float mapScale = 0.100f;
    float mapScale = 0.029f;

    public static List<RadarObject> radObjects = new List<RadarObject>();
    

    // Use this for initialization
    void Start()
    {
        
        
    }

    public void CheckMyPosition()
    {
        for (int i = 0; i < Ship_Controls.active_Ships_List.Count; i++)
        {
            if (Ship_Controls.active_Ships_List[i].player_Controlled == true)
            {  
                playerPos = Ship_Controls.active_Ships_List[i].transform;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
       
       CheckMyPosition();
       DrawRadarDots();

    }

    public void RegisterRadarObject(GameObject o, Image i)
    {

        Image EnemyImage = Instantiate(i);
        radObjects.Add(new RadarObject() { owner = o, icon = EnemyImage });
    
    
    }
  
    public static void RemoveRadarObject(GameObject o)
    {

        List<RadarObject> newList = new List<RadarObject>();
        
        for (int i = 0; i < radObjects.Count; i++)
        {

            if (radObjects[i].owner == o)
            {
                //remove from radar display
                Destroy(radObjects[i].icon);
                continue;


            }
            else
            {
                newList.Add(radObjects[i]);
            }
        
        
        }
        
        radObjects.RemoveRange(0, radObjects.Count);
       
        radObjects.AddRange(newList);

            
    }
    public void DrawRadarDots()
    {

        if (playerPos == null)
        {
            //Debug.Log("Radar Map -- Position Not Set Yet -- Radar Map");

        }
        else
        {
            foreach (RadarObject ro in radObjects)
            {

                 //gets owner position determines its position vs main player
                
                ro.icon.transform.SetParent(this.transform);
                if (ro.owner != null)
                {
                    ro.icon.transform.localPosition = (ro.owner.transform.position - playerPos.position) * mapScale;
                }

            }
        }




       }

  

 

}
