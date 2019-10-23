using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Niveau : MonoBehaviour
{
    public GameObject GameObjectNiveau;
    public GameObject GameObjectSelect;
    public GameObject GameObjectCommande;
    public GameObject GameObjectContenu;
    public GameObject Player1;
    public Sprite BoxToCreate;
    public bool isEndless;
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
        if (isEndless)
        {
            if (nbErreur <= 0)
            {
                GameObject.Find("endMessage").GetComponent<Text>().text = "Game Over";
                Destroy(GameObject.Find("ToDestroy"));
            }
            else
                GameObject.Find("endMessage").GetComponent<Text>().text = "";
            GameObject.Find("info").GetComponent<Text>().text = "Nombre de carton remplis: " + nbCarton + " Nombre d'erreur restantes: " + nbErreur;
        }
        else {
            if (nbCarton >= 5 || nbErreur <= 0)
            {
                if (nbCarton >= 5)
                    GameObject.Find("endMessage").GetComponent<Text>().text = "Good Job";
                else
                    GameObject.Find("endMessage").GetComponent<Text>().text = "You failed";
                Destroy(GameObject.Find("ToDestroy"));
            }
            else
                GameObject.Find("endMessage").GetComponent<Text>().text = "";
            GameObject.Find("info").GetComponent<Text>().text = "Nombre de carton à remplir: " + nbCarton + " / 5 Nombre d'erreur restantes: " + nbErreur;
        }
    }

    IEnumerator CreateBox()
    {
        yield return new WaitForSeconds(timer);
        if (GameObject.Find("ToDestroy")) {
            if (timer > 8)
                timer--;
            GameObject Carton = new GameObject("Carton");
            Carton.transform.parent = GameObject.Find("ToDestroy").transform;
            Carton.AddComponent<SpriteRenderer>().sprite = BoxToCreate;
            Carton.AddComponent<BoxCollider>().size = new Vector3(1.2f, 1.2f, 1);
            Carton.GetComponent<BoxCollider>().isTrigger = true;
            Carton.AddComponent<Rigidbody>().useGravity = false;
            Carton.AddComponent<Carton>().GameObjectNiveau = GameObjectNiveau;
            Carton.GetComponent<Carton>().GameObjectSelect = GameObjectSelect;
            Carton.GetComponent<Carton>().GameObjectCommande = GameObjectCommande;
            Carton.GetComponent<Carton>().GameObjectContenu = GameObjectContenu;
            if (gameObject.name == "Niveau1")
                Carton.transform.localPosition = new Vector3(-11, 0.05f, 3);
            Carton.transform.localScale = new Vector3(1.3f, 1.3f, 0);
            Carton.transform.localRotation = Quaternion.Euler(90, 0, 0);
            StartCoroutine(CreateBox());
        }
    }

    void ResetGame()
    {
        reset = false;
        nbCarton = 0;
        nbErreur = 3;
        timer = 8;

        if (gameObject.name == "Niveau1")
            GameObject.Find("SpawnerTeeShirt").GetComponent<SpawnElement>().isPicked = true;
        if (gameObject.name == "Niveau1")
            GameObject.Find("SpawnerPantalon").GetComponent<SpawnElement>().isPicked = true;
        if (gameObject.name == "Niveau1")
            GameObject.Find("SpawnerChemise").GetComponent<SpawnElement>().isPicked = true;
        if (gameObject.name == "Niveau1")
            GameObject.Find("SpawnerManteau").GetComponent<SpawnElement>().isPicked = true;
        if (gameObject.name == "Niveau1")
            GameObject.Find("SpawnerChapeau").GetComponent<SpawnElement>().isPicked = true;
        GameObjectCommande.GetComponent<Text>().text = "";
        GameObjectContenu.GetComponent<Text>().text = "";
        GameObjectSelect.GetComponent<Text>().text = "";
        GameObject.Find("item").GetComponent<Text>().text = "";
        if (GameObject.Find("ToDestroy"))
            Destroy(GameObject.Find("ToDestroy"));
        GameObject ToDestroy = new GameObject("ToDestroy");
        ToDestroy.transform.parent = GameObjectNiveau.transform;
        if (Player1)
            if (gameObject.name == "Niveau1")
                Player1.transform.localPosition = new Vector3(0, 0, 0);
        StartCoroutine(CreateBox());
    }
}
