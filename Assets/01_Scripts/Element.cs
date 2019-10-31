using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    public GameObject spawner;
    private bool selected;
    
    void Start()
    {

    }
    
    void Update()
    {
        // New element animation
        if (transform.localScale.x < 1)
            transform.localScale += new Vector3(0.1f, 0.1f, 0);
        
        // Check if the element is select
        if (GameObject.Find("select").GetComponent<Text>().text != gameObject.name) {
            selected = false;
        }
    }

    private void OnMouseUp()
    {
        // Detect the click and change the select variable and indication in the game
        if (!GameObject.Find("select").GetComponent<Text>().text.Contains("Personnage")) {
            selected = !selected;
            if (selected) {
                GameObject.Find("select").GetComponent<Text>().text = gameObject.name;
            } else {
                GameObject.Find("select").GetComponent<Text>().text = "";
            }
        }
    }
}
