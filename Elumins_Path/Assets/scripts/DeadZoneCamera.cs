using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneCamera : MonoBehaviour {

	public float SmoothSpeed;
	//public float deadZoneSize;
	/*
		rx and ry are needed for an elipses boundary check.
	 */
	public float rx;
	public float ry;
	public Transform target;

	private float squared_rx;
	private float squared_ry;
	private bool playerLeft = false;

	private void Start(){
		squared_rx = Mathf.Pow(rx,2);
		squared_ry = Mathf.Pow(ry,2);
	}
	void FixedUpdate () {

		Vector2 targetPos2 = target.position;
		Vector2 cameraPos2 = transform.position;
		//float distance = Vector3.Distance(targetPos2,cameraPos2);
		float distance = Mathf.Pow(transform.position.x - target.position.x,2)/squared_rx + Mathf.Pow(transform.position.y - target.position.y,2)/squared_ry;

		if(playerLeft || distance >= 1){
			playerLeft = true;
			Vector3 desiredPos = new Vector3 (target.position.x, target.position.y, -10);
			Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, SmoothSpeed);
			transform.position = smoothedPos;
		}
		if(playerLeft && distance < 0.1)
			playerLeft = false;
	}
}
