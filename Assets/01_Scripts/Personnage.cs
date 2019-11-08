using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Personnage : MonoBehaviour
{
    public Sprite teeShirt;
    public Sprite pantalon;
    public Sprite chemise;
    public Sprite chapeau;
    public Sprite manteau;
    public Animator animator;
    public AudioClip footstep;
    public AudioClip pick;
    public AudioClip drop;
    public string item = "";
    public bool isInAction = false;
    private GameObject currentItem;
    private GameObject selectHud;
    private GameObject mainCamera;
    private AudioSource playerAudio;
    private bool selected;
    
    void Start()
    {
        selectHud = GameObject.Find("select");
        mainCamera = GameObject.Find("Main Camera");
        playerAudio = gameObject.GetComponent<AudioSource>();
        // Configure the indication for when the player drag an item
        currentItem = new GameObject();
        currentItem.name = "Item";
        currentItem.transform.parent = gameObject.transform;
        currentItem.transform.localPosition = new Vector3(0, 0, 0);
        currentItem.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        currentItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
        currentItem.AddComponent<SpriteRenderer>();
        currentItem.SetActive(false);
    }
    
    void Update()
    {
        // Update the volume
        mainCamera.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("game");
        transform.localRotation = Quaternion.Euler(90, 0, 0);

        // Check if select
        if (selectHud.GetComponent<Text>().text != gameObject.name)
            selected = false;
        animator.SetBool("select", selected);

        // Check if the player drag an item
        if (item == "")
            currentItem.SetActive(false);

        // Manage The player movement and interaction
        if (selected) {
            if (Input.GetMouseButtonDown(0)) {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100) && !hit.transform.gameObject.name.Contains("Personnage")) {
                    animator.SetBool("isArrived", false);
                    animator.SetBool("goForward", false);
                    animator.SetBool("goBackward", false);
                    animator.SetBool("goRight", false);
                    animator.SetBool("goLeft", false);
                    AnimToDirection(hit);
                    gameObject.GetComponent<NavMeshAgent>().destination = hit.point;
                    if (hit.transform.gameObject.tag == "Element" && item == "")
                        StartCoroutine(GoPickElement(hit));
                    else if ((hit.transform.gameObject.name == "Carton" || hit.transform.gameObject.name == "Trash") && item != "")
                        StartCoroutine(GoDropElement(hit));
                    else
                        StartCoroutine(GoToNewPosition(hit));
                }
            }
        }
    }

    void AnimToDirection(RaycastHit hit)
    {
        // Manage which animation the player will do
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

    IEnumerator GoToNewPosition(RaycastHit hit)
    {
        // Manage the movement to a new position
        playerAudio.clip = footstep;
        playerAudio.loop = true;
        playerAudio.Play();
        if (gameObject.transform.position.x <= hit.point.x + 0.1 && gameObject.transform.position.x >= hit.point.x - 0.1 && gameObject.transform.position.z <= hit.point.z + 0.1 && gameObject.transform.position.z >= hit.point.z - 0.1) {
            isInAction = true;
            animator.SetBool("isArrived", true);
            animator.SetBool("goForward", false);
            animator.SetBool("goBackward", false);
            animator.SetBool("goRight", false);
            animator.SetBool("goLeft", false);
            playerAudio.Stop();
            yield return new WaitForSeconds(0.1f);
            currentItem.transform.localPosition = new Vector3(0, -0.20f, -0.001f);
            isInAction = false;
        } else if (!isInAction) {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(GoToNewPosition(hit));
        }
    }

    IEnumerator GoPickElement(RaycastHit hit)
    {
        // Manage the pick interaction with an element
        playerAudio.clip = footstep;
        playerAudio.loop = true;
        playerAudio.Play();
        if (gameObject.transform.position.x <= hit.point.x + 0.5 && gameObject.transform.position.x >= hit.point.x - 0.5 && gameObject.transform.position.z <= hit.point.z + 0.5 && gameObject.transform.position.z >= hit.point.z - 0.5) {
            isInAction = true;
            animator.SetBool("isArrived", true);
            animator.SetBool("goForward", false);
            animator.SetBool("goBackward", false);
            animator.SetBool("goRight", false);
            animator.SetBool("goLeft", false);
            playerAudio.Stop();
            playerAudio.clip = pick;
            playerAudio.loop = false;
            gameObject.GetComponent<NavMeshAgent>().destination = transform.position;
            playerAudio.Play();
            yield return new WaitForSeconds(0.1f);
            if (GameObject.Find(hit.transform.gameObject.name)) {
                item = hit.transform.gameObject.name;
                hit.transform.gameObject.GetComponent<Element>().spawner.GetComponent<SpawnElement>().isPicked = true;
                if (item == "tee-shirt")
                    currentItem.GetComponent<SpriteRenderer>().sprite = teeShirt;
                else if (item == "pantalon")
                    currentItem.GetComponent<SpriteRenderer>().sprite = pantalon;
                else if (item == "chemise")
                    currentItem.GetComponent<SpriteRenderer>().sprite = chemise;
                else if (item == "chapeau")
                    currentItem.GetComponent<SpriteRenderer>().sprite = chapeau;
                else if (item == "manteau")
                    currentItem.GetComponent<SpriteRenderer>().sprite = manteau;
                currentItem.SetActive(true);
                Destroy(hit.transform.gameObject);
                isInAction = false;
            }
        } else if (!isInAction) {
            yield return new WaitForSeconds(0.1f);
            if (hit.transform.gameObject)
                StartCoroutine(GoPickElement(hit));
        }
    }

    IEnumerator GoDropElement(RaycastHit hit)
    {
        // Manage the drop of an element in a box or trash
        playerAudio.clip = footstep;
        playerAudio.loop = true;
        playerAudio.Play();
        if ((gameObject.transform.position.x <= hit.point.x + 1.8 && gameObject.transform.position.x >= hit.point.x - 1.8 && gameObject.transform.position.z <= hit.point.z + 1.8 && gameObject.transform.position.z >= hit.point.z - 1.8 && hit.transform.gameObject.name == "Carton") || (gameObject.transform.position.x <= hit.point.x + 1.1 && gameObject.transform.position.x >= hit.point.x - 1.1 && gameObject.transform.position.z <= hit.point.z + 1.1 && gameObject.transform.position.z >= hit.point.z - 1.1 && hit.transform.gameObject.name == "Trash")) {
            isInAction = true;
            animator.SetBool("isArrived", true);
            animator.SetBool("goForward", false);
            animator.SetBool("goBackward", false);
            animator.SetBool("goRight", false);
            animator.SetBool("goLeft", false);
            playerAudio.Stop();
            playerAudio.clip = drop;
            playerAudio.loop = false;
            gameObject.GetComponent<NavMeshAgent>().destination = transform.position;
            playerAudio.Play();
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
                StartCoroutine(GoDropElement(hit));
            }
        }
    }

    private void OnMouseUp()
    {
        // Change the player select varibale and UI
        selected = !selected;
        if (selected)
            selectHud.GetComponent<Text>().text = gameObject.name;
        else
            selectHud.GetComponent<Text>().text = "";
    }
}
