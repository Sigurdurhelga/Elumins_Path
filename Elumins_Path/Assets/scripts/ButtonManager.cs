using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void ContinueButton(string level_to_load)
    {
        SceneManager.LoadScene(level_to_load);
    }

    public void NewGameButton(string level_to_load)
    {
        // Clear saved values and start at level 1
        SceneManager.LoadScene(level_to_load);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
