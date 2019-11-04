using UnityEngine;

public class MenuInGame : MonoBehaviour
{
    public GameObject currentGame;
    public GameObject currentMenuGame;
    public GameObject menuToLoad;
    public AudioClip clic;
    private GameObject mainCamera;
    private bool endlessness = false;
    
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        gameObject.AddComponent<AudioSource>().clip = clic;
    }
    
    void Update()
    {
        gameObject.GetComponent<AudioSource>().volume = mainCamera.GetComponent<ChangeCamera>().game;
    }

    // Activate the menu in game
    public void GetMenu()
    {
        gameObject.GetComponent<AudioSource>().Play();
        currentGame.SetActive(false);
        currentMenuGame.SetActive(false);
        menuToLoad.SetActive(true);
    }

    // close the menu in game
    public void Reprendre()
    {
        gameObject.GetComponent<AudioSource>().Play();
        currentGame.SetActive(true);
        currentMenuGame.SetActive(false);
        menuToLoad.SetActive(true);
    }

    // Restart a level
    public void Recommencer()
    {
        gameObject.GetComponent<AudioSource>().Play();
        currentGame.GetComponent<Niveau>().reset = true;
        currentGame.SetActive(true);
        currentMenuGame.SetActive(false);
        menuToLoad.SetActive(true);
    }

    // Go back to the selection level menu
    public void ChoixNiveau()
    {
        gameObject.GetComponent<AudioSource>().Play();
        currentGame.GetComponent<Niveau>().reset = true;
        currentGame.SetActive(false);
        currentMenuGame.SetActive(false);
        menuToLoad.SetActive(true);
    }

    // Go back to the main menu of the game
    public void Menu()
    {
        gameObject.GetComponent<AudioSource>().Play();
        currentGame.GetComponent<Niveau>().reset = true;
        currentMenuGame.SetActive(false);
        menuToLoad.SetActive(true);
    }


    // Launch a level
    public void LaunchGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        menuToLoad.GetComponent<Niveau>().isEndless = endlessness;
        currentGame.SetActive(false);
        currentMenuGame.SetActive(true);
        menuToLoad.SetActive(true);
    }

    // Set if the game will be endless or not
    public void EndlessGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        endlessness = !endlessness;
    }
}
