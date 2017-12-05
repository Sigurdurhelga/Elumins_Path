using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {


    private bool player_in_zone;

    public string level_to_load;

	// Use this for initialization
	void Start () {
        player_in_zone = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && player_in_zone)
        {
            Debug.Log("Pressed!");
            SceneManager.LoadScene(level_to_load);
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Orb")
        {
            player_in_zone = true;
            Debug.Log("Pressed!");
        }
        Debug.Log("Pressed!");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Orb")
        {
            player_in_zone = false;
        }
    }
}
