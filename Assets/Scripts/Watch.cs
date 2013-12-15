using UnityEngine;
using System.Collections;

public class Watch : MonoBehaviour
{
    public Transform viser;
    public PlayerControl player;
    float timer;

    void Update()
    {
        if (player != null)
        {
            if (player.timeSinceSpawn < 1f)
            {
                float rotZ = viser.eulerAngles.z;
                viser.rotation = Quaternion.Euler(0, 0, rotZ - 360 * TimeControl.DeltaTime);
                timer += TimeControl.DeltaTime;
            }
            else
            {
                viser.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }
}
