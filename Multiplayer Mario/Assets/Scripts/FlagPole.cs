using UnityEngine;
using System.Collections;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f;
    public int nextWorld = 1;
    public int nextStage = 1;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(flag, poleBottom.position));
            StartCoroutine(LevelCompleteSequence(other.transform));
        }
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
        //first stop player movement
        player.GetComponent<PlayerMovement>().enabled = false;
        
        yield return MoveTo(player, poleBottom.position); // move player down to the gameobject
        yield return MoveTo(player, player.position + Vector3.right);
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
        yield return MoveTo(player, castle.position);
        
        //once player reaches the castle position he dissapears
        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);
        
        //load next level. I need to change this to move player to an empty game object location (next level start within scene).
        // i need to set next world and next stage in the inspector. So each flagpole tells which gamem object the next map is
        GameManager.Instance.LoadLevel(nextWorld, nextStage);
    }

    // below is a loop to tell the player to continue moving towards a set destination (over and over)
    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while (Vector3.Distance(subject.position, destination) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
            yield return null;
        }
        
        subject.position = destination;
    }
}
