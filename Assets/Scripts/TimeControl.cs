using UnityEngine;
using System.Collections;

public class TimeControl : MonoBehaviour
{
    public static float timeModifier;
    static float deltaTime;

    public static float DeltaTime
    {
        get
        {
            return deltaTime;
        }
    }

    float time;

    void Update()
    {
        deltaTime = Time.deltaTime * timeModifier;

        time += deltaTime;
        //print(time);
    }
}
