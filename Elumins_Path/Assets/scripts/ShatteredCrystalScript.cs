﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatteredCrystalScript : MonoBehaviour
{
    public GameObject ShatterWall;
    public float maxDistance;
    private List<Rigidbody2D> wallShardRBs = new List<Rigidbody2D>();
    private GameObject player_ref;
    private GameObject shard_ref;
    private int connectedShards = 0;
    private bool player_in_shard = false;
    private bool player_in_shell = false;
    private bool connected = false;
    private List<string> doneNames = new List<string>();
    private AudioSource reconstructCrystal;

    private void Start()
    {
        for (int i = 0; i < ShatterWall.transform.childCount; i++)
        {
            wallShardRBs.Add(ShatterWall.transform.GetChild(i).GetComponent<Rigidbody2D>());
        }
        reconstructCrystal = GetComponent<AudioSource>();
    }

    IEnumerator followPlayer(GameObject shard)
    {
        yield return new WaitForSeconds(0.2f);

        while (connected)
        {
            shard.transform.position = Vector3.Lerp(shard.transform.position, player_ref.transform.position, 0.05f);

            float t = (Vector3.Distance(shard.transform.position, player_ref.transform.position)/maxDistance);
            if(t > 1)
            {
                shard.transform.position = Vector3.Lerp(player_ref.transform.position, shard.transform.position, Mathf.Max(maxDistance / t, 0.9f));
            }
            
            yield return null;
        }
    }

    IEnumerator mergeWithShell(GameObject shard)
    {

        doneNames.Add(shard.name);
        Debug.Log("in mergephase");

        float pos = 0;

        while (pos <= 1)
        {
            shard.transform.position = Vector3.Lerp(shard.transform.position, transform.position, pos);
            pos += 0.04f;
            yield return null;
        }
        connectedShards += 1;

        if (connectedShards == 4)
        {
            reconstructCrystal.Play();
            yield return new WaitForSeconds(reconstructCrystal.clip.length);
            Debug.Log("IM IN HERE");
            foreach (Rigidbody2D wallShard in wallShardRBs)
            {
                wallShard.bodyType = RigidbodyType2D.Dynamic;
                wallShard.gravityScale = 1;
            }
            // vector pointing from gem to wall
            Vector2 forcepush = ShatterWall.transform.position - transform.position;
            //Vector2 randompush = new Vector2(Random.RandomRange(-1000,1000), Random.RandomRange(-1000,1000));
            foreach (Rigidbody2D wallShard in wallShardRBs)
            {
                wallShard.AddForce(forcepush * Random.Range(50, 100));
            }
            yield return new WaitForSeconds(4f);

            foreach (Rigidbody2D wallShard in wallShardRBs)
            {
                Destroy(wallShard.gameObject);
            }
            Destroy(ShatterWall);

        }

    }

    private void Update()
    {
        if (connected)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                connected = false;
                if (player_in_shell && !doneNames.Contains(shard_ref.name))
                {
                    StartCoroutine(mergeWithShell(shard_ref));
                    Destroy(shard_ref.transform.GetChild(0).gameObject);
                }
            }
        }
        else 
        if (player_in_shard && !connected && shard_ref)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !doneNames.Contains(shard_ref.name))
            {
                connected = true;
                StartCoroutine(followPlayer(shard_ref));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player")
        {
            player_in_shell = true;
            if(connected)
            {
                connected = false;
                if (!doneNames.Contains(shard_ref.name))
                {
                    StartCoroutine(mergeWithShell(shard_ref));
                    Destroy(shard_ref.transform.GetChild(0).gameObject);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.transform.tag == "Player")
        {
            player_in_shell = false;
        }
    }

    public void OnTriggerEnter2DChild(Collider2D collider, GameObject shard)
    {
        if (!player_ref)
        {
            player_ref = collider.gameObject;
        }
        if (!connected)
            shard_ref = shard;
        player_in_shard = true;
        if (!connected)
        {
            if (shard.transform.childCount > 0) shard.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void OnTriggerStay2DChild(Collider2D collider, GameObject shard)
    {
        if (connected)
        {
            if (shard.transform.childCount > 0) shard.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            if (shard.transform.childCount > 0) shard.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit2DChild(Collider2D collider, GameObject shard)
    {
        //shard_ref = null;
        if (shard.transform.childCount > 0) shard.transform.GetChild(0).gameObject.SetActive(false);
        player_in_shard = false;
    }

}
