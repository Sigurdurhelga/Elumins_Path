using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private int CurrentLevel;
    // Use this for initialization
    void Start()
    {
        CurrentLevel = 1;

    }

    public void OnLevelWon()
    {
        //Run some level won text, subroutine and update main view.
    }
}
