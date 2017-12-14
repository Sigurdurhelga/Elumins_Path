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
        Sequence mySequence = DOTween.Sequence();
        DOTween.Init();
        Vector3 startpos = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 downy = new Vector3(transform.position.x - 150f, transform.position.y - 150f, 0);
        Vector3 downx = new Vector3(transform.position.x - 300f, transform.position.y, 0);
        Vector3 upy = new Vector3(transform.position.x - 150f, transform.position.y + 150, 0);
        Vector3 downy2 = new Vector3(transform.position.x + 150f, transform.position.y + 150f, 0);
        Vector3 downx2 = new Vector3(transform.position.x + 300f, transform.position.y, 0);
        Vector3 upy2 = new Vector3(transform.position.x + 150f, transform.position.y - 150, 0);
        Vector3[] way = new Vector3[9];
        way[0] = startpos;
        way[1] = downy;
        way[2] = downx;
        way[3] = upy;
        way[4] = startpos;
        way[5] = upy2;
        way[6] = downx2;
        way[7] = downy2;
        way[8] = startpos;
        transform.DOPath(way, 20.0f, PathType.CatmullRom, PathMode.Sidescroller2D).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);

        /* 
        mySequence.Append(transform.DOPath(way, 3.0f, PathType.CatmullRom, PathMode.Sidescroller2D));
        mySequence.Pause();//.SetLoops(-1, LoopType.Restart);
        startpos = new Vector3(transform.position.x, transform.position.y, 0);
        downy = new Vector3(transform.position.x + 150f, transform.position.y + 150f, 0);
        downx = new Vector3(transform.position.x + 300f, transform.position.y, 0);
        upy = new Vector3(transform.position.x + 150f, transform.position.y - 150, 0);
        way = new Vector3[5];
        way[0] = startpos;
        way[3] = downy;
        way[2] = downx;
        way[1] = upy;
        way[4] = startpos;
        mySequence.Append(transform.DOPath(way, 3.0f, PathType.CatmullRom, PathMode.Sidescroller2D));//.SetLoops(-1, LoopType.Restart);
        mySequence.SetLoops(-1, LoopType.Incremental);
        mySequence.Play();
        */

    }

    // Update is called once per frame
    void Update()
    {

    }
}
