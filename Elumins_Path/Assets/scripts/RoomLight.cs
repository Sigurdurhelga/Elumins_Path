using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;
using System.Linq;

public class RoomLight : MonoBehaviour {

    public int GemAmount;
    public GameObject Portal;

    private Light SunLight;
    private ParticleSystem orb;
    private CircleCollider2D circleCollider;


    private bool is_on = false;

    // Use this for initialization
    void Start()
    {
        SunLight = GetComponent<Light>();
        if (Portal != null)
        {
            orb = Portal.GetComponent<ParticleSystem>();
            if (orb != null)
            {
                var es = orb.emission;
                es.enabled = false;
            }
            circleCollider = Portal.GetComponent<CircleCollider2D>();
            if (circleCollider != null)
            {
                circleCollider.enabled = false;
            }
        }
        else
        {
            orb = null;
            circleCollider = null;
        }
    }

    // This function gets called everytime a gems is activated in the world
    public void GemActivated()
    {
        GemAmount -= 1;

        if(GemAmount == 0)
        {
            SunLight.enabled = true;
            if (circleCollider != null)
            {
                circleCollider.enabled = true;
            }
            if (orb != null)
            {
                var es = orb.emission;
                es.enabled = true;
            }
        }

    }
	
}
