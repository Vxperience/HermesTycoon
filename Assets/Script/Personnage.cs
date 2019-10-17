using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Personnage : MonoBehaviour
{
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(90, 0, 0);
        if (GameObject.Find("select").GetComponent<Text>().text != gameObject.name)
            selected = false;
        if (selected) {
            if (Input.GetMouseButtonDown(1)) {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                    gameObject.GetComponent<NavMeshAgent>().destination = hit.point;
                }
            }
            transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
    }

    private void OnMouseUp()
    {
        selected = !selected;
        if (selected)
            GameObject.Find("select").GetComponent<Text>().text = gameObject.name;
        else
            GameObject.Find("select").GetComponent<Text>().text = "";
    }
}
