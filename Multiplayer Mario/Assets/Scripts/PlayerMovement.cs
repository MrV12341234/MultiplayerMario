using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    public float moveSpeed = 8f; 

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HorizontalMovement();
    }
    
    private void HorizontalMovement()
}
