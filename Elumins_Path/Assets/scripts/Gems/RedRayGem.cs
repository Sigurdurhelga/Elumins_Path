using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicLight2D;
public class RedRayGem : GemParent
{
    public override void PowerUp()
    {
        //base.PowerUp();
        GameObject beam = gameObject.transform.parent.parent.gameObject; 
        beam.GetComponent<BeamCaster>().enabled = true;
    }

}