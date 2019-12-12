using UnityEngine;

public class HMT_CameraPosInGame : MonoBehaviour
{
    public GameObject[] scene;
    
    void Update()
    {
        // Change the position of the camera to switch between the different level
        if (scene[0].activeInHierarchy) {
            transform.localPosition = new Vector3(0, 5, -30);
            scene[3].SetActive(false);
        } else if (scene[1].activeInHierarchy) {
            transform.localPosition = new Vector3(0, 5, -15);
            scene[3].SetActive(false);
        } else if (scene[2].activeInHierarchy) {
            transform.localPosition = new Vector3(0, 5, 0);
            scene[3].SetActive(false);
        } else {
            transform.localPosition = new Vector3(30, 5, 0);
            scene[3].SetActive(true);
        }
    }
}
