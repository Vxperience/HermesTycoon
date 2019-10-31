using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public GameObject GScene;
    public GameObject GNewScene;
    public GameObject Level;
    public AudioClip clic;

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

    public void GoToNewScene()
    {
        gameObject.GetComponent<AudioSource>().Play();
        GScene.SetActive(false);
        GNewScene.SetActive(true);
    }

    public void GoToNewLevel()
    {
        gameObject.GetComponent<AudioSource>().Play();
        GScene.SetActive(false);
        GNewScene.SetActive(true);
        Level.SetActive(true);
    }
}
