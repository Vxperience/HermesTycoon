using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInGame : MonoBehaviour
{
    public GameObject CurrentGame;
    public GameObject CurrentMenuGame;
    public GameObject MenuToLoad;
    public AudioClip clic;
    private bool endlessness = false;
    
    void Start()
    {
        gameObject.AddComponent<AudioSource>().clip = clic;
    }
    
    void Update()
    {
        gameObject.GetComponent<AudioSource>().volume = GameObject.Find("Main Camera").GetComponent<ChangeCamera>().game;
    }

    // Activate the menu in game
    public void menuInGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        CurrentGame.SetActive(false);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    // close the menu in game
    public void reprendre()
    {
        gameObject.GetComponent<AudioSource>().Play();
        CurrentGame.SetActive(true);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    // Restart a level
    public void recommencer()
    {
        gameObject.GetComponent<AudioSource>().Play();
        CurrentGame.GetComponent<Niveau>().reset = true;
        CurrentGame.SetActive(true);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    // Go back to the selection level menu
    public void choixNiveau()
    {
        gameObject.GetComponent<AudioSource>().Play();
        CurrentGame.GetComponent<Niveau>().reset = true;
        CurrentGame.SetActive(false);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    // Go back to the main menu of the game
    public void menu()
    {
        gameObject.GetComponent<AudioSource>().Play();
        CurrentGame.GetComponent<Niveau>().reset = true;
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }


    // Launch a level
    public void launchGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        MenuToLoad.GetComponent<Niveau>().isEndless = endlessness;
        CurrentGame.SetActive(false);
        CurrentMenuGame.SetActive(true);
        MenuToLoad.SetActive(true);
    }

    // Set if the game will be endless or not
    public void endlessGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        endlessness = !endlessness;
    }
}
