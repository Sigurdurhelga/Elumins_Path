using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;             //Floating point variable to store the player's movement speed.

    public GameObject RedGem;

    private Vector3 PortalPosition;
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        PortalPosition.x = 2.0f;
        PortalPosition.y = 2.0f;
        PortalPosition.z = 0f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BlueGem")
        {
            GameObject red = GameObject.FindGameObjectWithTag("RedGem");
            if (red != null)
            {
                Destroy(red);
            }
            else
            {
                red = (GameObject)Instantiate(Resources.Load("Gem"), PortalPosition, Quaternion.identity);
                red.SetActive(true);
            }
        }
    }
}
