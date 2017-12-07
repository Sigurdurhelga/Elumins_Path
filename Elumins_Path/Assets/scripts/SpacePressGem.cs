using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacePressGem : MonoBehaviour
{
    public GameObject SpaceInstruction;
    public GameObject QInstruction;
    public GameObject EInstruction;

    private bool player_in_zone;


    // Use this for initialization
    void Start()
    {
        player_in_zone = false;
        if (SpaceInstruction) SpaceInstruction.SetActive(false);
        if (QInstruction) QInstruction.SetActive(false);
        if (EInstruction) EInstruction.SetActive(false);
    }
    public void CollisionEnter(Collider2D other)
    {
        if (other.name == "Orb")
        {
            player_in_zone = true;
            SpaceInstruction.SetActive(true);
        }
    }
    public void CollisionStay(Collider2D other)
    {
        if (other.name == "Orb")
        {
            QInstruction.SetActive(true);
            EInstruction.SetActive(true);
            SpaceInstruction.SetActive(false);
        }
    }

    public void CollisionExit(Collider2D other)
    {
        if (other.name == "Orb")
        {
            player_in_zone = false;
            SpaceInstruction.SetActive(false);
            QInstruction.SetActive(false);
            EInstruction.SetActive(false);
        }
    }
    void FixedUpdate()
    {

    }

}
