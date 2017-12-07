using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectorHandler : MonoBehaviour {
    public GameObject SpaceInstruction;
    public GameObject QInstruction;
    public GameObject EInstruction;
	private Rigidbody2D gem_rigidBody;
	private PolygonCollider2D gem_collider;
	private GameObject PlayerRef;
    private bool player_in_zone;
	private bool player_entered;
	private bool waiting = false;
    void Start()
    {
		gem_rigidBody = GetComponent<Rigidbody2D>();
		gem_collider = GetComponent<PolygonCollider2D>();
        player_in_zone = false; 
        if (SpaceInstruction) SpaceInstruction.SetActive(false);
        if (QInstruction) QInstruction.SetActive(false);
        if (EInstruction) EInstruction.SetActive(false);
    }

	void Update()
	{
		
		if(player_in_zone){
			if(Input.GetKeyDown(KeyCode.Space)){
				player_entered = true;
				PlayerRef.GetComponentInChildren<ParticleSystem>().Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
				PlayerRef.GetComponent<Rigidbody2D>().simulated = false;
				PlayerRef.GetComponent<CircleCollider2D>().enabled = false;

				gem_collider.isTrigger = false;

				waiting = true;
				QInstruction.SetActive(true);
				EInstruction.SetActive(true);
				StartCoroutine(clearQE());
				/* 
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
				SpacePressGem temp = col.GetComponent<SpacePressGem>();
				temp.CollisionStay(gameObject.GetComponent<Collider2D>());
				*/
			}
		} else if(player_entered){
			PlayerRef.transform.position = transform.position;
			if(Input.GetKeyDown(KeyCode.Space)){
				player_entered = false;
				PlayerRef.SetActive(true);
				PlayerRef.GetComponentInChildren<ParticleSystem>().Play();
				gem_collider.isTrigger = true;
				PlayerRef.GetComponent<Rigidbody2D>().simulated = true;
				PlayerRef.GetComponent<CircleCollider2D>().enabled = true;
				gem_rigidBody.velocity = Vector2.zero;
			}
		}
	}

	IEnumerator clearQE(){
		while(waiting){
			waiting = false;
			yield return new WaitForSeconds(7f);
		}
		QInstruction.SetActive(false);
		EInstruction.SetActive(false);
	}

	private void FixedUpdate()
	{
		if(player_entered){
			if(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E)){
				GameObject Instructions = GameObject.FindGameObjectWithTag("Instructions");
				Instructions.transform.parent = null;
				if (Input.GetKey(KeyCode.Q))
				{
					transform.Rotate( new Vector3(0,0,-1) * 5);
				}
				if (Input.GetKey(KeyCode.E))
				{
					transform.Rotate( new Vector3(0,0,1) * 5);
				}
        		Instructions.transform.parent = transform;
			}
			
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");
			Vector2 movement = new Vector2(moveHorizontal, moveVertical);
			gem_rigidBody.AddForce(movement * 50);
		}
	}
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
			if(!PlayerRef)
				PlayerRef = other.gameObject;
            player_in_zone = true;
            SpaceInstruction.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player_in_zone = false;
            SpaceInstruction.SetActive(false);
            QInstruction.SetActive(false);
            EInstruction.SetActive(false);
        }
    }
}
