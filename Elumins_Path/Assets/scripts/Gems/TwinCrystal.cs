using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;
using System.Collections;
public class TwinCrystal : GemParent
{
    private GameObject Brother;
    private GameObject MyWall;
    public bool Active;
    public override void Start()
    {
        base.Start();
        Active = false;
        if (tag == "Brother1") Brother = transform.parent.parent.GetChild(1).GetChild(2).gameObject;
        else Brother = transform.parent.parent.GetChild(1).GetChild(2).gameObject;
        if (tag == "Brother1") Brother = GameObject.FindGameObjectWithTag("Brother2");
        else Brother = GameObject.FindGameObjectWithTag("Brother1");
        MyWall = transform.parent.GetChild(5).gameObject;
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
        StartCoroutine(FadeOutWallColor(0.2f));
    }
    public void ShutDown()
    {
        Active = false;
        StartCoroutine(FadeOut(GemLight, 0));
        StartCoroutine(FadeInWallColor(1f));
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
    private IEnumerator FadeOutWallColor(float EndIntensity)
    {
        SpriteRenderer WallSprite = MyWall.GetComponent<SpriteRenderer>();
        Color WallColor = WallSprite.color;
        while (WallColor.a > EndIntensity)
        {
            WallColor.a -= 0.05f;
            WallSprite.color = WallColor;
            yield return new WaitForSeconds(0.05f);
        }
        MyWall.GetComponent<PolygonCollider2D>().enabled = false;
    }
    private IEnumerator FadeInWallColor(float EndIntensity)
    {
        MyWall.GetComponent<PolygonCollider2D>().enabled = true;
        SpriteRenderer WallSprite = MyWall.GetComponent<SpriteRenderer>();
        Color WallColor = WallSprite.color;
        while (WallColor.a < EndIntensity)
        {
            WallColor.a += 0.05f;
            WallSprite.color = WallColor;
            yield return new WaitForSeconds(0.05f);
        }

    }

}