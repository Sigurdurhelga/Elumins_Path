using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;

public enum DropDownColors
{
    blue, red, green, purple, white
}

public class GemScript : MonoBehaviour {


    public DropDownColors GemColor;
    public Light GemLight;
    public SpriteRenderer GemSprite;
    public float timeToCharge;
    public GameObject dynamicLight;
    public AudioSource success_sound;
    public Light SunLight;

    private Color[] Color_Codes = new Color[5]{ new Color(0,0.4f,0.8f,0.85f), Color.red, Color.green, Color.magenta, Color.white };
    private Color[] Light_Color_Codes = new Color[5]{ new Color(0,0.5f,1f,0.13f), new Color(1, 0,0,0.13f) , new Color(0,1,0,0.13f) , Color.magenta, Color.white };
    private Color selected_color;
    private Color selected_light;
    private Color powered_light = new Color(1, 1, 1, 1);
    private DynamicLight dynamicLightScript;
    private AudioSource gem_area_sound;
    private RoomLight roomLightScript;
    
    private int gem_power = 0;
    private bool gem_isPowered = false;
    private bool playerIn = false;
    private float poweringRecharge;
    private bool hitByRay = false;

    private void Start()
    {
        selected_color = Color_Codes[(int)GemColor];
        selected_light = Light_Color_Codes[(int)GemColor];
        GemSprite.color = selected_color;
        GemLight.color = selected_light;
        dynamicLightScript = dynamicLight.GetComponent<DynamicLight>();
        dynamicLight.SetActive(false);
        gem_area_sound = transform.parent.gameObject.GetComponent<AudioSource>();
        roomLightScript = SunLight.GetComponent<RoomLight>();
    }

	
	// Update is called once per frame
	/*void Update ()
    {
        if(playerIn && !gem_isPowered)
        {
            if(Time.time > poweringRecharge)
            {
                poweringRecharge = Time.time + 0.01f;
                gem_power += 1;
                GemLight.range += 0.03f;
                GemLight.intensity += 0.03f;
                if (gem_power >= timeToCharge / 0.01f)
                {
                    gem_isPowered = true;
                    PowerUp();

                }
            }
        }
        if(!playerIn && !gem_isPowered && GemLight.range > 0)
        {
            if(Time.time > poweringRecharge)
            {
                poweringRecharge = Time.time + 0.01f;
                gem_power -= 2;
                GemLight.range -= 0.06f;
                GemLight.intensity -= 0.06f;
            }
        }
        if(gem_power == 0 && !playerIn)
        {
            GemLight.enabled = false;
        }
    }*/

    void Update()
    {
        if (!gem_isPowered && Time.time > poweringRecharge)
        {
            poweringRecharge = Time.time + 0.01f;
            if (playerIn)
            {
                gem_power += 1;
                GemLight.range += 0.03f;
                GemLight.intensity += 0.03f;
                if (gem_power >= timeToCharge / 0.01f)
                {
                    gem_isPowered = true;
                    PowerUp();

                }   
            }
            if (!playerIn && !hitByRay &&  GemLight.range > 0)
            {
                gem_power -= 2;
                GemLight.range -= 0.06f;
                GemLight.intensity -= 0.06f;
            }
            if (hitByRay)
            {
                gem_power += 1;
                GemLight.range += 0.03f;
                GemLight.intensity += 0.03f;
                if (gem_power >= timeToCharge / 0.01f)
                {
                    gem_isPowered = true;
                    PowerUp();
                }
                hitByRay = false;
            }
        }
        if (gem_power == 0 && !playerIn)
        {
            GemLight.enabled = false;
        }
    }

    private void PowerUp()
    {
        success_sound.Play();
        dynamicLight.SetActive(true);
        //dynamicLightScript.lightRadius = 15;
        GemLight.color = powered_light;
        gem_area_sound.Stop();
        roomLightScript.GemActivated();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!gem_isPowered)
            {
                poweringRecharge = Time.time;
                GemLight.enabled = true;
                playerIn = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!gem_isPowered)
            {
                poweringRecharge = 0;
                playerIn = false;
            }
        }
    }

    public void OnHitRay()
    {
       //Debug.Log("Beam hitting gem");
        if (!gem_isPowered)
        {
            poweringRecharge = Time.time;
            GemLight.enabled = true;
            hitByRay = true;
        }
    }
}
