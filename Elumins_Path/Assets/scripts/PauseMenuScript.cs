using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour {

    public GameObject PauseMenu;
    private bool gamePaused;

    private void Start()
    {
        if (PauseMenu)
        {
            gamePaused = PauseMenu.activeSelf;
        }
        else
        {
            gamePaused = false;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("p") || Input.GetKeyDown("escape"))
        {
            TogglePause();
        }
    }

    public void ContinueGameButton()
    {
        UnPauseGame();
    }

    public void ExitLevelButton()
    {

    }

    private void TogglePause()
    {
        if (gamePaused)
        {
            UnPauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        gamePaused = true;
        Time.timeScale = 0;
        if (PauseMenu != null)
        {
            PauseMenu.SetActive(true);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void UnPauseGame()
    {
        gamePaused = false;
        Time.timeScale = 1;
        if (PauseMenu != null)
        {
            PauseMenu.SetActive(false);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
}
