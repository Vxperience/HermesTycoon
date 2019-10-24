using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Personnage : MonoBehaviour
{
    public Animator animator;
    private string item = "";
    private bool isInAction = false;
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Personnage") {
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
                if (Input.GetMouseButtonDown(0)) {
                    RaycastHit hit;
                    
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && hit.transform.gameObject.name != "Personnage") {
                        animator.SetBool("isArrived", false);
                        animToDirection(hit);
                        gameObject.GetComponent<NavMeshAgent>().destination = hit.point;
                        if (hit.transform.gameObject.tag == "Element" && item == "")
                            StartCoroutine(goPickElement(hit));
                        else if (hit.transform.gameObject.name == "Carton" && item != "")
                            StartCoroutine(goDropElement(hit));
                        else
                            StartCoroutine(goToNewPosition(hit));
                    }
                }
                transform.localRotation = Quaternion.Euler(90, 0, 0);
            }
        }
    }

    void animToDirection(RaycastHit hit)
    {
        float angle = Vector3.Angle(hit.point - transform.position, hit.transform.forward);
        
        if (hit.point.x - transform.position.x <= 0) {
            if (angle <= 45)
                animator.SetBool("goForward", true);
            else if (angle > 45 && angle <= 135)
                animator.SetBool("goLeft", true);
            else
                animator.SetBool("goBackward", true);
        } else {
            if (angle <= 45)
                animator.SetBool("goForward", true);
            else if (angle > 45 && angle <= 135)
                animator.SetBool("goRight", true);
            else
                animator.SetBool("goBackward", true);
        }
    }

    IEnumerator goToNewPosition(RaycastHit hit)
    {
        if (gameObject.transform.position.x <= hit.point.x + 0.1 && gameObject.transform.position.x >= hit.point.x - 0.1 && gameObject.transform.position.z <= hit.point.z + 0.1 && gameObject.transform.position.z >= hit.point.z - 0.1) {
            isInAction = true;
            animator.SetBool("isArrived", true);
            animator.SetBool("goForward", false);
            animator.SetBool("goBackward", false);
            animator.SetBool("goRight", false);
            animator.SetBool("goLeft", false);
            yield return new WaitForSeconds(1);
            isInAction = false;
        } else if (!isInAction) {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(goToNewPosition(hit));
        }
    }

    IEnumerator goPickElement(RaycastHit hit)
    {
        if (gameObject.transform.position.x <= hit.point.x + 0.3f && gameObject.transform.position.x >= hit.point.x - 0.3f && gameObject.transform.position.z <= hit.point.z + 0.3f && gameObject.transform.position.z >= hit.point.z - 0.3f) {
            isInAction = true;
            animator.SetBool("isArrived", true);
            animator.SetBool("goForward", false);
            animator.SetBool("goBackward", false);
            animator.SetBool("goRight", false);
            animator.SetBool("goLeft", false);
            gameObject.GetComponent<NavMeshAgent>().destination = transform.localPosition;
            yield return new WaitForSeconds(1);
            if (hit.transform.gameObject) {
                item = hit.transform.gameObject.name;
                hit.transform.gameObject.GetComponent<Element>().spawner.GetComponent<SpawnElement>().isPicked = true;
                Destroy(hit.transform.gameObject);
                isInAction = false;
            }
        } else if (!isInAction) {
            yield return new WaitForSeconds(0.25f);
            if (hit.transform.gameObject)
                StartCoroutine(goPickElement(hit));
        }
    }

    IEnumerator goDropElement(RaycastHit hit)
    {
        if (gameObject.transform.position.x <= hit.point.x + 1.5 && gameObject.transform.position.x >= hit.point.x - 1.5 && gameObject.transform.position.z <= hit.point.z + 1.5 && gameObject.transform.position.z >= hit.point.z - 1.5) {
            isInAction = true;
            animator.SetBool("isArrived", true);
            animator.SetBool("goForward", false);
            animator.SetBool("goBackward", false);
            animator.SetBool("goRight", false);
            animator.SetBool("goLeft", false);
            gameObject.GetComponent<NavMeshAgent>().destination = transform.localPosition;
            yield return new WaitForSeconds(0.5f);
            if (hit.transform.gameObject) {
                hit.transform.GetComponent<Carton>().chargeElement.Add(item);
                hit.transform.GetComponent<Carton>().newElement = true;
            }
            item = "";
            isInAction = false;
        } else if (!isInAction) {
            yield return new WaitForSeconds(0.25f);
            if (hit.transform.gameObject.name == "Carton") {
                StartCoroutine(goDropElement(hit));
            }
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
