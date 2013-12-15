using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    public Sprite activated;
    bool isActivated = false;

    void Update()
    {
        if (isActivated)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            color.a -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().sprite = activated;
            isActivated = true;
        }
    }
}
