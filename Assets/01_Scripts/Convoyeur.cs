using System.Collections;
using UnityEngine;

public class Convoyeur : MonoBehaviour
{
    public Sprite[] sprite;
    private int i;
    private float speed;

    void Start()
    {
        if (GameObject.Find("Niveau2"))
            speed = 0.204f;
        else
            speed = 0.068f;
        i = 0;
        StartCoroutine(ChangeSprite());
    }

    IEnumerator ChangeSprite()
    {
        yield return new WaitForSeconds(speed);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite[i + 1 >= sprite.Length ? i = 0 : i++];
        StartCoroutine(ChangeSprite());
    }
}
