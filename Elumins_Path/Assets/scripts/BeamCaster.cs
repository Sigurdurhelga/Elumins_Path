using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCaster : MonoBehaviour {

    private LineRenderer beamRender;
    private LayerMask layer;
    private bool DEBUG = false;

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
        Vector3[] points = GetBeamPositions(origin, direction);
        
        // Number of position needs to be set before passing in points, otherwise exess points are ignored
        beamRender.positionCount = points.Length;
        beamRender.SetPositions(points);
    }

    private Vector3[] GetBeamPositions(Vector2 origin, Vector2 direction)
    {
        // Since C# copies by reference create copy of input parameters
        Vector2 rayDirection = new Vector2(direction.x, direction.y);
        Vector2 raySource = new Vector2(origin.x, origin.y);

        // Create a list to hold all points in of the beam, initialize with origin point already
        List<Vector3> pointList = new List<Vector3>() { new Vector3(raySource.x, raySource.y)};

        // Get array of RaycastHit
        RaycastHit2D[] hit = Physics2D.RaycastAll(raySource, rayDirection, distance, layer);

        for (int i = 0; i < numberOfReflections; i++)
        {
            if(hit.Length <= 0)
            {
                if(DEBUG) Debug.Log("No collision");
                DrawNonCollidingRay(raySource, rayDirection);
                pointList.Add(new Vector3(raySource.x + rayDirection.x * distance, raySource.y + rayDirection.y * distance));
                break;
            }

            for (int j = 0; j < hit.Length; j++)
            {
                // If raycast hit no collider then draw a beam of set length and break
                if (hit[j].collider == null && hit[j].fraction != 0)
                {
                    if (DEBUG) Debug.Log("No collision");
                    DrawNonCollidingRay(raySource, rayDirection);
                    pointList.Add(new Vector3(raySource.x + rayDirection.x * distance, raySource.y + rayDirection.y * distance));

                    // To break outer loop, set i = numberOfReflections
                    i = numberOfReflections;
                    // To break inner loop call break
                    break;
                }
                else if (hit[j].fraction == 0) // Skip this hit as ray started inside collider
                {
                    if (DEBUG) Debug.Log("Inside collider, index: " + j.ToString());
                    if(j+1 == hit.Length)
                    {
                        if (DEBUG) Debug.Log("Doesn't collide with anything else");
                        DrawNonCollidingRay(raySource, rayDirection);
                        pointList.Add(new Vector3(raySource.x + rayDirection.x * distance, raySource.y + rayDirection.y * distance));
                    }
                    continue;
                }
                else if (hit[j].collider.CompareTag("LightThrough"))
                {
                    if (DEBUG) Debug.Log("Went through a block");
                    if (j + 1 == hit.Length)
                    {
                        if (DEBUG) Debug.Log("Doesn't collide with anything else");
                        DrawNonCollidingRay(raySource, rayDirection);
                        pointList.Add(new Vector3(raySource.x + rayDirection.x * distance, raySource.y + rayDirection.y * distance));
                    }
                    continue;
                }
                else if (hit[j].collider.CompareTag("LightReflect")) // If raycast hits a collider with the tag "LightReflect" then create a new raycast that represents reflection from hit
                {
                    if (DEBUG) Debug.Log("Beam reflected");
                    Debug.DrawLine(raySource, hit[j].point, Color.red);

                    raySource = hit[j].point;// + hit[j].normal * 0.01f; // Deprecated to fix non reflection 
                    pointList.Add(new Vector3(raySource.x, raySource.y));

                    rayDirection = Vector2.Reflect(rayDirection, hit[j].normal);

                    hit = Physics2D.RaycastAll(raySource, rayDirection, distance, layer);
                }
                else if (hit[j].collider.CompareTag("LightBlock"))
                {
                    if (DEBUG) Debug.Log("Beam blocked");
                    Debug.DrawLine(raySource, hit[j].point, Color.red);

                    raySource = hit[j].point;
                    pointList.Add(new Vector3(raySource.x, raySource.y));

                    // To break outer loop, set i = numberOfReflections
                    i = numberOfReflections;
                    // To break inner loop call break
                    break;
                }
                else // The catch all statement does the same as LightBlock
                {
                    if (DEBUG) Debug.Log("Catch all");
                    Debug.DrawLine(raySource, hit[j].point, Color.red);

                    raySource = hit[j].point; // Need to move raySource to avoid colliding with source. 
                    pointList.Add(new Vector3(raySource.x, raySource.y));

                    // To break outer loop, set i = numberOfReflections
                    i = numberOfReflections;
                    // To break inner loop call break
                    break;
                }
            }
        }

        Vector3[] points = new Vector3[pointList.Count];

        // Enter points into array
        for (int i = 0; i < pointList.Count; i++)
        {
            points[i] = pointList[i];
        }
        if (DEBUG) Debug.Log("pointList size " + pointList.Count.ToString());
        if (DEBUG) Debug.Log("Points array size " + points.Length.ToString());
        return points;
    }

    void DrawNonCollidingRay(Vector3 origin, Vector3 direction)
    {
        Debug.DrawLine(origin, origin + direction * distance, Color.yellow);
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
