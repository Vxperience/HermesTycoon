using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Niveau1 : MonoBehaviour
{
    public bool reset = false;
    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
            ResetGame();
    }

    void ResetGame()
    {
        GameObject test = GameObject.Find("NewSprite");

        test.transform.localPosition = new Vector3(-8, 1.25f, 0);
        reset = false;
    }
}
