using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    public GameObject spawner;
    public GameObject select;
    private bool selected;

    private void Start()
    {
        select = GameObject.Find("select");
    }

    void Update()
    {
        // New element animation
        if (transform.localScale.x < 1)
            transform.localScale += new Vector3(0.1f, 0.1f, 0);
        
        // Check if the element is select
        if (select.GetComponent<Text>().text != gameObject.name) {
            selected = false;
        }
    }

    private void OnMouseUp()
    {
        // Detect the click and change the select variable and indication in the game
        if (!select.GetComponent<Text>().text.Contains("Personnage")) {
            selected = !selected;
            if (selected) {
                select.GetComponent<Text>().text = gameObject.name;
            } else {
                select.GetComponent<Text>().text = "";
            }
        }
    }
}
