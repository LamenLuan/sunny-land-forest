using UnityEngine;

public class PointToPointEnemyAI : PointToPointAI
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _transform.position = _startPoint.position;
        if(_endPoint.position.x < _startPoint.position.x)
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    void Update()
    {
        if( HasValidPoints() ) {
            Move();
            if( _transform.position == TargetPosition() ) {
                _goingToEnd = !_goingToEnd;
                _spriteRenderer.flipX = !_spriteRenderer.flipX;
            }
        }
    }

}
