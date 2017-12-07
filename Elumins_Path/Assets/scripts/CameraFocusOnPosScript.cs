using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraSmooth2D;

public class CameraFocusOnPosScript : MonoBehaviour {

    private Transform camera_transform;
    private CameraManager camera_script;

    private bool playerIn = false;

	// Use this for initialization
	void Start () {
        camera_transform = Camera.main.transform;
        camera_script = camera_transform.GetComponent<CameraManager>();

	}

    IEnumerator smoothMoveCamera()
    {
        Vector3 source = camera_transform.position;
        float pos = 0;
        while (pos < 1)
        {
            pos += 0.01f;
            camera_transform.position = Vector3.Lerp(source, transform.position, pos);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hey");
        if(collision.transform.tag == "Player")
        {
            Debug.Log("Player entered");
            playerIn = true;
            camera_script.enabled = false;
            StartCoroutine(smoothMoveCamera());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log("Player exited");
            playerIn = false;
            camera_script.enabled = true;
        }
    }
}
