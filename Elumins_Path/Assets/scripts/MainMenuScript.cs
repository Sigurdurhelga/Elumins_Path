using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public void ContinueButton(string NewGameLevel)
    {
        SceneManager.LoadScene(NewGameLevel);
    }

    public void NewGameButton(string NewGameLevel)
    {
        SceneManager.LoadScene(NewGameLevel);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
