using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCaster : MonoBehaviour {

    private LineRenderer beamRender;
    private LayerMask layer;

    public float distance;
    public int numberOfReflections;

    // Use this for initialization
    void Start ()
    {
        // Obtain LineRendere from game object
        beamRender = GetComponent<LineRenderer>();
        beamRender.enabled = true;
        beamRender.useWorldSpace = true;
        layer = LayerMask.GetMask("Light", "Impassable");
	}

    // Need to check if this is the right one, example used this one.
    private void FixedUpdate()
    {
        BounceAround(transform.position, transform.up);
    }

    // Beam that bounces around, needs origin point and initial direction
    private void BounceAround(Vector2 origin, Vector2 direction)
    {
        // Since C# copies by reference create copy of input parameters
        Vector2 rayDirection = new Vector2(direction.x, direction.y);
        Vector2 raySource = new Vector2(origin.x, origin.y);

        // Create a list to hold all points in of the beam
        List<Vector3> pointList = new List<Vector3>();

        // Add initial point
        pointList.Add(new Vector3(raySource.x, raySource.y));
        // Get first RaycastHit
        RaycastHit2D hit = Physics2D.Raycast(raySource, rayDirection, distance, layer);
        for (int i = 0; i < numberOfReflections; i++)
        {
            // If raycast hit no collider then draw a beam of set length and break
            if (hit.collider == null)
            {
                Debug.DrawLine(raySource, raySource + rayDirection * distance, Color.red);
                pointList.Add(new Vector3(raySource.x + rayDirection.x * distance, raySource.y + rayDirection.y * distance));
                break;
            }
            else // If raycast hits a collider then create a new raycast that represents reflection from hit
            {
                Debug.DrawLine(raySource, hit.point, Color.red);
                raySource = hit.point + hit.normal * 0.01f; // Need to move raySource to avoid colliding with source.
                pointList.Add(new Vector3(raySource.x, raySource.y));

                // If beam should not reflect from point, break
                if(hit.collider.CompareTag("LightBlock"))
                {
                    break;
                }
                else if(hit.collider.CompareTag("LightTrigger"))
                {
                    Debug.Log("Hit trigger object");
                }

                rayDirection = Vector2.Reflect(rayDirection, hit.normal);
                hit = Physics2D.Raycast(raySource, rayDirection, distance, layer);
            }
        }
        
        Vector3[] points = new Vector3[pointList.Count];

        // Enter points into array
        for (int i = 0; i < pointList.Count; i++)
        {
            points[i] = pointList[i];
        }
        
        // Number of position needs to be set before passing in points, otherwise exess points are ignored
        beamRender.positionCount = pointList.Count;
        beamRender.SetPositions(points);
    }

    // Initial Beam effect, no bounce
    private void NoBounce()
    {
        Debug.Log("NoBounce");
        Vector3 hitPoint;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, distance, layer);
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
