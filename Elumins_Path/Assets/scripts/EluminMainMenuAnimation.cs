using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EluminMainMenuAnimation : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // EXAMPLE A: initialize with the preferences set in DOTween's Utility Panel
        DOTween.Init();
        Vector3 startpos = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 downy = new Vector3(transform.position.x, transform.position.y - 5f, 0);
        Vector3 downx = new Vector3(transform.position.x - 5f, transform.position.y - 5f, 0);
        Vector3 upy = new Vector3(transform.position.x - 5f, transform.position.y, 0);
        Vector3[] way = new Vector3[5];
        way[0] = startpos;
        way[1] = downy;
        way[2] = downx;
        way[3] = upy;
        way[4] = startpos;
        transform.DOPath(way, 5.0f, PathType.Linear, PathMode.Sidescroller2D).SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
