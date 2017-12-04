using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropDownColors
{
    blue, red, green, purple, white
}

public class GemScript : MonoBehaviour {


    public DropDownColors GemColor;
    public Light GemLight;
    public SpriteRenderer GemSprite;
    public int requiredPower;

    private Color[] Color_Codes = new Color[5]{ new Color(0,104,204,200), Color.red, Color.green, Color.magenta, Color.white };
    private Color selected_color;

    private int gem_power = 0;
    private bool gem_isPowered = false;
    private bool playerIn = false;
    private float poweringRecharge;

    private void Start()
    {
        selected_color = Color_Codes[(int)GemColor];
        GemSprite.color = selected_color;
    }

    // Use this for initialization
    private void Awake ()
    {
	}
	
	// Update is called once per frame
	void Update () {
		
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Time.time > poweringRecharge)
        {
            poweringRecharge = Time.time + 2;
            gem_power += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!gem_isPowered)
            {
                poweringRecharge = 0;
                GemLight.enabled = false;
                playerIn = false;
            }
        }
        
    }
}
