using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CameraSmooth2D;

public class PlayerController : MonoBehaviour
{

    public float speed;             //Floating point variable to store the player's movement speed.

    public GameObject RedGem;

    private Vector3 PortalPosition;
    private bool IsCrystal;
    private bool EnterableCrystal;
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    private Rigidbody2D Reflected_body;

    private Collider2D reflect_collider;

    private bool SpacePressed = false;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        PortalPosition.x = 2.0f;
        PortalPosition.y = 2.0f;
        PortalPosition.z = 0f;
        IsCrystal = false;
        reflect_collider = GameObject.FindGameObjectWithTag("Reflective_Gem").GetComponent<PolygonCollider2D>();
        EnterableCrystal = false;
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SpacePressed = true;
        }
    }
    void FixedUpdate()
    {
        if (IsCrystal && SpacePressed == true)
        {
            reflect_collider.gameObject.transform.parent = null;
            IsCrystal = false;
            reflect_collider.isTrigger = true;
            Reflected_body.mass = 10000;
            gameObject.transform.position = reflect_collider.gameObject.transform.position;
            Transform[] children = reflect_collider.transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in children)
            {
                if (child.parent == reflect_collider.transform) child.parent = transform;
            }

            gameObject.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<CameraManager>().focusObject = gameObject;
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            SpacePressed = false;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb2d.AddForce(movement * speed);

        if (IsCrystal)
        {
            Reflected_body.AddForce(movement * speed);
            if (Input.GetKey(KeyCode.Q))
            {
                reflect_collider.gameObject.transform.Rotate(new Vector3(0, 0, 90) * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                reflect_collider.gameObject.transform.Rotate(new Vector3(0, 0, -90) * Time.deltaTime);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Reflective_Gem")
        {
            if (col.transform.parent == null && SpacePressed == true)
            {
                gameObject.GetComponentInChildren<ParticleSystem>().Stop();
                col.gameObject.transform.parent = transform;
                IsCrystal = true;
                col.isTrigger = false;
                Reflected_body = col.GetComponent<Rigidbody2D>();
                Reflected_body.mass = gameObject.GetComponent<Rigidbody2D>().mass;
                gameObject.GetComponent<Collider2D>().enabled = false;
                gameObject.GetComponent<Collider2D>().isTrigger = false;
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;

                Transform[] children = transform.GetComponentsInChildren<Transform>();
                foreach (Transform child in children)
                {
                    if (child.parent == transform) child.parent = col.gameObject.transform;
                }
                GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
                camera.GetComponent<CameraManager>().focusObject = col.gameObject;
                SpacePressed = false;

            }
        }
    }
}
