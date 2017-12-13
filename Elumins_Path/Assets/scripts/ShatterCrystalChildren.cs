using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterCrystalChildren : MonoBehaviour
{

    private ShatteredCrystalScript parent;
    private GameObject SpaceBar;

    private bool PlayerInside = false;

    private void Start()
    {
        parent = transform.parent.GetComponent<ShatteredCrystalScript>();
        SpaceBar = transform.GetChild(0).gameObject;
        SpaceBar.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player")
        {
            parent.OnTriggerEnter2DChild(collider, gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            parent.OnTriggerStay2DChild(collider, gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player")
        {
            parent.OnTriggerExit2DChild(collider, gameObject);
        }
    }
}
