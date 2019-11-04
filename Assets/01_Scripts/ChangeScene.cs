using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public GameObject gScene;
    public GameObject gNewScene;
    public AudioClip clic;
    private GameObject mainCamera;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        gameObject.AddComponent<AudioSource>().clip = clic;
    }
    
    void Update()
    {
        gameObject.GetComponent<AudioSource>().volume = mainCamera.GetComponent<ChangeCamera>().game;
    }

    public void GoToNewScene()
    {
        // Change scene between two menu scene
        gameObject.GetComponent<AudioSource>().Play();
        gScene.SetActive(false);
        gNewScene.SetActive(true);
    }
}
