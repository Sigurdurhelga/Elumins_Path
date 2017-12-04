using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCaster : MonoBehaviour {

    private LineRenderer beamRender;

    public float distance;
    public int numberOfReflections = 5;

    // Use this for initialization
    void Start ()
    {
        beamRender = GetComponent<LineRenderer>();
        beamRender.enabled = true;
        beamRender.useWorldSpace = true;
	}

    private void FixedUpdate()
    {
        BounceAround(transform.position, transform.up);
        //NoBounce();
    }

    private void BounceAround(Vector2 origin, Vector2 direction)
    {
        Vector2 rayDirection = new Vector2(direction.x, direction.y);
        Vector2 raySource = new Vector2(origin.x, origin.y);

        List<Vector3> pointList = new List<Vector3>();
        pointList.Add(new Vector3(raySource.x, raySource.y));

        RaycastHit2D hit = Physics2D.Raycast(raySource, rayDirection, distance);
        for (int i = 0; i < numberOfReflections; i++)
        {
            if (hit.collider == null)
            {
                Debug.DrawLine(raySource, raySource + rayDirection * distance, Color.red);
                pointList.Add(new Vector3(raySource.x + rayDirection.x * distance, raySource.y + rayDirection.y * distance));
                break;
            }
            else
            {
                Debug.DrawLine(raySource, hit.point, Color.red);
                raySource = hit.point + hit.normal * 0.01f; // Need to move raySource to avoid colliding with source.
                pointList.Add(new Vector3(raySource.x, raySource.y));

                if(hit.collider.CompareTag("LightBlock"))
                {
                    break;
                }
                else if(hit.collider.CompareTag("LightTrigger"))
                {
                    Debug.Log("Hit trigger object");
                }

                rayDirection = Vector2.Reflect(rayDirection, hit.normal);
                hit = Physics2D.Raycast(raySource, rayDirection, distance);
            }
        }
        
        Vector3[] points = new Vector3[pointList.Count];

        for (int i = 0; i < pointList.Count; i++)
        {
            points[i] = pointList[i];
        }

        beamRender.positionCount = pointList.Count;
        beamRender.SetPositions(points);
    }

    private void NoBounce()
    {
        Debug.Log("NoBounce");
        Vector3 hitPoint;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance);
        if (hit.collider == null)
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
    }
}
