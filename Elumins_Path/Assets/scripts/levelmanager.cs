using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelmanager : MonoBehaviour
{
    public int a;

    private List<GameObject> gems = new List<GameObject>();
    private GameObject level_window;
    private Light roomLight;

    int requiredGems;

    // Use this for initialization
    void Start()
    {
        level_window = GameObject.FindGameObjectWithTag("levelPortal");
        level_window.GetComponent<LevelLoader>().enabled = false;
        roomLight = GetComponentInChildren<Light>();

        foreach (GameObject gem in GameObject.FindGameObjectsWithTag("levelGem"))
        {
            gems.Add(gem);
        }
        Debug.Log("gems found" + gems.Count);
        requiredGems = gems.Count;
    }

    public void gemActivated()
    {
        Debug.Log("ACTIVATED!");
        requiredGems -= 1;
        if (requiredGems <= 0)
        {
            level_window.GetComponent<SpriteRenderer>().sprite = Resources.Load("Window_Open_Star", typeof(Sprite)) as Sprite;
            level_window.GetComponent<LevelLoader>().enabled = true;
            roomLight.enabled = true;
        }
    }

}
