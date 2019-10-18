using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Personnage : MonoBehaviour
{
    private string item = "";
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Personnage")
        {
            transform.localRotation = Quaternion.Euler(90, 0, 0);
            if (GameObject.Find("select").GetComponent<Text>().text != gameObject.name) {
                GameObject.Find("item").GetComponent<Text>().text = "";
                selected = false;
            } else {
                if (item != "")
                    GameObject.Find("item").GetComponent<Text>().text = "contient: " + item;
                else
                    GameObject.Find("item").GetComponent<Text>().text = "";
            }
            if (selected) {
                if (Input.GetMouseButtonDown(1)) {
                    RaycastHit hit;
                    
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
                        gameObject.GetComponent<NavMeshAgent>().destination = hit.point;
                        if (hit.transform.gameObject.tag == "Element" && item == "")
                            StartCoroutine(goPickElement(hit));
                        if (hit.transform.gameObject.name == "Carton" && item != "")
                            StartCoroutine(goDropElement(hit));
                    }
                }
                transform.localRotation = Quaternion.Euler(90, 0, 0);
            }
        }
    }

    IEnumerator goPickElement(RaycastHit hit)
    {
        if ((gameObject.transform.position.x <= hit.point.x + 0.6f && gameObject.transform.position.x >= hit.point.x - 0.6f) && (gameObject.transform.position.y <= hit.point.y + 0.6f && gameObject.transform.position.y >= hit.point.y - 0.6f))
        {
            yield return new WaitForSeconds(1);
            item = hit.transform.gameObject.name;
            Destroy(hit.transform.gameObject);
        }
        else
        {
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(goPickElement(hit));
        }
    }

    IEnumerator goDropElement(RaycastHit hit)
    {
        if ((gameObject.transform.position.x <= hit.point.x + 0.6f || gameObject.transform.position.x >= hit.point.x - 0.6f) && (gameObject.transform.position.y <= hit.point.y + 0.6f || gameObject.transform.position.y >= hit.point.y - 0.6f))
        {
            yield return new WaitForSeconds(0.5f);
            hit.transform.GetComponent<Carton>().chargeElement.Add(item);
            hit.transform.GetComponent<Carton>().newElement = true;
            item = "";
        }
        else
        {
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(goDropElement(hit));
        }
    }

    private void OnMouseUp()
    {
        if (gameObject.name == "Personnage")
        {
            selected = !selected;
            if (selected)
                GameObject.Find("select").GetComponent<Text>().text = gameObject.name;
            else
                GameObject.Find("select").GetComponent<Text>().text = "";

        }
    }
}
