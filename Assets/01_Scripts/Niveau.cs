using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Niveau : MonoBehaviour
{
    public GameObject gameObjectNiveau;
    public GameObject gameObjectSelect;
    public GameObject player1;
    public GameObject player2;
    public GameObject Camera;
    public GameObject[] erreur;
    public GameObject hudCarton;
    public Sprite boxToCreate1;
    public Sprite boxToCreate2;
    public Sprite boxToCreate3;
    public Sprite boxToCreate4;
    public Sprite ceinture;
    public Sprite pantalon;
    public Sprite chemise;
    public Sprite chaussure;
    public Sprite montre;
    public Sprite[] spriteErreur;
    public bool isEndless;
    public bool reset = false;
    public bool updateScore;
    public int nbCarton;
    public int nbErreur;
    private GameObject endMessage;
    private GameObject info;
    private bool tuto;
    private bool tutoChecked;
    private bool firstBox;
    private int timer;
    private int nbBoxSprite;
    
    void Start()
    {
        endMessage = GameObject.Find("endMessage");
        info = GameObject.Find("info");
        Camera = GameObject.Find("Main Camera");
        isEndless = PlayerPrefs.GetInt("isendless") != 0 ? true : false;
        tuto = PlayerPrefs.GetInt("tuto") != 0 ? true : false;
        tutoChecked = false;
        ResetGame();
    }
    
    void Update()
    {
        tuto = PlayerPrefs.GetInt("tuto") != 0 ? true : false;
        // Check if the game as to be reset
        if (reset)
            ResetGame();

        // Manage the end of the tuto and the start of the game
        if (tuto && !tutoChecked) {
            StartCoroutine(CreateBox());
            tutoChecked = true;
        }

        // Manage the endgame
        if (isEndless) {
            hudCarton.GetComponentInChildren<Text>().text = nbCarton.ToString();
            if (nbErreur <= 0) {
                endMessage.GetComponent<Text>().text = "Game Over";
                Destroy(GameObject.Find("ToDestroy"));
                StartCoroutine(EndGame());
            } else
                endMessage.GetComponent<Text>().text = "";
        } else {
            hudCarton.GetComponentInChildren<Text>().text = nbCarton.ToString() + " / 5";
            if (nbCarton >= 5 || nbErreur <= 0) {
                if (nbCarton >= 5)
                    endMessage.GetComponent<Text>().text = "Good Job";
                else
                    endMessage.GetComponent<Text>().text = "Game Over";
                Destroy(GameObject.Find("ToDestroy"));
                StartCoroutine(EndGame());
            } else
                endMessage.GetComponent<Text>().text = "";
        }

        // Mange the nberreur UI
        if (updateScore) {
            int toMark = 3 - nbErreur;
            for (int i = 0; i < 3; i++) {
                if (toMark > 0) {
                    erreur[i].GetComponent<Image>().sprite = spriteErreur[1];
                    toMark--;
                } else
                    erreur[i].GetComponent<Image>().sprite = spriteErreur[0];
            }
            updateScore = false;
        }
    }

    IEnumerator CreateBox()
    {
        // Create and initialised box
        if (firstBox) {
            yield return new WaitForSeconds(2);
            firstBox = false;
        } else
            yield return new WaitForSeconds(timer);
        if (GameObject.Find("ToDestroy")) {
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
            Carton.GetComponent<Carton>().ceinture = ceinture;
            Carton.GetComponent<Carton>().pantalon = pantalon;
            Carton.GetComponent<Carton>().chemise = chemise;
            Carton.GetComponent<Carton>().chaussure = chaussure;
            Carton.GetComponent<Carton>().montre = montre;
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

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3);
        PlayerPrefs.SetString("menuToLoad", "Menu");
        SceneManager.LoadScene(0);
    }

    void ResetGame()
    {
        // Reset the whole Level
        reset = false;
        tutoChecked = false;
        updateScore = false;
        firstBox = true;
        nbCarton = 0;
        nbErreur = 3;
        
        if (gameObject.name == "Niveau1" || gameObject.name == "Niveau3")
            timer = 12;
        if (gameObject.name == "Niveau2")
            timer = 20;
        if (gameObject.name == "Niveau1" || gameObject.name == "Niveau2" || gameObject.name == "Niveau3")
            GameObject.Find("SpawnerCeinture").GetComponent<SpawnElement>().isPicked = true;
        if (gameObject.name == "Niveau1" || gameObject.name == "Niveau2" || gameObject.name == "Niveau3")
            GameObject.Find("SpawnerPantalon").GetComponent<SpawnElement>().isPicked = true;
        if (gameObject.name == "Niveau1" || gameObject.name == "Niveau2" || gameObject.name == "Niveau3")
            GameObject.Find("SpawnerChemise").GetComponent<SpawnElement>().isPicked = true;
        if (gameObject.name == "Niveau1" || gameObject.name == "Niveau2" || gameObject.name == "Niveau3")
            GameObject.Find("SpawnerMontre").GetComponent<SpawnElement>().isPicked = true;
        if (gameObject.name == "Niveau1")
            GameObject.Find("SpawnerChaussure").GetComponent<SpawnElement>().isPicked = true;
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
        if (tuto && !tutoChecked) {
            StartCoroutine(CreateBox());
            tutoChecked = true;
        }
        for (int i = 0; i < erreur.Length; i++)
            erreur[i].GetComponent<Image>().sprite = spriteErreur[0];
        hudCarton.GetComponentInChildren<Text>().text = nbCarton.ToString();
    }
}
