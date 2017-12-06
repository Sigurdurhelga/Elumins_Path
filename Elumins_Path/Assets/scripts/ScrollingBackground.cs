using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    public float background_width;
    public float background_height;
    public float z_pos;

    private Transform camera_transform;
    private Transform[] layers;
    private int tl_index;
    private int tm_index;
    private int tr_index;
    private int bl_index;
    private int bm_index;
    private int br_index;
    private int ml_index;
    private int mm_index;
    private int mr_index;

	// Use this for initialization
	void Start () {
        camera_transform = Camera.main.transform;
        layers = new Transform[this.transform.childCount];
        string child_name;

        for(int i = 0; i < this.transform.childCount; i++)
        {
            child_name = transform.GetChild(i).name;
            switch (child_name)
            {
                case "bl":
                    layers[i] = transform.GetChild(i);
                    bl_index = i;
                    break;
                case "bm":
                    layers[i] = transform.GetChild(i);
                    bm_index = i;
                    break;
                case "br":
                    layers[i] = transform.GetChild(i);
                    br_index = i;
                    break;
                case "ml":
                    layers[i] = transform.GetChild(i);
                    ml_index = i;
                    break;
                case "mm":
                    layers[i] = transform.GetChild(i);
                    mm_index = i;
                    break;
                case "mr":
                    layers[i] = transform.GetChild(i);
                    mr_index = i;
                    break;
                case "tl":
                    layers[i] = transform.GetChild(i);
                    tl_index = i;
                    break;
                case "tm":
                    layers[i] = transform.GetChild(i);
                    tm_index = i;
                    break;
                case "tr":
                    layers[i] = transform.GetChild(i);
                    tr_index = i;
                    break;
                default:
                    Debug.Log("something went wrong in the scrolling background switch case, Please be mindful of the names of the images");
                    break;
            }
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        if (camera_transform.position.x < layers[ml_index].transform.position.x + (background_width/2))
            ScrollLeft();
        else if (camera_transform.position.x > layers[mr_index].transform.position.x - (background_width/2))
            ScrollRight();
        else if (camera_transform.position.y > layers[tm_index].transform.position.y - (background_height/2))
            ScrollUp();
        else if (camera_transform.position.y < layers[bm_index].transform.position.y + (background_height/2))
            ScrollDown();
        if(Input.GetKeyDown(KeyCode.G))
            this.transform.position = camera_transform.position;
	}

    void ScrollUp()
    {
        layers[bl_index].position = new Vector3(layers[tl_index].position.x, layers[tl_index].position.y+background_height, z_pos);
        layers[bm_index].position = new Vector3(layers[tm_index].position.x, layers[tm_index].position.y+background_height, z_pos);
        layers[br_index].position = new Vector3(layers[tr_index].position.x, layers[tr_index].position.y+background_height, z_pos);

        int temptl = tl_index, temptm = tm_index, temptr = tr_index;
        int tempml = ml_index, tempmm = mm_index, tempmr = mr_index;

        tl_index = bl_index; tm_index = bm_index; tr_index = br_index;
        ml_index = temptl; mm_index = temptm; mr_index = temptr;
        bl_index = tempml; bm_index = tempmm; br_index = tempmr;
    }

    void ScrollDown()
    {
        layers[tl_index].position = new Vector3(layers[bl_index].position.x, layers[bl_index].position.y-background_height, z_pos);
        layers[tm_index].position = new Vector3(layers[bm_index].position.x, layers[bm_index].position.y-background_height, z_pos);
        layers[tr_index].position = new Vector3(layers[br_index].position.x, layers[br_index].position.y-background_height, z_pos);

        int temptl = tl_index, temptm = tm_index, temptr = tr_index;
        int tempml = ml_index, tempmm = mm_index, tempmr = mr_index;

        tl_index = tempml; tm_index = tempmm; tr_index = tempmr;
        ml_index = bl_index; mm_index = bm_index; mr_index = br_index;
        bl_index = temptl; bm_index = temptm; br_index = temptr;
    }

    void ScrollLeft()
    {
        layers[tr_index].position = new Vector3(layers[tl_index].position.x-background_width, layers[tl_index].position.y, z_pos);
        layers[mr_index].position = new Vector3(layers[ml_index].position.x-background_width, layers[ml_index].position.y, z_pos);
        layers[br_index].position = new Vector3(layers[bl_index].position.x-background_width, layers[bl_index].position.y, z_pos);

        int temptl = tl_index, tempml = ml_index, tempbl = bl_index;
        int temptm = tm_index, tempmm = mm_index, tempbm = bm_index;

        bl_index = br_index; ml_index = mr_index; tl_index = tr_index;
        br_index = tempbm; mr_index = tempmm; tr_index = temptm;
        bm_index = tempbl; mm_index = tempml; tm_index = temptl;
    }

    void ScrollRight()
    {
        layers[tl_index].position = new Vector3(layers[tr_index].position.x+background_width, layers[tr_index].position.y, z_pos);
        layers[ml_index].position = new Vector3(layers[mr_index].position.x+background_width, layers[mr_index].position.y, z_pos);
        layers[bl_index].position = new Vector3(layers[br_index].position.x+background_width, layers[br_index].position.y, z_pos);

        int temptl = tl_index, tempml = ml_index, tempbl = bl_index;
        int temptm = tm_index, tempmm = mm_index, tempbm = bm_index;

        bm_index = br_index; mm_index = mr_index; tm_index = tr_index;
        bl_index = tempbm; ml_index = tempmm; tl_index = temptm;
        br_index = tempbl; mr_index = tempml; tr_index = temptl;
    }


}
