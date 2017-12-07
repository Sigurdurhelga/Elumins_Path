using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;

public enum DropDownColors
{
    blue, red, green, purple, white
}

public class GemScript : MonoBehaviour
{


    public DropDownColors GemColor;
    public Light GemLight;
    public SpriteRenderer GemSprite;
    public float timeToCharge;
    public GameObject dynamicLight;
    public AudioSource success_sound;
    public Light SunLight;
    public GameObject portal;

    private Color[] Color_Codes = new Color[5] { new Color(0, 0.4f, 0.8f, 0.85f), Color.red, Color.green, Color.magenta, Color.white };
    private Color[] Light_Color_Codes = new Color[5] { new Color(0, 0.5f, 1f, 0.13f), new Color(1, 0, 0, 0.13f), new Color(0, 1, 0, 0.13f), Color.magenta, Color.white };
    private Color selected_color;
    private Color selected_light;
    //private Color powered_light = new Color(1, 1, 1, 1);
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
        if (portal != null) portal.SetActive(false);
    }

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
            //Debug.Log("Should Decrease: " + !playerIn + ", " + !hitByRay + ", " + gem_power.ToString());
            if (!playerIn && !hitByRay && gem_power > 0)
            {
                gem_power -= 2;
                GemLight.range -= 0.06f;
                GemLight.intensity -= 0.06f;
                if (gem_power < 0)
                {
                    GemLight.range -= 0.03f * gem_power;
                    GemLight.intensity -= 0.03f * gem_power;
                    gem_power = 0;
                }
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
        //Debug.Log("Gemlight is enabled: " + GemLight.enabled.ToString());
    }


    IEnumerator Fade(Light light, float endIntensity)
    {
        while (light.intensity > endIntensity)
        {
            light.intensity -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        dynamicLight.SetActive(true);
    }

    private void PowerUp()
    {
        if (success_sound) success_sound.Play();
        //dynamicLightScript.lightRadius = 15;
        GemLight.color = selected_light;
        StartCoroutine(Fade(GemLight, (GemLight.intensity * 0.75f)));
        ActAccordingToColor();
        if (gem_area_sound) gem_area_sound.Stop();
        roomLightScript.GemActivated();


    }
    private void ActAccordingToColor()
    {
        switch (GemColor)
        {
            case (DropDownColors.blue):
                portal.SetActive(true);
                break;
            case (DropDownColors.red):
                GameObject beam = GameObject.FindGameObjectWithTag("RedBeamSource");
                beam.GetComponent<LineRenderer>().enabled = true;
                beam.GetComponent<BeamCaster>().enabled = true;
                break;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
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
        if (other.tag == "Player")
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
        if (!gem_isPowered)
        {
            poweringRecharge = Time.time;
            GemLight.enabled = true;
            hitByRay = true;
        }
    }
}
