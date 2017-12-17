using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private GameObject Player;
    private GameObject mainCamera;
    private static int CurrentLevel;
    private static bool isWorldTree;
    private List<string> finished_levels;
    private List<string> bonus_levels = new List<string>{
        "level_bonus",
        "level_svess"
    };

    private static AudioSource MainAudio;

    private static Dictionary<string, int> MapNameToIndex;

    private bool firstPlay;

    // Use this for initialization
    void Start()
    {
        CurrentLevel = 0;
        isWorldTree = false;
        finished_levels = new List<string>();
        InitiliazeDict();
        if (SceneManager.GetActiveScene().name != "StartMenu")
        {
            Cursor.visible = false;
        }
        /*
        if (Application.isEditor == false)
        {
            if (PlayerPrefs.GetInt("FirstPlay", 1) == 1)
            {
                firstPlay = true;
                //PlayerPrefs.DeleteAll();
                //PlayerPrefs.SetInt("FirstPlay", 0);
                //PlayerPrefs.Save();
            }
            else
            {
                firstPlay = false;
            }
        }
        */
    }

    private void InitiliazeDict()
    {
        MapNameToIndex = new Dictionary<string, int>();
        MapNameToIndex["level_1_final"] = 1;
        MapNameToIndex["level_2_final"] = 3;
        MapNameToIndex["level_3_final"] = 4;
        MapNameToIndex["level_4_final"] = 5;
        MapNameToIndex["level_5_final"] = 6;
        MapNameToIndex["level_6_final"] = 7;
        MapNameToIndex["level_7_final"] = 8;
        MapNameToIndex["level_8_final"] = 9;
        MapNameToIndex["level_9_final"] = 10;
        MapNameToIndex["level_10_final"] = 11;
        MapNameToIndex["level_11_final"] = 12;
        MapNameToIndex["level_bonus"] = 13;
        MapNameToIndex["level_svess"] = 14;
    }

    void LateUpdate()
    {
        if (isWorldTree && SceneManager.GetActiveScene().name == "Level_Transitioner")
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (Player)
            {
                int current_level = CurrentLevel;
                int next_level = current_level + 1;
                if (current_level == 1) next_level++;

                PlayerPrefs.SetInt("CurrentLevel", current_level);
                PlayerPrefs.Save();

                GameObject[] levels = GameObject.FindGameObjectsWithTag("levelPortal");
                foreach (GameObject level in levels)
                {
                    if (Int32.Parse(level.name) <= current_level)
                    {
                        level.GetComponent<SpriteRenderer>().sprite = Resources.Load("Window_Open_Light_Blue", typeof(Sprite)) as Sprite;
                        level.GetComponentInChildren<Light>().enabled = true;
                    }
                    else if (level.name == next_level.ToString())
                    {
                        level.GetComponent<SpriteRenderer>().sprite = Resources.Load("Window_Open_Dark", typeof(Sprite)) as Sprite;
                    }
                    else
                    {
                        level.GetComponent<LevelLoader>().enabled = false;
                        level.GetComponent<SpriteRenderer>().sprite = Resources.Load("Window_Closed_Dark_Darker", typeof(Sprite)) as Sprite;
                    }

                    if (Int32.Parse(level.name) == CurrentLevel)
                    {
                        Player.transform.position = level.transform.position;
                        if (CurrentLevel > 1)
                        {
                            mainCamera.transform.position = new Vector3(level.transform.position.x, level.transform.position.y, -10);
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
        isWorldTree = true;
        if (level_finished != "")
        {
            if (!bonus_levels.Contains(level_finished) && !finished_levels.Contains(level_finished))
            {
                finished_levels.Add(level_finished);
                if (level_finished == "level_2_final") CurrentLevel += 2;
                else CurrentLevel++;
            }
        }
        SceneTransition(2);
    }
    public void LoadNextLevel(string level)
    {
        if (PlayerPrefs.GetInt("CurrentLevel", -1) == -1) CurrentLevel = 0;
        SceneTransition(Int32.Parse(level));
    }

    public void SceneChange(string sceneName)
    {
        int sceneIndex = SceneManager.GetSceneByName(sceneName).buildIndex;
        SceneTransition(sceneIndex);
    }

    public void SceneChange(int sceneIndex)
    {
        SceneTransition(sceneIndex);
    }

    private void SceneTransition(int level)
    {
        var fader = new FadeTransition()
        {
            nextScene = level,
            fadedDelay = 0.4f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate(fader);
    }
    public void ContinueGame()
    {
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", -1);
        if (CurrentLevel != -1)
        {
            finished_levels = new List<string>();
            for (int i = 0; i < CurrentLevel; i++)
            {
                finished_levels.Add(i.ToString());
            }
        }
        if (CurrentLevel == 0) CurrentLevel--;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        LoadWorldTree((CurrentLevel - 1).ToString());
    }

    public int GetCurrentLevel()
    {
        return CurrentLevel;
    }
}
