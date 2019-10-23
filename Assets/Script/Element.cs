using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    public GameObject spawner;
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("select").GetComponent<Text>().text != gameObject.name) {
            selected = false;
        }
    }

    private void OnMouseUp()
    {
        if (GameObject.Find("select").GetComponent<Text>().text != "Personnage") {
            selected = !selected;
            if (selected) {
                GameObject.Find("select").GetComponent<Text>().text = gameObject.name;
            } else {
                GameObject.Find("select").GetComponent<Text>().text = "";
            }
        }
    }
}
