using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;
using System.Collections;
public class TwinCrystal : GemParent
{
    private GameObject Brother;
    public bool Active;
    public override void Start()
    {
        base.Start();
        Active = false;
        if (tag == "Brother1") Brother = GameObject.FindGameObjectWithTag("Brother2");
        else Brother = GameObject.FindGameObjectWithTag("Brother1");
        Debug.Log(base.GemLight);
    }
    public override void PowerUp()
    {
        if (Brother.gameObject.GetComponent<TwinCrystal>().Active)
        {
            Brother.gameObject.GetComponent<TwinCrystal>().ShutDown();
        }
        Active = true;
        base.PowerUp();
    }
    public void ShutDown()
    {
        Active = false;
        StartCoroutine(FadeOut(GemLight, 0));
        base.Start();
    }
    private IEnumerator FadeOut(Light light, float EndIntensity)
    {
        while (GemLight.intensity > 0)
        {
            light.intensity -= 0.5f;
            yield return new WaitForSeconds(0.01f);
        }
        dynamicLight.SetActive(false);
        transform.parent.GetChild(3).gameObject.SetActive(false);


    }

}