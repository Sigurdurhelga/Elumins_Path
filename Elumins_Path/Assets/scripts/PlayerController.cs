using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraSmooth2D;

public class PlayerController : MonoBehaviour
{
    public float speed;             //Floating point variable to store the player's movement speed.
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    Vector2 GetMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        return movement;
    }
    void MovePlayer()
    {
        Vector2 movement = GetMovement();
        rb2d.AddForce(movement * speed);
    }
}

