﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Niveau : MonoBehaviour
{
    public GameObject gameObjectNiveau;
    public GameObject gameObjectSelect;
    public GameObject player1;
    public GameObject player2;
    public Sprite boxToCreate1;
    public Sprite boxToCreate2;
    public Sprite boxToCreate3;
    public Sprite boxToCreate4;
    public Sprite teeShirt;
    public Sprite pantalon;
    public Sprite chemise;
    public Sprite chapeau;
    public Sprite manteau;
    public bool isEndless;
    public bool reset = false;
    public int nbCarton;
    public int nbErreur;
    private GameObject endMessage;
    private GameObject info;
    private int timer;
    private int nbBoxSprite;
    
    void Start()
    {
        endMessage = GameObject.Find("endMessage");
        info = GameObject.Find("info");
        ResetGame();
    }
    
    void Update()
    {
        // Check if the game as to be reset
        if (reset)
            ResetGame();

        // Manage the endgame
        if (isEndless)
        {
            if (nbErreur <= 0) {
                endMessage.GetComponent<Text>().text = "Game Over";
                Destroy(GameObject.Find("ToDestroy"));
            } else
                endMessage.GetComponent<Text>().text = "";
            info.GetComponent<Text>().text = "Nombre de carton remplis: " + nbCarton + " Nombre d'erreur restantes: " + nbErreur;
        }
        else {
            if (nbCarton >= 5 || nbErreur <= 0) {
                if (nbCarton >= 5)
                    endMessage.GetComponent<Text>().text = "Good Job";
                else
                    endMessage.GetComponent<Text>().text = "Game Over";
                Destroy(GameObject.Find("ToDestroy"));
            } else
                endMessage.GetComponent<Text>().text = "";
            info.GetComponent<Text>().text = "Nombre de carton à remplir: " + nbCarton + " / 5 Nombre d'erreur restantes: " + nbErreur;
        }
    }

    IEnumerator CreateBox()
    {
        // Create and initialised box
        yield return new WaitForSeconds(timer);
        if (GameObject.Find("ToDestroy")) {
            if (timer > 8)
                timer--;
            GameObject Carton = new GameObject("Carton");
            Carton.transform.parent = GameObject.Find("ToDestroy").transform;
            nbBoxSprite = Random.Range(0, 4);
            if (nbBoxSprite == 0)
                Carton.AddComponent<SpriteRenderer>().sprite = boxToCreate1;
            else if (nbBoxSprite == 1)
                Carton.AddComponent<SpriteRenderer>().sprite = boxToCreate2;
            else if (nbBoxSprite == 2)
                Carton.AddComponent<SpriteRenderer>().sprite = boxToCreate3;
            else
                Carton.AddComponent<SpriteRenderer>().sprite = boxToCreate4;
            Carton.AddComponent<BoxCollider>().size = new Vector3(1.2f, 1.2f, 1);
            Carton.GetComponent<BoxCollider>().isTrigger = true;
            Carton.AddComponent<Rigidbody>().useGravity = false;
            Carton.AddComponent<Carton>().gameObjectNiveau = gameObjectNiveau;
            Carton.GetComponent<Carton>().gameObjectSelect = gameObjectSelect;
            Carton.GetComponent<Carton>().teeShirt = teeShirt;
            Carton.GetComponent<Carton>().pantalon = pantalon;
            Carton.GetComponent<Carton>().chemise = chemise;
            Carton.GetComponent<Carton>().chapeau = chapeau;
            Carton.GetComponent<Carton>().manteau = manteau;
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
        // Reset the whole Level
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
        gameObjectSelect.GetComponent<Text>().text = "";
        if (GameObject.Find("ToDestroy"))
            Destroy(GameObject.Find("ToDestroy"));
        GameObject ToDestroy = new GameObject("ToDestroy");
        ToDestroy.transform.parent = gameObjectNiveau.transform;
        if (gameObject.name == "Niveau1") {
            player1.transform.localPosition = new Vector3(0, 0.05f, 0);
            player1.GetComponent<Personnage>().item = "";
        } else if (gameObject.name == "Niveau2") {
            player1.transform.localPosition = new Vector3(-3, 0.05f, 0);
            player1.GetComponent<Personnage>().item = "";
            player2.transform.localPosition = new Vector3(3, 0.05f, 0);
            player2.GetComponent<Personnage>().item = "";
        } else if (gameObject.name == "Niveau3") {
            player1.transform.localPosition = new Vector3(0, 0.05f, -3.75f);
            player1.GetComponent<Personnage>().item = "";
            player2.transform.localPosition = new Vector3(0, 0.05f, 3.75f);
            player2.GetComponent<Personnage>().item = "";
        }
        StartCoroutine(CreateBox());
    }
}
