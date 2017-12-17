using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;
using System.Timers;
using System.Diagnostics;

public class DarkLevelGem : GemParent
{
    private GameObject[] fragments;
    private SpriteRenderer[] fragments_sprite;

    private SpriteRenderer[] decoration_gems_sprites;

    private GameObject[] BeamSources;

    private Dictionary<string, Stopwatch> Beams;

    private Dictionary<string, Stopwatch> TimeOfActivationBeams;

    private ParticleSystem smoke;
    private int NrOfRayHits = 0;

    private int CurrentIndex;
    private bool RayHit = false;

    private bool RayMiss = false;

    private bool levelWon = false;

    private bool FadeOut_Active = false;

    private bool FadeIn_Active = false;

    public override void Start()
    {
        base.Start();
        fragments = new GameObject[4];
        fragments_sprite = new SpriteRenderer[4];
        decoration_gems_sprites = new SpriteRenderer[4];
        BeamSources = GameObject.FindGameObjectsWithTag("RedBeamSource");
        GameObject[] decoration_gems = GameObject.FindGameObjectsWithTag("Decoration_Level_Gems");
        for (int i = 0; i < 4; i++)
        {
            fragments[i] = transform.parent.GetChild(4).transform.GetChild(i).gameObject;
            fragments_sprite[i] = fragments[i].GetComponent<SpriteRenderer>();
            Color WallColor = fragments_sprite[i].color;
            WallColor.a = 0;
            fragments_sprite[i].color = WallColor;
            fragments[i].SetActive(false);
            decoration_gems_sprites[i] = decoration_gems[i].GetComponent<SpriteRenderer>();
            decoration_gems_sprites[i].color = WallColor;
        }
        Beams = new Dictionary<string, Stopwatch>();
        smoke = transform.parent.GetChild(6).GetComponent<ParticleSystem>();
    }
    public void Update()
    {
        if (CurrentIndex > 0 && !levelWon && !RayHit && !FadeIn_Active)
        {
            List<string> removeable = new List<string>();
            foreach (KeyValuePair<string, Stopwatch> temp in Beams)
            {
                if (((float)temp.Value.Elapsed.Milliseconds) > 300)
                {
                    StartCoroutine(FadeOutFragments());
                    removeable.Add(temp.Key);
                    break;
                }
            }
            foreach (var str in removeable)
            {
                Beams.Remove(str);
                NrOfRayHits--;
            }
        }
    }
    public override void OnHitRay(string BeamCasterName)
    {
        if (!levelWon && !FadeOut_Active)
        {
            GetActiveBeamCaster(BeamCasterName);
            if (RayHit && NrOfRayHits <= 4)
            {
                RayHit = false;
                fragments[CurrentIndex].SetActive(true);
                StartCoroutine(FadeInFragments(BeamCasterName));
            }
        }


    }
    private IEnumerator FadeInFragments(string BeamCasterName)
    {
        FadeIn_Active = true;
        Color WallColor = fragments_sprite[CurrentIndex].color;
        while (WallColor.a < 1f)
        {
            WallColor.a += 0.05f;
            fragments_sprite[CurrentIndex].color = WallColor;
            yield return new WaitForSeconds(0.05f);
        }
        CurrentIndex++;
        if (NrOfRayHits == 4)
        {
            levelWon = true;
            GemLight.color = Light_Color_Codes[(int)DropDownGemColors.blue];
            StartCoroutine(FadeInDecorations());
            smoke.Stop();
            StartCoroutine(ChargeGem());
        }
        FadeIn_Active = false;
    }
    private IEnumerator FadeOutFragments()
    {
        FadeOut_Active = true;
        Color WallColor = fragments_sprite[CurrentIndex - 1].color;
        while (WallColor.a > 0f)
        {
            WallColor.a -= 0.05f;
            fragments_sprite[CurrentIndex - 1].color = WallColor;
            yield return new WaitForSeconds(0.05f);
        }
        fragments[CurrentIndex - 1].SetActive(false);
        CurrentIndex--;
        FadeOut_Active = false;
    }
    private IEnumerator FadeInDecorations()
    {
        Color WallColor = decoration_gems_sprites[0].color;
        while (WallColor.a < 1f)
        {
            WallColor.a += 0.05f;
            for (int i = 0; i < 4; i++)
            {
                decoration_gems_sprites[i].color = WallColor;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    private void GetActiveBeamCaster(string BeamCasterName)
    {

        if (!Beams.ContainsKey(BeamCasterName))
        {
            Stopwatch newguy = new Stopwatch();
            newguy.Start();
            Beams.Add(BeamCasterName, newguy);
        }
        else
        {
            Beams[BeamCasterName].Reset();
            Beams[BeamCasterName].Start();
        }
        int NewNumberOfRays = Beams.Count;
        if (NewNumberOfRays > NrOfRayHits)
        {
            RayHit = true;
            NrOfRayHits = NewNumberOfRays;
        }
    }
    public override void PowerUp()
    {
        GemSprite.enabled = false;
        levelman.gemActivated();
        if (success_sound) success_sound.Play();
        GemLight.color = Light_Color_Codes[(int)DropDownGemColors.blue];
        if (gem_area_sound) gem_area_sound.Stop();
    }
    protected IEnumerator ChargeGem()
    {
        hitByRay = true;
        float runningTime = 0;
        chargingGem = true;
        bool doneWithGem = false;
        while (!doneWithGem)
        {
            if (playerIn || hitByRay) // Charge Gem, increase light intesity and gem_power
            {
                runningTime += Time.deltaTime;
                GemLight.range = Mathf.Lerp(StartingLightRange, EndingLightRange, runningTime);
                GemLight.intensity = Mathf.Lerp(StartingLightIntensity, EndingLightIntesity, runningTime);
                if (runningTime >= timeToCharge)
                {
                    gem_isPowered = true;
                    base.PowerUp();
                    doneWithGem = true;
                    hitByRay = false;
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


}