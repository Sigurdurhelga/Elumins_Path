using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour {

    public GameObject PauseMenu;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("p"))
        {
            if (PauseMenu != null)
            {
                bool active = PauseMenu.activeSelf;
                PauseMenu.SetActive(!active);
            }
        }
    }

    public void ContinueGameButton()
    {

    }

    public void ExitLevelButton()
    {

    }
}
