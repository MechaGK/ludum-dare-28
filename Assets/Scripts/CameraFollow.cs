using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public static CameraFollow Main
    {
        get
        {
            return Camera.main.GetComponent<CameraFollow>();
        }
    }

    Rect allowedArea;

    void Start()
    {
        Rect StageDim = StagePropeties.Current.StageDimensions;

        allowedArea = new Rect(
            StageDim.x + GetComponent<Camera>().orthographicSize * (Screen.width / Screen.height),
            StageDim.y + GetComponent<Camera>().orthographicSize,
            StageDim.x + StageDim.width - (GetComponent<Camera>().orthographicSize * (Screen.width / Screen.height)),
            StageDim.y + StageDim.height - (GetComponent<Camera>().orthographicSize * (Screen.width / Screen.height)));
    }

    void Update()
    {
        Vector3 newPos = player.position;

        newPos.x = Mathf.Clamp(newPos.x, allowedArea.x, allowedArea.width);
        newPos.y = Mathf.Clamp(newPos.y, allowedArea.y, allowedArea.height);
        newPos.z = -10;

        transform.position = newPos;
    }
}
