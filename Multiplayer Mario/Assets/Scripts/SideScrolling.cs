using System.Collections;
using UnityEngine;

/// <summary> Smooth side-scroll camera that waits for the player to spawn. </summary>
[RequireComponent(typeof(Camera))]
public class SideScrolling : MonoBehaviour
{
    [Header("Follow target")]
    public Transform player;                      // filled once the player appears

    [Header("Vertical positions")]
    public float surfaceHeight     =  7f; // this is the Y value of your Main Camera
    public float undergroundHeight = -9f; // Y value of camera when mario goes underground

    private void Awake() => StartCoroutine(FindPlayerWhenReady());

    private IEnumerator FindPlayerWhenReady()
    {
        // Keep looking once per frame until we find an object tagged "Player"
        while (player == null)
        {
            var go = GameObject.FindWithTag("Player");
            if (go != null)
                player = go.transform;

            yield return null;   // wait a frame, then try again
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;               // safety-check

        Vector3 camPos = transform.position;// below code stops camera from allowing player to go back. If you want player to move right and left, use this: cameraPosition.x = player.position.x;
        camPos.x = Mathf.Max(camPos.x, player.position.x);  
        transform.position = camPos;
    }

    public void SetUnderground(bool underground)
    {
        Vector3 camPos = transform.position;
        camPos.y = underground ? undergroundHeight : surfaceHeight;
        transform.position = camPos;
    }
}