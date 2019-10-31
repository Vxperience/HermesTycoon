using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public GameObject GScene;
    public GameObject GNewScene;
    public GameObject Level;
    public AudioClip clic;

    void Start()
    {
        gameObject.AddComponent<AudioSource>().clip = clic;
    }
    
    void Update()
    {
        gameObject.GetComponent<AudioSource>().volume = GameObject.Find("Main Camera").GetComponent<ChangeCamera>().game;
    }

    public void GoToNewScene()
    {
        // Change scene between two menu scene
        gameObject.GetComponent<AudioSource>().Play();
        GScene.SetActive(false);
        GNewScene.SetActive(true);
    }
}
