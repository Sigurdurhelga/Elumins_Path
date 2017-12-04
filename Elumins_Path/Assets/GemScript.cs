using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropDownColors
{
    blue, red, green, purple, white
}

public class GemScript : MonoBehaviour {

    private Color[] Color_Codes = new Color[5]{ Color.blue, Color.red, Color.green, Color.magenta, Color.white };

    public DropDownColors GemColor;

    private Light gem_light;
    private SpriteRenderer gem_sprite;
    private Color selected_color;

	// Use this for initialization
	void Start () {

        selected_color = Color_Codes[(int)GemColor];
        selected_color.a = 255f;

        gem_sprite = GetComponentInChildren<SpriteRenderer>();
        gem_sprite.color = selected_color;

        gem_light = GetComponentInChildren<Light>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something Entered");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        
    }
}
