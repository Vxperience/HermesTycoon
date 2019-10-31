using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Personnage : MonoBehaviour
{
    public Sprite TeeShirt;
    public Sprite Pantalon;
    public Sprite Chemise;
    public Sprite Chapeau;
    public Sprite Manteau;
    public Animator animator;
    public AudioClip footstep;
    public AudioClip pick;
    public AudioClip drop;
    public string item = "";
    private GameObject currentItem;
    private bool isInAction = false;
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        currentItem = new GameObject();
        currentItem.name = "Item";
        currentItem.transform.parent = gameObject.transform;
        currentItem.transform.localPosition = new Vector3(0, 0, 0);
        currentItem.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        currentItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
        currentItem.AddComponent<SpriteRenderer>();
        currentItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<AudioSource>().volume = GameObject.Find("Main Camera").GetComponent<ChangeCamera>().game;
        transform.localRotation = Quaternion.Euler(90, 0, 0);
        if (GameObject.Find("select").GetComponent<Text>().text != gameObject.name)
            selected = false;
        animator.SetBool("select", selected);
        if (selected) {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && !hit.transform.gameObject.name.Contains("Personnage")) {
                    animator.SetBool("isArrived", false);
                    animator.SetBool("goForward", false);
                    animator.SetBool("goBackward", false);
                    animator.SetBool("goRight", false);
                    animator.SetBool("goLeft", false);
                    animToDirection(hit);
                    gameObject.GetComponent<NavMeshAgent>().destination = hit.point;
                    if (hit.transform.gameObject.tag == "Element" && item == "")
                        StartCoroutine(goPickElement(hit));
                    else if ((hit.transform.gameObject.name == "Carton" || hit.transform.gameObject.name == "Trash") && item != "")
                        StartCoroutine(goDropElement(hit));
                    else
                        StartCoroutine(goToNewPosition(hit));
                }
            }
        }
        if (item == "")
            currentItem.SetActive(false);
    }

    void animToDirection(RaycastHit hit)
    {
        float angle = Vector3.Angle(hit.point - transform.position, hit.transform.forward);
        
        if (hit.point.x - transform.position.x <= 0) {
            if (angle <= 45) {
                animator.SetBool("goForward", true);
                currentItem.transform.localPosition = new Vector3(0, 0.15f, 0.001f);
            } else if (angle > 45 && angle <= 135) {
                animator.SetBool("goLeft", true);
                currentItem.transform.localPosition = new Vector3(-0.3f, -0.1f, -0.001f);
            } else {
                animator.SetBool("goBackward", true);
                currentItem.transform.localPosition = new Vector3(0, -0.20f, -0.001f);
            }
        } else {
            if (angle <= 45) {
                animator.SetBool("goForward", true);
                currentItem.transform.localPosition = new Vector3(0, 0.15f, 0.001f);
            } else if (angle > 45 && angle <= 135) {
                animator.SetBool("goRight", true);
                currentItem.transform.localPosition = new Vector3(0.3f, -0.1f, -0.001f);
            } else {
                animator.SetBool("goBackward", true);
                currentItem.transform.localPosition = new Vector3(0, -0.20f, -0.001f);
            }
        }
    }

    IEnumerator goToNewPosition(RaycastHit hit)
    {
        gameObject.GetComponent<AudioSource>().clip = footstep;
        gameObject.GetComponent<AudioSource>().loop = true;
        gameObject.GetComponent<AudioSource>().Play();
        if (gameObject.transform.position.x <= hit.point.x + 0.1 && gameObject.transform.position.x >= hit.point.x - 0.1 && gameObject.transform.position.z <= hit.point.z + 0.1 && gameObject.transform.position.z >= hit.point.z - 0.1) {
            isInAction = true;
            animator.SetBool("isArrived", true);
            animator.SetBool("goForward", false);
            animator.SetBool("goBackward", false);
            animator.SetBool("goRight", false);
            animator.SetBool("goLeft", false);
            gameObject.GetComponent<AudioSource>().Stop();
            yield return new WaitForSeconds(0.1f);
            currentItem.transform.localPosition = new Vector3(0, -0.20f, -0.001f);
            isInAction = false;
        } else if (!isInAction) {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(goToNewPosition(hit));
        }
    }

    IEnumerator goPickElement(RaycastHit hit)
    {
        gameObject.GetComponent<AudioSource>().clip = footstep;
        gameObject.GetComponent<AudioSource>().loop = true;
        gameObject.GetComponent<AudioSource>().Play();
        if (gameObject.transform.position.x <= hit.point.x + 0.5 && gameObject.transform.position.x >= hit.point.x - 0.5 && gameObject.transform.position.z <= hit.point.z + 0.5 && gameObject.transform.position.z >= hit.point.z - 0.5) {
            isInAction = true;
            animator.SetBool("isArrived", true);
            animator.SetBool("goForward", false);
            animator.SetBool("goBackward", false);
            animator.SetBool("goRight", false);
            animator.SetBool("goLeft", false);
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().clip = pick;
            gameObject.GetComponent<AudioSource>().loop = false;
            gameObject.GetComponent<NavMeshAgent>().destination = transform.position;
            gameObject.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            if (GameObject.Find(hit.transform.gameObject.name)) {
                item = hit.transform.gameObject.name;
                hit.transform.gameObject.GetComponent<Element>().spawner.GetComponent<SpawnElement>().isPicked = true;
                if (item == "tee-shirt")
                    currentItem.GetComponent<SpriteRenderer>().sprite = TeeShirt;
                else if (item == "pantalon")
                    currentItem.GetComponent<SpriteRenderer>().sprite = Pantalon;
                else if (item == "chemise")
                    currentItem.GetComponent<SpriteRenderer>().sprite = Chemise;
                else if (item == "chapeau")
                    currentItem.GetComponent<SpriteRenderer>().sprite = Chapeau;
                else if (item == "manteau")
                    currentItem.GetComponent<SpriteRenderer>().sprite = Manteau;
                currentItem.SetActive(true);
                Destroy(hit.transform.gameObject);
                isInAction = false;
            }
        } else if (!isInAction) {
            yield return new WaitForSeconds(0.1f);
            if (hit.transform.gameObject)
                StartCoroutine(goPickElement(hit));
        }
    }

    IEnumerator goDropElement(RaycastHit hit)
    {
        gameObject.GetComponent<AudioSource>().clip = footstep;
        gameObject.GetComponent<AudioSource>().loop = true;
        gameObject.GetComponent<AudioSource>().Play();
        if ((gameObject.transform.position.x <= hit.point.x + 1.8 && gameObject.transform.position.x >= hit.point.x - 1.8 && gameObject.transform.position.z <= hit.point.z + 1.8 && gameObject.transform.position.z >= hit.point.z - 1.8 && hit.transform.gameObject.name == "Carton") || (gameObject.transform.position.x <= hit.point.x + 1.1 && gameObject.transform.position.x >= hit.point.x - 1.1 && gameObject.transform.position.z <= hit.point.z + 1.1 && gameObject.transform.position.z >= hit.point.z - 1.1 && hit.transform.gameObject.name == "Trash")) {
            isInAction = true;
            animator.SetBool("isArrived", true);
            animator.SetBool("goForward", false);
            animator.SetBool("goBackward", false);
            animator.SetBool("goRight", false);
            animator.SetBool("goLeft", false);
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().clip = drop;
            gameObject.GetComponent<AudioSource>().loop = false;
            gameObject.GetComponent<NavMeshAgent>().destination = transform.position;
            gameObject.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(0.1f);
            if (hit.transform.gameObject.name == "Carton") {
                hit.transform.GetComponent<Carton>().chargeElement.Add(item);
                hit.transform.GetComponent<Carton>().newElement = true;
            }
            currentItem.SetActive(false);
            item = "";
            isInAction = false;
        } else if (!isInAction) {
            yield return new WaitForSeconds(0.1f);
            if (hit.transform.gameObject.name == "Carton" || hit.transform.gameObject.name == "Trash") {
                StartCoroutine(goDropElement(hit));
            }
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
