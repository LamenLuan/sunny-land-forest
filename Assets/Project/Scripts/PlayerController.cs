using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _touchRun;
    private bool _isGrounded;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    void Update()
    {
        _touchRun = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        MovePlayer(_touchRun);
    }

    private void MovePlayer(float horizontalMove)
    {
        // Changing player sprite direction if horizontalMove changed
        if(horizontalMove != 0) _spriteRenderer.flipX = horizontalMove < 0;

        _animator.SetBool("IsWalking", (int) _rigidBody.velocity.x != 0);
        
        float y = _rigidBody.velocity.y;
        _rigidBody.velocity = new Vector2(horizontalMove * _speed, y);
    }

}
