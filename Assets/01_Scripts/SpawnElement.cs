﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElement : MonoBehaviour
{
    public Sprite sprite;
    public string item;
    public bool isPicked = true;
    
    void Start()
    {

    }
    
    void Update()
    {
        if (isPicked)
            StartCoroutine(createElement());
    }

    // Create an initialised the new element
    IEnumerator createElement()
    {
        isPicked = false;
        yield return new WaitForSeconds(3);
        GameObject Element = new GameObject(item);
        Element.tag = "Element";
        Element.transform.parent = GameObject.Find("ToDestroy").transform;
        Element.AddComponent<SpriteRenderer>().sprite = sprite;
        Element.AddComponent<BoxCollider>().size = new Vector3(1, 1, 0.2f);
        Element.GetComponent<BoxCollider>().isTrigger = true;
        Element.AddComponent<Element>().spawner = transform.gameObject;
        Element.transform.localPosition = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        Element.transform.localRotation = Quaternion.Euler(90, 0, 0);
        Element.transform.localScale = new Vector3(0, 0, 0);
    }
}
