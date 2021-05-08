using UnityEngine;
using System.Collections;

public class LevelEnd : MonoBehaviour
{
    ParticleSystem system;
    bool stopped;
    public GameObject player;
    bool enabled = true;
    float timer;

    void Start()
    {
        system = GetComponent<ParticleSystem>();
        StartCoroutine(Effects());
    }

    IEnumerator Effects()
    {
        yield return new WaitForSeconds(1.5f);
        system.emissionRate = 75;
        system.startSpeed = 1f;
        while (transform.position.y < StagePropeties.Current.stageDimensions.y + StagePropeties.Current.stageDimensions.height)
        {
            transform.Translate(Vector3.up * 10 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        system.Stop();
        print("FIN");
    }
}
