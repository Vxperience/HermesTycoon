using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInGame : MonoBehaviour
{
    public GameObject CurrentGame;
    public GameObject CurrentMenuGame;
    public GameObject MenuToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void menuingame()
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
        CurrentGame.GetComponent<Niveau1>().reset = true;
        CurrentGame.SetActive(true);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    public void choixniveau()
    {
        CurrentGame.GetComponent<Niveau1>().reset = true;
        CurrentGame.SetActive(false);
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }

    public void menu()
    {
        CurrentGame.GetComponent<Niveau1>().reset = true;
        CurrentMenuGame.SetActive(false);
        MenuToLoad.SetActive(true);
    }
}
