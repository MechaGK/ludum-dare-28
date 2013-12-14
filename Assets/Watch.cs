using UnityEngine;
using System.Collections;

public class Watch : MonoBehaviour
{
    public Transform viser;

    void Update()
    {
        float rotZ = viser.eulerAngles.z;
        viser.rotation = Quaternion.Euler(0, 0, rotZ - 360 * TimeControl.DeltaTime);
    }
}
