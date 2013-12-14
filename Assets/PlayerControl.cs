using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    bool jump;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        if (!jump)
        {
            TimeControl.timeModifier = h * (1 / 60) + 1 / 100;
        }
    }
}
