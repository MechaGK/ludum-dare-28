using UnityEngine;
using System.Collections;

public class StagePropeties : MonoBehaviour
{
    public static StagePropeties BLAH;
    public Rect stageDimensions;    //X and Y is left, bottom

    public Rect StageDimensions
    {
        get
        {
            return stageDimensions;
        }
    }

    void Awake()
    {
        BLAH = this;
    }
}
