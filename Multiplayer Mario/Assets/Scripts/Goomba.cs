using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check what Goomba collided with
        if (collision.gameObject.CompareTag("Player"))
        {
            // dot task to ensure kill only when mario hits goomba going down
            if (collision.transform.DotTest(transform, Vector2.down))
            {
                Flatten();
            }
        }
    }

    private void Flatten() // function when enemy is killed
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f); // wait 5 seconds before destroying so player has .5 seconds to see flattened goomba
    }
    
}
