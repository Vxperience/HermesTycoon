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
    public GameObject Player2;
    public Sprite BoxToCreate1;
    public Sprite BoxToCreate2;
    public Sprite BoxToCreate3;
    public Sprite BoxToCreate4;
    public bool isEndless;
    public bool reset = false;
    public int nbCarton;
    public int nbErreur;
    private int timer;
    private int nbBoxSprite;

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
            nbBoxSprite = Random.Range(0, 4);
            if (nbBoxSprite == 0)
                Carton.AddComponent<SpriteRenderer>().sprite = BoxToCreate1;
            else if (nbBoxSprite == 1)
                Carton.AddComponent<SpriteRenderer>().sprite = BoxToCreate2;
            else if (nbBoxSprite == 2)
                Carton.AddComponent<SpriteRenderer>().sprite = BoxToCreate3;
            else
                Carton.AddComponent<SpriteRenderer>().sprite = BoxToCreate4;
            Carton.AddComponent<BoxCollider>().size = new Vector3(1.2f, 1.2f, 1);
            Carton.GetComponent<BoxCollider>().isTrigger = true;
            Carton.AddComponent<Rigidbody>().useGravity = false;
            Carton.AddComponent<Carton>().GameObjectNiveau = GameObjectNiveau;
            Carton.GetComponent<Carton>().GameObjectSelect = GameObjectSelect;
            Carton.GetComponent<Carton>().GameObjectCommande = GameObjectCommande;
            Carton.GetComponent<Carton>().GameObjectContenu = GameObjectContenu;
            if (gameObject.name == "Niveau1")
                Carton.transform.localPosition = new Vector3(-11, 0.05f, -27);
            if (gameObject.name == "Niveau2")
                Carton.transform.localPosition = new Vector3(0, 0.05f, -9);
            if (gameObject.name == "Niveau3")
                Carton.transform.localPosition = new Vector3(-11, 0.05f, 0);
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
        
        if (gameObject.name == "Niveau1" || gameObject.name == "Niveau3")
            timer = 8;
        if (gameObject.name == "Niveau2")
            timer = 16;
        if (gameObject.name == "Niveau1" || gameObject.name == "Niveau2" || gameObject.name == "Niveau3")
            GameObject.Find("SpawnerTeeShirt").GetComponent<SpawnElement>().isPicked = true;
        if (gameObject.name == "Niveau1" || gameObject.name == "Niveau2" || gameObject.name == "Niveau3")
            GameObject.Find("SpawnerPantalon").GetComponent<SpawnElement>().isPicked = true;
        if (gameObject.name == "Niveau1" || gameObject.name == "Niveau2" || gameObject.name == "Niveau3")
            GameObject.Find("SpawnerChemise").GetComponent<SpawnElement>().isPicked = true;
        if (gameObject.name == "Niveau1" || gameObject.name == "Niveau2" || gameObject.name == "Niveau3")
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
        if (gameObject.name == "Niveau1") {
            Player1.transform.localPosition = new Vector3(0, 0.05f, 0);
        } else if (gameObject.name == "Niveau2") {
            Player1.transform.localPosition = new Vector3(-3, 0.05f, 0);
            Player2.transform.localPosition = new Vector3(3, 0.05f, 0);
        } else if (gameObject.name == "Niveau3") {
            Player1.transform.localPosition = new Vector3(0, 0.05f, -3.75f);
            Player2.transform.localPosition = new Vector3(0, 0.05f, 3.75f);
        }
        StartCoroutine(CreateBox());
    }
}
