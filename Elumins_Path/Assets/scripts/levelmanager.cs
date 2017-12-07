using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelmanager : MonoBehaviour
{
    public int a;

    private List<GameObject> gems = new List<GameObject>();
    private ParticleSystem portal;
    private Light roomLight;

    int requiredGems;

    // Use this for initialization
    void Start()
    {
        portal = GetComponentInChildren<ParticleSystem>();
        roomLight = GetComponentInChildren<Light>();

        portal.Stop();
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
            roomLight.enabled = true;
            portal.Play();
            GameObject dis_port = GameObject.FindGameObjectWithTag("Disabled_Portal");
            dis_port.SetActive(false);
        }
    }

}
