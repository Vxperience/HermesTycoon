using UnityEngine;
using UnityEngine.UI;

public class ChangeCamera : MonoBehaviour
{
    public GameObject niveau1;
    public GameObject niveau2;
    public GameObject niveau3;
    public GameObject menu;
    public float music; // Use to set the volume of the music audio element
    public float game; // Use to set the volume of all audio element except the music
    private bool isaudio;
    
    void Start()
    {
        music = 1;
        game = 1;
        isaudio = true;
    }
    
    void Update()
    {
        // Change the position of the camera to switch between the different level
        if (niveau1.activeInHierarchy) {
            transform.localPosition = new Vector3(0, 5, -30);
            menu.SetActive(false);
        } else if (niveau2.activeInHierarchy) {
            transform.localPosition = new Vector3(0, 5, -15);
            menu.SetActive(false);
        } else if (niveau3.activeInHierarchy) {
            transform.localPosition = new Vector3(0, 5, 0);
            menu.SetActive(false);
        } else {
            transform.localPosition = new Vector3(30, 5, 0);
            menu.SetActive(true);
        }
        

        // Interaction with the option menu
        if (GameObject.Find("SliderMusique")) {
            isaudio = GameObject.Find("Audio").GetComponent<Toggle>().isOn;
            if (isaudio) {
                GameObject.Find("SliderMusique").GetComponent<Slider>().interactable = true;
                GameObject.Find("SliderGame").GetComponent<Slider>().interactable = true;
                music = GameObject.Find("SliderMusique").GetComponent<Slider>().value;
                game = GameObject.Find("SliderGame").GetComponent<Slider>().value;
            } else {
                GameObject.Find("SliderMusique").GetComponent<Slider>().interactable = false;
                GameObject.Find("SliderGame").GetComponent<Slider>().interactable = false;
                music = 0;
                game = 0;
            }
        }
        GameObject.Find("Music").GetComponent<AudioSource>().volume = music;
    }
}
