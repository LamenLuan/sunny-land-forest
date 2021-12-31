using UnityEngine;

public class PointToPointEnemyAI : PointToPointAI
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    new void Start()
    {
        base.Start();
        if (_endPoint.position.x < _startPoint.position.x)
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

    protected override void ChangeTarget()
    {
        base.ChangeTarget();
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }

}
