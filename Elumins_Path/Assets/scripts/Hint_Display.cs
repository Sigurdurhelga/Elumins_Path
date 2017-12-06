using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint_Display : MonoBehaviour {


    public GameObject e_button;
    public GameObject q_button;

    // Use this for initialization
    void Start () {
        e_button.SetActive(false);
        q_button.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Orb")
        {
            e_button.SetActive(true);
            q_button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Orb")
        {
            e_button.SetActive(false);
            q_button.SetActive(false);
        }
    }
}
