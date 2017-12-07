using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public static GameController instance;

    private GameObject Player;
    private static int CurrentLevel;
    private static bool isWorldTree = false;
    // Use this for initialization
    void Start()
    {
        CurrentLevel = 1;
    }
    void Update()
    {
        if (isWorldTree)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            if (Player)
            {
                float temp = (Player.transform.position.y + 7f * CurrentLevel);
                Vector3 newPos = new Vector3(Player.transform.position.x, temp, 0);
                Player.transform.position = newPos;
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
    public void LoadWorldTree()
    {
        SceneManager.LoadScene(0);
        isWorldTree = true;
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(++CurrentLevel);
    }
}
