using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    private bool player_in_zone;

    private GameObject space_bar;
    private string level;
    GameController controller;

    // Use this for initialization
    void Start()
    {
        controller = GameController.instance;
        player_in_zone = false;
        space_bar = transform.GetChild(0).gameObject;
        space_bar.SetActive(false);
        level = this.name;
        if(level == "1")
        {
            level = "0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && player_in_zone)
        {
            if (SceneManager.GetActiveScene().name == "Level_Transitioner")
            {
                controller.LoadNextLevel(level);
            }
            else
            {
                controller.LoadWorldTree(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Orb")
        {
            if (!GameObject.FindGameObjectWithTag("Disabled_Portal"))
            {
                player_in_zone = true;
                if(this.enabled == true)
                {
                    space_bar.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Orb")
        {
            player_in_zone = false;
            space_bar.SetActive(false);
        }
    }
}
