using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusScript : MonoBehaviour
{

    public GameObject startPos;
    public GameObject endPos;
    public float zoomPosition;
    private Transform camera_transform;
    private DeadZoneCamera camera_script;
    private Transform playerRef;
    private Vector3 originalStart;
    private Vector3 originalEnd;


    private bool playerIn = false;

    // Use this for initialization
    void Start()
    {
        camera_transform = Camera.main.transform;
        camera_script = camera_transform.GetComponent<DeadZoneCamera>();
        originalStart = startPos.transform.position;
        originalEnd = endPos.transform.position;
    }

    IEnumerator smoothZoomCamera(float newZoom)
    {
        float difference = newZoom - Camera.main.orthographicSize;
        float time = 0;
        float timestep = 0.01f;
        float nsteps = 1 / timestep;
        float step = difference / nsteps;
        while (time < 1)
        {
            Camera.main.orthographicSize += step;
            time += timestep;
            yield return new WaitForSeconds(timestep);
        }
    }
    IEnumerator smoothMoveCamera(GameObject source, GameObject dest)
    {
        Vector3 source3;
        Vector3 dest3;

        float difference = 1; // 1 as in lerp is from [0,1] so difference from 0 to 1 is 1
        float time = 0;
        float timestep = 0.01f;
        float nstep = 1 / timestep;
        float step = difference / nstep;
        float position = 0;
        while (time < 1)
        {
            source3 = source.transform.position;
            dest3 = dest.transform.position;
            source3.z = -10;
            dest3.z = -10;
            position += step;
            Camera.main.transform.position = Vector3.Lerp(source3, dest3, position);
            time += timestep;
            yield return new WaitForSeconds(timestep);
        }
        if (!playerIn)
        {
            Debug.Log("enabling camre");
            camera_script.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.name == "Crystal_Reflective")
        {
            if (!playerRef)
                playerRef = collision.transform;
            playerIn = true;
            camera_script.enabled = false;
            endPos.transform.position = originalEnd;
            startPos.transform.position = Camera.main.transform.position;
            StopAllCoroutines();
            StartCoroutine(smoothMoveCamera(startPos, endPos));
            StartCoroutine(smoothZoomCamera(zoomPosition));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.name == "Crystal_Reflective")
        {
            playerIn = false;
            startPos.transform.position = originalStart;
            endPos.transform.position = Camera.main.transform.position;
            StopAllCoroutines();
            StartCoroutine(smoothMoveCamera(endPos, playerRef.gameObject));
            StartCoroutine(smoothZoomCamera(7));
        }
    }


}
