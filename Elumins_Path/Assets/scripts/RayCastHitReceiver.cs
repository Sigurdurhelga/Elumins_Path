using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class RayCastHitReceiver : MonoBehaviour
{

    //public GameObject objectHit;
    public void OnHitRay(string tag, string BeamCasterName)
    {
        GemParent gem = null;
        switch (tag)
        {
            case ("levelGem"):
                gem = gameObject.GetComponent<LevelGem>();
                break;
            case ("RedGem"):
                gem = gameObject.GetComponent<RedRayGem>();
                break;
            case ("TwinGem"):
                gem = gameObject.GetComponent<TwinCrystal>();
                break;
            case ("DarkLevelGem"):
                gem = gameObject.GetComponent<DarkLevelGem>();
                break;
            default:
                gem = gameObject.GetComponent<LevelGem>();
                break;
        }
        if (gem == null)
        {
            gem = gameObject.GetComponent<LevelGem>();
        }
        gem.OnHitRay(BeamCasterName);
    }



}
