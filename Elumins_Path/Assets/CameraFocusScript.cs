using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraSmooth2D;

public class CameraFocusScript : MonoBehaviour {

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
        Vector3 target = Vector3.zero;
        while (playerIn)
        {
            camera_transform.position = Vector3.Lerp(source, target, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log("Player exited");
            playerIn = false;
            camera_script.enabled = true;
        }
    }


}
