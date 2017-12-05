using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;
using System.Linq;

public class RoomLight : MonoBehaviour {

    public int GemAmount;

    private Light SunLight;

    private bool is_on = false;

	// Use this for initialization
	void Start () {
        SunLight = GetComponent<Light>();
	}

    // This function gets called everytime a gems is activated in the world
    public void GemActivated()
    {
        GemAmount -= 1;

        if(GemAmount == 0)
        {
            SunLight.enabled = true;
        }

    }
	
}
