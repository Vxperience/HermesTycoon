using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject Niveau1;
    public GameObject Niveau2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Niveau1.activeInHierarchy)
            transform.localPosition = new Vector3(0, 5, -30);
        else if (Niveau2.activeInHierarchy)
            transform.localPosition = new Vector3(0, 5, -15);
        else
            transform.localPosition = new Vector3(0, 5, 0);
    }
}
