using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Niveau1 : MonoBehaviour
{
    public GameObject GameObjectNiveau;
    public GameObject GameObjectSelect;
    public GameObject GameObjectCommande;
    public GameObject GameObjectContenu;
    public Sprite BoxToCreate;
    public bool reset = false;
    public int nbCarton;
    public int nbErreur;
    private int timer;
    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
            ResetGame();
        GameObject.Find("nbCarton").GetComponent<Text>().text = nbCarton.ToString();
        GameObject.Find("nbErreur").GetComponent<Text>().text = nbErreur.ToString();
        if (nbCarton <= 0 || nbErreur <= 0) {
            if (nbCarton <= 0) {
                GameObject.Find("endMessage").GetComponent<Text>().text = "Good Job";
            } else {
                GameObject.Find("endMessage").GetComponent<Text>().text = "You failed";
            }
            Destroy(GameObject.Find("ToDestroy"));
        } else {
            GameObject.Find("endMessage").GetComponent<Text>().text = "";
        }
    }

    IEnumerator CreateBox()
    {
        yield return new WaitForSeconds(timer);
        if (GameObject.Find("ToDestroy")) {
            if (timer > 8)
            {
                timer--;
            }
            Debug.Log(Time.time);
            GameObject Carton = new GameObject("Carton");
            Carton.transform.parent = GameObject.Find("ToDestroy").transform;
            Carton.AddComponent<SpriteRenderer>().sprite = BoxToCreate;
            Carton.AddComponent<BoxCollider>().size = new Vector3(20, 18, 1);
            Carton.GetComponent<BoxCollider>().isTrigger = true;
            Carton.AddComponent<Rigidbody>().useGravity = false;
            Carton.AddComponent<Carton>().GameObjectNiveau = GameObjectNiveau;
            Carton.GetComponent<Carton>().GameObjectSelect = GameObjectSelect;
            Carton.GetComponent<Carton>().GameObjectCommande = GameObjectCommande;
            Carton.GetComponent<Carton>().GameObjectContenu = GameObjectContenu;
            Carton.transform.localPosition = new Vector3(-9, 0.05f, 4);
            Carton.transform.localScale = new Vector3(0.1f, 0.1f, 0);
            Carton.transform.localRotation = Quaternion.Euler(90, 0, 0);
            StartCoroutine(CreateBox());
        }
    }

    void ResetGame()
    {
        reset = false;
        nbCarton = 5;
        nbErreur = 3;
        timer = 8;

        if (GameObject.Find("ToDestroy"))
            Destroy(GameObject.Find("ToDestroy"));
        GameObject ToDestroy = new GameObject("ToDestroy");
        ToDestroy.transform.parent = GameObjectNiveau.transform;
        StartCoroutine(CreateBox());
    }
}
