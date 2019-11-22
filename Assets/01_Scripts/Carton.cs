using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carton : MonoBehaviour
{
    public GameObject gameObjectNiveau;
    public GameObject gameObjectSelect;
    public List<GameObject> indicCommande = new List<GameObject>();
    public Sprite ceinture;
    public Sprite pantalon;
    public Sprite chemise;
    public Sprite chaussure;
    public Sprite montre;
    public bool newElement = false;
    private int nextContenu = 0;
    private string[] element = new string[] {"ceinture", "pantalon", "chemise" , "chaussure", "montre"};
    private int nbElement;
    private string[] elementToCharge;
    public List<string> chargeElement = new List<string>();
    private bool selected;
    private int path;
    
    void Start()
    {
        selected = false;

        // Create a random list of element to charge and the indications
        nbElement = Random.Range(1, 4);
        elementToCharge = new string[nbElement];
        for (int i = 0; i < nbElement; i++)
        {
            // random element
            if (gameObjectNiveau.name == "Niveau2" || gameObjectNiveau.name == "Niveau3")
                elementToCharge[i] = element[GetIntInRange(Random.Range(0, 5), 0, 5, new int[] { 3 })];
            else
                elementToCharge[i] = element[Random.Range(0, 5)];
            // indications
            GameObject indicElement = new GameObject();
            indicElement.name = elementToCharge[i];
            if (elementToCharge[i] == "ceinture")
                indicElement.AddComponent<SpriteRenderer>().sprite = ceinture;
            else if (elementToCharge[i] == "pantalon")
                indicElement.AddComponent<SpriteRenderer>().sprite = pantalon;
            else if (elementToCharge[i] == "chemise")
                indicElement.AddComponent<SpriteRenderer>().sprite = chemise;
            else if (elementToCharge[i] == "chaussure")
                indicElement.AddComponent<SpriteRenderer>().sprite = chaussure;
            else if (elementToCharge[i] == "montre")
                indicElement.AddComponent<SpriteRenderer>().sprite = montre;
            indicElement.transform.parent = gameObject.transform;
            indicElement.transform.localPosition = new Vector3(0, 0, 0);
            indicElement.transform.localScale = new Vector3(0.1f, 0.1f, 1);
            indicElement.transform.localRotation = Quaternion.Euler(0, 0, 0);
            indicElement.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
            indicCommande.Add(indicElement);
        }

        // If the level have multiple path for the box, He will be assign by a random
        if (gameObjectNiveau.name == "Niveau3")
            path = Random.Range(1, 3);
    }
    
    void Update()
    {
        // The move of the box
        if (gameObjectNiveau.name == "Niveau1") {
            transform.localPosition += new Vector3(0.007f, 0, 0);
        } else if (gameObjectNiveau.name == "Niveau2") {
            transform.localPosition -= new Vector3(0, 0, 0.004f);
        } else if (gameObjectNiveau.name == "Niveau3") {
            if (transform.position.x >= -11 && transform.position.x <= -3.85)
                transform.localPosition += new Vector3(0.007f, 0, 0);
            else if (transform.position.x >= -3.85 && transform.position.x <= -3.5 && transform.position.z >= 0 && transform.position.z <= 2 && (path == 1 || path == 3))
                transform.localPosition += new Vector3(0, 0, 0.004f);
            else if (transform.position.x >= -3.85 && transform.position.x <= -3.5 && transform.position.z >= -2 && transform.position.z <= 0 && path == 2)
                transform.localPosition -= new Vector3(0, 0, 0.004f);
            else if ((transform.position.z >= 2 || transform.position.z <= -2) && transform.position.x >= -3.85 && transform.position.x <= 3.85)
                transform.localPosition += new Vector3(0.007f, 0, 0);
            else if (transform.position.x >= 3.85 && transform.position.z <= 2.5 && transform.position.z >= 0 && (path == 1 || path == 3))
                transform.localPosition -= new Vector3(0, 0, 0.004f);
            else if (transform.position.x >= 3.85 && transform.position.z <= 0 && transform.position.z >= -2.5 && path == 2)
                transform.localPosition += new Vector3(0, 0, 0.004f);
            else if (transform.position.x >= 3.85 && transform.position.z <= 0.1 && transform.position.z >= -0.1)
                transform.localPosition += new Vector3(0.007f, 0, 0);
        }

        // If the box have a new element
        if (newElement) {
            AddContenu();
            newElement = false;
        }

        // Check if select
        if (gameObjectSelect.GetComponent<Text>().text != gameObject.name) {
            selected = false;
        }

        // Display the indications and the element in the box
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
        // Check if a player is select before changed the select variable and indication in the game
        if (!gameObjectSelect.GetComponent<Text>().text.Contains("Personnage")) {
            selected = !selected;
            if (selected)
                gameObjectSelect.GetComponent<Text>().text = gameObject.name;
            else
                gameObjectSelect.GetComponent<Text>().text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the box enter in the checkzone and proceed to the validation of the box or not. Change the score on the UI. Check if the box was select then destroy it
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
                if (!findElement)
                    goodBox = false;
                findElement = false;
            }
            for (int i = 0; i < elementToCharge.Length; i++)
                if (elementToCharge[i] != "done")
                    goodBox = false;
            if (goodBox)
                gameObjectNiveau.GetComponent<Niveau>().nbCarton++;
            else
                gameObjectNiveau.GetComponent<Niveau>().nbErreur--;
            if (selected)
                gameObjectSelect.GetComponent<Text>().text = "";
            gameObjectNiveau.GetComponent<Niveau>().updateScore = true;
            Destroy(gameObject);
        }
    }

    void AddContenu()
    {
        // Add the new element to the box and update or create new indication of the element in the box
        GameObject currentElement;
        
        nextContenu++;
        if (transform.Find(chargeElement[nextContenu - 1])) {
            currentElement = transform.Find(chargeElement[nextContenu - 1]).gameObject;
            currentElement.name = "done";
            currentElement.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
        } else {
            currentElement = new GameObject();
            currentElement.name = "wrong";
            
            if (chargeElement[nextContenu - 1] == "ceinture")
                currentElement.AddComponent<SpriteRenderer>().sprite = ceinture;
            else if (chargeElement[nextContenu - 1] == "pantalon")
                currentElement.AddComponent<SpriteRenderer>().sprite = pantalon;
            else if (chargeElement[nextContenu - 1] == "chemise")
                currentElement.AddComponent<SpriteRenderer>().sprite = chemise;
            else if (chargeElement[nextContenu - 1] == "chaussure")
                currentElement.AddComponent<SpriteRenderer>().sprite = chaussure;
            else if (chargeElement[nextContenu - 1] == "montre")
                currentElement.AddComponent<SpriteRenderer>().sprite = montre;
            currentElement.transform.parent = gameObject.transform;
            currentElement.transform.localPosition = new Vector3(0, 0, 0);
            currentElement.transform.localScale = new Vector3(0.1f, 0.1f, 1);
            currentElement.transform.localRotation = Quaternion.Euler(0, 0, 0);
            currentElement.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
            indicCommande.Add(currentElement);
        }
    }

    int GetIntInRange(int res, int x, int y, int[] tab)
    {
        // Get a random int "res" between "x" and "y" with a array "tab" of value that the random will not be able to return
        for (int i = 0; i < tab.Length; i++) {
            if (res == tab[i])
                return GetIntInRange(Random.Range(x, y), x, y, tab);
        }
        return res;
    }
}
