using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private AudioController _audioController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public const byte MAX_LIFE_POINTS = 3;
    private byte _lifePoints = MAX_LIFE_POINTS;
    private bool _intangible;

    // Walk
    private float _touchRun;
    [SerializeField] private float _speed;
    
    // Jump
    private const int MAX_JUMPS = 2;
    private bool _isGrounded, _isJumping;
    private int _numberOfJumps;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheck;

    public byte LifePoints
    {
        get => _lifePoints;
        set {
            if(value >= 0 && value <= MAX_LIFE_POINTS) {
                _lifePoints = value;
                _gameController.UpdateLifePointsHud(_lifePoints);
            }
        }
    }

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

    // Runs before OnCollisionEnter2D()
    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject gameObject = collider.gameObject;
        switch (gameObject.tag)
        {
            case "Colectable": DealWithCollectable(gameObject); break;
            case "Enemy": DealWithEnemyCollider(gameObject); break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy": CheckIfHurtOrDead(); break;
        }
    }

    private void DealWithCollectable(GameObject collectable)
    {
        Destroy(collectable);
        _gameController.GetCollectable();
    }

    private void DealWithEnemyCollider(GameObject enemy)
    {
        if(!_isGrounded) {
            _gameController.DestroyEnemy(enemy);
            Jump();
        }
    }

    private void CheckIfHurtOrDead()
    {
        if(!_intangible) {
            LifePoints--;
            if (_lifePoints == 0) print("Morri");
            else StartCoroutine( IntangibleEffect() );
        }
    }

    private void Jump()
    {
        // Reseting y velocity to have independent jumps in double jumps
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
        _rigidBody.AddForce( new Vector2(0f, _jumpForce) );
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
            Jump();
            _audioController.PlayJumpAudio();
            _isGrounded = false;
        }
        _isJumping = false;
    }

    IEnumerator DamageTakenEffect()
    {
        _spriteRenderer.color = new Color(1, 0, 0, 0.8f);
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator IntangibleEffect()
    {   
        Color[] colors = { new Color(1, 1, 1, 0.3f), new Color(1, 1, 1) };

        _intangible = true;
        yield return DamageTakenEffect();

        // 18x 0.1s iterations results in 1.8s intangible animation
        for (int i = 0; i < 18; i++)
        {
            _spriteRenderer.color = colors[i % 2 == 0 ? 0 : 1];
            yield return new WaitForSeconds(0.1f);
        }
        _intangible = false;
    }

}
