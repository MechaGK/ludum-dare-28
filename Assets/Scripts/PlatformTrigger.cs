using UnityEngine;
using System.Collections;

public class PlatformTrigger : MonoBehaviour
{
    public Transform platform;

    void OnTriggerEnter2D(Collider2D col)
    {
        print("ARGH!!!");

        if (col.CompareTag("Player"))
        {
            col.transform.parent = transform.parent;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        print("cmon");

        if (col.CompareTag("Player"))
        {
            col.transform.parent = null;
        }
    }
}
