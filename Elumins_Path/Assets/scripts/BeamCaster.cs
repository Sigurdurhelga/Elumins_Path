using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCaster : MonoBehaviour {

    private LineRenderer beamRender;
    private Vector3 hitPoint;

    public float distance;

	// Use this for initialization
	void Start ()
    {
        beamRender = GetComponent<LineRenderer>();
        beamRender.enabled = true;
        beamRender.useWorldSpace = true;
	}

    // Update is called once per frame
    /*
    void Update ()
    {
		
	}
    */

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance);

        if(hit.collider == null)
        {
            Debug.DrawLine(transform.position, transform.position + transform.up * distance);
            hitPoint = transform.position + transform.up * distance;
        }
        else
        {
            Debug.DrawLine(transform.position, hit.point);
            hitPoint = hit.point;
        }

        beamRender.SetPosition(0, transform.position);
        beamRender.SetPosition(1, hitPoint);

        //Debug.Log("Hit" + hit.point.ToString());
    
    }
}
