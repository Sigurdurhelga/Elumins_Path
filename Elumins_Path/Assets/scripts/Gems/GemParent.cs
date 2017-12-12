using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;



public enum DropDownGemColors
{
    blue, red, green, purple, white
}

public class GemParent : MonoBehaviour
{

    //public levelmanager levelman;
    public DropDownGemColors GemColor;
    protected Light GemLight;
    protected SpriteRenderer GemSprite;
    public float timeToCharge = 0.5f;
    protected GameObject dynamicLight;
    protected AudioSource success_sound;
    protected Color[] Color_Codes = new Color[5] { new Color(0, 0.4f, 0.8f, 1f), Color.red, Color.green, Color.magenta, Color.white };
    protected Color[] Light_Color_Codes = new Color[5] { new Color(0, 0.5f, 1f, 0.13f), new Color(1, 0, 0, 0.13f), new Color(0, 1, 0, 0.13f), Color.magenta, Color.white };
    protected Color selected_color;
    protected Color selected_light;
    protected AudioSource gem_area_sound;
    protected levelmanager levelman;

    protected int gem_power = 0;
    protected bool gem_isPowered = false;
    protected bool playerIn = false;
    protected float poweringRecharge;
    protected bool hitByRay = false;

    protected float InitialIntensity = 0;

    public virtual void Start()
    {
        InitializeGemValues();
        levelman = GameObject.FindGameObjectWithTag("levelManager").GetComponent<levelmanager>();
        selected_color = Color_Codes[(int)GemColor];
        selected_light = Light_Color_Codes[(int)GemColor];
        GemSprite.color = selected_color;
        GemLight.color = selected_light;
        dynamicLight.SetActive(false);
        gem_area_sound = transform.parent.gameObject.GetComponent<AudioSource>();
    }
    private void InitializeGemValues()
    {
        GemSprite = transform.parent.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        GemLight = transform.parent.GetChild(2).gameObject.GetComponent<Light>();
        dynamicLight = transform.parent.GetChild(3).gameObject;
        success_sound = GetComponent<AudioSource>();
        gem_isPowered = false;
        playerIn = false;
        hitByRay = false;
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

            InitialIntensity = GemLight.intensity;
            GemLight.intensity = 0;

        }
    }
    protected IEnumerator Fade(Light light, float endIntensity)
    {
        while (light.intensity > endIntensity)
        {
            light.intensity -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        dynamicLight.SetActive(true);
    }
    public virtual void PowerUp()
    {
        if (tag == "levelGem")
        {
            levelman.gemActivated();
        }
        if (success_sound) success_sound.Play();
        GemLight.color = selected_light;
        StartCoroutine(Fade(GemLight, (GemLight.intensity * 0.75f)));
        if (gem_area_sound) gem_area_sound.Stop();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!gem_isPowered)
            {
                poweringRecharge = Time.time;
                //GemLight.enabled = true;
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
            hitByRay = true;
        }
    }
}