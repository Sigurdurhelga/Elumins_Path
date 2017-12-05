using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Proximity : MonoBehaviour {

	public Transform target;

	public AudioSource audio;

	
	void Start () {

		StartCoroutine(AdjustVolume());
    }
    
	IEnumerator AdjustVolume () {

		while(true) {

		    float distanceToTarget = Vector3.Distance(transform.position, target.position); // Assuming that the target is the player or the audio listener
                
		    if(distanceToTarget < 1) { distanceToTarget = 1; }
				
		    if (distanceToTarget > 10) {
			    audio.volume = 0;
		    } 
		    else 
		    {
			    audio.volume = 1/Mathf.Pow(distanceToTarget, 2); // this works as a linear function, while the 3D sound works like a logarithmic function, so the effect will be a little different (correct me if I'm wrong)
		    }
                
		    yield return new WaitForSeconds(0.01F); // this will adjust the volume based on distance every 1 second (Obviously, You can reduce this to a lower value if you want more updates per second)

		}

	}
}
