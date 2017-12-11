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
        CurrentLevel = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (isWorldTree)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            if (Player)
            {
                int multiplyer = CurrentLevel;
                if (CurrentLevel == 0) multiplyer = 1;
                float temp = (Player.transform.position.y + 7f * multiplyer);
                Vector3 newPos = new Vector3(Player.transform.position.x, temp, 0);
                Player.transform.position = newPos;
            }
            isWorldTree = false;
            CurrentLevel = 1;
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
        SceneManager.LoadScene(1);
        isWorldTree = true;
    }
    public void LoadNextLevel(int portal = -1)
    {
        if (portal == -1) SceneManager.LoadScene(++CurrentLevel);
        else
        {
            CurrentLevel = portal;
            SceneManager.LoadScene(CurrentLevel);
        }
    }
}
