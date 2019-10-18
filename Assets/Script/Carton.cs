using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carton : MonoBehaviour
{
    public GameObject GameObjectNiveau;
    public GameObject GameObjectSelect;
    public GameObject GameObjectCommande;
    public GameObject GameObjectContenu;
    public bool newElement = false;
    private int NextContenu = 0;
    private string[] element = new string[] { "tee-shirt", "pantalon", "chemise" };
    private int nbElement;
    private string[] elementToCharge;
    public List<string> chargeElement = new List<string>();
    private string commande = "Commande:";
    private string contenu = "Contenu:";
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        selected = false;
        nbElement = Random.Range(1, 1);
        elementToCharge = new string[nbElement];
        for (int i = 0; i < nbElement; i++)
        {
            elementToCharge[i] = element[Random.Range(0, 2)];
            commande += "\n" + elementToCharge[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObjectNiveau.name == "Niveau1") {
            transform.localPosition += new Vector3(0.01f, 0, 0);
        }
        if (newElement) {
            AddContenu();
            newElement = false;
        }
        if (GameObjectSelect.GetComponent<Text>().text != gameObject.name) {
            selected = false;
            GameObjectContenu.GetComponent<Text>().text = "";
            GameObjectCommande.GetComponent<Text>().text = "";
        }
    }

    private void OnMouseUp()
    {
        selected = !selected;
        if (selected) {
            GameObjectSelect.GetComponent<Text>().text = gameObject.name;
            GameObjectContenu.GetComponent<Text>().text = contenu;
            GameObjectCommande.GetComponent<Text>().text = commande;
        } else {
            GameObjectSelect.GetComponent<Text>().text = "";
            GameObjectContenu.GetComponent<Text>().text = "";
            GameObjectCommande.GetComponent<Text>().text = "";
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
                    }
                }
                if (!findElement) {
                    goodBox = false;
                }
                findElement = false;
            }
            if (goodBox) {
                GameObjectNiveau.GetComponent<Niveau1>().nbCarton--;
            } else {
                GameObjectNiveau.GetComponent<Niveau1>().nbErreur--;
            }
            if (selected) {
                GameObjectSelect.GetComponent<Text>().text = "";
                GameObjectContenu.GetComponent<Text>().text = "";
                GameObjectCommande.GetComponent<Text>().text = "";
            }
            Destroy(gameObject);
        }
    }

    void AddContenu()
    {
        contenu += "\n" + chargeElement[NextContenu];
        NextContenu++;
    }
}
