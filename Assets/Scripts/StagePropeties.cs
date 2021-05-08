using UnityEngine;
using System.Collections;

public class StagePropeties : MonoBehaviour
{
    public static StagePropeties Current;
    public Rect stageDimensions;    //X and Y is left, bottom
    public Transform spawn;
    public new Camera camera;

    public Rect StageDimensions => stageDimensions;

    private void Awake()
    {
        Current = this;
    }
}
