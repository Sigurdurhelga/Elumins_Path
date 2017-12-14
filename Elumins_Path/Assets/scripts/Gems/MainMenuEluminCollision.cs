using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuEluminCollision : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hello");
        Button temp = GetComponent<Button>();
        if (temp) temp.Select();
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

}
