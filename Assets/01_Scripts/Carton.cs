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
            if (GameObjectNiveau.name == "Niveau2" && GameObjectNiveau.name == "Niveau3")
                elementToCharge[i] = element[GetIntInRange(Random.Range(0, 5), 0, 5, new int[] { 3 })];
            else
                elementToCharge[i] = element[Random.Range(0, 5)];
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
            GameObjectContenu.GetComponent<Text>().text = "";
            GameObjectCommande.GetComponent<Text>().text = "";
        }
    }

    private void OnMouseUp()
    {
        if (!GameObjectSelect.GetComponent<Text>().text.Contains("Personnage")) {
            selected = !selected;
            if (selected)
            {
                GameObjectSelect.GetComponent<Text>().text = gameObject.name;
                GameObjectContenu.GetComponent<Text>().text = contenu;
                GameObjectCommande.GetComponent<Text>().text = commande;
            }
            else
            {
                GameObjectSelect.GetComponent<Text>().text = "";
                GameObjectContenu.GetComponent<Text>().text = "";
                GameObjectCommande.GetComponent<Text>().text = "";
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

    int GetIntInRange(int res, int x, int y, int[] tab)
    {
        for (int i = 0; i < tab.Length; i++) {
            if (res == tab[i])
                return GetIntInRange(Random.Range(x, y), x, y, tab);
        }
        return res;
    }
}
