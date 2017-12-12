﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private GameController controller;

    public void Start()
    {
        controller = GameController.instance;
    }
    public void ContinueButton(string NewGameLevel)
    {
        controller.ContinueGame();
    }

    public void NewGameButton(string NewGameLevel)
    {
        string temp = "CurrentLevel";
        PlayerPrefs.SetInt(temp, 1);
        PlayerPrefs.Save();
        controller.LoadNextLevel(1.ToString());
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
