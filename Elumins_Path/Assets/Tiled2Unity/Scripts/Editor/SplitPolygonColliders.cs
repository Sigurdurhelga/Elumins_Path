using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;

[Tiled2Unity.CustomTiledImporter]
public class SplitPolygonColliders : Tiled2Unity.ICustomTiledImporter
{

    public void HandleCustomProperties(GameObject gameObject, IDictionary<string, string> customProperties)
    {
        // do nothing
    }

    public void CustomizePrefab(GameObject prefab)
    {
        var polygon2Ds = prefab.GetComponentsInChildren<PolygonCollider2D>();
        if (polygon2Ds == null)
            return;


        int wallMask = LayerMask.NameToLayer("ShadowLayer");
        var wallPolygons = from polygon in polygon2Ds where polygon.gameObject.layer == wallMask select polygon;


        foreach (var poly in wallPolygons)
        {
            for (int p = 0; p < poly.pathCount; p++)
            {
                // make a new gameobject for the collider
                GameObject newCollider = new GameObject("Collider_" + p);
                newCollider.transform.parent = poly.transform;
                newCollider.layer = wallMask;

                // Make the PolygonCollider2D
                PolygonCollider2D newPolygonCollider2D = newCollider.AddComponent<PolygonCollider2D>() as PolygonCollider2D;
                newPolygonCollider2D.SetPath(0, poly.GetPath(p));
                newPolygonCollider2D.transform.parent = poly.gameObject.transform;
            }
        }
        int childCount = prefab.transform.childCount;
        Debug.Log(childCount);
        for(int i = 0; i < childCount; i++)
        {
            if (prefab.transform.GetChild(i).tag == "CastShadow")
                prefab.transform.GetChild(i).GetComponentInChildren<PolygonCollider2D>().enabled = false;
            if (prefab.transform.GetChild(i).tag == "LightThrough")
                prefab.transform.GetChild(i).GetComponentInChildren<PolygonCollider2D>().tag = "LightThrough";
        }
    }
}