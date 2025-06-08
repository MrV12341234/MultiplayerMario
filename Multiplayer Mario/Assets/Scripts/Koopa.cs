using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check what Goomba collided with
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            // dot task to ensure kill only when mario hits goomba going down
            if (collision.transform.DotTest(transform, Vector2.down))
            {
                EnterShell(); // Koopa enters stationary shell
            }
            else
            {
                player.Hit(); // player hits enemy and either shrinks or dies(inside the Hit() function in Player.cs) 
            }
        }
    }

    private void EnterShell() // function when enemy is killed
    {
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
        
    }
}
