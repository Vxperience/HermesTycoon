using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HMT_Menu : MonoBehaviour
{
    public GameObject[] scene;
    public GameObject cam;
    public AudioClip clic;
    public bool isendless;

    // search in the playerpref to know which scene launched
    void Start()
    {
        cam.GetComponent<AudioSource>().clip = clic;
        if (PlayerPrefs.GetString("menuToLoad") == "ChoixNiveau")
            scene[1].SetActive(true);
        else
            scene[0].SetActive(true);
    }
    
    // adjust volume of the game
    void Update()
    {
        cam.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("game");
    }

    // go to the choice level scene
    public void MenuToChoixNiveau()
    {
        cam.GetComponent<AudioSource>().Play();
        scene[0].SetActive(false);
        scene[1].SetActive(true);
    }

    // go to hte option menu
    public void MenuToOption()
    {
        cam.GetComponent<AudioSource>().Play();
        scene[0].SetActive(false);
        scene[2].SetActive(true);
    }

    // return to the menu
    public void ChoixNiveauToMenu()
    {
        cam.GetComponent<AudioSource>().Play();
        scene[1].SetActive(false);
        scene[0].SetActive(true);
    }

    // set the game in endless or normale mode
    public void EndlessGame()
    {
        cam.GetComponent<AudioSource>().Play();
        isendless = GameObject.Find("Endless").GetComponent<Toggle>().isOn;
        PlayerPrefs.SetInt("isendless", isendless ? 1 : 0);
    }

    // launch a level
    public void LaunchGame(string level)
    {
        cam.GetComponent<AudioSource>().Play();
        PlayerPrefs.SetString("niveau", level);
        SceneManager.LoadScene("HMT_level");
    }

    // return to the menu
    public void OptionToMenu()
    {
        cam.GetComponent<AudioSource>().Play();
        scene[2].SetActive(false);
        scene[0].SetActive(true);
    }

    // quit the game
    public void QuitGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.DeleteKey("isendless");
        PlayerPrefs.DeleteKey("tuto");
        PlayerPrefs.DeleteKey("menuToLoad");
        PlayerPrefs.DeleteKey("niveau");
        PlayerPrefs.DeleteKey("setaudio");
        PlayerPrefs.DeleteKey("music");
        PlayerPrefs.DeleteKey("game");
        SceneManager.LoadScene(1);
    }
}
