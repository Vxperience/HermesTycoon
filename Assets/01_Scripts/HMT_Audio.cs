using UnityEngine;
using UnityEngine.UI;

public class HMT_Audio : MonoBehaviour
{
    public bool isaudio;
    public float music; // manage the volume of the music
    public float game; // manage the volume of the diiferent sound of the game execpt the music
    private bool resetSlider;

    void Start()
    {
        isaudio = true;
        if (PlayerPrefs.GetInt("setaudio") != 0) {
            music = PlayerPrefs.GetFloat("music");
            game = PlayerPrefs.GetFloat("game");
        } else {
            music = 1f;
            game = 1f;
            PlayerPrefs.SetInt("setaudio", 1);
        }
        PlayerPrefs.SetFloat("music", music);
        PlayerPrefs.SetFloat("game", game);
        PlayerPrefs.SetInt("isaudio", isaudio ? 1 : 0);
    }
    
    void Update()
    {
        // Interaction with the option menu
        if (GameObject.Find("SliderMusique"))
        {
            if (resetSlider) {
                GameObject.Find("SliderMusique").GetComponent<Slider>().value = PlayerPrefs.GetFloat("music");
                GameObject.Find("SliderGame").GetComponent<Slider>().value = PlayerPrefs.GetFloat("game");
                resetSlider = false;
            }
            isaudio = GameObject.Find("IsAudio").GetComponent<Toggle>().isOn;
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
            PlayerPrefs.SetFloat("music", music);
            PlayerPrefs.SetFloat("game", game);
            PlayerPrefs.SetInt("isaudio", isaudio ? 1 : 0);
        }
        else
            resetSlider = true;
        GameObject.Find("Music").GetComponent<AudioSource>().volume = music;
    }
}
