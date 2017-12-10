using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusScript : MonoBehaviour {

    private Transform camera_transform;
    private DeadZoneCamera camera_script;
    private Transform playerRef;

    private bool playerIn = false;

	// Use this for initialization
	void Start () {
        camera_transform = Camera.main.transform;
        camera_script = camera_transform.GetComponent<DeadZoneCamera>();
	}

    IEnumerator smoothMoveCameraToPos()
    {
        Debug.Log("entered smooth to pos");
        Vector3 source = camera_transform.position;
        float pos = 0;
        while (pos < 1 && playerIn)
        {
            pos += 0.01f;
            camera_transform.position = Vector3.Lerp(source, transform.position, pos);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator smoothMoveCameraToPlayer()
    {
        Vector3 source = camera_transform.position;
        float pos = 0;
        Debug.Log("entered smooth to player" + pos);
        while (pos < 1)
        {
            pos += 0.01f;
            Debug.Log("loop smooth to player" + pos);
            Vector3 desiredPos = new Vector3(playerRef.position.x, playerRef.position.y, -10);
            camera_transform.position = Vector3.Lerp(source, desiredPos , pos);
            yield return new WaitForSeconds(0.01f);
        }
        camera_script.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if(!playerRef)
                playerRef = collision.transform;
            Debug.Log("plaer entered");
            playerIn = true;
            camera_script.enabled = false;
            StartCoroutine(smoothMoveCameraToPos());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log("plaer exit");
            playerIn = false;
            StopCoroutine(smoothMoveCameraToPos());
            StartCoroutine(smoothMoveCameraToPlayer());
        }
    }


}
