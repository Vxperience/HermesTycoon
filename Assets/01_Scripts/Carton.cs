﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carton : MonoBehaviour
{
    public GameObject GameObjectNiveau;
    public GameObject GameObjectSelect;
    public List<GameObject> indicCommande = new List<GameObject>();
    public Sprite TeeShirt;
    public Sprite Pantalon;
    public Sprite Chemise;
    public Sprite Chapeau;
    public Sprite Manteau;
    public bool newElement = false;
    private int NextContenu = 0;
    private string[] element = new string[] {"tee-shirt", "pantalon", "chemise" , "chapeau", "manteau"};
    private int nbElement;
    private string[] elementToCharge;
    public List<string> chargeElement = new List<string>();
    private string commande = "Commande:";
    private string contenu = "Contenu:";
    private bool selected;
    private int path;

    // Start is called before the first frame update
    void Start()
    {
        selected = false;
        nbElement = Random.Range(1, 4);
        elementToCharge = new string[nbElement];
        for (int i = 0; i < nbElement; i++)
        {
            if (GameObjectNiveau.name == "Niveau2" || GameObjectNiveau.name == "Niveau3")
                elementToCharge[i] = element[GetIntInRange(Random.Range(0, 5), 0, 5, new int[] { 3 })];
            else
                elementToCharge[i] = element[Random.Range(0, 5)];
            GameObject indicElement = new GameObject();
            indicElement.name = elementToCharge[i];
            if (elementToCharge[i] == "tee-shirt")
                indicElement.AddComponent<SpriteRenderer>().sprite = TeeShirt;
            else if (elementToCharge[i] == "pantalon")
                indicElement.AddComponent<SpriteRenderer>().sprite = Pantalon;
            else if (elementToCharge[i] == "chemise")
                indicElement.AddComponent<SpriteRenderer>().sprite = Chemise;
            else if (elementToCharge[i] == "chapeau")
                indicElement.AddComponent<SpriteRenderer>().sprite = Chapeau;
            else if (elementToCharge[i] == "manteau")
                indicElement.AddComponent<SpriteRenderer>().sprite = Manteau;
            indicElement.transform.parent = gameObject.transform;
            indicElement.transform.localPosition = new Vector3(0, 0, 0);
            indicElement.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            indicElement.transform.localRotation = Quaternion.Euler(0, 0, 0);
            indicElement.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
            indicCommande.Add(indicElement);
            commande += "\n" + elementToCharge[i];
        }
        if (GameObjectNiveau.name == "Niveau3")
            path = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObjectNiveau.name == "Niveau1") {
            transform.localPosition += new Vector3(0.01f, 0, 0);
        } else if (GameObjectNiveau.name == "Niveau2") {
            transform.localPosition -= new Vector3(0, 0, 0.004f);
        } else if (GameObjectNiveau.name == "Niveau3") {
            if (transform.position.x >= -11 && transform.position.x <= -3.85)
                transform.localPosition += new Vector3(0.01f, 0, 0);
            else if (transform.position.x >= -3.85 && transform.position.x <= -3.5 && transform.position.z >= 0 && transform.position.z <= 2 && (path == 1 || path == 3))
                transform.localPosition += new Vector3(0, 0, 0.004f);
            else if (transform.position.x >= -3.85 && transform.position.x <= -3.5 && transform.position.z >= -2 && transform.position.z <= 0 && path == 2)
                transform.localPosition -= new Vector3(0, 0, 0.004f);
            else if ((transform.position.z >= 2 || transform.position.z <= -2) && transform.position.x >= -3.85 && transform.position.x <= 3.85)
                transform.localPosition += new Vector3(0.01f, 0, 0);
            else if (transform.position.x >= 3.85 && transform.position.z <= 2.5 && transform.position.z >= 0 && (path == 1 || path == 3))
                transform.localPosition -= new Vector3(0, 0, 0.004f);
            else if (transform.position.x >= 3.85 && transform.position.z <= 0 && transform.position.z >= -2.5 && path == 2)
                transform.localPosition += new Vector3(0, 0, 0.004f);
            else if (transform.position.x >= 3.85 && transform.position.z <= 0.1 && transform.position.z >= -0.1)
                transform.localPosition += new Vector3(0.01f, 0, 0);
        }
        if (newElement) {
            AddContenu();
            newElement = false;
        }
        if (GameObjectSelect.GetComponent<Text>().text != gameObject.name) {
            selected = false;
        }
        float x = 0;
        foreach (GameObject element in indicCommande) {
            if (indicCommande.Count % 2 == 0)
                element.transform.localPosition = new Vector3(x - (indicCommande.Count / 2) * 0.5f + 0.25f, -0.5f, 0);
            else
            element.transform.localPosition = new Vector3(x - (indicCommande.Count / 2) * 0.5f, -0.5f, 0);
            x += 0.5f;
        }
    }

    private void OnMouseUp()
    {
        if (!GameObjectSelect.GetComponent<Text>().text.Contains("Personnage")) {
            selected = !selected;
            if (selected)
            {
                GameObjectSelect.GetComponent<Text>().text = gameObject.name;
            }
            else
            {
                GameObjectSelect.GetComponent<Text>().text = "";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "CheckZone") {
            bool findElement = false;
            bool goodBox = true;

            if (chargeElement.Count == 0)
                goodBox = false;
            foreach (string element in chargeElement) {
                for (int i = 0; i < elementToCharge.Length; i++) {
                    if (element == elementToCharge[i]) {
                        findElement = true;
                        elementToCharge[i] = "done";
                        break;
                    }
                }
                if (!findElement) {
                    goodBox = false;
                }
                findElement = false;
            }
            for (int i = 0; i < elementToCharge.Length; i++)
                if (elementToCharge[i] != "done")
                    goodBox = false;
            if (goodBox) {
                GameObjectNiveau.GetComponent<Niveau>().nbCarton++;
            } else {
                GameObjectNiveau.GetComponent<Niveau>().nbErreur--;
            }
            if (selected) {
                GameObjectSelect.GetComponent<Text>().text = "";
            }
            Destroy(gameObject);
        }
    }

    void AddContenu()
    {
        GameObject currentElement;

        contenu += "\n" + chargeElement[NextContenu];
        NextContenu++;
        if (transform.Find(chargeElement[NextContenu - 1])) {
            currentElement = transform.Find(chargeElement[NextContenu - 1]).gameObject;
            currentElement.name = "done";
            currentElement.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
        } else {
            currentElement = new GameObject();
            currentElement.name = "wrong";

            if (chargeElement[NextContenu - 1] == "tee-shirt")
                currentElement.AddComponent<SpriteRenderer>().sprite = TeeShirt;
            else if (chargeElement[NextContenu - 1] == "pantalon")
                currentElement.AddComponent<SpriteRenderer>().sprite = Pantalon;
            else if (chargeElement[NextContenu - 1] == "chemise")
                currentElement.AddComponent<SpriteRenderer>().sprite = Chemise;
            else if (chargeElement[NextContenu - 1] == "chapeau")
                currentElement.AddComponent<SpriteRenderer>().sprite = Chapeau;
            else if (chargeElement[NextContenu - 1] == "manteau")
                currentElement.AddComponent<SpriteRenderer>().sprite = Manteau;
            currentElement.transform.parent = gameObject.transform;
            currentElement.transform.localPosition = new Vector3(0, 0, 0);
            currentElement.transform.localScale = new Vector3(0.5f, 0.5f, 1);
            currentElement.transform.localRotation = Quaternion.Euler(0, 0, 0);
            currentElement.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
            indicCommande.Add(currentElement);
        }
    }

    int GetIntInRange(int res, int x, int y, int[] tab)
    {
        for (int i = 0; i < tab.Length; i++) {
            if (res == tab[i])
                return GetIntInRange(Random.Range(x, y), x, y, tab);
        }
        return res;
    }
}