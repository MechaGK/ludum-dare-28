using UnityEngine;
using System.Collections;

public class Spawing : MonoBehaviour
{
    ParticleSystem system;
    bool stopped;
    public GameObject player;
    bool enabled = true;

    void Start()
    {
        system = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (enabled)
        {
            if (system.startSpeed > -12f && !stopped)
            {
                system.startSpeed -= Time.deltaTime * 2;
                system.emissionRate += Time.deltaTime;
            }
            else
            {
                system.startSpeed = 0;
                system.emissionRate = 0;
                stopped = true;

                system.startSpeed = 5;
                system.startColor = Color.black;
                system.Emit(40);

                GameObject playera = Instantiate(player, transform.position, Quaternion.identity) as GameObject;
                Camera.main.GetComponent<CameraFollow>().player = playera.transform;
                enabled = false;
            }
        }
    }
}
