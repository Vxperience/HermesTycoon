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

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>().clip = clic;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<AudioSource>().volume = GameObject.Find("Main Camera").GetComponent<ChangeCamera>().game;
    }

    public void menuInGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        CurrentGame.SetActive(false);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    public void reprendre()
    {
        gameObject.GetComponent<AudioSource>().Play();
        CurrentGame.SetActive(true);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    public void recommencer()
    {
        gameObject.GetComponent<AudioSource>().Play();
        CurrentGame.GetComponent<Niveau>().reset = true;
        CurrentGame.SetActive(true);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    public void choixNiveau()
    {
        gameObject.GetComponent<AudioSource>().Play();
        CurrentGame.GetComponent<Niveau>().reset = true;
        CurrentGame.SetActive(false);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    public void menu()
    {
        gameObject.GetComponent<AudioSource>().Play();
        CurrentGame.GetComponent<Niveau>().reset = true;
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    public void launchGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        MenuToLoad.GetComponent<Niveau>().isEndless = endlessness;
        CurrentGame.SetActive(false);
        CurrentMenuGame.SetActive(true);
        MenuToLoad.SetActive(true);
    }

    public void endlessGame()
    {
        gameObject.GetComponent<AudioSource>().Play();
        endlessness = !endlessness;
    }
}
