using UnityEngine;
using System.Collections;

public class pulsingborder : MonoBehaviour {
    
    public float alphaLevel = .5f;

    // Update is called once per frame
    void Update() {

        if (alphaLevel < 1f)
        { 
            alphaLevel += .05f;
        }
        else if (alphaLevel > 0f)
            alphaLevel -= .05f;

        GetComponent<Material>().color = new Color(1, 1, 1, alphaLevel);
    }
}
