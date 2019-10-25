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
        if (transform.localScale.x < 1)
            transform.localScale += new Vector3(0.1f, 0.1f, 0);
        if (GameObject.Find("select").GetComponent<Text>().text != gameObject.name) {
            selected = false;
        }
    }

    private void OnMouseUp()
    {
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
