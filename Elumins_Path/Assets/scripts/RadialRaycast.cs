using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialRaycast : MonoBehaviour {

    public bool raycastAll;
    public int numberOfRays;
    public float degreeOffset;
    public float distance;
    private LayerMask layer;


    // Use this for initialization
    void Start ()
    {
        layer = LayerMask.GetMask("Light", "Impassable", "ShadowLayer");
    }

    private void FixedUpdate()
    {
        if (numberOfRays > 0)
        {
            RadialRaycaster();
        }
    }

    private void RadialRaycaster()
    {
        float radians = 2*Mathf.PI / numberOfRays;
        float radianOffset = degreeOffset / 180 * Mathf.PI;
        for (int i = 0; i < numberOfRays; i++)
        {
            float x = Mathf.Cos(i * radians + radianOffset);
            float y = Mathf.Sin(i * radians + radianOffset);
            Vector2 direction = new Vector2(x, y);

            if(raycastAll)
            {
                UseRaycastAll(direction);
            }
            else
            {
                UseRaycast(direction);
            }
        }
    }

    private void UseRaycast(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, layer);
        if (hit.collider != null)
        {
            if (hit.collider == gameObject.GetComponent<Collider2D>() || hit.fraction <= 0) // RayCast hit own collider
            {
                // Do nothing
            }
            else
            {
                ActivateRayCastHit(hit.collider);
                Debug.DrawLine(transform.position, hit.point, Color.blue);
            }
        }
        else
        {
            DrawRayLine(transform.position, direction);
        }
    }

    private void UseRaycastAll(Vector2 direction)
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, distance, layer);

        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider == gameObject.GetComponent<Collider2D>()) // RayCast hit own collider, skip it
                {
                    if (i + 1 >= hit.Length)
                    {
                        DrawRayLine(transform.position, direction);
                    }
                    continue;
                }
                else if (hit[i].fraction <= 0) // Raycast started inside collider other than self, skip it
                {
                    if (i+1 >= hit.Length)
                    {
                        DrawRayLine(transform.position, direction);
                    }
                    continue;
                }
                else
                {
                    ActivateRayCastHit(hit[i].collider);
                    Debug.DrawLine(transform.position, hit[i].point, Color.blue);
                    break;
                }
            }
        }
        else
        {
            DrawRayLine(transform.position, direction);
        }
    }

    private void DrawRayLine(Vector2 origin, Vector2 direction)
    {
        Debug.DrawLine(origin, origin + direction*distance, Color.red);
    }

    private void ActivateRayCastHit(Collider2D collider)
    {
        if (collider != null)
        {
            RayCastHitReceiver rayHit = collider.gameObject.GetComponent<RayCastHitReceiver>();
            if (rayHit != null)
            {
                rayHit.OnHitRay();
            }

        }
    }
}
