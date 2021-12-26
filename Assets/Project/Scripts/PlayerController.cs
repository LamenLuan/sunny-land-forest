using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameController gameController; 
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    // Walk
    private float _touchRun;
    [SerializeField] private float _speed;
    
    // Jump
    private const int MAX_JUMPS = 2;
    private bool _isGrounded, _isJumping;
    private int _numberOfJumps;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheck;

    void Update()
    {
        CheckIfGrounded();
        _touchRun = Input.GetAxisRaw("Horizontal");
        if( Input.GetButtonDown("Jump") ) _isJumping = true;
        UpdateSprite();
    }

    void FixedUpdate()
    {
        PlayerMove(_touchRun);
        if(_isJumping) PlayerJump();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Colectable":
                Destroy(collider.gameObject);
                gameController.Score++;
            break;
        }
    }

    private void CheckIfGrounded()
    {
        _isGrounded = Physics2D.Linecast(
            transform.position,
            _groundCheck.position,
            1 << LayerMask.NameToLayer("Ground")
        );
        if(_isGrounded) _numberOfJumps = 0;
    }

    private void UpdateSprite()
    {
        bool isWalking = ((int)_rigidBody.velocity.x) != 0 && _isGrounded;

        _animator.SetBool("IsWalking", isWalking);
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetBool("IsJumping", !_isGrounded);
        _animator.SetFloat("Y", transform.position.y);
    }

    private void PlayerMove(float horizontalMove)
    {
        // Changing player sprite direction if horizontalMove changed
        if (horizontalMove != 0) _spriteRenderer.flipX = horizontalMove < 0;

        float y = _rigidBody.velocity.y;
        _rigidBody.velocity = new Vector2(horizontalMove * _speed, y);
    }

    private void PlayerJump()
    {
        if(_numberOfJumps < MAX_JUMPS) {
            _numberOfJumps++;
            _rigidBody.AddForce( new Vector2(0f, _jumpForce) );
            _isGrounded = false;
        }
        _isJumping = false;
    }

}
