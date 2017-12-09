using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneCamera : MonoBehaviour {

	public float SmoothSpeed;
	public float deadZoneSize;
	public Transform target;

	private bool playerLeft = false;
	// LateUpdate is called once per frame after the Update function
	void FixedUpdate () {

		Vector2 targetPos2 = target.position;
		Vector2 cameraPos2 = transform.position;
		float distance = Vector3.Distance(targetPos2,cameraPos2);

		if(playerLeft || distance > deadZoneSize){
			playerLeft = true;
			Vector3 desiredPos = new Vector3 (target.position.x, target.position.y, -10);
			Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, SmoothSpeed);
			transform.position = smoothedPos;
		}
		if(playerLeft && distance < deadZoneSize *0.2)
			playerLeft = false;
	}
}
