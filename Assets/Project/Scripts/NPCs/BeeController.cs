using UnityEngine;

public class BeeController : PointToPointEnemyAI
{
    void OnTriggerEnter2D(Collider2D collider) // Before OnCollisionEnter2D()
    {
        GameObject playerObj = collider.gameObject;
        switch (playerObj.tag)
        {
            case "Player": DealWithPlayerCollision(playerObj); break;
        }
    }

    private void DealWithPlayerCollision(GameObject playerObj)
    {
        playerObj.GetComponent<PlayerController>().GetDamage();
        DestroyEnemy();
    }
}
