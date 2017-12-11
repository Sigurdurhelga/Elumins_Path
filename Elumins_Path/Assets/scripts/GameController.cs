using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static GameController instance;

    private GameObject Player;
    private GameObject camera;
    private static int CurrentLevel;
    private static bool isWorldTree;
    private List<string> finished_levels;

    // Use this for initialization
    void Start()
    {
        CurrentLevel = 0;
        isWorldTree = false;
        finished_levels = new List<string>();
    }

    void Update()
    {
        if (isWorldTree)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            camera = GameObject.FindGameObjectWithTag("MainCamera");
            if (Player)
            {

                int current_level = CurrentLevel;
                int next_level = current_level + 1;
                
                GameObject[] levels = GameObject.FindGameObjectsWithTag("levelPortal");
                foreach(GameObject level in levels)
                { 
                    if(Int32.Parse(level.name) <= current_level)
                    {
                        level.GetComponent<SpriteRenderer>().sprite = Resources.Load("Window_Open_Light", typeof(Sprite)) as Sprite;
                        level.GetComponentInChildren<Light>().enabled = true;
                    }
                    else if(level.name == next_level.ToString())
                    {
                        level.GetComponent<SpriteRenderer>().sprite = Resources.Load("Window_Open_Dark", typeof(Sprite)) as Sprite;
                    }
                    else
                    {
                        level.GetComponent<LevelLoader>().enabled = false;
                    }

                    if(Int32.Parse(level.name) == CurrentLevel)
                    {
                        Player.transform.position = level.transform.position;
                        if(CurrentLevel > 1)
                        {
                            camera.transform.position = new Vector3(level.transform.position.x, level.transform.position.y, -10);
                        }
                    }
                }
            }
            isWorldTree = false;
        }
    }
    /// <summary>Awake is called when the script instance is being loaded.</summary>
    void Awake()
    {
        // If the instance reference has not been set, yet, 
        if (instance == null)
        {
            // Set this instance as the instance reference.
            instance = this;
        }
        else if (instance != this)
        {
            // If the instance reference has already been set, and this is not the
            // the instance reference, destroy this game object.
            Destroy(gameObject);
        }

        // Do not destroy this object, when we load a new scene.
        DontDestroyOnLoad(gameObject);
    }

    public void LoadWorldTree(string level_finished)
    {
        SceneManager.LoadScene(1);
        isWorldTree = true;
        if(!finished_levels.Contains(level_finished))
        {
            finished_levels.Add(level_finished);
            CurrentLevel++;
        }
    }

    public void LoadNextLevel(string level)
    {
        SceneManager.LoadScene(Int32.Parse(level));
    }
}
