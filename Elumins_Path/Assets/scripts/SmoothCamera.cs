using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{

    //The target or Orb
    public Transform target;
    //Resets the velocity
    public Vector3 velocity = Vector3.zero;
    //Time it takes to follow the target
    public float smoothTime = .15f;
    public bool YMaxEnabled = false;
    public float YMaxValue = 0;

    public bool YMinEnabled = false;
    public float YMinValue = 0;

    public bool XMaxEnabled = false;
    public float XMaxValue = 0;

    public bool XMinEnabled = false;
    public float XMinValue = 0;

    public float DeadZoneRadius = 3;

    public float speed = 3;
    private float XDifference;
    private float YDifference;

    private Vector3 MoveTemp;

    void Update()
    {
        //Target position
        Vector3 targetPos = target.position;

        XDifference = Mathf.Abs(target.transform.position.x - transform.position.x);
        YDifference = Mathf.Abs(target.transform.position.y - transform.position.y);

        if (XDifference >= DeadZoneRadius || YDifference >= DeadZoneRadius)
        {
            MoveTemp = target.transform.position;
            MoveTemp.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, MoveTemp, speed * Time.deltaTime);
        }
        /*
        else
        {

            //Verical manipulation
            if (YMinEnabled && YMaxEnabled)
            {
                targetPos.y = Mathf.Clamp(target.position.y, YMinValue, YMaxValue);
            }
            else if (YMinEnabled)
            {
                targetPos.y = Mathf.Clamp(target.position.y, YMinValue, target.position.y);
            }
            else if (YMaxEnabled)
            {
                Mathf.Clamp(target.position.y, target.position.y, YMaxValue);
            }
            //Horizontal
            if (XMinEnabled && XMaxEnabled)
            {
                targetPos.x = Mathf.Clamp(target.position.x, XMinValue, XMaxValue);
            }
            else if (XMinEnabled)
            {
                targetPos.x = Mathf.Clamp(target.position.x, XMinValue, target.position.x);
            }
            else if (XMaxEnabled)
            {
                Mathf.Clamp(target.position.x, target.position.x, XMaxValue);
            }
            //Align the camera and the target z position to match
            targetPos.z = transform.position.z;
            //using smooth damp to gradually change the camera position to the target based on the cameras transform velocity
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        }
        */

    }
}
