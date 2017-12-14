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
    public float timeToCharge;
    public float StartingLightIntensity;
    public float EndingLightIntesity;
    public float StartingLightRange;
    public float EndingLightRange;

    protected Light GemLight;
    protected SpriteRenderer GemSprite;

    protected GameObject dynamicLight;
    protected AudioSource success_sound;
    protected Color[] Color_Codes = new Color[5] { new Color(0, 0.4f, 0.8f, 1f), Color.red, Color.green, Color.magenta, Color.white };
    protected Color[] Light_Color_Codes = new Color[5] { new Color(0, 0.5f, 1f, 0.13f), new Color(1, 0, 0, 0.13f), new Color(0, 1, 0, 0.13f), Color.magenta, Color.white };
    protected Color selected_color;
    protected Color selected_light;
    protected AudioSource gem_area_sound;
    protected levelmanager levelman;

    protected bool gem_isPowered = false;
    protected bool playerIn = false;
    protected float poweringRecharge;
    protected bool hitByRay = false;

    protected bool chargingGem = false;

    private float IncreaseFactor;

    public virtual void Start()
    {
        InitializeGemValues();
        GetIncreaseFactor();
        levelman = GameObject.FindGameObjectWithTag("levelManager").GetComponent<levelmanager>();
        selected_color = Color_Codes[(int)GemColor];
        selected_light = Light_Color_Codes[(int)GemColor];
        GemSprite.color = selected_color;
        GemLight.color = selected_light;
        dynamicLight.SetActive(false);
        gem_area_sound = transform.parent.gameObject.GetComponent<AudioSource>();


        GemLight.intensity = StartingLightIntensity;
        GemLight.range = StartingLightRange;
        //InitialIntensity = GemLight.intensity;
        //InitialRange = GemLight.range;
    }
    private void GetIncreaseFactor()
    {
        switch (this.tag)
        {
            case ("levelGem"):
                IncreaseFactor = 0.075f;
                break;
            default:
                IncreaseFactor = 0.05f;
                break;
        }
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

    }

    protected IEnumerator Charge()
    {
        float runningTime = 0;
        chargingGem = true;
        bool doneWithGem = false;
        while (!doneWithGem)
        {
            if (playerIn || hitByRay) // Charge Gem, increase light intesity and gem_power
            {
                if (hitByRay)
                {
                    hitByRay = false;
                }
                runningTime += Time.deltaTime;
                if (!GemLight) InitializeGemValues();
                GemLight.range = Mathf.Lerp(StartingLightRange, EndingLightRange, runningTime);
                GemLight.intensity = Mathf.Lerp(StartingLightIntensity, EndingLightIntesity, runningTime);
                if (runningTime >= timeToCharge)
                {
                    gem_isPowered = true;
                    PowerUp();
                    doneWithGem = true;
                }
            }
            else // Stop Chargin gem, drain its power. Decrease light intensity and gem_power
            {
                runningTime -= Time.deltaTime;
                GemLight.range = Mathf.Lerp(StartingLightRange, EndingLightRange, runningTime);
                GemLight.intensity = Mathf.Lerp(StartingLightIntensity, EndingLightIntesity, runningTime);
                if (runningTime <= 0)
                {
                    doneWithGem = true;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
        chargingGem = false;

    }

    protected IEnumerator Fade(Light light, float endIntensity)
    {
        float currentIntensity = light.intensity;
        float time = 0;// timeToCharge;
        while (true)
        {
            if (time >= timeToCharge)
            {
                break;
            }
            time += Time.deltaTime;
            light.intensity = Mathf.Lerp(currentIntensity, endIntensity, time);
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
        //StartCoroutine(Fade(GemLight, (GemLight.intensity * 1.25f)));
        if (gem_area_sound) gem_area_sound.Stop();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!gem_isPowered)
            {
                //poweringRecharge = Time.time;
                //GemLight.enabled = true;
                playerIn = true;
                if (!chargingGem) { StartCoroutine(Charge()); }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!gem_isPowered)
            {
                //poweringRecharge = 0;
                playerIn = false;
            }
        }
    }

    public void OnHitRay()
    {
        if (!gem_isPowered)
        {
            //poweringRecharge = Time.time;
            hitByRay = true;
            if (!chargingGem) { StartCoroutine(Charge()); }
        }
    }
}