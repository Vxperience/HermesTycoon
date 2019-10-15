using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public GameObject GScene;
    public GameObject GNewScene;
    public GameObject Level;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToNewScene()
    {
        GScene.SetActive(false);
        GNewScene.SetActive(true);
    }

    public void GoToNewLevel()
    {
        GScene.SetActive(false);
        GNewScene.SetActive(true);
        Level.SetActive(true);
    }
}
