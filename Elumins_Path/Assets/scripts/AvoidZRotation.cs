using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidZRotation : MonoBehaviour
{

    Quaternion rotation;
    void Awake()
    {
        rotation = transform.rotation;
    }
    void FixedUpdate()
    {
        transform.rotation = rotation;
    }
}
