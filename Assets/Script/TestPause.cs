﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPause : MonoBehaviour
{
    private bool selected;
    // Start is called before the first frame update
    void Start()
    {
        selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("select").GetComponent<Text>().text != gameObject.name)
            selected = false;
        transform.localPosition += new Vector3(0.01f, 0, 0);
    }

    private void OnMouseUp()
    {
        selected = !selected;
        if (selected)
            GameObject.Find("select").GetComponent<Text>().text = gameObject.name;
        else
            GameObject.Find("select").GetComponent<Text>().text = "";
    }
}
