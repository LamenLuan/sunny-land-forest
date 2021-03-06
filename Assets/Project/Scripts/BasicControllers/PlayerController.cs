using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private AudioController _audioController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _dustParticle;
    public const byte MAX_LIFE_POINTS = 3;
    private byte _lifePoints = MAX_LIFE_POINTS;
    private bool _intangible, _dead, _loseControl;

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

    public float JumpForce { get => _jumpForce; }
    public bool IsGrounded { get => _isGrounded; }

    void Update()
    {
        if(_dead) return;

        CheckIfGrounded();
        _touchRun = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump")) _isJumping = true;
        UpdateSprite();
    }

    void FixedUpdate()
    {
        if(_loseControl) return;
        PlayerMove(_touchRun);
        CheckJump();
    }

    void OnTriggerEnter2D(Collider2D collider) // Before OnCollisionEnter2D()
    {
        GameObject gameObject = collider.gameObject;
        switch(gameObject.tag)
        {
            case "Harmfull": DealWithHarmfullTile(); break;
            case "Colectable": DealWithCollectable(gameObject); break;
        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        if(_gameController) _gameController.ReloadLevel(4f);
    }

    private void DealWithHarmfullTile()
    {
        if(!_intangible) Jump();
        GetDamage();
    }

    private void DealWithCollectable(GameObject collectable)
    {
        Destroy(collectable);
        _gameController.GetCollectable();
    }

    public void GetDamage()
    {
        if(!_intangible) {
            LifePoints--;
            _audioController.PlayPlayerHurtAudio();
            if (_lifePoints == 0) Die();
            else StartCoroutine( IntangibleEffect() );
        }
    }

    private void Die()
    {
        _audioController.PlayPlayerDeathAudio();
        SetDeadAnimation();
        _dead = true;
        _gameController.ReloadLevel(4f);
    }

    private void SetDeadAnimation()
    {
        Collider2D collider = gameObject.GetComponent<Collider2D>();
        Jump();
        collider.enabled = false;
        _animator.SetBool("IsJumping", false);
        _animator.SetBool("IsDead", true);
    }

    private void Jump()
    {
        // Reseting y velocity to have independent jumps in double jumps
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
        _rigidBody.AddForce( new Vector2(0f, _jumpForce) );
    }

    private void CheckIfGrounded()
    {
        bool isGrounded = Physics2D.Linecast(
            transform.position,
            _groundCheck.position,
            1 << LayerMask.NameToLayer("Ground")
        );
        if(isGrounded && !_isGrounded) {
            _isGrounded = true;
            _numberOfJumps = 0;
            _dustParticle.Play();
        }
    }

    private bool IsWalking()
    {
        return ((int)_rigidBody.velocity.x) != 0 && _isGrounded;
    }

    private void UpdateSprite()
    {
        _animator.SetBool( "IsWalking", IsWalking() );
        _animator.SetBool("IsGrounded", _isGrounded);
        _animator.SetBool("IsJumping", !_isGrounded);
    }

    private void FlipPlayer(float move)
    {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(move < 0 ? -1 : 1, scale.y, scale.z);
    }

    private void PlayerMove(float horizontalMove)
    {
        // Changing player sprite direction if horizontalMove changed
        if (horizontalMove != 0) {
            FlipPlayer(horizontalMove);
            if( IsWalking() ) _dustParticle.Play();
        }

        float y = _rigidBody.velocity.y;
        _rigidBody.velocity = new Vector2(horizontalMove * _speed, y);
    }

    private void PlayerJump()
    {
        _dustParticle.Play();
        _audioController.PlayJumpAudio();
        _numberOfJumps++;
        Jump();
        _isGrounded = false;
    }

    private void CheckJump()
    {
        if(!_isJumping) return;
        if(_numberOfJumps < MAX_JUMPS) PlayerJump();
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
