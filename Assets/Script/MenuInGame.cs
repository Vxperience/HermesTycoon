using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInGame : MonoBehaviour
{
    public GameObject CurrentGame;
    public GameObject CurrentMenuGame;
    public GameObject MenuToLoad;
    private bool endlessness = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void menuInGame()
    {
        CurrentGame.SetActive(false);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    public void reprendre()
    {
        CurrentGame.SetActive(true);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    public void recommencer()
    {
        CurrentGame.GetComponent<Niveau>().reset = true;
        CurrentGame.SetActive(true);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    public void choixNiveau()
    {
        CurrentGame.GetComponent<Niveau>().reset = true;
        CurrentGame.SetActive(false);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    public void menu()
    {
        CurrentGame.GetComponent<Niveau>().reset = true;
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    public void launchGame()
    {
        MenuToLoad.GetComponent<Niveau>().isEndless = endlessness;
        CurrentGame.SetActive(false);
        CurrentMenuGame.SetActive(true);
        MenuToLoad.SetActive(true);
    }

    public void endlessGame()
    {
        endlessness = !endlessness;
    }
}
