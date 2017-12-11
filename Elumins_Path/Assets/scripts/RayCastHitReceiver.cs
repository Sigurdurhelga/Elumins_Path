using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class RayCastHitReceiver : MonoBehaviour
{

    //public GameObject objectHit;
    public void OnHitRay(string tag)
    {
        GemParent gem = new GemParent();
        switch (tag)
        {
            case ("levelGem"):
                gem = gameObject.GetComponent<LevelGem>();
                break;
            case ("RedGem"):
                gem = gameObject.GetComponent<RedRayGem>();
                break;
        }
        gem.OnHitRay();
    }



}
