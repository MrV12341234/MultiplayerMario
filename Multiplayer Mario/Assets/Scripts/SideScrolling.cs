using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    public Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        // below code stops camera from allowing player to go back. If you want player to move right and left, use this: cameraPosition.x = player.position.x;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);
        transform.position = cameraPosition;
    }
}
