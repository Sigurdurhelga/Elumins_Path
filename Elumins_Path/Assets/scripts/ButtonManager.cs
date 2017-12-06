using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void ContinueButton(string NewGameLevel)
    {
        SceneManager.LoadScene(NewGameLevel);
    }

    public void NewGameButton(string NewGameLevel)
    {

    }

    public void CreditButton(string NewGameLevel)
    {

    }

    public void SettingButton(string NewGameLevel)
    {

    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
