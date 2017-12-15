using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{

    public GameObject PauseMenu;
    private bool gamePaused;
    private bool adjustedSlider;
    private GameController controller;

    private void Start()
    {
        controller = GameController.instance;
        if (PauseMenu)
        {
            gamePaused = PauseMenu.activeSelf;
        }
        else
        {
            gamePaused = false;
        }
        UnPauseGame();
        adjustedSlider = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("p") || Input.GetKeyUp("escape"))
        {
            TogglePause();
        }
        if (Input.GetKeyUp("r") && SceneManager.GetActiveScene().name != "Level_Transitioner")
        {
            RestartLevel();
        }
    }

    public void ContinueGameButton()
    {
        UnPauseGame();
    }

    public void ExitLevelButton()
    {
        UnPauseGame();
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level_Transitioner"))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("StartMenu");
        }
        else
        {
            controller.LoadWorldTree("");
        }
    }

    public void RestartLevelButton()
    {
        RestartLevel();
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
            if (!adjustedSlider)
            {
                GameObject volumeSlider = GameObject.Find("VolumeSlider");
                Slider slider = volumeSlider.GetComponent<Slider>();
                SettingsMenuScript settings = gameObject.GetComponent<SettingsMenuScript>();
                settings.SetSlider(slider);
                adjustedSlider = true;
            }
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

    private void RestartLevel()
    {
        // Reload level
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
    }

}
