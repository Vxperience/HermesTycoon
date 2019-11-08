using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuGame : MonoBehaviour
{
    public GameObject[] scene;
    public GameObject cam;
    public AudioClip clic;
    private int currentScene;

    // get the variable niveau in playerprefs to know wich of the level is launched
    void Start()
    {
        cam.GetComponent<AudioSource>().clip = clic;
        if (PlayerPrefs.GetString("niveau") == "Niveau1") {
            scene[0].SetActive(true);
            currentScene = 0;
        } else if (PlayerPrefs.GetString("niveau") == "Niveau2") {
            scene[1].SetActive(true);
            currentScene = 1;
        } else {
            scene[2].SetActive(true);
            currentScene = 2;
        }
        scene[3].SetActive(true);
    }

    // adjust the volume of the sound
    void Update()
    {
        cam.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("game");
    }

    // get the menu
    public void GameToMenu()
    {
        cam.GetComponent<AudioSource>().Play();
        scene[currentScene].SetActive(false);
        scene[3].SetActive(false);
        scene[4].SetActive(true);
    }

    // continue the game
    public void MenuToGame()
    {
        cam.GetComponent<AudioSource>().Play();
        scene[currentScene].SetActive(true);
        scene[3].SetActive(true);
        scene[4].SetActive(false);
    }

    // restart the level
    public void RestartLevel()
    {
        cam.GetComponent<AudioSource>().Play();
        scene[currentScene].SetActive(true);
        scene[3].SetActive(true);
        scene[4].SetActive(false);
        scene[currentScene].GetComponent<Niveau>().reset = true;
        GameObject.Find("Tutoriel").GetComponent<Tutoriel>().reset = true;
        GameObject.Find("select").GetComponent<Text>().text = "";
    }

    // return to the choice level scene
    public void GameToChoixNiveau()
    {
        PlayerPrefs.SetString("menuToLoad", "ChoixNiveau");
        SceneManager.LoadScene(0);
    }

    // go to the option menu
    public void GameToOption()
    {
        cam.GetComponent<AudioSource>().Play();
        scene[4].SetActive(false);
        scene[5].SetActive(true);
    }

    // return to the game menu
    public void OptionToGame()
    {
        cam.GetComponent<AudioSource>().Play();
        scene[5].SetActive(false);
        scene[4].SetActive(true);
    }

    // go to the main menu
    public void GameToMainMenu()
    {
        PlayerPrefs.SetString("menuToLoad", "Menu");
        SceneManager.LoadScene(0);
    }

    // quit the application
    public void QuitGame()
    {
        Application.Quit();
    }
}
