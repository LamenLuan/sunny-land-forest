using UnityEngine;

public class SlugController : PointToPointEnemyAI
{
    void OnTriggerEnter2D(Collider2D collider) // Before OnCollisionEnter2D()
    {
        GameObject playerObj = collider.gameObject;
        switch (playerObj.tag)
        {
            case "Player": DealWithPlayerAttack(playerObj); break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject playerObj = collision.gameObject;
        switch (playerObj.tag)
        {
            case "Player": DealWithPlayerCollision(playerObj); break;
        }
    }

    private void ThrowPlayer(GameObject playerObj)
    {
        float jumpForce = playerObj.GetComponent<PlayerController>().JumpForce;
        Rigidbody2D playerRb = playerObj.GetComponent<Rigidbody2D>();

        playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
        playerRb.AddForce(new Vector2(0f, jumpForce));
    }

    private void DealWithPlayerAttack(GameObject playerObj)
    {
        bool grounded = playerObj.GetComponent<PlayerController>().IsGrounded;
        if(grounded) return;

        ThrowPlayer(playerObj);
        DestroyEnemy();
    }

    private void DealWithPlayerCollision(GameObject playerObj)
    {
        playerObj.GetComponent<PlayerController>().GetDamage();
    }
}
