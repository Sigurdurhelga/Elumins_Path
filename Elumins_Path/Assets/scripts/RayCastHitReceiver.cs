using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastHitReceiver : MonoBehaviour {

    //public GameObject objectHit;

    void Start()
    {
        
    }


    public void OnHitRay()
    {
        // do something hit by ray
        GemScript gems = gameObject.GetComponent<GemScript>();
        if (gems != null)
        {
            gems.OnHitRay();
        }
    }



}
