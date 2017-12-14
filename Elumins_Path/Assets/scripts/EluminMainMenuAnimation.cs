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
        Vector3 downx150y150 = new Vector3(transform.position.x - 150f, transform.position.y - 150f, 0);
        Vector3 downx300 = new Vector3(transform.position.x - 300f, transform.position.y, 0);
        Vector3 downx150upy150 = new Vector3(transform.position.x - 150f, transform.position.y + 150, 0);
        Vector3 upx150y150 = new Vector3(transform.position.x + 150f, transform.position.y + 150f, 0);
        Vector3 upx300y150 = new Vector3(transform.position.x + 300f, transform.position.y + 150f, 0);
        Vector3 upx300 = new Vector3(transform.position.x + 300f, transform.position.y, 0);
        Vector3 upx150downy150 = new Vector3(transform.position.x + 150f, transform.position.y - 150, 0);
        Vector3 upx150downy300 = new Vector3(transform.position.x + 150f, transform.position.y - 300, 0);
        Vector3 upx150 = new Vector3(transform.position.x + 150f, transform.position.y, 0);
        Vector3 downx300y150 = new Vector3(transform.position.x - 300f, transform.position.y - 150f, 0);
        Vector3 upx150y300 = new Vector3(transform.position.x + 150f, transform.position.y + 300f, 0);
        Vector3[] way = new Vector3[23];
        way[0] = startpos;
        way[1] = downx150y150;
        way[2] = downx300;
        way[3] = downx150upy150;
        way[4] = startpos;
        way[5] = upx150downy150;
        way[6] = upx300;
        way[7] = upx150y150;
        way[8] = startpos;
        way[9] = downx150y150;
        way[10] = downx300;
        way[11] = downx150upy150;
        way[12] = upx150y150;
        way[13] = upx300;
        way[14] = upx150downy150;
        way[15] = downx150y150;
        way[16] = downx300;
        way[17] = downx150upy150;
        way[18] = startpos;
        way[19] = upx150downy150;
        way[20] = upx300;
        way[21] = upx150y150;
        way[22] = startpos;

        transform.DOPath(way, 40.0f, PathType.CatmullRom, PathMode.Sidescroller2D).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);



    }

    // Update is called once per frame
    void Update()
    {

    }
}
