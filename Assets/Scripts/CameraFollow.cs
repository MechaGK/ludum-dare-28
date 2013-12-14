using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Rect allowedArea;

    void Start()
    {
        Rect StageDim = StagePropeties.BLAH.StageDimensions;

        allowedArea = new Rect(
            StageDim.x + camera.orthographicSize * (Screen.width / Screen.height),
            StageDim.y + camera.orthographicSize,
            StageDim.x + StageDim.width - (camera.orthographicSize * (Screen.width / Screen.height)),
            StageDim.y + StageDim.height - (camera.orthographicSize * (Screen.width / Screen.height)));
    }

    void Update()
    {
        Vector3 newPos = player.position;

        newPos.x = Mathf.Clamp(newPos.x, allowedArea.x, allowedArea.width);
        newPos.y = Mathf.Clamp(newPos.y, allowedArea.height, allowedArea.y);
        newPos.z = -10;

        transform.position = newPos;
    }
}
