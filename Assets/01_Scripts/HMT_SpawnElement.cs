using System.Collections;
using UnityEngine;

public class HMT_SpawnElement : MonoBehaviour
{
    public Sprite sprite;
    public string item;
    public bool isPicked = true;
   
    void Update()
    {
        if (isPicked)
            StartCoroutine(CreateElement());
    }

    // Create an initialised the new element
    IEnumerator CreateElement()
    {
        isPicked = false;
        yield return new WaitForSeconds(3);
        GameObject Element = new GameObject(item);
        Element.tag = "Element";
        Element.transform.parent = GameObject.Find("ToDestroy").transform;
        Element.AddComponent<SpriteRenderer>().sprite = sprite;
        Element.AddComponent<BoxCollider>().size = new Vector3(6, 6, 0.2f);
        Element.GetComponent<BoxCollider>().isTrigger = true;
        Element.AddComponent<HMT_Element>().spawner = transform.gameObject;
        Element.transform.localPosition = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        Element.transform.localRotation = Quaternion.Euler(90, 0, 0);
        Element.transform.localScale = new Vector3(0, 0, 0);
    }
}
